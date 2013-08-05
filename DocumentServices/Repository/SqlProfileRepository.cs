using System.Collections.Generic;
using DocumentServices.Abstractions;
using DocumentServices.Interceptors;
using DocumentServices.Models;
using ServiceStack.OrmLite;

namespace DocumentServices.Repository
{
    public class SqlProfileRepository : IProfileRepository, IRepository
    {
        [RequestCache]
        public virtual IList<Profile> GetProfileForUser(int id)
        {
            using (var connection = DbHelper.CreateConnection())
            {
                return connection.Select<Profile>();
            }
        }
    }
}