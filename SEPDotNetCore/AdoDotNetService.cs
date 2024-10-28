using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPDotNetCore.Shared
{
    private readonly string _connectionString;
   
    public AdoDotNetService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Query()
    {
        SqlConnection connection = new SqlConnection();

    }

    public class SqlParameterModel
    {
        public string Name { get; set; }
        public object Value { get; set; }
}
