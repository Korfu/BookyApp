using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace BookyApi.SqlExtensionMethods
{
    public static class SqlExtensionMethods
    {
        public static DynamicParameters ToDynamicParameters(this IDbCommand command)
        {
            var args = new DynamicParameters(new { });
            foreach (SqlParameter item in command.Parameters) args.Add(item.ParameterName, item.Value);

            return args;
        }
    }
}
