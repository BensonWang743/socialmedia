using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace SocialMediaCommonHelper
{
    public class DatabaseHelper
    {
        string conStr = string.Empty;
        public DatabaseHelper(string connection= "Server=tcp:socialmediaserver.database.windows.net,1433;Initial Catalog=socialmediadb;Persist Security Info=False;User ID=socialmedia;Password=S0cialmedia!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=300;")
        {
            conStr = connection;
        }

        public void ExecuteNonQuery(string commandTxt)
        {
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(commandTxt, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}