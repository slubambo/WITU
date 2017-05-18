using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;

namespace WITU.Core.Repositories.Interfaces
{
    public interface IOauthRepository
    {
        Client FindClient(string clientId);
        bool AddRefreshToken(RefreshToken token);
        bool RemoveRefreshToken(string refreshTokenId);
        bool RemoveRefreshToken(RefreshToken refreshToken);
        RefreshToken FindRefreshToken(string refreshTokenId);
        IList<RefreshToken> GetAllRefreshTokens();
    }
}
