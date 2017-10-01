
namespace SqlOrm
{
    public abstract class Query
    {
        public Query(DBContext _dbContext)
        {
            DbContext = _dbContext;
        }

        public DBContext DbContext
        { 
            get; 
            protected set; 
        }

        public DBConnection Connection
        {
            get
            {
                return DbContext.Connection;
            }
        }
    }
}
