using System.Threading.Tasks;
using Core.Model.RequestModel;
using FilmApi.Model.Entities;
using FilmApi.Model.RequestModels;
using FilmApi.Model.ResponseModels;
using FilmApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmApi.Controllers
{
    [ApiController]
    [Route("api/films")]
    
    public class FilmController : ControllerBase
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDto createDto)
        {
            var film = new FilmModel()
            {
                Title = createDto.Title,
                Year = createDto.Year,
                ImdbId = createDto.ImdbId,
                Type = createDto.Type,
                Poster = createDto.Poster,
                Rated = createDto.Rated,
                Released = createDto.Released,
                Runtime = createDto.Runtime,
                Genre = createDto.Genre,
                Director = createDto.Director,
                Writer = createDto.Writer,
                Actors = createDto.Actors,
                Plot = createDto.Plot,
                Language = createDto.Language,
                Country = createDto.Country,
                Awards = createDto.Awards,
                Ratings = createDto.Ratings,
                MetaScore = createDto.MetaScore,
                ImdbRating = createDto.ImdbRating,
                ImdbVotes = createDto.ImdbVotes,
                Dvd = createDto.Dvd,
                BoxOffice = createDto.BoxOffice,
                Production = createDto.Production,
                Website = createDto.Website,
                Response = createDto.Response
            };

            await _filmService.InsertAsync(film);

            var response = new CreateResponse()
            {
                ImdbId= film.ImdbId
            };
            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var findOne = await _filmService.GetByIdAsync(id);
            return Ok(findOne);
        }
        
        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync()
        {
            var getAll = await _filmService.GetAllAsync();
            return Ok(getAll);
        }
        [HttpGet("AllWithSkipTake")]
        public async Task<IActionResult> GetAllSkipTakeAsync([FromQuery] GetAllDto getAllDto)
        {
            var getAll = await _filmService.GetAllSkipTakeAsync(getAllDto);
            return Ok(getAll);
        }
        
        [HttpDelete("HardDelete")]
        public async Task<IActionResult> HardDeleteAsync(string id)
        {
            var byId = await _filmService.GetByIdAsync(id);
            _filmService.Delete(byId.ImdbId);
            
            return Ok(id);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromQuery]string id, [FromBody] UpdateDto updateDto)
        {
            var result = await _filmService.Update(id, updateDto);
            return Ok(result);
        }
    }
}