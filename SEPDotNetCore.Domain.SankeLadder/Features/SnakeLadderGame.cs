using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SEPDotNetCore.SankeLadder.Domain.Models;
using SEPDotNetCore.SnakeLadder.Database.AppDbContextModels;

namespace SEPDotNetCore.SankeLadder.Domain.Features
{
    public class SnakeLadderGame
    {


        private readonly AppDbContext _db;

        public SnakeLadderGame(AppDbContext db)
        {
            _db = db;
        }

        //    public async Task<GameResponseModel> CreateGameCode()
        //    {
        //        var GenerateCode = Ulid.NewUlid().ToString().ToLower().Substring(0,3);

        //        var GameCode = _db.TblGames
        //            .Where(x => x.GameCode == GenerateCode);

        //        if (GameCode is null) 
        //        { 

        //        }
        //}
    }
};