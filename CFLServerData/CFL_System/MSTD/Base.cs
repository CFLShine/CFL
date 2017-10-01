using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MSTD
{
    public abstract class Base
    {
        public Base()
        {
            PropertiesTypesNames = new Dictionary<string, string>();

            
        }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(AutoGenerateField = false)]
        public Guid ID { get; set; } = Guid.NewGuid();
        
        [NotMapped]
        public Dictionary<string, string> PropertiesTypesNames
        { 
            get
            {
                Dictionary<string, string> _propertiesTypesNames = new Dictionary<string, string>();
                foreach(PropertyInfo _prInfo in GetType().GetProperties())
                {
                    if(_prInfo.PropertyType.IsPublic
                    && _prInfo.CanRead
                    && _prInfo.CanWrite)
                    {
                        _propertiesTypesNames[_prInfo.Name] = _prInfo.PropertyType.Name;
                    }
                }
                return _propertiesTypesNames;
            }
            set
            { // do nothing }
        }
    }   
}
