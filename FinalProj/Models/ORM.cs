using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace FinalProj.Models
{
    public static class ORM
    {
        private static string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=DapperDB;Integrated Security=True;";
        // If you cannot connect to database, please check your instance name in sqlexpress and change "sqlexpress" in the row above to your value 
        public static void NoReturn(string procedureName, DynamicParameters parameter = null)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(procedureName, parameter, commandType:CommandType.StoredProcedure);
            }
        }
        public static T ReturnScalar<T>(string procedureName, DynamicParameters parameter = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return (T)Convert.ChangeType(connection.Execute(procedureName, parameter, commandType: CommandType.StoredProcedure),typeof(T));
            }
        }
        public static IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters parameter = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<T>(procedureName, parameter, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
