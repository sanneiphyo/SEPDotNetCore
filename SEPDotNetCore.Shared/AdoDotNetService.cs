using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace SEPDotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;
      
        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DataTable Query(string query, SqlParameterModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            foreach(var sqlParameter in sqlParameters)
            {
                cmd.Parameters.AddWithValue(sqlParameter.Name, sqlParameter.Value);
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            return dt;
        }
        
    }

    public class SqlParameterModel
    {
        public string Name { get; set; }
        public object Value { get; set; }
    };
}
