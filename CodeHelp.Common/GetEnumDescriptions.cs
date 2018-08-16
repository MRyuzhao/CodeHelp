using System;
using CodeHelp.Common.Enums;

namespace CodeHelp.Common
{
    public static class GetEnumDescriptions
    {
        public static string IsNullDescription(this IsNull isNull)
        {
            switch (isNull)
            {
                case IsNull.Null:
                    return "";
                case IsNull.NotNull:
                    return "是";
                default:
                    return "";
            }
        }
        public static string IsPrimaryKeyDescription(this IsPrimaryKey isPrimaryKey)
        {
            switch (isPrimaryKey)
            {
                case IsPrimaryKey.NotPrimaryKey:
                    return "";
                case IsPrimaryKey.PrimaryKey:
                    return "是";
                default:
                    return "";
            }
        }
        public static string IsIdentityDescription(this IsIdentity isIdentity)
        {
            switch (isIdentity)
            {
                case IsIdentity.NotIdentity:
                    return "";
                case IsIdentity.Identity:
                    return "是";
                default:
                    return "";
            }
        }
    }
}