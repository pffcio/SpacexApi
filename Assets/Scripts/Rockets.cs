namespace Rocketings
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Rocketing
    {
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("rocket_id", NullValueHandling = NullValueHandling.Ignore)]
        public string RocketId { get; set; }
    }

    public partial class Rocketing
    {
        public static Rocketing FromJson(string json) => JsonConvert.DeserializeObject<Rocketing>(json, Rocketings.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Rocketing self) => JsonConvert.SerializeObject(self, Rocketings.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}