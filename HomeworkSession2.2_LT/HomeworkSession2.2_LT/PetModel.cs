﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSession2._2_LT
{
    public class PetModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("category")]
        public Category Category { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photoUrls")]
        public List<string> PhotoUrls { get; set; }

        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Category
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Tag
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
