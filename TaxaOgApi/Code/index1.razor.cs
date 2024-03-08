using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using TaxaOgApi.Shared;
using Microsoft.Extensions.DependencyInjection;
using TaxaOgApi;
using System.Text.Json;
using TaxaOgApi.Code;

namespace TaxaOgApi.Pages
{
    public partial class Index
    {
        public string? AddressStart { get; set; }

        public string? AddressEnd { get; set; }

        public string? SelectedTime { get; set; }

        public string? SelectedCar { get; set; }

        public int Checkbox1 { get; set; }

        public bool Checkbox2 { get; set; }

        public bool Checkbox3 { get; set; }

        public bool Checkbox4 { get; set; }

        public bool Checkbox5 { get; set; }

        public bool Checkbox6 { get; set; }

        public bool Checkbox7 { get; set; }

        public bool Button { get; set; }

        public string? MapUrl { get; set; }

        public string? DistanceResult { get; set; }

        [Inject]
        public ApiService ApiService { get; set; }

        public async Task Calculate()
        {
            var taxaPriser = new TaxaPriser(SelectedTime, SelectedCar);

            taxaPriser.BeregnTillaeg(Checkbox1, Checkbox2, Checkbox3, Checkbox4, Checkbox5, Checkbox6, Checkbox7);

            double startPris = taxaPriser.StartPris;
            double prisPerKm = taxaPriser.PrisPerKm;
            double prisPerMinut = taxaPriser.PrisPerMinut;
            double cykelTillaeg = taxaPriser.CykelTillaeg;
            double opbaeringTillaeg = taxaPriser.OpbaeringTillaeg;
            double lufthavnTillaeg = taxaPriser.LufthavnTillaeg;
            double passagerTillaeg = taxaPriser.PassagerTillaeg;
            double liftvognTillaeg = taxaPriser.LiftvognTillaeg;
            double broTillaeg = taxaPriser.BroTillaeg;

            
            var response = await ApiService.GetDistance(AddressStart, AddressEnd);
            if (response != null)
            {
                var result = JsonSerializer.Deserialize<GoogleMapsDistanceMatrixResponse>(response);

                string distance = result?.rows?.FirstOrDefault()?.elements?.FirstOrDefault()?.distance?.text;

                string duration = result?.rows?.FirstOrDefault()?.elements?.FirstOrDefault()?.duration?.text;


                if (duration.Contains("hours"))
                {
                    string[] parts = duration.Split(' ');

                    int hours = int.Parse(parts[0]);
                    int minutes = int.Parse(parts[2]);

                    int totalMinutes = hours * 60 + minutes;


                    double distanceInKm = double.Parse(distance?.Replace(" km", ""));

                    double totalPrice = startPris + (distanceInKm * prisPerKm) + (totalMinutes * prisPerMinut) +
                                        cykelTillaeg + opbaeringTillaeg + lufthavnTillaeg + passagerTillaeg + liftvognTillaeg + broTillaeg;

                    double pris = Math.Round(totalPrice, 2);

                    DistanceResult = $"afstand: {distance}, tid: {duration}, pris: {pris} kr";
                }
                else
                {
                    double distanceInKm = double.Parse(distance?.Replace(" km", ""));
                    double durationInMin = double.Parse(duration?.Replace(" mins", ""));

                    double totalPrice = startPris + (distanceInKm * prisPerKm) + (durationInMin * prisPerMinut) +
                                        cykelTillaeg + opbaeringTillaeg + lufthavnTillaeg + passagerTillaeg + liftvognTillaeg + broTillaeg;

                    double pris = Math.Round(totalPrice, 2);

                    DistanceResult = distance != null ? $"afstand: {distance}, tid: {duration}, pris: {pris} kr" : "";
                }

                string ApiMap = "AIzaSyBH1LLJchXHqhquPfqwe8KUCcc2yu7HWG0";

                MapUrl = $"https://www.google.com/maps/embed/v1/directions?key={ApiMap}&origin={AddressStart}&destination={AddressEnd}";

                Button = true;
            }
        }
    }
}