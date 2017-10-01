
namespace RuntimeExec
{
    public abstract class REUnaryOperator : REOperator
    {
        public REUnaryOperator(){ }

        public REUnaryOperator(object operand)
        {
            if(operand is REExpression _expr)
                Operand = _expr;
            else
                Operand = new REValue(operand);
        }

        public override REExpression Invoke()
        {
            object _val = (Operand != null)? Operand.CValue : null;
            __revalue = new REValue(Result(_val));
            return this;
        }

        public override REBase[] Children => new REBase[1]{ Operand };

        protected abstract object Result(dynamic val);

        public REExpression Operand { get; set; } = null;
    }
}
