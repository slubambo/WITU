using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Repositories.Interfaces;
using WITU.Core.Model;
using NHibernate;
using NHibernate.Linq;

namespace WITU.Core.Repositories.Impl
{
    public class OauthRepository: IOauthRepository
    {
        private readonly ISession _session;
        

        public OauthRepository(ISession session)
        {
            _session = session;
        }
        public Client FindClient(string clientId)
        {
            var client = _session.Query<Client>().FirstOrDefault(x => x.Id.Equals(clientId));
            return client;
        }

        public bool AddRefreshToken(RefreshToken token)
        {
            var existingToken = _session.Query<RefreshToken>().FirstOrDefault(r => r.Subject == token.Subject && r.ClientId == token.ClientId);

            if (existingToken != null)
            {
                var result =  RemoveRefreshToken(existingToken);
            }



            return SaveOrUpdate(token); ;
        }

        public bool RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken =_session.Query<RefreshToken>().FirstOrDefault(r => r.Id.Equals(refreshTokenId));

            if (refreshToken != null)
            {
                bool result = Delete(refreshToken);
                return  result;
            }

            return false;
        }

        public bool RemoveRefreshToken(RefreshToken refreshToken)
        {
            
            return Delete(refreshToken);
        }

        public RefreshToken FindRefreshToken(string refreshTokenId)
        {
            var refreshToken =  _session.Query<RefreshToken>().FirstOrDefault(r => r.Id.Equals(refreshTokenId));

            return refreshToken;
        }

        public IList<RefreshToken> GetAllRefreshTokens()
        {
            var query =_session.CreateCriteria<RefreshToken>();
            return query.List<RefreshToken>();
        }

        private bool SaveOrUpdate(object entity)
        {
            var result = false;

            ////check if the session is closed..
            //if (!_session.IsOpen)
            //{
            //    _session.Connection.Open();
            //}


            using (var txn = _session.BeginTransaction())
            {
                try
                {
                    _session.SaveOrUpdate(entity);
                    txn.Commit();
                    result = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    txn.Rollback();
                    //_session.Close();
                }
            }
            //sess.Close();
            return result;

            //_session.SaveOrUpdate(entity);
            //result = true;
            //return result;
        }

        private bool Delete(object entity)
        {
            var result = false;
            using (var txn = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(entity);
                    txn.Commit();
                    result = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    txn.Rollback();
                }
            }
            return result;
        }
    }
}
