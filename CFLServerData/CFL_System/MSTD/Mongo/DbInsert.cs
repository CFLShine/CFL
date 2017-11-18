using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MSTD.Mongo;
using MSTD.ShBase;

namespace CFL_1.CFL_System.MSTD.Mongo
{
    public class DbInsert
    {
        public DbInsert(ShContext context, ShDbConnection connection)
        {
            __context = context;
            __connection = connection;
        }

        /// <summary>
        /// Recherche dans le <see cref="ShContext"/> toutes les <see cref="ClassProxy"/>
        /// représentant des entités non enregistrées dans la db et les sauvegarde
        /// dans leurs collections respectives.
        /// </summary>
        public void InsertNewProxies()
        {
            foreach(Set _set in __context.GetSets())
            {
                List<ClassProxy> _inserteds = new List<ClassProxy>();
                List<BsonDocument> _documents = new List<BsonDocument>();

                foreach(ClassProxy _proxy in _set.GetProxies())
                {
                    if(_proxy.IsNew && _proxy.Entity != null)
                    {
                        _documents.Add(Serializer.Document(_proxy.Entity));
                        _inserteds.Add(_proxy);
                    }
                }

                
                if(_documents.Count() > 0)
                {
                    // Execution de la requète
                    IEnumerable<WriteConcernResult> _ins = __connection.DataBase.GetCollection(_set.Type.Name).InsertBatch(_documents);
                    
                    // Vérification que les inserts ont été effectués,
                    // le cas échéant, IsNew = false pour le proxy correspondant au résultat
                    int _i = 0;
                    foreach(var _consernResult in _ins)
                    {
                        if(_consernResult.Ok)
                        {
                            _inserteds[_i].IsNew = false;
                        }
                    }
                }
                    
            }
        }

        public void Prepare(ClassProxy proxy)
        {
            if(proxy == null)
        }

        
        private Dictionary<string, List<ClassProxy>> __prepareds = new Dictionary<string, List<ClassProxy>>();
        private ShContext __context = null;
        private ShDbConnection __connection = null;
    }
}
