using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Database
{
    public class SqlServerDbProvider : DbContext, IDatabaseProvider
    {
        public SqlServerDbProvider()
            : base("Default")
        {
            
        }

        public DbDataReader GetReader(string commandText)
        {
            try
            {
                if (Database.Connection.State == ConnectionState.Executing ||
                    Database.Connection.State == ConnectionState.Fetching ||
                    Database.Connection.State == ConnectionState.Connecting)
                {
                    Thread.Sleep(500);
                    return GetReader(commandText);
                }

                if (Database.Connection.State == ConnectionState.Closed)
                    Database.Connection.Open();

                var dbCommand = Database.Connection.CreateCommand();
                dbCommand.CommandText = commandText;
                dbCommand.CommandType = CommandType.Text;
                var reader = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception)
            {
                if (Database.Connection.State == ConnectionState.Open)
                    Database.Connection.Close();
                throw;
            }
        }

        public void GetDataSet(string commandText)
        {
            throw new System.NotImplementedException();
        }
    }

}
