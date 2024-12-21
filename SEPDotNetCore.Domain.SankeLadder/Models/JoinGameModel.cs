using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEPDotNetCore.SnakeLadder.Database.AppDbContextModels;

namespace SEPDotNetCore.SankeLadder.Domain.Models
{
    public class PlayerModel
    {
        public int PlayerId { get; set; }

        public string PlayerName { get; set; } = null!;

        public int CurrentPosition { get; set; }

        //public virtual TblBoard CurrentPositionNavigation { get; set; } = null!;

        public virtual ICollection<TblGamePlay> TblGamePlays { get; set; } = new List<TblGamePlay>();

        public virtual ICollection<TblGame> TblGames { get; set; } = new List<TblGame>();
    }

    public class PlayerMoveModel
    {
        public int MoveId { get; set; }

        public int PlayerId { get; set; }

        public string GameCode { get; set; } = null!;

        public int DiceRoll { get; set; }

        public int NewPosition { get; set; }

        //public virtual TblBoard NewPositionNavigation { get; set; } = null!;

        public virtual TblPlayer Player { get; set; } = null!;
    }
}
