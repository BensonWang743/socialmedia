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
        public DatabaseHelper(string connection)
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