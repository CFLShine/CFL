
using System.Runtime.Serialization;
using CFL_1.CFL_System;

namespace CFL_1.CFL_Data
{
    [DataContract]
    public class Config 
    {
        public Config()
        {}
        
        public static bool load(ref Config _config)
        { return Gate.load.config(ref _config) ; }

        public void save()
        {
            Gate.save.config(this);
        }

        // DB
        [DataMember]
        public string hostname { get { return __hostname; } set { __hostname = value; } }
        [DataMember]
        public string username { get { return __username; } set { __username = value; } }
        [DataMember]
        public string password { get { return __password; } set { __password = value; } }
        [DataMember]
        public string dbname   { get { return __dbname  ; } set { __dbname = value; } }
        //


        //private:
        private string __hostname = "";
        private string __username = "";
        private string __password = "";
        private string __dbname   = "";
    }   
}       
