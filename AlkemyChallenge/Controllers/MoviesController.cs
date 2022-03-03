using Microsoft.AspNetCore.Mvc;
using Interfaces;

namespace AlkemyChallenge.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieServices movieServices;

        public MoviesController(IMovieServices movieServices)
        {
            this.movieServices = movieServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieDto>>> List(string name ="", int genre =0, string order ="")
        {
            try
            {
                return await movieServices.List(name, genre, order);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"MoviesController.List: {e.Message}");
                return BadRequest("No se pudo obtener las películas/series");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDetailDto>> List(int id)
        {
            try
            {
                return await movieServices.Get(id);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"MoviesController.List: {e.Message}");
                return BadRequest("No se pudo obtener la película/serie");
            }
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] MovieCreationDto dto)
        {
            try
            {
                return await movieServices.Create(dto);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"MoviesController.Create: {e.Message}");
                return BadRequest("No se pudo crear la película/serie");
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update([FromBody] MovieCreationDto dto)
        {
            try
            {
                return await movieServices.Update(dto);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"MoviesController.Update: {e.Message}");
                return BadRequest("No se pudo actualizar la película/serie");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                return await movieServices.Delete(id);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"MoviesController.Delete: {e.Message}");
                return BadRequest("No se pudo elimiar la película/serie");
            }
        }

    }
}
