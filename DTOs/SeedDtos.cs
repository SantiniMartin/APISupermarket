using System.Collections.Generic;

namespace ComparadorPreciosAPI.DTOs
{
    public class RootSeedDto
    {
        public List<SupermarketSeedDto> supermarkets { get; set; }
    }

    public class SupermarketSeedDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string logo_url { get; set; }
        public List<ProductSeedDto> products { get; set; }
    }

    public class ProductSeedDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string currency { get; set; }
        public int stock { get; set; }
        public bool has_offer { get; set; }
        public int discount_percent { get; set; }
        public string category { get; set; }
        public string category_image_url { get; set; }
        public string image_url { get; set; }
        public string brand { get; set; }
    }
} 