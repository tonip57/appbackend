using System;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using WebApp.Dtos;
using WebApp.Models;

namespace WebApp.Services
{
    public class ElectricityPriceService : IElectricityPriceService
    {
        public async Task<List<ElectricityPriceDto>> GetElectricityPrices()
        {
            var list = new List<ElectricityPriceDto>();

            // Dummy values for testing.
            /*
            for (var i = 1; i < 8; i++)
            {
                for (var x = 0; x < 24; x++)
                {
                    Random random = new Random();
                    list.Add(new ElectricityPriceDto(new DateTime(2023, 6, i, x, 0, 0).ToUniversalTime(), Math.Round(random.NextDouble() * (70.00 - 1.00) + 1.00, 2)));
                }
            }
            */

            // Fingrid | Ei haluttua dataa
            // ENTSO-E Transparency Platform | En kerennyt saada tokenia
            // api.spot-hinta.fi | porssisahko.net | sahkohinta-api.fi | Joutuu tehdä monta kyselyä

            // Elering LIVE API
            // https://dashboard.elering.ee/assets/api-doc.html#/nps-controller/getPriceUsingGET

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://dashboard.elering.ee");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var dateNow = DateTime.Now;
                var dateEndString = dateNow.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
                var dateStartString = dateNow.AddDays(-7).ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

                var apiString = "/api/nps/price?start=" + dateStartString + "&end=" + dateEndString;

                HttpResponseMessage response = await client.GetAsync(apiString);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var json = JsonSerializer.Deserialize<EleringObject>(jsonString) ?? throw new Exception();
                    foreach (var item in json.data.fi)
                    {
                        DateTimeOffset offset = DateTimeOffset.FromUnixTimeSeconds(item.timestamp);
                        list.Add(new ElectricityPriceDto(offset.DateTime, item.price / 10));
                    }
                }
                else
                {
                    throw new Exception();
                }
            }

            return list;
        }
    }
}