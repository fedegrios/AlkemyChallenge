﻿using Microsoft.AspNetCore.Mvc;
using Interfaces;

namespace AlkemyChallenge.Controllers
{
    [ApiController]
    [Route("characters")]
    public class CharacterController : Controller
    {
        private readonly ICharacterServices characterServices;

        //public CharacterController(ICharacterServices characterServices)
        //{
        //    this.characterServices = characterServices;
        //}

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CharacterDetailDto>> Get(int id)
        {
            try
            {
                return await characterServices.Get(id);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"CharacterController.Get: {e.Message}");
                return BadRequest("Error al intentar obtener el personaje.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<CharacterDto>> List(string name ="", int age =0, int movieId =0)
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
        [Route("UpdateImage")]
        public async Task<ActionResult<int>> Post([FromBody] CharacterCreationDto dto)
        {
            try
            {
                return await characterServices.Create(dto);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"CharacterController.Post: {e.Message}");
                return BadRequest("Error al intentar crear el personaje.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> UpdateImage([FromForm] ImageUpdateDto dto)
        {
            try
            {
                return await characterServices.UpdateImage(dto);
            }
            catch (Exception e)
            {
                Console.WriteLine($@"CharacterController.UpdateImage: {e.Message}");
                return BadRequest("Error al intentar actualizar la imagen.");
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