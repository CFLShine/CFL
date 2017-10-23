
using System.Collections.Generic;
using System.Reflection;
using MSTD;
using MSTD.ShBase;

namespace SqlOrm
{
    public class DBSelect<T> : Query where T : Base, new()
    {
        public DBSelect(ShContext _context, DBConnection connection)
            : base(_context, connection)
        {}

        public DBSelect<T> Select(params string[] _members)
        {
            if(_members != null)
            {
                foreach(string _member in _members)
                    SelectedMembers.Add(_member);
            }

            return this;
        }

        public List<string> SelectedMembers
        {
            get;
            private set;
        } = new List<string>();

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
                IncludeAll(); // IncludeAll appèle Include(string _memberName)
                              // sur chaque membre de type Base ou dérivé ou List<Base ou dérivé>
            else
            {
                if(!Includeds.Contains(_memberName))
                    Includeds.Add(_memberName);
                if(!SelectedMembers.Contains(_memberName))
                    SelectedMembers.Add(_memberName);
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
                if(_sqlType == SqlType.CLASS
                || (_sqlType == SqlType.LIST 
                    && PropertyHelper.IsMappableProperty(_prInfo)
                    && TypeHelper.IsListOf(_prInfo.PropertyType, typeof(Base))))
                        Include(_prInfo.Name);
            }
        }
    }
}
