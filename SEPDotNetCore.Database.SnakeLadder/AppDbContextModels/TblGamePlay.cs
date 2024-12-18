using System;
using System.Collections.Generic;

namespace SEPDotNetCore.SnakeLadder.Database.AppDbContextModels;

public partial class TblGamePlay
{
    public int MoveId { get; set; }

    public int PlayerId { get; set; }

    public string GameCode { get; set; } = null!;

    public int DiceRoll { get; set; }

    public int NewPosition { get; set; }

    public virtual TblBoard NewPositionNavigation { get; set; } = null!;

    public virtual TblPlayer Player { get; set; } = null!;
}
