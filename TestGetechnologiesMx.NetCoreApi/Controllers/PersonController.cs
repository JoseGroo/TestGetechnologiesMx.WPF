using Microsoft.AspNetCore.Mvc;
using TestGetechnologiesMx.Logic;
using TestGetechnologiesMx.Repository.Entities;

namespace TestGetechnologiesMx.NetCoreApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonLogic _personLogic;

        public PersonController(IPersonLogic personLogic)
        {
            _personLogic = personLogic;
        }

        [Route("Get")]
        public IActionResult Get(int personId)
        {
            try
            {
                var listPeople = _personLogic.Get(personId);
                return Ok(listPeople);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrio un error al intentar obtener la persona.");
            }
        }

        [Route("GetPeople")]
        public IActionResult GetPeople()
        {
            try
            {
                var listPeople = _personLogic.Get();
                return Ok(listPeople);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrio un error al intentar obtener el listado de personas.");
            }
        }

        [HttpPost("Save")]
        public IActionResult Save([FromBody] Person person)
        {
            try
            {
                bool saved = _personLogic.Save(person);
                if (saved)
                    return Ok();

                return BadRequest(_personLogic.LastError);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrio un error al intentar guardar la persona.");
            }
        }

        [HttpPost("Delete")]
        public IActionResult Delete([FromQuery] int personId)
        {
            try
            {
                bool saved = _personLogic.Delete(personId);
                if (saved)
                    return Ok();

                return BadRequest(_personLogic.LastError);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocurrio un error al intentar eliminar la persona.");
            }
        }
    }
}
