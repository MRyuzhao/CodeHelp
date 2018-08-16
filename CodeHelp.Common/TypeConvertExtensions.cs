namespace CodeHelp.Common
{
    public static class TypeConvertExtensions
    {
        public static string ToUpperString(this object param)
        {
            return param.ToString().ToUpper();
        }
    }
}