using ReyBanPac.ModeloCanonico.Type;

namespace ReyBanPac.TokenES.Service.Contract
{
    public interface IService
    {
        public Task<TokenType> Guardar(TokenType TokenType);

        public Task<TokenType> Actualizar(TokenType TokenType);

        public Task<int> Eliminar(int Id);

        public Task<List<TokenType>> Consultar();

        public Task<TokenType> ConsultarPorId(int Id);

        public Task<TokenType> ConsultarPorVigencia();

    }
}
