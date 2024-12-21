using System;
using System.Collections.Generic;

namespace SEPDotNetCore.SnakeLadder.Database.AppDbContextModels;

public partial class TblPlayer
{
    public int PlayerId { get; set; }

    public string PlayerName { get; set; } = null!;

    public int CurrentPosition { get; set; }


    public virtual ICollection<TblGamePlay> TblGamePlays { get; set; } = new List<TblGamePlay>();

    public virtual ICollection<TblGame> TblGames { get; set; } = new List<TblGame>();
}
