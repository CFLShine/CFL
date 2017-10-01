using System;
using System.Collections.Generic;
using Npgsql;

namespace SqlOrm
{
    public class DBField
    {
        public string Name
        {
            get;
            set;
        } = "";

        public object Value
        {
            get;
            set;
        } = null;
    }

    public class DBRow : List<DBField>
    {
        public object GetValue(int _i)
        {
            return this[_i].Value;
        }

        public object GetValue(string _fieldName)
        {
            for(int _i = 0 ; _i < this.Count; _i++)
            {
                if (this[_i].Name == _fieldName)
                    return this[_i].Value;
            }
            return null;
        }

        public string GetFieldName(int _index)
        {
            return this[_index].Name;
        }

        public Guid GetId()
        {
            return (Guid)GetValue("id");
        }

        public string GetTableName()
        {
            return (string)GetValue("tablename");
        }
    }

    public class DBReader
    {
        public DBReader(DBConnection _connection, string _query)
        {
            NpgsqlCommand _command = new NpgsqlCommand(_query);
            Init(_connection.ExecuteQuery(_command));
        }

        private void Init(NpgsqlDataReader _reader)
        {
            if(_reader == null)
                return;

            __rows = new List<DBRow>();

            using(_reader)
            {
                while (_reader.Read())
                {
                    DBRow _row = null;

                    for (int _i = 0; _i < _reader.FieldCount; _i++)
                    {
                        DBField _field = new DBField();
                        _field.Name = _reader.GetName(_i);
                        _field.Value = _reader.GetValue(_i);

                        if (_field.Name == "tablename")
                        {
                            _row = new DBRow();
                            __rows.Add(_row);
                        }

                        if (_row != null)
                            _row.Add(_field);
                        else
                            throw new Exception("_row == null. Le champs 'tablename' n'aurait-il pas été trouvé en première position ?");
                    }
                    _reader.NextResult();
                }
            }
        }

        /// <summary>
        /// Se positionne avant la première ligne.
        /// </summary>
        public void GotoStart()
        {
            __currentRowIndex = -1;
            __currentRow = null;
        }

        /// <summary>
        /// S'il reste au moins une ligne à lire, se positionne dessus et retourne true, sinon false.
        /// </summary>
        public bool Read()
        {
            if (__rows == null || ++__currentRowIndex >= __rows.Count)
                return false;
            __currentRow = __rows[__currentRowIndex];
            return true;
        }

        /// <summary>
        /// Permet de s'assurer qu'au moins un Read a été fait et qu'il y a une ligne en cours à lire.
        /// </summary>
        public bool OnRow
        {
            get
            {
                return __currentRow != null;
            }
        }

        /// <summary>
        /// Retourne le nombre de lignes.
        /// </summary>
        /// <returns></returns>
        public int RowCount
        { 
            get 
            { 
                return __rows.Count; 
            } 
        }

        public DBRow CurrentRow
        {
            get
            {
                return __currentRow;
            }
        }

        private DBRow __currentRow = null;
        private int __currentRowIndex = -1;

        private List<DBRow> __rows = null;
    }
}
