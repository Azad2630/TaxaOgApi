using System;
namespace TaxaOgApi.Code
{
    public class TaxaPriser
    {
        public double StartPris { get; private set; }
        public double PrisPerKm { get; private set; }
        public double PrisPerMinut { get; private set; }
        public double CykelTillaeg { get; private set; }
        public double OpbaeringTillaeg { get; private set; }
        public double LufthavnTillaeg { get; private set; }
        public double PassagerTillaeg { get; private set; }
        public double LiftvognTillaeg { get; private set; }
        public double BroTillaeg { get; private set; }

        public TaxaPriser(string SelectedTime, string SelectedCar)
        {
            if (SelectedTime == "dag" && SelectedCar == "Almindelige Vogne")
            {
                StartPris = 37;
                PrisPerKm = 12.75;
                PrisPerMinut = 5.75;
            }
            else if (SelectedTime == "nat" && SelectedCar == "Almindelige Vogne")
            {
                StartPris = 47;
                PrisPerKm = 16;
                PrisPerMinut = 7;
            }
            else if (SelectedTime == "dag" && SelectedCar == "Store Vogne")
            {
                StartPris = 77;
                PrisPerKm = 17;
                PrisPerMinut = 5.75;
            }
            else if (SelectedTime == "nat" && SelectedCar == "Store Vogne")
            {
                StartPris = 87;
                PrisPerKm = 19;
                PrisPerMinut = 7;
            }

            CykelTillaeg = 0;
            OpbaeringTillaeg = 0;
            LufthavnTillaeg = 0;
            PassagerTillaeg = 0;
            LiftvognTillaeg = 0;
            BroTillaeg = 0;
        }

        public void BeregnTillaeg(int checkbox1, bool checkbox2, bool checkbox3, bool checkbox4, bool checkbox5, bool checkbox6, bool checkbox7)
        {
            if (checkbox1 > 0)
            {
                CykelTillaeg = checkbox1 > 2 ? 60 : 30 * checkbox1;
            }

            OpbaeringTillaeg = checkbox2 ? 30 : 0;
            LufthavnTillaeg = checkbox3 ? 15 : 0;
            PassagerTillaeg = checkbox4 ? 40 : 0;
            LiftvognTillaeg = checkbox5 ? 350 : 0;
            BroTillaeg = checkbox6 ? 350 : 0;
            BroTillaeg += checkbox7 ? 540 : 0;
        }
    }
}
