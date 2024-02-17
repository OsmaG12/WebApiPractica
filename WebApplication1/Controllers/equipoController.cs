using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equipoController : ControllerBase
    {

        private readonly equiposContext _equiposContexto;

        public equipoController(equiposContext equiposContexto)
        {
            _equiposContexto = equiposContexto;
        }

        //Método para leer los registros de la tabla de la base de datos
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<equipos> listadoEquipo = (from e in _equiposContexto.equipos
                                           select e).ToList();

            if (listadoEquipo.Count() == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);
        }
        //Fin del método

        //Método para buscar por ID
        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id) 
        {
            //El "?" sirve para que acepte nulos
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.id_equipos == id
                               select e).FirstOrDefault();

            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }
        //Fin del método

        //Método para buscar por descripción
        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByDescription(string filtro)
        {
            equipos? equipo = (from e in _equiposContexto.equipos
                               where e.descripcion.Contains(filtro)
                               select e).FirstOrDefault();
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }
        //Fin del método

    }
}
