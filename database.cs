using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChungChi
{
    class database
    {
        private static string sqlConnection = @"Data Source=LAPTOP-P1S55MPK\SQLEXPRESS;Initial Catalog=QuanLyChungChi;Integrated Security=True;";
        private static SqlConnection connection;

        public static DataTable Query(string sql)
        {
            connection = new SqlConnection(sqlConnection);
            connection.Open();
            SqlDataAdapter adapter=new SqlDataAdapter(sql, connection);
            DataTable dt=new DataTable();
            adapter.Fill(dt);
            connection.Close();
            return dt;
        }

        public static void Execute(string sql)
        {
            connection = new SqlConnection(sqlConnection);
            connection.Open();
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
