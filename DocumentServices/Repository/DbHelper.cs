using System.Configuration;
using System.Data;
using DocumentServices.Exceptions;
using ServiceStack.OrmLite;

namespace DocumentServices.Repository
{
    public class DbHelper
    {
        private static OrmLiteConnectionFactory _dbFactory;

        public static IDbConnection CreateConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            if (connectionString == null)
            {
                throw new ConnectionStringNotFoundException();
            }
            if (_dbFactory == null)
            {
                _dbFactory = new OrmLiteConnectionFactory(connectionString.ConnectionString, SqlServerDialect.Provider);
            }
            return _dbFactory.OpenDbConnection();
        }
    }
}