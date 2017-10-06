using System.Windows;

namespace BoxLayouts
{
    public static class MesureHelper
    {
        /// <summary>
        /// Un MinHeight non défini est à 0.
        /// Un Height non définit est à NaN.
        /// 
        /// Si e.MinHeight est à 0, il n'a peut-être pas été défini, 
        /// dans ce cas, si Height est défini, e.Height est retourné,
        /// sinon e.MinHeight est retourné
        /// </summary>
        public static double MinHeight(FrameworkElement e)
        {
            if(e.MinHeight == 0 && !double.IsNaN(e.Height))
                return e.Height;
            return e.MinHeight;
        }

        /// <summary>
        /// Un MinWidth non défini est à 0.
        /// Un Width non définit est à NaN.
        /// 
        /// Si e.MinWidth est à 0, il n'a peut-être pas été défini, 
        /// dans ce cas, si Width est défini, e.Width est retourné,
        /// sinon e.MinWidth est retourné
        /// </summary>
        public static double MinWidth(FrameworkElement e)
        {
            if(e.MinWidth == 0 && !double.IsNaN(e.Width))
                return e.Width;
            return e.MinWidth;
        }

        /// <summary>
        /// Un MaxHeight non défini est à PositiveInfinity.
        /// Un Height non défini est à Nan.
        /// 
        /// Si e.MaxHeight est infini, il n'a peut-être pas été définit et dans ce cas
        /// e.Height est retourné si e.Height est défini,
        /// sinon, retourne e.MaxHeight.
        /// </summary>
        public static double MaxHeight(FrameworkElement e)
        {
            if(double.IsPositiveInfinity(e.MaxHeight) && !double.IsNaN(e.Height))
                return e.Height;
            return e.MaxHeight;
        }

        /// <summary>
        /// Un MaxWidth non défini est à PositiveInfinity.
        /// Un Width non défini est à Nan.
        /// 
        /// Si e.MaxWidth est infini, il n'a peut-être pas été définit et dans ce cas
        /// e.Width est retourné si e.Width est défini,
        /// sinon, retourne e.MaxWidth.
        /// </summary>
        public static double MaxWidth(FrameworkElement e)
        {
            if(double.IsPositiveInfinity(e.MaxWidth) && !double.IsNaN(e.Width))
                return e.Width;
            return e.MaxWidth;
        }
    }
    
}
