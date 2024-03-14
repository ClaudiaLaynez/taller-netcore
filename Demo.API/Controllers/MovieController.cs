using Microsoft.AspNetCore.Mvc;
using Demo.Application.Interfaces;
using Demo.Domain;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _memberService;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMovieService memberService, ILogger<MovieController> logger)
        {
            _memberService = memberService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetAll()
        {
            _logger.LogInformation("Iniciando Operacion Obtener Todos");
            var result = await _memberService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IList<Movie>>> GetMovie(int id)
        {
            _logger.LogInformation("Iniciando Operacion Obtener por ID");
            var result = await _memberService.GetById(id);

            if (result == null)
            {
                _logger.LogError("No se encontro el registro con identificador {ID}", id);
                return NotFound($"No se encontro el registro con id: {id}");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Movie movie)
        {

            _logger.LogInformation("Iniciando Operacion Creación");
            var result = await _memberService.Create(movie);
            return CreatedAtAction(nameof(Create), new { result.Id }, result);
        }

        [HttpPut]
        public async Task<ActionResult> Update(Movie movie)
        {
            _logger.LogInformation("Iniciando Operacion Actualización");

            var itemToUpdate = await _memberService.GetById(movie.Id);

            if (itemToUpdate == null)
            {
                _logger.LogError("Registro no disponible para actualización con id: {ID}", movie.Id);
                return NotFound($"Registro no disponible para actualización con id: {movie.Id}");
            }

            itemToUpdate.Id = movie.Id;
            itemToUpdate.Title = movie.Title;
            itemToUpdate.Genre = movie.Genre;
            itemToUpdate.ReleaseDate = movie.ReleaseDate;


            var result = await _memberService.Update(itemToUpdate);
            return AcceptedAtAction(nameof(Update), new { result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Iniciando Operacion Eliminación");

            var itemToRemove = await _memberService.GetById(id);

            if (itemToRemove == null)
            {
                _logger.LogError("Registro no disponible para eliminación con id: {ID}", id);
                return NotFound($"Registro no disponible para eliminación con id: {id}");
            }

            await _memberService.Delete(itemToRemove);
            return NoContent();
        }
    }

}
