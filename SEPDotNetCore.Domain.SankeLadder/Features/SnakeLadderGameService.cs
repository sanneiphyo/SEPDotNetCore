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
    public class SnakeLadderGameService
    {

        private readonly AppDbContext _db;

        public SnakeLadderGameService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<GameResponseModel>> GetGameCode(int id )
        {
            Result<GameResponseModel> model = new Result<GameResponseModel>();

            var GetGameCode =await _db.TblGames.AsNoTracking().FirstOrDefaultAsync(x => x.GameId == id);

            if (GetGameCode is null)
            {
                model = Result<GameResponseModel>.SystemError(null, "Game code does not exist");
                goto Result;
            };

            //Console.WriteLine($"GameCode: {GetGameCode.GameCode}");

            var responseModel = new GameResponseModel
            {
                //GameId = GetGameCode.GameId,
                GameCode = GetGameCode.GameCode
                
            };

            model = Result<GameResponseModel>.Success(responseModel, "Here is your Game Code");
            goto Result;

        Result:
            return model;
        }

        public async Task<Result<GameResponseModel>> CreateGameCodeAsync()
        {
                Result<GameResponseModel> model = new Result<GameResponseModel>();

                //create new game code
                var newGame = new TblGame
                {
                    GameCode = Guid.NewGuid().ToString().Substring(0, 8),
                };

                _db.TblGames.Add(newGame);
                await _db.SaveChangesAsync();

                model.data = new GameResponseModel
                {
                    GameCode = newGame.GameCode,
                };

                model = Result<GameResponseModel>.Success(model.data,"New game code genereate successfully");
                goto Result;

                Result:
                return model;
          
         
        }

        //public async Task<Result<GameResponseModel>> JoinGameAsync()
        //{

        //}
    }
};