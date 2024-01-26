using Microsoft.EntityFrameworkCore;
using ReyBanPac.ModeloCanonico.Constans;
using ReyBanPac.ModeloCanonico.Model;
using ReyBanPac.TokenES.Constans;
using ReyBanPac.TokenES.Repository.Context;
using ReyBanPac.TokenES.Repository.Contract;


namespace ReyBanPac.TokenES.Repository.Impl
{
    public class RepositoryImpl : IRepository
    {
        private readonly Db db;
        private readonly ILogger<RepositoryImpl> _logger;
        public RepositoryImpl(Db dbContex, ILogger<RepositoryImpl> logger)
        {
            this.db = dbContex;
            _logger = logger;
        }

        public async Task<TokenModel> Guardar(TokenModel TokenModel)
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Repository");
            try
            {
                TokenModel.Fecha_Creacion = DateTime.Now;

                db.Database.ExecuteSqlRaw("UPDATE [dbo].[Token] SET [estado] = 'I' ,[fecha_actualizacion] = GETDATE();");
                var Item = db.Models.Add(TokenModel);
                await db.SaveChangesAsync();
                return Item.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                throw;
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Repository");
            }
        }

        public async Task<TokenModel> Actualizar(TokenModel TokenModel)
        {
            try
            {
                TokenModel.Fecha_Actualizacion = DateTime.Now;
                var Item = db.Models.Add(TokenModel);
                db.Entry(TokenModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Item.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                throw;
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Repository");
            }
        }

        public async Task<int> Eliminar(int Id)
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Repository");

            try
            {

                var Reg = await db.Models.FindAsync(Id) ?? new TokenModel();
                Reg.Fecha_Actualizacion = DateTime.Now;
                Reg.Estado = Estados.ELIMINADO;
                return await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                throw;
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Repository");
            }
        }

        public async Task<List<TokenModel>> Consultar()
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Repository");

            try
            {
                return await db.Models.Where(x => x.Estado != Estados.ELIMINADO).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                throw;
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Repository");
            }
        }

        public async Task<TokenModel> ConsultarPorId(int Id)
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Repository");

            try
            {
                return await db.Models.FindAsync(Id) ?? new TokenModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                throw;
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Repository");
            }
        }

        public async Task<TokenModel> ConsultarPorVigencia()
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Repository");

            try
            {
                return await db.Models.Where(x=> x.Estado == Estados.ACTIVO && x.Vigencia >= DateTime.Now).FirstOrDefaultAsync() ?? new TokenModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                throw;
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Repository");
            }
        }

        public async Task<bool> ValidarExistencia(int Id)
        {
            _logger.LogInformation($"{General.Nombre_Servicio}  Inicio Repository");

            try
            {
                return await db.Models.AnyAsync(item => item.Id == Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{General.Nombre_Servicio}  Error Interno del Servidor");
                throw;
            }
            finally
            {
                _logger.LogInformation($"{General.Nombre_Servicio}  Fin Repository");
            }
        }

    }
}
