
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CFL_1.CFL_System; // utile pour le commentaire de la classe
using MSTD;

namespace SqlOrm
{
    /// <summary>
    /// Utilisation : 
    /// Pour chaque classe <see cref="Base"/> qui devra être représentée dans la db,
    /// déclarer et instancier un <see cref="DBSet"/> dans le corps de la classe héritant de <see cref="DBContext"/>comme suit : 
    /// <c><see cref="DBSet"/>{MaClass} MaClass { get; set }</c>
    /// </summary>
    public abstract class DBContext
    {
        public DBContext(DBConnection _connection)
        {
            __connection = _connection;
            BuildDBSetList();
        }

        public DBConnection Connection
        {
            get
            {
                return __connection;
            }
        }

        public bool CreateOrCompleteDataBase()
        {
            DBCreation _creator = new DBCreation(Connection, this);
            return _creator.CreateOrCompleteDataBase();
        }

        #region GetDbSet

        /// <summary>
        /// Retourne le DBSet dont les entités contenues sont de type _type.
        /// </summary>
        public DBSet GetDBSet(Type _type)
        {
            foreach(DBSet _dbSet in __dbSets)
            {
                if(_dbSet.EntitiesType == _type)
                    return _dbSet;
            }
            return null;
        }

        /// <summary>
        /// Retourne le DBSet dont les entités contenues sont de type _typeName.
        /// Non sensible à la casse.
        /// </summary>
        public DBSet GetDBSet(string _typeName)
        {
            foreach(DBSet _dbSet in __dbSets)
            {
                if(_dbSet.EntitiesType.Name.ToLower() == _typeName.ToLower())
                    return _dbSet;
            }
            return null;
        }

        /// <summary>
        /// Retourne le DBSet dont les entités contenues sont de type T.
        /// </summary>
        public DBSet<T> GetDBSet <T>() where T : class, new()
        {
            foreach(DBSet _dbset in __dbSets)
            {
                if(typeof(T) == _dbset.EntitiesType)
                    return (DBSet<T>)_dbset;
            }
            return null;
        }

        public IEnumerable<DBSet> GetDbSets()
        {
            foreach(DBSet _dbset in __dbSets)
                yield return _dbset;
        }

        public ClassProxy GetClassProxy(Guid _guid)
        {
            ClassProxy _classProxy = null;

            foreach(DBSet _dbset in GetDbSets())
            {
                _classProxy = _dbset.GetClassProxy(_guid);
                if(_classProxy != null)
                    return _classProxy;
            }
            return null;
        }

        #endregion GetDbSet

        #region Processeds

        /// <summary>
        /// Ajoute _guid.ToString() à la liste des objets pour lesquels une requète
        /// a été préparée ou envoyée à la DB,
        /// évitant de traiter plusieurs fois le même objet.
        /// Lève une exeption si <see cref="StartProcess"/> n'a pas été invoqué.
        /// </summary>
        public void Process(ClassProxy _proxy)
        {
            if(__procededs == null)
                throw new Exception("StartSelect n'a pas été invoqué");
            __procededs[_proxy.EntityId.ToString()] = _proxy;
        }

        /// <summary>
        /// Ajoute _guidstr à la liste des objets pour lesquels une requète
        /// a été préparée ou envoyée à la DB,
        /// évitant de traiter plusieurs fois le même objet.
        /// Lève une exeption si <see cref="StartProcess"/> n'a pas été invoqué.
        /// </summary>
        public void Process(string _guidstr)
        {
            if(__procededs == null)
                throw new Exception("StartSelect n'a pas été invoqué");
            if(__procededs.ContainsKey(_guidstr) == false)
                __procededs[_guidstr] = null;
        }

        public bool IsProceded(Guid _id)
        {
            return IsProssed(_id.ToString());
        }

        public bool IsProssed(string _guidstr)
        {
            return __procededs.ContainsKey(_guidstr);
        }

        public bool IsStartedProcessing
        {
            get
            {
                return __procededs != null;
            }
        }

        public void StartProcess()
        {
            __procededs = new Dictionary<string, ClassProxy>();
        }

        public void EndProcess()
        {
            __procededs.Clear();
            __procededs = null;
        }

                //guid toString(), proxy
        private Dictionary<string, ClassProxy> __procededs = null;

        #endregion Processeds

        private void BuildDBSetList()
        {
            foreach(PropertyInfo _prInfo in this.GetType().GetProperties())
            {
                if(_prInfo.PropertyType.IsSubclassOf(typeof(DBSet)))
                {
                    DBSet _dbset = (DBSet)(Activator.CreateInstance(_prInfo.PropertyType));
                    Type _entityType = _dbset.EntitiesType;

                    if(_entityType.Name.Contains("_"))
                        throw new Exception("Le symbol '_' est présent dans le nom de la classe " + _entityType.Name + 
                                            " ce qui est interdit pour un type représenté dans la db.");

                    foreach(PropertyInfo _pr in _entityType.GetProperties())
                    {
                        if(ObjectHelper.IsMappableProperty(_pr) && _pr.Name.Contains("_"))
                            throw new Exception("Le symbol '_' est présent dans le nom de la propriété " + _pr.Name + " de la classe " + _entityType.Name +
                                                " ce qui est interdit pour une propriété représentée dans la db.");
                    }

                    _dbset.Context = this;
                    _prInfo.SetValue(this, _dbset);
                    __dbSets.Add(_dbset);
                }
            }
        }

        #region Attach

        /// <summary>
        /// Si _entity n'est pas déja contenu dans le DBSet correspondant au type de _entity,
        /// ajoute un ClassProxy de _entity dans ce DBSet,
        /// puis ajoute toutes les entité contenues dans _entity (comme membre ou dans une liste).
        /// </summary>
        public void Attach(object _entity)
        {
            __attacheds.Clear();
            if(_entity != null)
            {
                DBSet _dbset = GetDBSet(_entity.GetType());
                // Le type d'_entity n'a pas été représenté dans le DBContext
                if(_dbset == null)
                    throw new Exception("Le type d'_entity " + _entity.GetType().Name + " n'a pas été représenté dans le DBContext");
                
                ClassProxy _proxy = GetProxy(_entity);
                Attach(_proxy);
            }
        }

        private ClassProxy GetProxy(object _entity)
        {
            DBSet _dbset = GetDBSet(_entity.GetType());
            // Le type d'_entity n'a pas été représenté dans le DBContext
            if(_dbset == null)
                throw new Exception("Le type d'_entity " + _entity.GetType().Name + " n'a pas été représenté dans le DBContext");
            return _dbset.Attach(_entity);
        }

        private void Attach(ClassProxy _proxy)
        {
            if(!__attacheds.Contains(_proxy))
            {
                __attacheds.Add(_proxy);
                AttachMembers(_proxy);
            }
        }

        private List<ClassProxy> __attacheds = new List<ClassProxy>();

        public void AttachProxy(ClassProxy _proxy)
        {
            DBSet _dbset = GetDBSet(_proxy.EntityType);
            _dbset.Attach(_proxy.EntityId, _proxy);
        }

        private void AttachMembers(ClassProxy _proxy)
        {
            foreach(PropertyInfo _prInfo in _proxy.Properties)
            {
                if(ObjectHelper.IsMappableClassType(_prInfo.PropertyType))
                {
                    object _classMember = _prInfo.GetValue(_proxy.Entity);
                    if(_classMember != null)
                        Attach(GetProxy(_classMember));
                }
                else
                if(ObjectHelper.IsMappableListOfClass(_prInfo))
                {
                    IList _list = _prInfo.GetValue(_proxy.Entity) as IList;
                    foreach(object _entity in _list)
                    {
                        if(_entity != null)
                            Attach(GetProxy(_entity));  
                    }
                }
            }
        }

        #endregion Attach

        /// <summary>
        /// Met à jour les membres de classe et les listes de classes de l'entité
        /// de chaque ClassProxy contenus dans chaque DBSet en bouclant sur ses proxis de membres et de listes, 
        /// lesquels récupèrent les entités dans le DBContext si elles existent.
        /// </summary>
        public void UpdateClassProxiesEntities()
        {
            foreach(DBSet _dbset in GetDbSets())
                _dbset.UpdateClassProxiesEntities();
        }

        public bool SaveChanges()
        {
            return new DBSaveChanges(this).Exe();
        }

        public void CancelProcessedsChanges()
        {
            if(__procededs == null)
                throw new Exception("StartSelect n'a pas été invoqué");
            foreach(KeyValuePair<string, ClassProxy> _kvp in __procededs)
                _kvp.Value.CancelChanges();
        }

        public DBSelect<T> Select<T>(params string[] _fields) where T : class, new()
        {
            DBSelect<T> _select = new DBSelect<T>(this);
            _select.Select(_fields);
            return _select;
        }

        private List<DBSet> __dbSets = new List<DBSet>();
        private DBConnection __connection = null;

    }
}
