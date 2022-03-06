using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Interfaces;

namespace AlkemyChallenge.Controllers
{
    [ApiController]
    [Route("characters")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CharacterController : Controller
    {
        private readonly ICharacterServices characterServices;

        public CharacterController(ICharacterServices characterServices)
        {
            this.characterServices = characterServices;
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<CharacterDetailDto>> Get(int id)
        {
            try
            {
                var character = await characterServices.Get(id);

                if (character == null)
                    return BadRequest(@"No se pudo encontrar el personaje.");

                return Ok(character);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"CharacterController.Get: {e.Message}");
                return BadRequest("Error al intentar obtener el personaje.");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<CharacterDto>>> List(string name ="", int age =0, int movieId =0)
        {
            try
            {
                return await characterServices.List(name, age, movieId);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"CharacterController.Get: {e.Message}");
                return BadRequest("Error al intentar listar los personajes.");
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromForm] CharacterCreationDto dto)
        {
            try
            {
                var characterId = await characterServices.Create(dto);

                if (dto.Image != null)
                {
                    var updateImageDto = new ImageUpdateDto();

                    updateImageDto.Id = characterId;
                    updateImageDto.Extension = Path.GetExtension(dto.Image.FileName);

                    using (var memoryStream = new MemoryStream())
                    {
                        await dto.Image.CopyToAsync(memoryStream);
                        updateImageDto.Image = memoryStream.ToArray();
                    }

                    await characterServices.UpdateImage(updateImageDto);
                }

                return Ok(characterId);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"CharacterController.Post: {e.Message}");
                return BadRequest("Error al intentar crear el personaje.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] CharacterCreationDto dto)
        {
            try
            {
                if(await characterServices.Update(dto))
                    return Ok();

                return BadRequest("No se pudo modificar el personaje.");
            }
            catch (Exception e)
            {
                Console.WriteLine($@"CharacterController.Put: {e.Message}");
                return BadRequest("Error al intentar modificar el personaje.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if(await characterServices.Delete(id))
                    return Ok();

                return BadRequest("No se pudo elimiar el personaje.");
            }
            catch (Exception e)
            {
                Console.WriteLine($@"CharacterController.Delete: {e.Message}");
                return BadRequest("Error al intentar elimiar el personaje.");
            }
        }

    }
}
