using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MongoDB.Bson;
using MSTD.ShBase;

namespace MSTD.Mongo
{
    public class Serializer
    {
        public static BsonDocument Document(Base entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");

            BsonDocument _document = new BsonDocument();

            foreach(PropertyInfo _prInfo in entity.GetType().GetProperties())
            {
                Type _t = _prInfo.PropertyType;
                if(PropertyHelper.IsMappableProperty(_prInfo))
                {
                    object _value = _prInfo.GetValue(entity);
                    _document.Add(_prInfo.Name, Value(_value));
                }
            }

            return _document;
        }

        public static BsonValue Value(object _value)
        {
            if(_value == null)
                return BsonValue.Create(null);

            Type _t = _value.GetType();
            Type _baseType = typeof(Base);

            if(_t == _baseType || _t.IsSubclassOf(_baseType))
            {
                return BsonValue.Create(((Base)_value).ID);
            }

            if(TypeHelper.IsListOf(_t, _baseType))
            {
                List<Guid> _guids = new List<Guid>();
                foreach(object _o in ((IList)_value))
                {
                    if(_o != null)
                        _guids.Add(((Base)_o).ID);
                }
                return new BsonArray(_guids);
            }

            return BsonValue.Create(_value);
        }

    }
}
