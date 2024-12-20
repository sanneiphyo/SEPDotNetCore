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
       
        public static class BoardConfig
        {
            public static readonly Dictionary<int, int> SnakesAndLadders = new Dictionary<int, int>
         {
        // Snakes မျိုခံရလျှင် အမြင့်မှ အနိမ့်သို့ဆင်းသောကြောင်း number အကြီးမှအသေးသို့သွားမယ်
        { 99, 78 }, { 95, 75 }, { 92, 88 }, { 74, 53 }, { 62, 19 }, { 49, 11 }, { 46, 25 },

        //Ladder သည် နိမ့်ရာမှ အမြင့်သို့တက်သောကြောင့် numbber အသေးမှအကြီးသို့သွားမယ်
        { 2, 38 }, { 7, 14 }, { 8, 31 }, { 15, 26 }, { 21, 42 }, { 28, 84 }, { 36, 44 },
        { 51, 67 }, { 71, 91 }, { 78, 98 }
            };
        }

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

        public async Task<Result<PlayerModel>> GetPlayerAsync(int id, string playerName)
        {

            Result<PlayerModel> model = new Result<PlayerModel>();

       
            var player = await _db.TblPlayers.AsNoTracking()
                                             .FirstOrDefaultAsync(x => x.PlayerId == id);

          
            if (player is null)
            {
                model = Result<PlayerModel>.SystemError(null, "Player does not exist");
                goto Result;
            }

            var newPlayer = new TblPlayer
            {
                PlayerName = playerName,
                CurrentPosition = 1
            };

            
            await _db.TblPlayers.AddAsync(newPlayer);
            await _db.SaveChangesAsync();

            
            var playerModel = new PlayerModel
            {
                CurrentPosition = newPlayer.CurrentPosition,
                PlayerName = newPlayer.PlayerName
            };

            
            model = Result<PlayerModel>.Success(playerModel, "Player created successfully");

           Result:
            return model;
        }

        public async Task<Result<PlayerMoveModel>>StartGameAsync(int id, string gameCode)
        {
            Result<PlayerMoveModel> model = new Result<PlayerMoveModel>();

            var player =await  _db.TblPlayers.AsNoTracking().FirstOrDefaultAsync(x => x.PlayerId ==id);

            if (player is null)
            {
                model = Result<PlayerMoveModel>.SystemError(null,"Player does not exist");
                goto Result;
            }

            var game = await _db.TblGames.AsNoTracking().FirstOrDefaultAsync(x => x.GameCode == gameCode);

            if (game is null)
            {
                model = Result<PlayerMoveModel>.SystemError(null, "GameCode does not exist");
                goto Result;

            }

            Random r = new Random();
            int diceRoll = r.Next(1, 7);

            int newPosition = player.CurrentPosition + diceRoll;

            //player ရဲ့ positionသည် 100 ထက်မကြီးနိုင်
            if(newPosition > 100)
            {
                newPosition = 100;
            }

            //new position သည် snake and ladder ရဲ့ number တစ်ခုခုကိုကျသွားသလားစစ်မယ်
            if (BoardConfig.SnakesAndLadders.ContainsKey(newPosition)) 
            {
                newPosition = BoardConfig.SnakesAndLadders[newPosition];
            }

            player.CurrentPosition = newPosition;
             _db.TblPlayers.Update(player);
            await _db.SaveChangesAsync();

            var responseModel = new PlayerMoveModel
            {            
                NewPosition = newPosition,
                DiceRoll = diceRoll,
            };

            model = Result<PlayerMoveModel>.Success(responseModel, "player moved");
            goto Result;

        Result:
            return model;
        }

    }
};