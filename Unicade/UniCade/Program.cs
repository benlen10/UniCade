﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using System.Collections;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace UniCade
{
    class Program
    {
        public static Database dat;
        public static string databasePath = @"C:\UniCade\Databse.txt";
        public static string romPath = @"C:\UniCade\ROMS";
        public static string mediaPath = @"C:\UniCade\Media";
        public static string emuPath = @"C:\UniCade\Emulators";
        public static string prefPath = @"C:\UniCade\Preferences.txt";
        public static User curUser;
        public static int coins = 0;
        public static bool playtimeRemaining = true;
        public static string userLicenseName;
        public static string userLicenseKey;
        public static bool validLicense = true;
        [System.STAThreadAttribute]

        public static void Main(string[] args)
        {

            


            dat = new Database();
            
            if (!FileOps.loadPreferences(prefPath))
            {
                
                
                FileOps.defaultPreferences();
                NotificationWindow nfw = new NotificationWindow("WARNING", "Preference file not found.\n Loading defaults...");
                nfw.Show();
            }

            
            if(!ValidateSHA256(userLicenseName + Database.getHashKey(), userLicenseKey))
            {
                validLicense = false;


            }

            if (!FileOps.loadDatabase(databasePath))
            {

                FileOps.loadDefaultConsoles();
                FileOps.scan(romPath);
                FileOps.saveDatabase(databasePath);
                NotificationWindow nfw = new NotificationWindow("WARNING", "Database file not found.\n Loading defaults...");
                nfw.Show();
            }

            if (curUser == null)
            {
                curUser = new User("UniCade", "temp", 0, "unicade@unicade.com", 0, " ", "","");
                dat.userList.Add(curUser);
            }

            var app = new App();
            app.InitializeComponent();
            app.Run();



        }



       public static string displayGameInfo(Game g)
        {
            string txt = "";
                txt = txt + ("\nRelease Date: " + g.getReleaseDate() + "\n");
                txt = txt + ("\nDeveloper: " + g.getDeveloper() + "\n");
                txt = txt + ("\nPublisher: " + g.getPublisher() + "\n");
                txt = txt + ("\nPlayers: " + g.getPlayers() + "\n");
                txt = txt + ("\nUser Score: " + g.getUserScore() + "\n");
                txt = txt + ("\nCritic Score: " + g.getCriticScore() + "\n");
                txt = txt + ("\nESRB Rating: " + g.getEsrb() + "\n");
                txt = txt + ("\nESRB Descriptors: " + g.getEsrbDescriptor() + "\n");
                txt = txt + ("\nmame Description: " + g.getDescription() + "\n");
                return txt;
            }


        public static string SHA256Hash(string data)
        {
            if(data == null)
            {
                return null;
            }
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hashData = sha256.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder returnValue = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }
            return returnValue.ToString();
        }


        public static bool ValidateSHA256(string input, string storedHashData)
        {
            string getHashInputData = SHA256Hash(input);
            if (string.Compare(getHashInputData, storedHashData) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





    }

    }