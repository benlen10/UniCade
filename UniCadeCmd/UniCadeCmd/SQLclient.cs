﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace UniCadeCmd
{
    class SQLclient
    {
        public static MySqlConnection conn;

        public static string connectSQL()
        {
            
            conn = new MySqlConnection("server=127.0.0.1;"+ "uid=root;" +"pwd=Star6120;"+"database=unicade;");

            try
            {
                conn.Open();
                return "connected";
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                return e.ToString();
            }

        }

        public static string processSQLcommand(string s)
        {
            MySqlCommand myCommand = new MySqlCommand(s, conn);

            StringBuilder sb = new StringBuilder();
            try
            {
                MySqlDataReader myReader = null;

                 myReader= myCommand.ExecuteReader();
                int col = 0;
                while (myReader.Read())
                {
                    sb.Append(myReader.GetString(col));
                }
                myReader.Close();
                myCommand.Dispose();
                return sb.ToString();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                
                return e.ToString();
            }


        }

        public static bool addGame()
        {

        }

        public static string getGameByConsole(string con)
        {

        }

        public static string getSingleGame(string con, string g)
        {
            MySqlCommand myCommand = new MySqlCommand("Use Database unicade;"+ "select * FROM games WHERE title = "+ g + "AND console = " + con+  ";", conn);

            StringBuilder sb = new StringBuilder();
            try
            {
                MySqlDataReader myReader = null;

                myReader = myCommand.ExecuteReader();
                int col = 1;
                while (myReader.Read())
                {
                    sb.Append(myReader.GetString(col));
                }
                myReader.Close();
                myCommand.Dispose();
                return sb.ToString();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());

                return e.ToString();
            }

        }

        public static string getAllGames()
        {

        }

        public static getUsers()
        {

        }

        authiencateUser()
        {

        } 

    }
}
