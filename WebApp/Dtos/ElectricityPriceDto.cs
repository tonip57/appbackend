namespace WebApp.Dtos
{
    public class ElectricityPriceDto
    {
        public ElectricityPriceDto(DateTime dateTime, double price) { 
            DateTime = dateTime;
            Price = price;
        }

        public DateTime DateTime { get; set; }

        public double Price { get; set; }
    }
}