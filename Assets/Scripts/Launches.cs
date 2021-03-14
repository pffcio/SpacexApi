using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

public partial class Launch
{
    [JsonProperty("mission_name", NullValueHandling = NullValueHandling.Ignore)]
    public string MissionName { get; set; }

    [JsonProperty("upcoming", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Upcoming { get; set; }

    [JsonProperty("rocket", NullValueHandling = NullValueHandling.Ignore)]
    public Rocket Rocket { get; set; }

    [JsonProperty("ships", NullValueHandling = NullValueHandling.Ignore)]
    public List<object> Ships { get; set; }

}



public partial class Links
{
    [JsonProperty("flickr_images", NullValueHandling = NullValueHandling.Ignore)]
    public List<object> FlickrImages { get; set; }
}

public partial class Rocket
{

    [JsonProperty("rocket_name", NullValueHandling = NullValueHandling.Ignore)]
    public string RocketName { get; set; }

    [JsonProperty("rocket_id", NullValueHandling = NullValueHandling.Ignore)]
    public string RocketID { get; set; }

    [JsonProperty("second_stage", NullValueHandling = NullValueHandling.Ignore)]
    public SecondStage SecondStage { get; set; }

}


public partial class SecondStage
{
    [JsonProperty("payloads", NullValueHandling = NullValueHandling.Ignore)]
    public List<Payload> Payloads { get; set; }
}

public partial class Payload
{

    [JsonProperty("norad_id", NullValueHandling = NullValueHandling.Ignore)]
    public List<object> NoradId { get; set; }

    [JsonProperty("nationality", NullValueHandling = NullValueHandling.Ignore)]
    public string Nationality { get; set; }
}




public partial class Launches
{
    public static Launches FromJson(string json) => JsonConvert.DeserializeObject<Launches>(json, Converterr.Settings);
}

public static class Serializee
{
    public static string ToJson(this Launches self) => JsonConvert.SerializeObject(self, Converterr.Settings);
}

internal static class Converterr
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

internal class ParseStringConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return null;
        var value = serializer.Deserialize<string>(reader);
        long l;
        if (Int64.TryParse(value, out l))
        {
            return l;
        }
        throw new Exception("Cannot unmarshal type long");
    }

    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }
        var value = (long)untypedValue;
        serializer.Serialize(writer, value.ToString());
        return;
    }

    public static readonly ParseStringConverter Singleton = new ParseStringConverter();
}