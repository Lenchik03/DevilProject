using System;
using System.Collections.Generic;

namespace Devil_sOffice;

public partial class Rack
{
    public int Id { get; set; }

    /// <summary>
    /// название &quot;стеллажа&quot;
    /// </summary>
    public string Title { get; set; } = null!;

    public int IdDevil { get; set; }

    public int YearBuy { get; set; }

    /// <summary>
    /// макс кол-во применений
    /// </summary>
    public int UseCount { get; set; }

    /// <summary>
    /// кол-во применений
    /// </summary>
    public int CurrentCount { get; set; }

    public virtual Devil IdDevilNavigation { get; set; } = null!;
}
