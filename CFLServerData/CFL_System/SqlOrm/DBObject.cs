using System;
using System.Runtime.Serialization;

namespace CFL_1.CFL_System.SqlServerOrm
{
    [DataContract]
    public class DBObject
    {
        public DBObject(){}

        public DBObject(string _tableName, Guid _id)
        {
            TableName = _tableName;
            ID = _id;
        }

        [DataMember]
        public string TableName { get; set; }

        [DataMember]
        public Guid ID { get; set; }
    }
}
