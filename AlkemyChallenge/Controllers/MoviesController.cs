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
        public async Task<ActionResult<MovieDetailDto>> Get(int id)
        {
            try
            {
                var movie = await movieServices.Get(id);

                if (movie == null)
                    return BadRequest(@"No se pudo encontrar la película/serie");

                return Ok(movie);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"MoviesController.List: {e.Message}");
                return BadRequest("No se pudo obtener la película/serie");
            }
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post([FromForm] MovieCreationDto dto)
        {
            try
            {
                var movieId = await movieServices.Create(dto);

                if (dto.Image != null)
                {
                    var updateImageDto = new ImageUpdateDto();

                    updateImageDto.Id = movieId;
                    updateImageDto.Extension = Path.GetExtension(dto.Image.FileName);

                    using (var memoryStream = new MemoryStream())
                    {
                        await dto.Image.CopyToAsync(memoryStream);
                        updateImageDto.Image = memoryStream.ToArray();
                    }

                    await movieServices.UpdateImage(updateImageDto);
                }

                return Ok(movieId);
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
                var result = await movieServices.Update(dto);

                if(!result)
                    return BadRequest("No se pudo actualizar la película/serie");

                return Ok();
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
