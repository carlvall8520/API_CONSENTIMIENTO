using Microsoft.AspNetCore.Mvc;
using ReyBanPac.ModeloCanonico.Type;

namespace ReyBanPac.TokenES.Controllers.Contract
{
    public interface IController
    {
        public Task<ActionResult<object>> Guardar(TokenType TokenType);

        public Task<ActionResult<object>> Actualizar(TokenType TokenType);

        public Task<ActionResult<object>> Eliminar(int Id);

        public Task<ActionResult<object>> Consultar();

        public Task<ActionResult<object>> ConsultarPorId(int Id);



    }
}
