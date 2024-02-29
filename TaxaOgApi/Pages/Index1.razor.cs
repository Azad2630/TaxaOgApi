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

namespace TaxaOgApi.Pages
{
    public partial class Index
    {
        public string? AddressStart /* = "ballerup";*/ { get; set; }

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
            if (!string.IsNullOrWhiteSpace(AddressStart) && !string.IsNullOrWhiteSpace(AddressEnd))
            {
                double startPrice = 0;
                double pricePerKm = 0;
                double pricePerMin = 0;
                if (SelectedTime == "dag" && SelectedCar == "Almindelige Vogne")
                {
                    startPrice = 37;
                    pricePerKm = 12.75;
                    pricePerMin = 5.75;
                }
                else if (SelectedTime == "nat" && SelectedCar == "Almindelige Vogne")
                {
                    startPrice = 47;
                    pricePerKm = 16;
                    pricePerMin = 7;
                }
                else if (SelectedTime == "dag" && SelectedCar == "Store Vogne")
                {
                    startPrice = 77;
                    pricePerKm = 17;
                    pricePerMin = 5.75;
                }
                else if (SelectedTime == "nat" && SelectedCar == "Store Vogne")
                {
                    startPrice = 87;
                    pricePerKm = 19;
                    pricePerMin = 7;
                }

                double cykelTillæg = 0;
                if (Checkbox1 > 0)
                {
                    cykelTillæg = 30 * Checkbox1;
                }

                double opbæringTillæg = Checkbox2 ? 30 : 0;
                double lufthavnTillæg = Checkbox3 ? 15 : 0;
                double passagerTillæg = Checkbox4 ? 40 : 0;
                double liftvognTillæg = Checkbox5 ? 350 : 0;
                double broTillæg = 0;
                if (Checkbox6)
                {
                    broTillæg += 350;
                }

                if (Checkbox7)
                {
                    broTillæg += 540;
                }

                var response = await ApiService.GetDistance(AddressStart, AddressEnd);
                if (response != null)
                {
                    var result = JsonSerializer.Deserialize<GoogleMapsDistanceMatrixResponse>(response);

                    string distance = result?.rows?.FirstOrDefault()?.elements?.FirstOrDefault()?.distance?.text;
                    string duration = result?.rows?.FirstOrDefault()?.elements?.FirstOrDefault()?.duration?.text;


                    double distanceInKm = double.Parse(distance?.Replace(" km", ""));
                    double durationInMin = double.Parse(duration?.Replace(" mins", ""));

                    double totalPrice = startPrice + (distanceInKm * pricePerKm) + (durationInMin * pricePerMin) + cykelTillæg + opbæringTillæg + lufthavnTillæg + passagerTillæg + liftvognTillæg + broTillæg;


                    double value = 0;
                    value = (double)System.Math.Round(totalPrice, 2);

                    DistanceResult = distance != null ? $"afstand: {distance}, tid: {duration}, pris: {value} kr" : "";


                    string ApiMap = "AIzaSyBH1LLJchXHqhquPfqwe8KUCcc2yu7HWG0";

                    MapUrl = $"https://www.google.com/maps/embed/v1/directions?key={ApiMap}&origin={AddressStart}&destination={AddressEnd}";

                    Button = true;
                }
            }
        }
    }
}