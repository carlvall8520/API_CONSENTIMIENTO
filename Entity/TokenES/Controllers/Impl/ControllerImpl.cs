using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReyBanPac.ModeloCanonico.Constans;
using ReyBanPac.ModeloCanonico.Type;
using ReyBanPac.ModeloCanonico.Utils;
using ReyBanPac.TokenES.Constans;
using ReyBanPac.TokenES.Controllers.Contract;
using ReyBanPac.TokenES.Service.Contract;
using ReyBanPac.TokenES.Utils;
using System.Text.Json;

namespace ReyBanPac.TokenES.Controllers.Impl
{
    [Route("api/" + General.Tipo_Servicio + "/" + General.Nombre_Servicio)]
    [Tags(General.Nombre_Servicio)]
    [ApiController]
    //[Authorize]
    public class ControllerImpl : ControllerBase, IController
    {
        private readonly ILogger<ControllerImpl> _logger;
        private readonly IService Svc;

        public ControllerImpl(IService Servicio, ILogger<ControllerImpl> logger)
        {
            Svc = Servicio;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MimeType.JSON)]
        [Produces(MimeType.JSON)]
        public async Task<ActionResult<object>> Guardar(TokenType TokenType)
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Controller");
            try
            {
                TokenType Resultado;
                Resultado = await Svc.Guardar(TokenType);
                return StatusCode(StatusCodes.Status201Created, DataValidator.ValidarResultadoByCreate(Resultado));
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  {ex.Message}");
                return StatusCode(ex.Codigo, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Interno del Servidor");
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Controller");
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MimeType.JSON)]
        [Produces(MimeType.JSON)]
        public async Task<ActionResult<object>> Actualizar(TokenType TokenType)
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Controller");
            try
            {
                TokenType Resultado;
                Resultado = await Svc.Actualizar(TokenType);
                return Ok(DataValidator.ValidarResultadoByUpdate(Resultado));
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  {ex.Message}");
                return StatusCode(ex.Codigo, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Interno del Servidor");
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Controller");
            }
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MimeType.JSON)]
        public async Task<ActionResult<object>> Eliminar(int Id)
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Controller");
            try
            {
                int ConfirmarEliminacion = 0;
                ConfirmarEliminacion = await Svc.Eliminar(Id);
                return Ok(DataValidator.ValidarResultadoByDelete(ConfirmarEliminacion));
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  {ex.Message}");
                return StatusCode(ex.Codigo, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Interno del Servidor");
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Controller");
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TokenType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MimeType.JSON)]
        public async Task<ActionResult<object>> Consultar()
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Controller");
            try
            {
                List<TokenType> Listado;
                Listado = await Svc.Consultar();
                return Ok(DataValidator.ValidarResultadoConsulta(Listado));
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  {ex.Message}");
                return StatusCode(ex.Codigo, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Interno del Servidor");
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Controller");
            }
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(TokenType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MimeType.JSON)]
        public async Task<ActionResult<object>> ConsultarPorId(int Id)
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Controller");
            try
            {
                TokenType TokenType;
                TokenType = await Svc.ConsultarPorId(Id);
                return Ok(DataValidator.ValidarResultadoConsulta(TokenType));
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  {ex.Message}");
                return StatusCode(ex.Codigo, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Interno del Servidor");
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Controller");
            }
        }


        [HttpGet("vigencia")]
        [ProducesResponseType(typeof(TokenType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MimeType.JSON)]
        public async Task<ActionResult<object>> ConsultarPorVigencia()
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Controller");
            try
            {
                TokenType TokenType;
                TokenType = await Svc.ConsultarPorVigencia();
                return Ok(DataValidator.ValidarResultadoConsulta(TokenType));
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  {ex.Message}");
                return StatusCode(ex.Codigo, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Interno del Servidor");
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Controller");
            }
        }


        

    }
}
