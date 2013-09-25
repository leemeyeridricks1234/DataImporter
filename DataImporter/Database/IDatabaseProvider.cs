using System.Data.Common;

namespace Database
{
    public interface IDatabaseProvider
    {
        DbDataReader GetReader(string commandText);
        void GetDataSet(string commandText);
    }
}