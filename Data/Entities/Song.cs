using System;
using System.Collections.Generic;

namespace TaylorsVersionApi.Data.Entities;

public partial class Song
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Lyrics { get; set; }

    public int? Tracknumber { get; set; }

    public int? Albumid { get; set; }

    public string? Featuredartists { get; set; }

    public bool? Isvault { get; set; }

    public virtual Album? Album { get; set; }

    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
}
