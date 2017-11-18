
using MSTD.ShBase;

namespace SqlOrm
{
    public abstract class Query
    {
        public Query(ShContext _dbContext, DBConnection connection)
        {
            Context = _dbContext;
            Connection = connection;
        }

        public ShContext Context
        { 
            get; 
            private set; 
        }

        public DBConnection Connection
        {
            get;
            private set;
        }

    }
}
