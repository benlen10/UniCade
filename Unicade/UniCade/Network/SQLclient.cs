﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace UniCade
{
    class SQLclient
    {
        public static string sqlUser;
        public static MySqlConnection conn;
        

        public static string connectSQL()
        {

            conn = new MySqlConnection("server=127.0.0.1;"+ "uid=root;" +"pwd=Star6120;"+"database=unicade;");

            try
            {
                sqlUser = null;
                conn.Open();
                return "connected";
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                MessageBox.Show("UniCade Cloud connection Error");
                return string.Empty;
            }

        }

        public static string processSQLcommand(string s)
        {
            if (conn == null)
            {
                connectSQL();
            }
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

        public static bool uploadAllGames()
        {
            foreach (Console c in Database.ConsoleList)
            {
                    foreach (Game g in c.GameList)
                    {
                    uploadGame(g);
                    }
                }
            return true;
        }


        public static bool downloadAllGames()
        {

            foreach (Console c in Database.ConsoleList)
            {

                for(int i = 0; i<c.GameList.Count; i++)
                {
                    Game g = (Game) c.GameList[i];
                    Game gam = null;
                    gam = getSingleGame(g.Console, g.Title);
                    if (gam != null)
                    {
                        if (gam.FileName.Length > 3)
                        {
                            c.GameList[i] = gam;
                        }
                    }

                }

            }

            return true;

        }

        public static bool uploadGame(Game g)
        {
            if (conn == null)
            {
                connectSQL();
            }

            MySqlCommand myCommand = new MySqlCommand("Use unicade;" + "select * FROM "+ sqlUser+ "_games WHERE filename = " + "\"" + g.FileName + "\"" + " AND console = " + "\"" + g.Console + "\"" + ";", conn);


            MySqlDataReader myReader = myCommand.ExecuteReader();
            if (myReader.Read())
            {
                if ((SafeGetString(myReader, 1).Equals(g.FileName, StringComparison.InvariantCultureIgnoreCase)))
                {
                    System.Console.WriteLine("User Already Exists");
                    myReader.Close();
                    myCommand.Dispose();
                    return false;
                }
            }
            myReader.Close();
            myCommand.Dispose();


            string command = "Use unicade;" + " INSERT INTO " +sqlUser+ "_games (filename,title, Console, LaunchCount, releaseDate, publisher, developer, userscore, criticscore, players, trivia, esrb, esrbdescriptors, esrbsummary, description, genres, tags, favorite)" + " VALUES (" + "\"" + g.FileName + "\",\"" + g.Title + "\",\"" + g.Console + "\",\"" + g.LaunchCount + "\",\"" + g.ReleaseDate + "\",\"" + g.Publisher + "\",\"" + g.Developer + "\",\"" + g.UserScore + "\",\"" + g.CriticScore + "\",\"" + g.Players + "\",\"" + g.Trivia + "\",\"" + g.Esrb + "\",\"" + g.EsrbDescriptor + "\",\"" + g.EsrbSummary + "\",\"" + g.Description + "\",\"" + g.Genres + "\",\"" + g.Tags + "\",\"" + g.Favorite + "\");";
             myCommand = new MySqlCommand(command, conn);
            myCommand.ExecuteNonQuery();
            return true;
        }

        public static string getGameByConsole(string con)
        {
            return null;
        }

        public static Game getSingleGame(string con, string gam)
        {
            //System.Console.WriteLine("SINGLE GAME: " + con +", "+ gam);
            if (conn == null)
            {
                connectSQL();
            }
            MySqlCommand myCommand = new MySqlCommand("Use unicade;"+ "select * FROM " + sqlUser + "_games WHERE title = "+ "\""+ gam + "\""+ " AND console = " + "\""+ con + "\""  +  ";", conn);
            MySqlDataReader myReader = null;
            try
            {
                 myReader = myCommand.ExecuteReader();


                Game g = null;
                if (myReader.Read())
                {
                     g = new Game(SafeGetString(myReader, 1), SafeGetString(myReader, 3), SafeGetInt32(myReader, 4), SafeGetString(myReader, 5), SafeGetString(myReader, 6), SafeGetString(myReader, 7), SafeGetString(myReader, 8), SafeGetString(myReader, 9), SafeGetString(myReader, 10), SafeGetString(myReader, 11), SafeGetString(myReader, 12), SafeGetString(myReader, 13), SafeGetString(myReader, 14), SafeGetString(myReader, 15), SafeGetString(myReader, 16), SafeGetString(myReader, 17), SafeGetInt32(myReader, 18));
                }
                else
                {
                    myCommand.Dispose();
                    myReader.Close();
                    return null;
                }

                myReader.Close();
                myCommand.Dispose();
                return g;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
                myReader.Close();
                return null;
            }

        }

        public static string getAllGames()
        {
            if (conn == null)
            {
                connectSQL();
            }
            return null;
        }

        public static string getUsers()
        {
            if (conn == null)
            {
                connectSQL();
            }
            return null;
        }

        public static bool authiencateUser(string user, string pass)
        {
            if (conn == null)
            {
                connectSQL();
            }

            string command = "Use unicade;" + "select * FROM users WHERE username = " + "\"" + user + "\"" + " OR email = " + "\"" + user + "\"" + ";";
            MySqlCommand myCommand = new MySqlCommand(command, conn);

            MySqlDataReader myReader = myCommand.ExecuteReader();
            myReader.Read();
            if(pass.Equals(SafeGetString(myReader, 2), StringComparison.InvariantCultureIgnoreCase)){
                sqlUser = user;
                myReader.Close();
                myCommand.Dispose();
                return true;
            }
            else
            {
                myReader.Close();
                myCommand.Dispose();
                return false;
            }


        }

        public static bool createUser(User u)
        {
            if (conn == null)
            {
                connectSQL();
            }

            return false;
        }

        public static bool createUser(string username, string pass, string email, string info, string esrb, string profPic)
        {
            if (conn == null)
            {
                connectSQL();
            }

            MySqlCommand myCommand = new MySqlCommand("Use unicade;" + "select * FROM users WHERE username = " + "\"" + username + "\"" + " OR email = " + "\"" + email + "\"" + ";", conn);


            MySqlDataReader myReader = myCommand.ExecuteReader();
            if (myReader.Read())
            {
                if ((SafeGetString(myReader, 1).Equals(username, StringComparison.InvariantCultureIgnoreCase)) || (SafeGetString(myReader, 3).Equals(email, StringComparison.InvariantCultureIgnoreCase)))
                {

                    myReader.Close();
                    myCommand.Dispose();
                    return false;
                }
            }
            myReader.Close();
            myCommand.Dispose();


            string command = "Use unicade;" + "INSERT INTO users (username,password,email,info,allowedEsrb,logincount,LaunchCount,profilepic) VALUES (\"" + username + "\",\"" + pass + "\",\"" + email + "\",\"" + info + "\",\"" + esrb + "\",\"" + "0" + "\",\"" + "0" + "\",\"" + "nullProfPath" + "\");";
             myCommand = new MySqlCommand(command,conn);
            myCommand.ExecuteNonQuery();
            command = @"USE unicade;

DROP TABLE IF EXISTS games;
            CREATE TABLE games
            (
              id              int unsigned NOT NULL auto_increment, # Unique ID for the record
  filename        varchar(255),     # full ROM filename
  title           varchar(255),     # Game title
  Console         varchar(255),     # Console game
  LaunchCount     smallint(127),     # Number of times the game has been launched
  releaseDate     varchar(255),      # Release date
  publisher       varchar(255),      # Game publisher
  developer       varchar(255),      # Game developer
  userscore       varchar(255),      # Average user score
  criticscore     varchar(255),      # Average critic score
  players         varchar(255),      # Number of supported local players
  trivia          varchar(255),      # Trivia or extra info related to the game
  esrb            varchar(255),      # ESRB content rating
  esrbdescriptors varchar(255),     # ESRB content rating descriptors
  esrbsummary     varchar(255),      # ESRB content rating summary
  description     varchar(9000),      # Brief game description
  genres          varchar(255),      # Main genres associated with the game
  tags            varchar(255),      # Revelant game tags
  favorite       smallint(127),     # int value describing the favorite status


  PRIMARY KEY(id)
            );


#18 total fields";

            command = command.Replace("games", (username + "_games"));
            myCommand = new MySqlCommand(command, conn);
            myCommand.ExecuteNonQuery();

            myReader.Close();
            myCommand.Dispose();
            return true;
        }

        public static bool deletegames()
        {
            if (conn == null)
            {
                connectSQL();
            }

            string command = "Use unicade;" + "DELETE FROM " + sqlUser + "_games WHERE id>0;";
            MySqlCommand myCommand = new MySqlCommand(command, conn);
            myCommand.ExecuteNonQuery();

            myCommand.Dispose();
            return true;
        }

        public static bool deleteUser()
        {
            if (conn == null)
            {
                connectSQL();
            }
            string command = "Use unicade;" + "DELETE FROM users WHERE username = "  + "\"" + sqlUser + "\";";
            MySqlCommand myCommand = new MySqlCommand(command, conn);
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();

            command = "Use unicade;" + "DROP TABLE " +  sqlUser + "_games;";
            myCommand = new MySqlCommand(command, conn);
            myCommand.ExecuteNonQuery();
            myCommand.Dispose();

            sqlUser = null;

            return true;
        }

        public static string SafeGetString(MySqlDataReader reader, int colIndex)
        {
            try {

                if (!reader.IsDBNull(colIndex))
                    return reader.GetString(colIndex);
                else
                    return string.Empty;
            }
            catch
            {
                return null;
            }
            }

        public static int SafeGetInt32(MySqlDataReader reader, int colIndex)
        {
            try {
                if (!reader.IsDBNull(colIndex))
                    return reader.GetInt32(colIndex);
                else
                    return 0;
            }
            catch
            {
                return 0;
            }
        }

    }
}
