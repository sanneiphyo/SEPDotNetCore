using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEPDotNetCore.SnakeLadder.Database.AppDbContextModels;

namespace SEPDotNetCore.SankeLadder.Domain.Models
{
    public class GameRequestModel
    {
        public int GameId { get; set; }

        public string GameCode { get; set; } = null!;

        public int? WinnerPlayerId { get; set; }

        public virtual TblPlayer? WinnerPlayer { get; set; }
    }
}
