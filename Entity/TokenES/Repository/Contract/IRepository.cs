using ReyBanPac.ModeloCanonico.Model;

namespace ReyBanPac.TokenES.Repository.Contract
{
    public interface IRepository
    {
        public Task<TokenModel> Guardar(TokenModel TokenModel);

        public Task<TokenModel> Actualizar(TokenModel TokenModel);

        public Task<int> Eliminar(int Id);

        public Task<List<TokenModel>> Consultar();

        public Task<TokenModel> ConsultarPorId(int Id);

        public Task<TokenModel> ConsultarPorVigencia();

        public Task<bool> ValidarExistencia(int Id);
    }
}
