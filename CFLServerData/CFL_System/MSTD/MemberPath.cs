
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace MSTD
{
    /// <summary>
    /// <see cref="REMemberPath"/> retient une expression de membre sous
    /// forme d'une liste de string, les noms de chaque membre de l'expression
    /// et d'une liste de PropertyInfo, les propriétés invoquées par l'expression.
    /// <see cref="PropertyHolder"/> utilise un <see cref="REMemberPath"/> pour retrouver
    /// la propriété initialement indiquée, de l'objet qui lui est donné, puis procurer ou assigner sa valeur.
    /// </summary>
    public class REMemberPath
    {
        public Guid ID { get; set; } = Guid.NewGuid();

        public REMemberPath(){ }

        /// <summary>
        /// Expression (exemple): ()=>_object.a.nom
        /// </summary>
        public REMemberPath(Expression<Func<object>> _expression)
        {
            extractMembersTypesAndNames(_expression);
        }

        public REMemberPath(REMemberPath _other)
        {
            foreach(string _s in _other.Names)
                Names.Add(_s);
            foreach(PropertyInfo _prInfo in _other.Properties)
                Properties.Add(_prInfo);
        }

        public REMemberPath(Type _classType, params string[] _names)
        {
            foreach(string _name in _names)
                Names.Add(_name);
            extractMembersTypes(_classType);
        }

        public REMemberPath(Type _classType, string _path)
        {
            string[] _names = _path.Split('.');
            foreach(string _name in _names)
                Names.Add(_name);
            extractMembersTypes(_classType);
        }

        /// <summary>
        /// _expression (exemple) : ()=>_object.a.nom
        /// </summary>
        /// <param name="_expression"></param>
        public void SetExpression(Expression<Func<object>> _expression)
        {
            extractMembersTypesAndNames(_expression);
        }

        public List<string> Names { get; private set; } = new List<string>();

        /// <summary>
        /// Retourne une chaine représentant l'expression du membre,
        /// ex "Member1.Member2.Member3"
        /// </summary>
        public string ExpressionString
        {
            get
            {
                string _path = "";
                if(Names != null)
                {
                    foreach(string _name in Names)
                    {
                        if(_path != "")
                            _path += ".";
                        _path += _name;
                    }
                }
                return _path;
            }
        }

        /// <summary>
        /// Retourne les PropertyInfo des propriétés de l'expression.
        /// </summary>
        public List<PropertyInfo> Properties { get; private set; } = new List<PropertyInfo>();

        /// <summary>
        /// Retourne le nom de la dernière propriété de l'expression.
        /// Ex : expression a.b.c, retourne "c".
        /// </summary>
        public string PropertyName
        {
            get
            {
                if(Names.Count == 0)
                    return "";
                return Names[Names.Count - 1];
            }
        }

        /// <summary>
        /// Retourne la PropertyInfo de la dernière propriété de l'expression.
        /// Ex : expression a.b.c, retourne la PropertyInfo de c.
        /// </summary>
        public PropertyInfo PropertyInfo
        {
            get
            {
                if(Properties.Count == 0)
                    return null;
                return Properties[Properties.Count - 1];
            }
        }

        private void extractMembersTypesAndNames(Expression<Func<object>> _expression)
        {
            Names.Clear();
            Properties.Clear();
            if(_expression.Body is UnaryExpression _unary)
                extractMembersTypeAndNames((MemberExpression)_unary.Operand);
            else
                extractMembersTypeAndNames((MemberExpression)_expression.Body);
        }

        private void extractMembersTypeAndNames(MemberExpression _memberExpression)
        {
            if(_memberExpression.Member is PropertyInfo _prInfo)
            {
                Properties.Insert(0, _prInfo);
                Names.Insert(0, _memberExpression.Member.Name);

                // appele recursif
                if(_memberExpression.Expression != null && _memberExpression.Expression is MemberExpression _memberExpr)
                    extractMembersTypeAndNames(_memberExpr);
            }
        }

        private void extractMembersTypes(Type _classType)
        {
            Properties.Clear();
            Type _current = _classType;

            foreach(string _name in Names)
            {
                PropertyInfo _prInfo = ObjectHelper.Property(_current, _name);
                if(_prInfo == null)
                {
                    throw new Exception("La propriété " + _name + " du chemin " + ExpressionString +" est introuvable dans le type " + _current.Name);
                }
                _current = _prInfo.PropertyType;    
                Properties.Add(_prInfo);
            }
        }
    }
}
