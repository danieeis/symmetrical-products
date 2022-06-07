using System;
using Newtonsoft.Json;

namespace ProductsApp.Models
{
    public class Rating
    {
        [JsonConstructor]
        public Rating(
            [JsonProperty("rate")] double rate,
            [JsonProperty("count")] int count
        )
        {
            this.Rate = rate;
            this.Count = count;
        }

        [JsonProperty("rate")]
        public double Rate { get; }

        [JsonProperty("count")]
        public int Count { get; }
    }

    public class Product
    {
        [JsonConstructor]
        public Product(
            [JsonProperty("id")] int id,
            [JsonProperty("title")] string title,
            [JsonProperty("price")] double price,
            [JsonProperty("description")] string description,
            [JsonProperty("category")] string category,
            [JsonProperty("image")] string image,
            [JsonProperty("rating")] Rating rating
        )
        {
            this.Id = id;
            this.Title = title;
            this.Price = price;
            this.Description = description;
            this.Category = category;
            this.Image = image;
            this.Rating = rating;
        }

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("title")]
        public string Title { get; }

        [JsonProperty("price")]
        public double Price { get; }

        [JsonProperty("description")]
        public string Description { get; }

        [JsonProperty("category")]
        public string Category { get; }

        [JsonProperty("image")]
        public string Image { get; }

        [JsonProperty("rating")]
        public Rating Rating { get; }
    }
}

