using System;
using System.Collections.Generic;

namespace TaylorsVersionApi.Data.Entities;

public partial class Quote
{
    public int Id { get; set; }

    public string Quote1 { get; set; } = null!;

    public int? Songid { get; set; }

    public string? FieldType { get; set; }

    public virtual Song? Song { get; set; }
}
