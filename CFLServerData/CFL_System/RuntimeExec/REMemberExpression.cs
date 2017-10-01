using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace RuntimeExec
{
    /// <summary>
    /// Exemple :
    /// Soit un object ObjectA de classe A, une propriété PrB de ObjectA retournant un objet ObjectB de class B, une propriété PrInt de objectB retournant un type int,
    /// l'expression est : 
    /// 
    ///           ObjectA.PrB.PrInt.
    ///           
    /// Le <see cref="REMemberExpression"/> sera alors fait comme suit :
    /// - un <see cref="REMemberExpression"/> correspondant à PrB, <see cref="REMemberExpression.Parent"/>  <see cref="REClassObject"/>(ObjectA)
    /// - un <see cref="REMemberExpression"/> correspondant à PrInt, <see cref="REMemberExpression.Parent"/>  <see cref="REClassObject"/>(ObjectB),
    /// qui contient la <see cref="REProperty"/> PrInt.
    /// 
    /// <see cref="REMemberExpression.ReValue"/> donne ou retourne la valeur <see cref="REExpression"/>
    /// de la <see cref="REExpression"/> contenue dans dernier membre de l'expression.
    /// Le premier objet de l'expression peut être changé en assignant <see cref="REMemberExpression.Parent"/>
    /// ce qui provoque la reconstruction de l'expression.
    /// </summary>
    public class REMemberExpression : REMember
    {
        public REMemberExpression(){ }

        public REMemberExpression(REClassObject _parent, Expression<Func<object>> _expression)
        {
            MembersNames(_expression);
            
            Parent = _parent??throw new Exception
                              (@"_parent ne doit pas être null. 
                                 Si _parent ne peut être aquis au moment de cet instanciation, 
                                 utiliser un constructeur réclamant string _parentTypeName, ou Type _parentType");
        }

        /// <summary>
        /// 
        /// </summary>
        public REMemberExpression(REClassObject _parent, string _expression)
        {
            string[] _names = _expression.Split('.');
            Names = new List<string>();
            foreach(string _name in _names)
                Names.Add(_name);
            
            Parent = _parent??throw new Exception
                              (@"_parent ne doit pas être null. 
                                 Si _parent ne peut être aquis au moment de cet instanciation, 
                                 utiliser un constructeur réclamant string _parentTypeName, ou Type _parentType");
        }


        public REMemberExpression(REClassObject _parent, params string[] _expression)
        {
            if(_expression == null)
                throw new ArgumentNullException("_expression");
            Names = new List<string>();
            foreach(string _name in _expression)
                Names.Add(_name);
            
            Parent = _parent??throw new Exception
                              (@"_parent ne doit pas être null. 
                                 Si _parent ne peut être aquis au moment de cet instanciation, 
                                 utiliser un constructeur réclamant string _parentTypeName, ou Type _parentType");
        }

        public REMemberExpression(Type _parentType, Expression<Func<object>> _expression)
        {
            if(_expression == null)
                throw new ArgumentNullException("_expression");
            if(_parentType == null)
                throw new ArgumentNullException("_objectType");
            MembersNames(_expression);
            ParentTypeName = _parentType.Name;
        }

        public REMemberExpression(string _parentTypeName, string _expression)
        {
            ParentTypeName = _parentTypeName;
            string[] _names = _expression.Split('.');
            Names = new List<string>();
            foreach(string _name in _names)
                Names.Add(_name);
        }

        public REMemberExpression(string _parentTypeName, params string[] _expression)
        {
            if(_expression == null)
                throw new ArgumentNullException("_expression");
            ParentTypeName = _parentTypeName;
            Names = new List<string>();
            foreach(string _name in _expression)
                Names.Add(_name);
        }

        public REMemberExpression(string _parentTypeName, List<string> _expression)
        {
            Names = _expression??throw new ArgumentNullException("_expression");
            ParentTypeName = _parentTypeName;
        }

        public override REBase Copy()
        {
            return new REMemberExpression(ParentTypeName, Names);         
        }

        /// <summary>
        /// set retient le <see cref="REClassObject"/> et invoque <see cref="Build"/> 
        /// pour construire ce <see cref="REMemberExpression"/>.
        /// </summary>
        public override REBase Parent 
        { 
            get => base.Parent; 
            set
            {
                base.Parent = value; 
                Build();
            }
        }

        public override REBase ReValue 
        { 
            get
            {
                if(Parent == null || Expression == null)
                    return NULL;
                return Expression.ReValue;
            }

            set
            {
                if(ReValue is REValue _revalue && value is REExpression _expr)
                {
                    _revalue.CValue = _expr.CValue;
                }
                else
                    throw new NotImplementedException();
            }
        }

        public override object CValue 
        { 
            get
            {
                if(ReValue is REExpression _expr)
                    return _expr.CValue;
                throw new NotImplementedException(); 
            }
            set
            {
                if(ReValue is REExpression _expr)
                    _expr.CValue = value;
                else
                throw new NotImplementedException();  
            }
        }

        public override REExpression Invoke()
        {
            if(Expression != null)
                Expression.Invoke();
            return this;
        }

        /// <summary>
        /// Retourne l'expression représentée par une string ( ex. "a.b.c")
        /// </summary>
        public string Display
        {
            get
            {
                string _expression = "";
                if(Names != null)
                {
                    foreach(string _name in Names)
                    {
                        if(_expression != "")
                            _expression += ".";
                        _expression += _name;
                    }
                }
                return _expression;
            }
        }

        public REExpression Expression { get; set; } = null;

        public override REBase[] Children => new REBase[1]{ Expression };

        public string LastMemberName()
        {
            if(Names != null && Names.Count > 0)
                return Names[Children.Length - 1];
            return "";
        }

        private List<string> Names { get; set; } = null;

        #region Build

        private void Build()
        {
            if(Names == null)
                throw new Exception("Names null.");

            if(Parent is REClassObject _parent && Names.Count != 0)
            {
                REMember _member = _parent.GetMember(Names[0]);

                if(Names.Count > 1) // nous sommes sur un objet de classe
                {
                    // copie de Names sauf le premier élément
                    String[] _names = new string[Names.Count - 1];
                    Names.CopyTo(1, _names, 0, _names.Length);

                    Expression = new REMemberExpression((REClassObject)(_member.ReValue), _names);
                }
                else
                {
                    Expression = _member;
                }
            }
        }

        private void MembersNames(Expression<Func<object>> _expression)
        {
            Names = new List<string>();

            string[] _elements = _expression.ToString().Split('.');

            for(int _i = _elements.Length - 1; _i >= 0; _i--)
            {
                if(_elements[_i].EndsWith(")"))
                    break;
                Names.Insert(0, _elements[_i]);
            }

            Names.RemoveAt(0);
        }

        #endregion Build
    }
}
