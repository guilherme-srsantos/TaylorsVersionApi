using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using TaylorsVersionApi.Data.Entities;
using static TaylorsVersionApi.Data.Entities.JsonDeseria;
using Microsoft.EntityFrameworkCore;

namespace TaylorsVersionApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly TaylorapiContext _context;
        public ApiController(TaylorapiContext context)
        {
            _context = context;
        }


        [NonAction]
        public async Task<IActionResult> PopulateDB()
        {

            var file = string.Join('\n', (await System.IO.File.ReadAllLinesAsync("./data/every_song.json")));

            var data = JsonSerializer.Deserialize<AlbumData[]>(file);

            foreach (var album in data!)
            {
                var newAlbum = new Album
                {
                    Name = album.Title,
                    IsTv = album.Title.Contains("Version", StringComparison.InvariantCultureIgnoreCase),
                    Releaseyear = album.Year,
                };
                _context.Albums.Add(newAlbum);
                _context.SaveChanges();

                foreach (var song in album.Songs)
                {
                    var newSong = new Data.Entities.Song
                    {
                        Albumid = newAlbum.Id,
                        Featuredartists = string.Join(", ", song.FeaturedArtists),
                        Isvault = song.FromTheVault,
                        Lyrics = string.Join('\n', song.Lyrics.Select(l => l.Text)),
                        Title = song.Title,
                        Tracknumber = song.TrackNumber
                    };

                    _context.Songs.Add(newSong);
                }

            }

            var total = _context.SaveChanges();

            return Ok(total);
        }

        [HttpGet("albuns")]
        public async Task<IActionResult> Albums(string? lyrics, int? year)
        {

            var query = _context.Albums.AsQueryable();

            if (lyrics is not null)
                query = query.Where(x => x.Songs.Any(s => s.Lyrics!.Contains(lyrics)));

            if (year.HasValue)
                query = query.Where(x => x.Releaseyear == year.Value);

            var album = await query.ToListAsync();

            return Ok(album);
        }

        [HttpPost]
        public IActionResult AddQuotes(QuoteBody quote)
        {
            var newQuote = new Quote
            {
                Quote1 = quote.Quote,
                Songid = quote.Songid,

            };

            _context.Quotes.Add(newQuote);

            _context.SaveChanges();

            return Created("api/addquote", newQuote);
        }
    }

    public class QuoteBody
    {
        public string Quote { get; set; } = null!;

        public int? Songid { get; set; }

    }
}