using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_zadumin
{
    internal class databases
    {

        SqlConnection sqlConnection =
           new SqlConnection(@"Data Source=DESKTOP-8OGSDKS\SQLEXPRESS;Initial Catalog=Kurs1;Integrated Security=True");
        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

        }

        public void CloseConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }

        }
        public SqlConnection GetSqlConnection()
        {
            return sqlConnection;
        }

    }
}

