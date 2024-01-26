using ReyBanPac.ModeloCanonico.Model;
using ReyBanPac.ModeloCanonico.Type;

namespace ReyBanPac.TokenES.Utils
{
    public static class Converts
    {
        public static TokenType ConvertirModelAType(TokenModel Model)
        {
            TokenType TokenType = new TokenType();

            if (Model != null)
            {
                TokenType.Id = Model.Id;
                TokenType.Token = Model.Token;
                TokenType.Vigencia = Model.Vigencia;
                TokenType.Canal = Model.Canal;
                TokenType.Estado = Model.Estado;
            }

            return TokenType;
        }

        public static TokenModel ConvertirTypeAModel(TokenType TokenType)
        {
            TokenModel Model = new TokenModel();
            if (TokenType != null)
            {
                Model.Id = TokenType.Id;
                Model.Token = TokenType.Token;
                Model.Vigencia = TokenType.Vigencia;
                Model.Canal = TokenType.Canal;
                Model.Estado = TokenType.Estado;
            }

            return Model;
        }

        public static List<TokenType> ConvertirListModelToListType(List<TokenModel> ListadoModel)
        {
            List<TokenType> ListadoType = new List<TokenType>();
            if (ListadoModel != null)
            {
                foreach (TokenModel Item in ListadoModel)
                {
                    ListadoType.Add(ConvertirModelAType(Item));
                }
            }
            return ListadoType;
        }
    }
}
