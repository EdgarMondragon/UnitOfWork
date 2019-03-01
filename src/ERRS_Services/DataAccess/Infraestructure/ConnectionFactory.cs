using GodSharp.Data.Common.DbProvider;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Infraestructure
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration connectionString;
        private IDbConnection Connection;

        public ConnectionFactory(IConfiguration config)
        {
            connectionString = config;
        }
        

        public IDbConnection GetConnection(bool IsIarBD = false)
        {
           
                DbProviderManager.LoadConfiguration(connectionString);

                Connection = DbProviderFactories.GetFactory(connectionString.GetConnectionString("ProviderName")).CreateConnection();
                Connection.ConnectionString = connectionString.GetConnectionString(IsIarBD ? "IarConnectionString" : "ConnectionString");

                return Connection;
        }

        private bool disposedValue = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
    }


}

