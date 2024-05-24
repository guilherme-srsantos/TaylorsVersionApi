using System;
using System.Collections.Generic;

namespace TaylorsVersionApi.Data.Entities;

public partial class Album
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Releaseyear { get; set; }

    public bool? IsTv { get; set; }

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
