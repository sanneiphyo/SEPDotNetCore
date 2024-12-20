using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEPDotNetCore.SankeLadder.Domain.Features;
using SEPDotNetCore.SankeLadder.Domain.Models;

namespace SEPDotNetCore.SnakeLadder.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SnakeLadderGameController : ControllerBase


    {
        private readonly SnakeLadderGameService _service;

        public SnakeLadderGameController(SnakeLadderGameService service)
        {
            _service = service;
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetGameCode(int id)
        {
            var GetGameCode = await _service.GetGameCode(id);
            return Ok(GetGameCode);
        }

        [HttpPost("create-game-code")]
        public async Task<IActionResult> CreateGameCode()
        {
            var GameCode =await _service.CreateGameCodeAsync();
            return new JsonResult(GameCode);
        }

        [HttpPost("play-game")]
        public async Task<IActionResult> StartGame(int id,string GameCode)
        {
            var PlayGame = await _service.StartGameAsync(id ,GameCode);
            return Ok(PlayGame);
        }

        [HttpGet("check-winner")]
        public async Task<IActionResult> CheckWinner(string gameCode)
        {
            var CheckWinner = await _service.CheckWinnerAsync(gameCode);
            return new JsonResult(CheckWinner);
        }
    }
}
