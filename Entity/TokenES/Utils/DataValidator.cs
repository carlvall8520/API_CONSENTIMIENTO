using ReyBanPac.ModeloCanonico.Type;
using ReyBanPac.ModeloCanonico.Utils;

namespace ReyBanPac.TokenES.Utils
{
    public static class DataValidator
    {
        public static string ValidarResultadoByCreate(TokenType TokenType)
        {
            if (TokenType == null)
            {
                throw new ServiceException("Error al crear el registro") { Codigo = StatusCodes.Status400BadRequest };
            }
            return "Registro creado con id: " + TokenType.Id;
        }

        public static string ValidarResultadoByUpdate(TokenType TokenType)
        {
            if (TokenType == null)
            {
                throw new ServiceException("Error al actualizar el registro") { Codigo = StatusCodes.Status400BadRequest };
            }
            return "El registro con id: " + TokenType.Id + " fue actualizado de forma exitosa";
        }

        public static string ValidarResultadoByDelete(int Confirmacion)
        {
            if (Confirmacion == 0)
            {
                throw new ServiceException("Error al eliminar el registro") { Codigo = StatusCodes.Status400BadRequest };
            }
            else
            {
                return "Registro eliminado de forma exitosa";
            }

        }
        public static Object ValidarResultadoConsulta(List<TokenType> Configuracion)
        {
            if (Configuracion != null)
            {
                return Configuracion;
            }
            else
            {
                throw new ServiceException("No hay Contenido") { Codigo = StatusCodes.Status404NotFound };
            }
        }

        public static Object ValidarResultadoConsulta(TokenType TokenType)
        {
            if (TokenType != null && TokenType.Id != 0)
            {
                return TokenType;
            }
            else
            {
                throw new ServiceException("No hay Contenido") { Codigo = StatusCodes.Status404NotFound };
            }
        }
    }
}
