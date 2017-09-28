﻿using System;
using System.Diagnostics;
using System.IO;
using UniCade.ConsoleInterface;
using UniCade.Constants;
using UniCade.Interfaces;

namespace UniCade.Backend
{
    internal class Program
    {
        #region Properties

        /// <summary>
        /// Specifies if the UniCade splash screen should be displayed when the interface is launched
        /// </summary>
        public static bool ShowSplashScreen;

        /// <summary>
        /// Specifies if the the ROM directories should be automatically rescanned 
        /// when the interface is launched
        /// </summary>
        public static bool RescanOnStartup;

        /// <summary>
        /// True if there is a current game process running
        /// </summary>
        public static bool IsProcessActive;

        /// <summary>
        /// The instance of the current game process
        /// </summary>
        public static Process CurrentProcess;

        /// <summary>
        /// Specifies if certain ESRB ratings should be restricted globally (regardless of user)
        /// </summary>
        public static Enums.EsrbRatings RestrictGlobalEsrbRatings;

        /// <summary>
        /// Specifies if the command line interface should be launched on startup instead of the GUI
        /// </summary>
        public static bool PerferCmdInterface;

        /// <summary>
        /// Specifies if a loading screen should be displayed when launching a game
        /// </summary>
        public static bool ShowLoadingScreen;

        /// <summary>
        /// If this value is greater than 0, passcode protection is enabled
        /// </summary>
        public static int PasswordProtection;

        /// <summary>
        /// Specifies if ROM files are required to have the proper extension in order to be imported
        /// </summary>
        public static bool EnforceFileExtensions;

        /// <summary>
        /// The current application 
        /// </summary>
        public static App App;

        #endregion

        #region  Public Methods

        /// <summary>
        /// Entry point for the program
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        public static void Main(string[] args)
        {
            //Initalize the database, preform an initial scan and refresh the total game count
            Database.Initalize();
            FileOps.StartupScan();
            Database.RefreshTotalGameCount();


            //Launch either the GUI or the legacy command line interface
            if (PerferCmdInterface)
            {
                UniCadeCmd.Run();
            }
            else
            {
                App = new App();
                App.InitializeComponent();
                App.Run();
            }
        }

        /// <summary>
        /// Display all game info fields in plain text format
        /// </summary>
        public static string DisplayGameInfo(IGame game)
        {
            string text = "";
            text += ("\nTitle: " + game.Title + "\n");
            text += ("\nRelease Date: " + game.ReleaseDate + "\n");
            text += ("\nConsole: " + game.ConsoleName + "\n");
            text += ("\nLaunch Count: " + game.GetLaunchCount().ToString() + "\n");
            text += ("\nDeveloper: " + game.DeveloperName + "\n");
            text += ("\nPublisher: " + game.PublisherName + "\n");
            text += ("\nPlayers: " + game.SupportedPlayerCount + "\n");
            text += ("\nCritic Score: " + game.CriticReviewScore + "\n");
            text += ("\nESRB Rating: " + game.Tags + "\n");
            text += ("\nESRB Descriptors: " + game.GetEsrbDescriptorsString() + "\n");
            text += ("\nGame Description: " + game.Description + "\n");
            return text;
        }

        #endregion

        #region Helper Methods

        #endregion
    }
}