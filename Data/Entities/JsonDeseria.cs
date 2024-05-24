using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaylorsVersionApi.Data.Entities
{
    public class JsonDeseria
    {
        // Root myDeserializedClass = JsonSerializer.Deserialize<List<Root>>(myJsonResponse);
    public class Lyric
    {
        [JsonPropertyName("Order")]
        public int Order { get; set; }

        [JsonPropertyName("Text")]
        public string Text { get; set; }

        [JsonPropertyName("SongPart")]
        public string SongPart { get; set; }
    }

    public class AlbumData
    {
        [JsonPropertyName("Code")]
        public string Code { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("SubTitle")]
        public string SubTitle { get; set; }

        [JsonPropertyName("Year")]
        public int? Year { get; set; }

        [JsonPropertyName("Songs")]
        public List<Song> Songs { get; set; }
    }

    public class Song
    {
        [JsonPropertyName("TrackNumber")]
        public int TrackNumber { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("FromTheVault")]
        public bool FromTheVault { get; set; }

        [JsonPropertyName("FeaturedArtists")]
        public List<string> FeaturedArtists { get; set; }

        [JsonPropertyName("Lyrics")]
        public List<Lyric> Lyrics { get; set; }
    }


    }
}