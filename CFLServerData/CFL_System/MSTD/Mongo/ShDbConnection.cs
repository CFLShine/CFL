
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MSTD.Mongo
{
    public class ShDbConnection
    {
        public ShDbConnection(string databaseName)
        {
            __databaseName = databaseName;
        }

        public bool Connect()
        {
            if(ServerIsAlive())
                return true;

            if(string.IsNullOrWhiteSpace(__databaseName))
                throw new System.Exception("__databaseName doit être renseigné");

            // pas utile de donner une connectionstring
            // pour un serveur local.
            MongoServer _server = __client.GetServer();
            __database = _server.GetDatabase(__databaseName); 

            return ServerIsAlive();
        }

        public bool ServerIsAlive()
        {
            MongoServer _server = __client.GetServer();
            _server.VerifyState();
            return _server.State == MongoServerState.Connected;
        }

        public MongoDatabase DataBase { get => __database; }

        private MongoDatabase __database = null;
        MongoClient __client = new MongoClient();

        private string __connectionString = "";
        private string __databaseName = "";
    }
}
