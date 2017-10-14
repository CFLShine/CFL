
using System.Collections.Generic;
using System.Reflection;
using MSTD;
using MSTD.ShBase;

namespace SqlOrm
{
    public class DBSelect<T> : Query where T : Base, new()
    {
        public DBSelect(DBContext _dbContext)
            : base(_dbContext)
        {}

        public DBSelect<T> Select(params string[] _members)
        {
            SelectedMembers = _members;
            return this;
        }

        public string[] SelectedMembers
        {
            get;
            private set;
        } 

        public List<T> ToList() 
        {
            DBLoader<T> _loader = new DBLoader<T>(this);
            return _loader.ToList();
        }

        public T First()
        {
            DBLoader<T> _loader = new DBLoader<T>(this);
            T _class =  _loader.First();
            return _class;
        }

        public DBSelect<T> Where(string _conditions)
        {
            WherePredicats = _conditions;
            return this;
        }

        public string WherePredicats
        {
            get;
            private set;
        } = "";

        public X LoadMother<X> () where X : Base, new()
        {
            DBLoader<T> _loader = new DBLoader<T>(this);
            return _loader.LoadMother<X>();
        }


        /// <summary>
        /// Inclu un membre de type class ou list de class.
        /// Pour inclure tous les membres de type class ou list de class,
        /// passer "ALL" en argument.
        /// Non sensible à la casse.
        /// </summary>
        public DBSelect<T> Include(string _memberName)
        {
            if(_memberName.ToLower() == "all" || _memberName == "*")
                IncludeAll();
            else
            {
                if(!Includeds.Contains(_memberName))
                    Includeds.Add(_memberName);
            }
            return this;
        }

        public List<string> Includeds
        {
            get;
        } = new List<string>();

        private void IncludeAll()
        {
            foreach(PropertyInfo _prInfo in typeof(T).GetProperties())
            {
                SqlType _sqlType = SqlCSharp.GetSqlType(_prInfo.PropertyType);
                if(_sqlType == SqlType.CLASS)
                    Include(_prInfo.Name);
                else
                if(_sqlType == SqlType.LIST 
                    && PropertyHelper.IsMappableProperty(_prInfo)
                    && TypeHelper.IsListOf(_prInfo.PropertyType, typeof(Base)))
                        Include(_prInfo.Name);
            }
        }

    }
}
