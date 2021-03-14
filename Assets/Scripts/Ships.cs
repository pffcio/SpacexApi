
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class Ships
{
    [JsonProperty("ship_id", NullValueHandling = NullValueHandling.Ignore)]
    public string ShipId { get; set; }

    [JsonProperty("ship_name", NullValueHandling = NullValueHandling.Ignore)]
    public string ShipName { get; set; }

    [JsonProperty("ship_type", NullValueHandling = NullValueHandling.Ignore)]
    public string ShipType { get; set; }

    [JsonProperty("home_port", NullValueHandling = NullValueHandling.Ignore)]
    public string HomePort { get; set; }

    [JsonProperty("missions", NullValueHandling = NullValueHandling.Ignore)]
    public List<Mission> Missions { get; set; }

    [JsonProperty("image", NullValueHandling = NullValueHandling.Ignore)]
    public Uri Image { get; set; }
}

public partial class Mission
{
    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string Name { get; set; }
}

public partial class Ships
{
    public static Ships FromJson(string json) => JsonConvert.DeserializeObject<Ships>(json, Converter.Settings);
}

public static class Serialize
{
    public static string ToJson(this Ships self) => JsonConvert.SerializeObject(self, Converter.Settings);
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

