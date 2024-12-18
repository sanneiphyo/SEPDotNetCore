using System;
using System.Collections.Generic;

namespace SEPDotNetCore.SnakeLadder.Database.AppDbContextModels;

public partial class TblGame
{
    public int GameId { get; set; }

    public string GameCode { get; set; } = null!;

    public int? WinnerPlayerId { get; set; }

    public virtual TblPlayer? WinnerPlayer { get; set; }
}
