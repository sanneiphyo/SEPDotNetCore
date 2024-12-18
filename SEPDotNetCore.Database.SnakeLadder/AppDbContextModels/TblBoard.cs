using System;
using System.Collections.Generic;

namespace SEPDotNetCore.SnakeLadder.Database.AppDbContextModels;

public partial class TblBoard
{
    public int BoardId { get; set; }

    public int BoardNumber { get; set; }

    public virtual ICollection<TblGamePlay> TblGamePlays { get; set; } = new List<TblGamePlay>();

    public virtual ICollection<TblPlayer> TblPlayers { get; set; } = new List<TblPlayer>();
}
