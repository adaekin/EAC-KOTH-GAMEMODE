using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data;
using System.Linq.Expressions;
using SDG.Unturned;
using Rocket.Unturned.Chat;
using UnityEngine;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace EACProject.Modules
{
    internal class datastoragetxt
    {

        public static datastoragetxt instance { get; set; }

        public void sqlcon()
        {
            MySqlConnectionStringBuilder sb2 = new MySqlConnectionStringBuilder();
            sb2.Database = "localhost";
            sb2.UserID = "root1";
            sb2.Password = "asdqwe123";
            

            
            
            UnturnedChat.Say("Connecting...");
            MySqlConnection conn = new MySqlConnection( sb2.ConnectionString);



            try
            {
                conn.Open();
                //sqlCommand = new SqlCommand("CREATE DATABASE unturned", conn);

                //sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                UnturnedChat.Say(e.Message);

            }
            finally
            {
                conn.Close();
            }
        }
        
    }
    
}
