using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace SEPDotNetCore.RestApi3.Controllers // constructor injection
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurmaProjectIdeaController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly RestClient _restClient;
        private readonly ISnakeApi _snakeApi;

        public BurmaProjectIdeaController(HttpClient httpClient, RestClient restClient, ISnakeApi snakeApi)
        {
            _httpClient = httpClient;
            _restClient = restClient;
            _snakeApi = snakeApi;
        }

        [HttpGet("birds")]
        public async Task<IActionResult> BirdsAsync()
        {
            var response = await _httpClient.GetAsync("birds");
            var str = await response.Content.ReadAsStringAsync();
            return Ok(str);
        }

        [HttpGet("pick-a-pil")]
        public async Task<IActionResult> PickAPileAsync()
        {
            RestRequest request = new RestRequest("pick-a-pile", Method.Get);
            var response = await _restClient.GetAsync(request);
            var str =  response.Content;
            return Ok(str);
        }

        [HttpGet("snakes")]
        public async Task<IActionResult> GetSnakeAsync()
        {
            var response = await _snakeApi.GetSnakes();
            return Ok( response);
        }
    }
}
