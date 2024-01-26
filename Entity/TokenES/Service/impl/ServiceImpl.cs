using ReyBanPac.ModeloCanonico.Model;
using ReyBanPac.ModeloCanonico.Type;
using ReyBanPac.ModeloCanonico.Utils;
using ReyBanPac.TokenES.Constans;
using ReyBanPac.TokenES.Repository.Contract;
using ReyBanPac.TokenES.Service.Contract;
using ReyBanPac.TokenES.Utils;


namespace ReyBanPac.TokenES.Service.Impl
{
    public class ServiceImpl : IService
    {
        private readonly ILogger<ServiceImpl> _logger;
        private readonly IRepository Repository;

        public ServiceImpl(IRepository repositorio, ILogger<ServiceImpl> logger)
        {
            Repository = repositorio;
            _logger = logger;
        }

        public async Task<TokenType> Guardar(TokenType TokenType)
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Service");

            try
            {
                TokenModel TokenModel = Converts.ConvertirTypeAModel(TokenType);
                TokenModel = await Repository.Guardar(TokenModel);
                return Converts.ConvertirModelAType(TokenModel);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                throw;
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Service");
            }
        }

        public async Task<TokenType> Actualizar(TokenType TokenType)
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Service");

            try
            {
                bool Existe = await Repository.ValidarExistencia(TokenType.Id);
                if (Existe)
                {
                    TokenModel TokenModel = Converts.ConvertirTypeAModel(TokenType);
                    TokenModel = await Repository.Actualizar(TokenModel);
                    return Converts.ConvertirModelAType(TokenModel);
                }
                else
                {
                    throw new ServiceException("El registro no fue encontrado") { Codigo = StatusCodes.Status404NotFound };
                }
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                throw;
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Service");
            }



        }

        public async Task<int> Eliminar(int Id)
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Service");

            try
            {
                bool Existe = await Repository.ValidarExistencia(Id);
                if (Existe)
                {
                    return await Repository.Eliminar(Id);
                }
                else
                {
                    throw new ServiceException("El registro no fue encontrado") { Codigo = StatusCodes.Status404NotFound };
                }
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                throw;
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Service");
            }



        }

        public async Task<List<TokenType>> Consultar()
        {

            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Service");

            try
            {
                List<TokenModel> ListadoModel = await Repository.Consultar();
                List<TokenType> ListadoType = Converts.ConvertirListModelToListType(ListadoModel);
                return ListadoType;
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                throw;
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Service");
            }


        }

        public async Task<TokenType> ConsultarPorId(int Id)
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Service");

            try
            {
                TokenModel TokenModel = await Repository.ConsultarPorId(Id);

                return Converts.ConvertirModelAType(TokenModel);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                throw;
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Service");
            }

        }


        public async Task<TokenType> ConsultarPorVigencia()
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Service");

            try
            {
                TokenModel TokenModel = await Repository.ConsultarPorVigencia();

                return Converts.ConvertirModelAType(TokenModel);
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                throw;
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Service");
            }

        }


    }
}
