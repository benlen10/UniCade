﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using UniCade.Backend;
using UniCade.ConsoleInterface;
using UniCade.Constants;
using UniCade.Interfaces;
using UniCade.Network;
using UniCade.Objects;
using Console = UniCade.Objects.Console;

namespace UniCade.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    [SuppressMessage("ReSharper", "PossibleInvalidOperationException")]
    public partial class SettingsWindow
    {

        #region Private Instance Fields

        /// <summary>
        /// The game that is currently selected within the Games tab
        /// </summary>
        private static IGame _currentGame;

        /// <summary>
        /// The current console that is selected within the games tab
        /// </summary>
        private static IConsole _currentConsole;

        /// <summary>
        /// The current emulator that is selected within the Emulators tab
        /// </summary>
        private static IConsole _currentEmulator;

        #endregion

        #region Constructors

        /// <summary>
        /// Public constructor for the SettingsWindowClass
        /// </summary>
        public SettingsWindow()
        {
            InitializeComponent();
            Populate();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Called on window close event
        /// </summary>
        void SettingsWindow_Closing(object sender, CancelEventArgs e)
        {
            MainWindow.IsSettingsWindowActive = false;
            MainWindow.KeyboardHook.HookKeys();
        }

        /// <summary>
        /// Populate settings window fields under all tabs
        /// </summary>
        private void Populate()
        {
            //Populate console list with the currently active games
            var consoleList = Database.GetConsoleList();
            foreach (string consoleName in consoleList)
            {
                IConsole console = Database.GetConsole(consoleName);
                GamesTabListboxConsoleList.Items.Add(console.ConsoleName);
                EmulatorsTabListboxConsoleList.Items.Add(console.ConsoleName);
            }

            //Set initial selected indexes
            EmulatorsTabListboxConsoleList.SelectedIndex = 0;
            GamesTabListboxConsoleList.SelectedIndex = 0;

            //Poplate ESRB dropdown combo boxes
            foreach (Enums.EsrbRatings esrb in Enum.GetValues(typeof(Enums.EsrbRatings)))
            {
                GlobalTabDropdownAllowedEsrb.Items.Add(esrb.GetStringValue());
                UsersTabDropdownAllowedEsrb.Items.Add(esrb.GetStringValue());
            }

            //Populate the API combo box in the web tab
            foreach (Enums.Api api in Enum.GetValues(typeof(Enums.Api)))
            {
                WebTabApiComboBox.Items.Add(api.GetStringValue());
            }

            //Load UniCade Logo images within the settings window
            try
            {
                AboutTabImageUniCadeLogo.Source =
                    new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Media\Backgrounds\UniCade Logo.png"));
                CloudTabImageUniCadeLogo.Source =
                    new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Media\Backgrounds\UniCade Logo.png"));
                EmulatorsTabImageUniCadeLogo.Source =
                    new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Media\Backgrounds\UniCade Logo.png"));
                WebTabImageUniCadeLogo.Source =
                    new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Media\Backgrounds\UniCade Logo.png"));
            }
            catch (DirectoryNotFoundException)
            {
                //Do nothing
            }

            //Disable editing userinfo unless logged in
            UsersTabTextboxUsername.IsEnabled = false;
            UsersTabTextboxEmail.IsEnabled = false;
            UsersTabTextboxUserInfo.IsEnabled = false;

            //Set specific textboxes as readonly
            GlobalTabTextboxCoins.IsEnabled = false;
            GlobalTabTextboxPlaytime.IsEnabled = false;

            //Populate features textbox under the About tab
            AboutTabTextboxSoftwareInfo.Text = TextFiles.Features + "\n\n\n\n\n\n" + TextFiles.Instructions;
            AboutTabTextboxSoftwareInfo.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            AboutTabTextboxSoftwareInfo.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;

            //Populate textbox fields
            if (Program.PasswordProtection.Length >= 4)
            {
                GlobalTabTextboxPassword.Password = "Active";
            }

            //Populate checkboxes
            WebTabCheckboxReleaseDate.IsChecked = WebOps.ParseReleaseDate;
            WebTabCheckboxCriticScore.IsChecked = WebOps.ParseCriticScore;
            WebTabCheckboxPublisher.IsChecked = WebOps.ParsePublisher;
            WebTabCheckboxDeveloper.IsChecked = WebOps.ParseDeveloper;
            WebTabCheckboxEsrbRating.IsChecked = WebOps.ParseEsrbRating;
            WebTabCheckboxEsrbDescriptor.IsChecked = WebOps.ParseEsrbDescriptors;
            WebTabCheckboxPlayers.IsChecked = WebOps.ParsePlayerCount;
            WebTabCheckboxDescription.IsChecked = WebOps.ParseDescription;
            WebTabCheckboxBoxFront.IsChecked = WebOps.ParseBoxFrontImage;
            WebTabCheckboxBoxBack.IsChecked = WebOps.ParseBoxBackImage;
            WebTabCheckboxScreenshot.IsChecked = WebOps.ParseScreenshot;
            GlobalTabCheckboxDisplaySplash.IsChecked = Program.ShowSplashScreen;
            GlobalTabCheckboxDisplayLoadingScreen.IsChecked = Program.ShowLoadingScreen;
            GlobalTabCheckboxRescanAllLibraries.IsChecked = Program.RescanOnStartup;
            GlobalTabCheckboxEnforceFileExtension.IsChecked = Program.EnforceFileExtensions;
            GlobalTabCheckboxDisplayEsrb.IsChecked = MainWindow.DisplayEsrbWhileBrowsing;
            GlobalTabCheckboxEnablePayPerPlay.IsChecked = PayPerPlay.PayPerPlayEnabled;
            GlobalTabTextboxCoins.IsEnabled = PayPerPlay.PayPerPlayEnabled;
            GlobalTabTextboxPlaytime.IsEnabled = PayPerPlay.PayPerPlayEnabled;
            GlobalTabDropdownAllowedEsrb.Text = Program.RestrictGlobalEsrbRatings.GetStringValue();
            GamesTabCheckBoxGlobalFavorite.IsChecked = MainWindow.DisplayEsrbWhileBrowsing;

            //Populate payPerPlay fields
            GlobalTabTextboxCoins.Text = PayPerPlay.CoinsRequired.ToString();
            GlobalTabTextboxPlaytime.Text = PayPerPlay.Playtime.ToString();

            var userList = Database.GetUserList();
            foreach (string username in userList)
            {
                UsersTabListboxCurrentUser.Items.Add(username);
            }

            //Refresh the global favorites list
            RefreshGlobalFavs();

            //Populate user license info
            AboutTabLabelLicensedTo.Content = "Licensed to: " + Program.UserLicenseName;
            AboutTabLabelEdition.Content = Program.IsLicenseValid ? "License Status: Full Version" : "License Status: Invalid";
            AboutTabLabelLicenseKey.Content = "License Key: " + Program.UserLicenseKey;
        }

        #endregion

        #region Games Tab

        /// <summary>
        /// Download game button
        /// Download metadata for the selected game from UniCade Cloud
        /// </summary>
        private void GamesTab_DownloadGameButton_Click(object sender, RoutedEventArgs e)
        {
            //Check if a UniCade Cloud user is currently active
            if (SqlLiteClient.GetCurrentUsername() == null)
            {
                MessageBox.Show("UniCade Cloud Login Required");
                return;
            }

            //Invalid input checks
            if (GamesTabListboxGamesList.Items.Count < 1)
            {
                MessageBox.Show("No games to download");
                return;
            }

            if (GamesTabListboxGamesList.SelectedItem == null)
            {
                MessageBox.Show("Must select a console/game");
                return;
            }

            if (_currentGame == null)
            {
                MessageBox.Show("Must select a game");
                return;
            }
            if (SqlLiteClient.DownloadGameInfo(_currentGame))
            {
                RefreshGameInfo(_currentGame);
                MessageBox.Show("Game metadata updated");
            }
            else
            {
                MessageBox.Show("SQL operation failed");
            }
        }

        /// <summary>
        /// Upload console button
        /// Upload all games from the selected console to UniCade Cloud
        /// </summary>
        private void GamesTab_UploadConsoleButton_Click(object sender, EventArgs e)
        {
            //Invalid input checks
            if (SqlLiteClient.GetCurrentUsername() == null)
            {
                MessageBox.Show("UniCade Cloud Login Required");
                return;
            }
            if (GamesTabListboxConsoleList.SelectedItem == null)
            {
                MessageBox.Show("Must select a console");
                return;
            }
            if (GamesTabListboxGamesList.Items.Count < 1)
            {
                MessageBox.Show("No games to upload");
                return;
            }

            if (SqlLiteClient.UploadAllGamesForConsole(_currentConsole))
            {
                MessageBox.Show("Console Uploaded");
            }
            else
            {
                MessageBox.Show("SQL operation failed");
            }
        }

        /// <summary>
        /// Download console info button
        /// Downloads all game metadata for the current console from the current user's Unicade Cloud account
        /// </summary>
        private void GamesTab_DownloadConsoleButton_Click(object sender, EventArgs e)
        {
            //Invalid input checks
            if (GamesTabListboxConsoleList.SelectedItem == null)
            {
                MessageBox.Show("Must select a console");
                return;
            }
            if (SqlLiteClient.GetCurrentUsername() == null)
            {
                MessageBox.Show("UniCade Cloud Login Required");
                return;
            }
            if (GamesTabListboxGamesList.Items.Count < 1)
            {
                MessageBox.Show("No games to delete");
                return;
            }
            if (_currentConsole == null)
            {
                MessageBox.Show("Please select console");
                return;
            }

            var gameList = _currentConsole.GetGameList();
            foreach (string gameTitle in gameList)
            {
                IGame game = _currentConsole.GetGame(gameTitle);
                SqlLiteClient.DownloadGameInfo(game);

            }

            if (SqlLiteClient.DownloadAllGamesForConsole(_currentConsole))
            {
                MessageBox.Show("Metadata download");
                RefreshGameInfo(_currentGame);
            }
            else
            {
                MessageBox.Show("SQL operation failed");
            }
            
        }

        /// <summary>
        /// Called when the select index is changed. Update the proper game info in the details fields. 
        /// </summary>
        private void GamesTab_GamesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GamesTabListboxGamesList.SelectedItem == null) { return; }
            string currentGame = GamesTabListboxGamesList.SelectedItem.ToString();
            var gameList = _currentConsole.GetGameList();
            foreach (string gameTitle in gameList)
            {
                IGame game = _currentConsole.GetGame(gameTitle);
                if (game.Title.Equals(currentGame))
                {
                    _currentGame = game;
                }
            }
            RefreshGameInfo(_currentGame);
            RefreshEsrbIcon(_currentGame);
        }

        /// <summary>
        /// Called when the select index is changed for the console listbox. Update the games list for the selected console. 
        /// </summary>
        private void GamesTab_ConsoleListBox__SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GamesTabListboxConsoleList.SelectedItem == null) { return; }
            string curItem = GamesTabListboxConsoleList.SelectedItem.ToString();
            GamesTabListboxGamesList.Items.Clear();
            var consoleList = Database.GetConsoleList();
            foreach (string consoleName in consoleList)
            {
                IConsole console = Database.GetConsole(consoleName);
                if (console.ConsoleName.Equals(curItem))
                {
                    _currentConsole = console;
                    GamesTabTextboxGamesForConsole.Text = console.GetGameCount().ToString();
                    GamesTabTextboxTotalGames.Text = Database.GetTotalGameCount().ToString();

                    //Populate the games list
                    console.GetGameList().ForEach(g => GamesTabListboxGamesList.Items.Add(g));
                }
            }
            if (GamesTabListboxGamesList.Items.Count > 0)
            {
                GamesTabListboxGamesList.SelectedIndex = 0;
                var gameList = _currentConsole.GetGameList();
                foreach (string gameTitle in gameList)
                {
                    IGame g = _currentConsole.GetGame(gameTitle);
                    if (g != null)
                    {
                        if (g.Title.Equals(GamesTabListboxGamesList.SelectedItem.ToString()))
                        {
                            _currentGame = g;
                        }
                    }
                }
            }
            else
            {
                RefreshGameInfo(null);
            }
        }

        /// <summary>
        /// Rescrape game info button.
        /// Rescrapes info the the specified game from the web
        /// </summary>
        internal void GamesTab_RescrapeGameButton_Click(object sender, EventArgs e)
        {
            //Require that a user select a valid game to rglescrape
            if (GamesTabListboxGamesList.SelectedItem == null)
            {
                MessageBox.Show("Must select a console/game");
                return;
            }

            //Scrape info and populate local fields
            WebOps.ScrapeInfo(_currentGame);

            //Refresh the info for the current game
            RefreshGameInfo(_currentGame);
        }

        /// <summary>
        /// Save database button
        /// Save all active info to the text database
        /// </summary>
        private void GamesTab_SaveToDatabaseButton_Click(object sender, EventArgs e)
        {
            if (GamesTabListboxGamesList.SelectedItem == null)
            {
                MessageBox.Show("Must select a console/game");
                return;
            }
            SaveGameInfo();
            RefreshGameInfo(_currentGame);
            MessageBox.Show("Success");
        }

        /// <summary>
        /// Save game info button
        /// </summary>
        private void GamesTab_SaveInfoButton_Click(object sender, EventArgs e)
        {
            if (GamesTabListboxGamesList.SelectedItem == null)
            {
                MessageBox.Show("Must select a console/game");
                return;
            }
            if (GamesTabListboxGamesList.Items.Count < 1)
            {
                MessageBox.Show("No games to save");
                return;
            }
            SaveGameInfo();
            RefreshGameInfo(_currentGame);
            MessageBox.Show("Success");
        }

        /// <summary>
        /// Uplod game button
        /// Upload the currently selected game to UniCade cloud
        /// </summary>
        private void GamesTab_UploadButton_Click(object sender, EventArgs e)
        {

            if (SqlLiteClient.GetCurrentUsername() == null)
            {
                MessageBox.Show("UniCade Cloud login required");
                return;
            }

            if (GamesTabListboxGamesList.Items.Count < 1)
            {
                MessageBox.Show("No games to upload");
                return;
            }

            if (GamesTabListboxGamesList.SelectedItem == null)
            {
                MessageBox.Show("Must select a console/game");
                return;
            }
            if (SqlLiteClient.UploadGame(_currentGame))
            {
                MessageBox.Show("Game Uploaded");
            }
            else
            {
                MessageBox.Show("SQL operation failed");
            }
        }

        /// <summary>
        /// Rescrape console button
        /// Rescrape metadata for all games within teh current console
        /// </summary>
        private void GamesTab_RescrapeConsoleMetadataButton_Click(object sender, EventArgs e)
        {
            if (GamesTabListboxConsoleList.SelectedItem == null)
            {
                MessageBox.Show("Must select a console");
                return;
            }
            if (GamesTabListboxConsoleList.SelectedItem == null)
            {
                MessageBox.Show("Must select a console");
                return;
            }

            MessageBox.Show("This may take a while... Please wait for a completed nofication.");
            var gameList = _currentConsole.GetGameList();
            foreach (string gameTitle in gameList)
            {
                IGame game1 = _currentConsole.GetGame(gameTitle);
                WebOps.ScrapeInfo(game1);
            }
            MessageBox.Show("Operation Successful");
        }

        /// <summary>
        /// Toggle expansion of the boxfront image
        /// </summary>
        private void Image_Boxfront_Expand(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string imagePath = Directory.GetCurrentDirectory() + @"\Media\Games\" + _currentGame.ConsoleName + "\\" + _currentGame.Title + "_BoxFront.jpg";
            if (File.Exists(imagePath))
            {
                ImagePopup imagePopup = new ImagePopup(imagePath);
                imagePopup.Title = _currentGame.Title + " BoxFront Image";
                imagePopup.ShowDialog();
            }
        }

        /// <summary>
        /// Toggle expansion of the boxfront image
        /// </summary>
        private void Image_Boxback_Expand(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string imagePath = Directory.GetCurrentDirectory() + @"\Media\Games\" + _currentGame.ConsoleName + "\\" + _currentGame.Title + "_BoxBack.jpg";
            if (File.Exists(imagePath))
            {
                ImagePopup imagePopup = new ImagePopup(imagePath);
                imagePopup.Title = _currentGame.Title + " BoxBack Image";
                imagePopup.ShowDialog();
            }
        }

        /// <summary>
        /// Toggle expansion of the screenshot image
        /// </summary>
        private void Image_Screenshot_Expand(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string imagePath = Directory.GetCurrentDirectory() + @"\Media\Games\" + _currentGame.ConsoleName + "\\" + _currentGame.Title + "_Screenshot.jpg";
            if (File.Exists(imagePath))
            {
                ImagePopup imagePopup = new ImagePopup(imagePath);
                imagePopup.Title = _currentGame.Title + " Screenshot Image";
                imagePopup.ShowDialog();
            }
        }

        /// <summary>
        /// Save the current game info to the database file
        /// Display an error popup if any of the inputs contain invalid data
        /// </summary>
        internal void SaveGameInfo()
        {
            try
            {
                _currentGame.ReleaseDate = GamesTabTextboxReleaseDate.Text;
                _currentGame.CriticReviewScore = GamesTabTextboxCriticScore.Text;
                _currentGame.SupportedPlayerCount = GamesTabTextboxPlayers.Text;
                _currentGame.EsrbRatingsRating = Enums.ConvertStringToEsrbEnum(GamesTabTextboxEsrb.Text);
                _currentGame.PublisherName = GamesTabTextboxPublisher.Text;
                _currentGame.DeveloperName = GamesTabTextboxDeveloper.Text;
                _currentGame.Description = GamesTabTextboxDescription.Text;
                _currentGame.AddEsrbDescriptorsFromString(GamesTabTextboxEsrbDescriptor.Text);
                _currentGame.Genres = GamesTabTextboxGenres.Text;
                _currentGame.Favorite = GamesTabCheckBoxGlobalFavorite.IsChecked.Value;
            }
            catch (ArgumentException e)
            {
                MessageBox.Show("Error: " + e.Message);

            }

            //If all input fields are valid, save the database
            FileOps.SaveDatabase();
        }

        /// <summary>
        /// Refresh the ESRB rating icon to the current ESRB rating
        /// </summary>
        public void RefreshEsrbIcon(IGame game)
        {
            if (game == null) { return; }
            GamesTabImageEsrb.Source = null;
            if (game.EsrbRatingsRating.Equals(Enums.EsrbRatings.Everyone))
            {
                GamesTabImageEsrb.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Media\Esrb\Everyone.png"));
            }
            else if (game.EsrbRatingsRating.Equals(Enums.EsrbRatings.Everyone10))
            {
                GamesTabImageEsrb.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Media\Esrb\Everyone 10+.png"));
            }
            else if (game.EsrbRatingsRating.Equals(Enums.EsrbRatings.Teen))
            {
                GamesTabImageEsrb.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Media\Esrb\Teen.png"));
            }
            else if (game.EsrbRatingsRating.Equals(Enums.EsrbRatings.Mature))
            {
                GamesTabImageEsrb.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Media\Esrb\Mature.png"));
            }

            if (game.EsrbRatingsRating.Equals(Enums.EsrbRatings.Ao))
            {
                GamesTabImageEsrb.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Media\Esrb\Adults Only (Ao).png"));
            }
        }

        #endregion

        #region Emulators Tab

        /// <summary>
        /// Update the console info fields when the selected console is changed
        /// </summary>
        private void EmulatorsTab_ConsoleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EmulatorsTabListboxConsoleList.SelectedItem == null) { return; }
            string curItem = EmulatorsTabListboxConsoleList.SelectedItem.ToString();
            var consoleList = Database.GetConsoleList();
            foreach (string consoleName in consoleList)
            {
                IConsole console = Database.GetConsole(consoleName);
                if (console.ConsoleName.Equals(curItem))
                {
                    _currentEmulator = console;
                    EmulatorsTabTextboxConsoleName1.Text = console.ConsoleName;
                    EmulatorsTabTextboxRomExtension.Text = console.RomExtension;
                    EmulatorsTabTextboxEmulatorArgs.Text = console.LaunchParams;
                    EmulatorsTabTextboxEmulatorExe.Text = console.EmulatorExePath;
                    EmulatorsTabTextboxConsoleInfo.Text = console.ConsoleInfo;
                    EmulatorsTabTextboxGameCount.Text = console.GetGameCount().ToString();
                    EmulatorsTabTextboxReleaseDate.Text = console.ReleaseDate;
                }
            }
        }

        /// <summary>
        /// Save console button
        /// Save current console info to database file
        /// </summary>
        private void EmulatorsTab_SaveDatabaseFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                _currentEmulator.ConsoleName = EmulatorsTabTextboxConsoleName1.Text;
                _currentEmulator.RomExtension = EmulatorsTabTextboxRomExtension.Text;
                _currentEmulator.EmulatorExePath = EmulatorsTabTextboxEmulatorExe.Text;
                _currentEmulator.LaunchParams = EmulatorsTabTextboxEmulatorArgs.Text;
                _currentEmulator.ReleaseDate = EmulatorsTabTextboxReleaseDate.Text;
                _currentEmulator.ConsoleInfo = EmulatorsTabTextboxConsoleInfo.Text;
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show(exception.Message);
            }
            FileOps.SaveDatabase();
            MainWindow.RefreshConsoleList();
        }

        /// <summary>
        /// Close button
        /// </summary>
        private void EmulatorsTab_CloseButton_Click(object sender, EventArgs e)
        {
            MainWindow.IsSettingsWindowActive = false;
            Close();
        }

        /// <summary>
        /// Delete console button
        /// Deletes a consle and all associated games from the database
        /// </summary>
        private void EmulatorsTab_DeleteConsoleButton_Click(object sender, EventArgs e)
        {
            //Ensure that at least one console exists
            if (Database.GetConsoleCount() <= 1)
            {
                MessageBox.Show("Cannot have an empty console list");
                return;
            }
            EmulatorsTabListboxConsoleList.Items.Clear();
            GamesTabListboxConsoleList.Items.Clear();
            Database.RemoveConsole(_currentEmulator.ConsoleName);
            var consoleList = Database.GetConsoleList();
            foreach (string consoleName in consoleList)
            {
                IConsole console = Database.GetConsole(consoleName);
                EmulatorsTabListboxConsoleList.Items.Add(console.ConsoleName);
                GamesTabListboxConsoleList.Items.Add(console.ConsoleName);
            }
            EmulatorsTabListboxConsoleList.SelectedIndex = 0;

            MainWindow.RefreshConsoleList();
        }

        /// <summary>
        /// Add a new console
        /// </summary>
        private void EmulatorsTab_AddNewConsoleButton_Click(object sender, EventArgs e)
        {
            //Clear all text boxes initially 
            EmulatorsTabTextboxRomExtension.Text = null;
            EmulatorsTabTextboxEmulatorArgs.Text = null;
            EmulatorsTabTextboxConsoleInfo.Text = null;
            EmulatorsTabTextboxGameCount.Text = null;
            EmulatorsTabTextboxReleaseDate.Text = null;

            //Create a new console and add it to the datbase
            string newConsoleName = "New Console";
            IConsole newConsole = new Console(newConsoleName);

            Database.AddConsole(newConsole);
            EmulatorsTabListboxConsoleList.Items.Clear();
            GamesTabListboxConsoleList.Items.Clear();
            var consoleList = Database.GetConsoleList();
            foreach (string consoleName in consoleList)
            {
                IConsole con = Database.GetConsole(consoleName);
                EmulatorsTabListboxConsoleList.Items.Add(con.ConsoleName);
                GamesTabListboxConsoleList.Items.Add(con.ConsoleName);
            }
            EmulatorsTabListboxConsoleList.SelectedIndex = (EmulatorsTabListboxConsoleList.Items.Count - 1);
            MainWindow.RefreshConsoleList();
        }

        /// <summary>
        /// //Force metadata rescrape (All games within console)
        /// </summary>
        private void EmulatorsTab_ForceGlobalMetadataRescrapeButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Save the custom info fields for the current emulator
        /// </summary>
        private void EmulatorsTab_SaveInfoButton_Click(object sender, EventArgs e)
        {
            try
            {
                _currentEmulator.ConsoleName = EmulatorsTabTextboxConsoleName1.Text;
                _currentEmulator.RomExtension = EmulatorsTabTextboxRomExtension.Text;
                _currentEmulator.LaunchParams = EmulatorsTabTextboxEmulatorArgs.Text;
                _currentEmulator.ConsoleInfo = EmulatorsTabTextboxConsoleInfo.Text;
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show("Error: " + exception.Message);
            }
            MainWindow.RefreshConsoleList();
            MessageBox.Show("Saved");
        }

        /// <summary>
        /// Rescan all games across all emulators
        /// </summary>
        private void EmulatorsTab_GlobalRescanButton_Click(object sender, EventArgs e)
        {
            FileOps.ScanAllConsoles();
            MessageBox.Show("Global Rescan Successful");
        }

        /// <summary>
        /// Rescan console button
        /// Rescans all ROM files for the current console
        /// </summary>
        private void EmulatorsTab_RescanSingleConsoleButton_Click(object sender, EventArgs e)
        {
            //Ensure that a console is currently selected
            if (EmulatorsTabListboxConsoleList.SelectedItem == null)
            {
                MessageBox.Show("Must select a console");
                return;
            }

            //Fetch the currently selected console
            IConsole console = Database.GetConsole(EmulatorsTabListboxConsoleList.SelectedItem.ToString());

            if (FileOps.ScanSingleConsole(_currentEmulator))
            {
                MessageBox.Show(console.ConsoleName + " Successfully Scanned");
                return;
            }
            MessageBox.Show(console.ConsoleName + " Library Rescan Failed");
        }

        #endregion

        #region Users Tab

        /// <summary>
        /// Close and save button
        /// </summary>
        private void UsersTab_CloseButton_Click(object sender, EventArgs e)
        {
            MainWindow.IsSettingsWindowActive = false;
            FileOps.SavePreferences();
            Close();
        }

        /// <summary>
        /// Refresh user info under the User tab every time a new user is selected
        /// </summary>
        private void UsersTab_UsersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Fetch the current user
            IUser user = Database.GetUser(UsersTabListboxCurrentUser.SelectedItem.ToString());

            //Populate the favorites list for each user
            UsersTabListboxUserFavorites.Items.Clear();
            user.GetFavoritesList().ForEach(g => UsersTabListboxUserFavorites.Items.Add(g.Title + " - " + g.ConsoleName));

            //Populate the user fields
            UsersTabTextboxUsername.Text = user.Username;
            UsersTabTextboxEmail.Text = user.Email;
            UsersTabTextboxUserInfo.Text = user.UserInfo;
            UsersTabTextboxLoginCount.Text = user.GetUserLoginCount().ToString();
            UsersTabTextboxLaunchCount.Text = user.GetUserLaunchCount().ToString();
            UsersTabDropdownAllowedEsrb.Text = user.AllowedEsrbRatings.GetStringValue();

            //Only allow the current user to edit their own userdata
            if (user.Username.Equals(Database.GetCurrentUser().Username))
            {
                UsersTabTextboxUsername.IsEnabled = true;
                UsersTabTextboxEmail.IsEnabled = true;
                UsersTabTextboxUserInfo.IsEnabled = true;
                UsersTabDropdownAllowedEsrb.IsEnabled = true;
                UsersTabListboxUserFavorites.IsEnabled = true;
            }
        }

        /// <summary>
        /// Create new user button
        /// Create a new user and save the userdata to the preferences file
        /// </summary>
        private void UsersTab_NewUserButton_Click(object sender, EventArgs e)
        {
            //Create a new unicade account and display the dialog
            AccountWindow uc = new AccountWindow(Enums.UserType.LocalAccount);
            uc.ShowDialog();

            //Save the user info to the preferences file
            FileOps.SavePreferences();

            //Refresh the listbox contents
            UsersTabListboxCurrentUser.Items.Clear();
            Database.GetUserList().ForEach(u => UsersTabListboxCurrentUser.Items.Add(u));
        }

        /// <summary>
        /// Save button
        /// </summary>
        private void UsersTab_SaveAllUsersButton_Click(object sender, EventArgs e)
        {
            FileOps.SavePreferences();
        }

        /// <summary>
        /// Delete the currently selected user from the database
        /// </summary>
        private void UsersTab_DeleteUserButton_Click(object sender, EventArgs e)
        {
            IUser user = Database.GetUser(UsersTabListboxCurrentUser.SelectedItem.ToString());

            try
            {
                //Remove the user and refresh the database
                Database.RemoveUser(user.Username);
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show("Error: " + exception.Message);
            }

            //Refresh the user list
            UsersTabListboxCurrentUser.Items.Clear();
            Database.GetUserList().ForEach(u => UsersTabListboxCurrentUser.Items.Add(u));
        }

        /// <summary>
        /// Save button (Global Settings tab)
        /// Save the current global settings to the preferences file
        /// </summary>
        private void UsersTab_SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Database.GetCurrentUser().Username = UsersTabTextboxUsername.Text;
                Database.GetCurrentUser().SetUserPassword(UsersTabTextboxEmail.Text);
                Database.GetCurrentUser().UserInfo = UsersTabTextboxUserInfo.Text;
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show("Error: " + exception.Message);
            }


            Database.GetCurrentUser().AllowedEsrbRatings = UsersTabDropdownAllowedEsrb.SelectedItem == null ? Enums.EsrbRatings.Null : Enums.ConvertStringToEsrbEnum(UsersTabDropdownAllowedEsrb.SelectedItem.ToString());



            //Refresh the user list
            UsersTabListboxCurrentUser.Items.Clear();
            Database.GetUserList().ForEach(u => UsersTabListboxCurrentUser.Items.Add(u));
        }

        /// <summary>
        /// Delete user favorite
        /// </summary>
        private void UsersTab_DeleteFavoriteButton_Click(object sender, EventArgs e)
        {
            //Remove the favorite game at the current index
            Database.GetCurrentUser().GetFavoritesList().RemoveAt(UsersTabListboxUserFavorites.SelectedIndex);

            //Refresh the user favorites list
            UsersTabListboxUserFavorites.Items.Clear();
            Database.GetCurrentUser().GetFavoritesList().ForEach(g => UsersTabListboxUserFavorites.Items.Add(g.Title + " - " + g.ConsoleName));
        }

        /// <summary>
        /// Login local user button
        /// </summary>
        private void UsersTab_LoginButton_Click(object sender, EventArgs e)
        {
            //Display the login dialog
            LoginWindow login = new LoginWindow(Enums.UserType.LocalAccount);
            login.ShowDialog();

            //If the user is logged in sucuesfully, save the current user and preferences file
            UsersTabLabelCurrentUser.Content = "Current User: " + Database.GetCurrentUser().Username;
        }

        /// <summary>
        /// Refresh the current users list and userdata
        /// </summary>
        private void UsersTab_RefreshButton_Click(object sender, EventArgs e)
        {
            UsersTabLabelCurrentUser.Content = "Current User: " + Database.GetCurrentUser().Username;
        }

        #endregion

        #region Global Settings Tab

        /// <summary>
        /// Modify the global ESRB rating when the dropdown is changed
        /// </summary>
        private void GlobalSettingsTab_AllowedEsrbRatingDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.RestrictGlobalEsrbRatings = Enums.ConvertStringToEsrbEnum(GlobalTabDropdownAllowedEsrb.SelectedItem.ToString());
        }

        /// <summary>
        /// Save Global Settings button
        /// </summary>
        private void GlobalSettings_SavePreferenceFileButton_Click(object sender, EventArgs e)
        {

            try
            {
                Program.RestrictGlobalEsrbRatings = GlobalTabDropdownAllowedEsrb.SelectedItem == null ? Enums.EsrbRatings.Null : Enums.ConvertStringToEsrbEnum(GlobalTabDropdownAllowedEsrb.SelectedItem.ToString());
                Program.EnforceFileExtensions = GlobalTabCheckboxEnforceFileExtension.IsChecked.Value;
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show("Error: " + exception.Message);
            }

            //Save checkboxes
            MainWindow.DisplayEsrbWhileBrowsing = GlobalTabCheckboxToView.IsChecked.Value;
            Program.ShowSplashScreen = GlobalTabCheckboxDisplaySplash.IsChecked.Value;
            Program.RescanOnStartup = GlobalTabCheckboxRescanAllLibraries.IsChecked.Value;
            MainWindow.DisplayEsrbWhileBrowsing = GlobalTabCheckboxDisplayEsrb.IsChecked.Value;

            if (!GlobalTabTextboxPassword.Password.Equals("Active"))
            {
                if ((GlobalTabTextboxPassword.Password.Length >= 4) && (GlobalTabTextboxPassword.Password.Length <= 20))
                {
                    Program.PasswordProtection = CryptoEngine.Sha256Hash(GlobalTabTextboxPassword.Password);
                }
                else
                {
                    MessageBox.Show("Password must be between 4 and 20 chars");
                }
            }
            int n;
            int.TryParse(GlobalTabTextboxCoins.Text, out n);
            PayPerPlay.CoinsRequired = int.Parse(GlobalTabTextboxCoins.Text);

            //Save all active preferences to the local preferences file
            FileOps.SavePreferences();
        }

        /// <summary>
        /// Toggle payPerPlay checkbox
        /// </summary>
        private void GlobalSettingsTab_TogglePayPerPlayCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            PayPerPlay.PayPerPlayEnabled = false;
            GlobalTabTextboxCoins.IsEnabled = false;
            GlobalTabTextboxPlaytime.IsEnabled = false;
            PayPerPlay.PayPerPlayEnabled = GlobalTabCheckboxEnablePayPerPlay.IsChecked.Value;
            GlobalTabTextboxCoins.IsEnabled = GlobalTabCheckboxEnablePayPerPlay.IsChecked.Value;
            GlobalTabTextboxPlaytime.IsEnabled = GlobalTabCheckboxEnablePayPerPlay.IsChecked.Value;
        }

        /// <summary>
        /// Refresh global favorites button
        /// </summary>
        private void GlobalSettingsTab_RefreshGlobalFavoritesButton_Click(object sender, EventArgs e)
        {
            RefreshGlobalFavs();
        }

        /// <summary>
        /// Refresh the most played games list
        /// </summary>
        private void GlobalTabButtonRefreshMostPlayedList_Click(object sender, EventArgs e)
        {
            List<Game> unsortedList = new List<Game>();

            foreach (string consoleName in Database.GetConsoleList())
            {
                Console console = (Console)Database.GetConsole(consoleName);
                foreach (string gameName in console.GetGameList())
                {
                    unsortedList.Add((Game)console.GetGame(gameName));
                }
            }
            List<Game> sortedList = unsortedList.OrderBy(g => g.GetLaunchCount()).ToList();

            List<Game> mostPlayedList = sortedList.Take(10).ToList();
            mostPlayedList.ForEach(g => GlobalTabListBoxMostPlayedList.Items.Add($"({g.GetLaunchCount()}) " + g.Title + " - " + g.ConsoleName));
        }

        /// <summary>
        /// Close buttonl
        /// </summary>
        private void GlobalSettingsTab_CloseButton_Click(object sender, EventArgs e)
        {
            MainWindow.IsSettingsWindowActive = false;
            Close();
        }

        #endregion

        #region Web Tab

        /// <summary>
        /// Close button
        /// </summary>
        private void WebTab_Button_Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.IsSettingsWindowActive = false;
            Close();
        }

        /// <summary>
        /// Save all current webscraper settings to the database
        /// </summary>
        private void WebTab_Button_SaveScraperSettings_Click(object sender, RoutedEventArgs e)
        {
            WebOps.ParseReleaseDate = WebTabCheckboxReleaseDate.IsChecked.Value;
            WebOps.ParseCriticScore = WebTabCheckboxCriticScore.IsChecked.Value;
            WebOps.ParsePublisher = WebTabCheckboxPublisher.IsChecked.Value;
            WebOps.ParseDeveloper = WebTabCheckboxDeveloper.IsChecked.Value;
            WebOps.ParseEsrbRating = WebTabCheckboxEsrbRating.IsChecked.Value;
            WebOps.ParseDescription = WebTabCheckboxEsrbDescriptor.IsChecked.Value;
            WebOps.ParsePlayerCount = WebTabCheckboxPlayers.IsChecked.Value;
            WebOps.ParseDescription = WebTabCheckboxEsrbDescriptor.IsChecked.Value;
            WebOps.ParseBoxFrontImage = WebTabCheckboxBoxFront.IsChecked.Value;
            WebOps.ParseBoxBackImage = WebTabCheckboxBoxBack.IsChecked.Value;
            WebOps.ParseScreenshot = WebTabCheckboxScreenshot.IsChecked.Value;
            WebOps.ParseReleaseDate = WebTabCheckboxReleaseDate.IsChecked.Value;
            WebOps.ParseCriticScore = WebTabCheckboxCriticScore.IsChecked.Value;
            WebOps.ParsePublisher = WebTabCheckboxPublisher.IsChecked.Value;
            WebOps.ParseDeveloper = WebTabCheckboxDeveloper.IsChecked.Value;
            WebOps.ParseEsrbRating = WebTabCheckboxEsrbRating.IsChecked.Value;
            WebOps.ParseDescription = WebTabCheckboxEsrbDescriptor.IsChecked.Value;
            WebOps.ParsePlayerCount = WebTabCheckboxPlayers.IsChecked.Value;
            WebOps.ParseDescription = WebTabCheckboxEsrbDescriptor.IsChecked.Value;
            WebOps.ParseBoxFrontImage = WebTabCheckboxBoxFront.IsChecked.Value;
            WebOps.ParseBoxBackImage = WebTabCheckboxBoxBack.IsChecked.Value;
            WebOps.ParseScreenshot = WebTabCheckboxScreenshot.IsChecked.Value;
        }

        private void WebTabApiComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WebOps.CurrentApi = (Enums.Api)WebTabApiComboBox.SelectedIndex;
        }

        #endregion

        #region UniCade Cloud Tab

        /// <summary>
        /// Create new user button (UniCade Cloud tab)
        /// Create a new SQL UniCade Cloud user
        /// </summary>
        private void CloudTab_Button_CreateNewAccount_Click(object sender, RoutedEventArgs e)
        {
            AccountWindow ua = new AccountWindow(Enums.UserType.CloudAccount);
            ua.ShowDialog();
        }

        /// <summary>
        /// Login button (UniCade Cloud tab)
        /// Login a UniCade Cloud SQL user
        /// </summary>
        private void CloudTab_Button_Login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(Enums.UserType.CloudAccount);
            loginWindow.ShowDialog();
            if (SqlLiteClient.GetCurrentUsername() != null)
            {
                WebTabLabelCurrentWebUser.Content = "Current Web User: " + SqlLiteClient.GetCurrentUsername();
            }
        }

        /// <summary>
        /// Logout button (UniCade Cloud tab)
        /// Logs out the current SQL user 
        /// </summary>
        private void CloudTab_Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            //Check if a user is actually logged in
            if (SqlLiteClient.GetCurrentUsername() == null)
            {
                MessageBox.Show("User is already logged out");
            }
            else
            {
                //Log the current user out and update the interface
                SqlLiteClient.Logout();
                WebTabLabelCurrentWebUser.Content = "Current Web User: ";
            }
        }

        /// <summary>
        /// Delete user button
        /// Delete the SQL user and update the interface
        /// </summary>
        private void CloudTab_Button_DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (SqlLiteClient.GetCurrentUsername() == null)
            {
                MessageBox.Show("UniCade Cloud Login Required");
                return;
            }

            //Delete the current SQL user and update the label
            SqlLiteClient.DeleteCurrentUser();
            MessageBox.Show("Account deleted");
            WebTabLabelCurrentWebUser.Content = "Current Web User: ";
        }

        /// <summary>
        /// Upload all games button
        /// Upload all games across all consoles to UniCade Cloud
        /// </summary>
        private void CloudTab_Button_UploadAllGames_Click(object sender, RoutedEventArgs e)
        {
            if (SqlLiteClient.GetCurrentUsername() == null)
            {
                MessageBox.Show("UniCade Cloud Login Required");
                return;
            }
            SqlLiteClient.UploadAllGames();
            MessageBox.Show("Library successfully uploaded");
        }

        /// <summary>
        /// Delete all games from the current user's UniCade Cloud account
        /// </summary>
        private void CloudTab_Button_DeleteAllGamesInCloud_Click(object sender, RoutedEventArgs e)
        {
            //Check if a SQL user is currently logged in
            if (SqlLiteClient.GetCurrentUsername() == null)
            {
                MessageBox.Show("UniCade Cloud Login Required");
                return;
            }

            SqlLiteClient.DeleteAllUserGames();
            MessageBox.Show("Library successfully deleted");
        }

        /// <summary>
        /// Download all games button
        /// Download all game metadata across all consoles
        /// </summary>
        private void CloudTab_Button_DownloadAllGames_Click(object sender, RoutedEventArgs e)
        {
            if (SqlLiteClient.GetCurrentUsername() == null)
            {
                MessageBox.Show("UniCade Cloud Login Required");
                return;
            }
            if (SqlLiteClient.DownloadAllGames())
            {
                MessageBox.Show("Library metadata sucuessfully updated");
            }
            else
            {
                MessageBox.Show("SQL Operation failed");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CloudTab_Button_EndSession_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region About Tab

        /// <summary>
        /// Enter license button
        /// </summary>
        private void AboutTab_Button_EnterLicenseKey_Click(object sender, RoutedEventArgs e)
        {
            //Create a new license entry info and validate the key
            LicenseEntry le = new LicenseEntry();
            le.ShowDialog();
            AboutTabLabelLicensedTo.Content = "Licensed to: " + Program.UserLicenseName;
            AboutTabLabelLicenseKey.Content = "License Key: " + Program.UserLicenseKey;

            //Set the license text depending on if the key is valid
            AboutTabLabelEdition.Content = Program.IsLicenseValid ? "License Status: Full Version" : "License Status: Invalid";
        }

        private void LaunchCmdInterface_Click(object sender, RoutedEventArgs e)
        {
            Close();
            UniCadeCmd.PrepAndRun();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Refresh the current game info passed in as a Game object
        /// </summary>
        public void RefreshGameInfo(IGame game)
        {
            if (game == null)
            {
                //If no game is currently selected, set all info fields to null
                GamesTabTextboxTitle.Text = null;
                GamesTabTextboxConsole.Text = null;
                GamesTabTextboxReleaseDate.Text = null;
                GamesTabTextboxCriticScore.Text = null;
                GamesTabTextboxPublisher.Text = null;
                GamesTabTextboxDeveloper.Text = null;
                GamesTabTextboxEsrb.Text = null;
                GamesTabTextboxPlayers.Text = null;
                GamesTabTextboxEsrbDescriptor.Text = null;
                GamesTabTextboxDescription.Text = null;
                return;
            }

            //If a valid game is selected, update all info fields
            GamesTabTextboxTitle.Text = game.Title;
            GamesTabTextboxConsole.Text = game.ConsoleName;
            GamesTabTextboxReleaseDate.Text = game.ReleaseDate;
            GamesTabTextboxCriticScore.Text = game.CriticReviewScore;
            GamesTabTextboxPublisher.Text = game.PublisherName;
            GamesTabTextboxDeveloper.Text = game.DeveloperName;
            GamesTabTextboxEsrb.Text = game.EsrbRatingsRating.GetStringValue();
            GamesTabTextboxPlayers.Text = game.SupportedPlayerCount;
            GamesTabTextboxEsrbDescriptor.Text = game.GetEsrbDescriptorsString();
            GamesTabTextboxGenres.Text = game.Genres;
            GamesTabTextboxDescription.Text = game.Description;
            GamesTabTextboxLaunchCount.Text = game.GetLaunchCount().ToString();
            GamesTabCheckBoxGlobalFavorite.IsChecked = game.Favorite;

            RefreshEsrbIcon(_currentGame);

            GamesTabImageBoxfront.Source = null;
            GamesTabImageBoxback.Source = null;
            GamesTabImageScreeshot.Source = null;
            if (File.Exists(Directory.GetCurrentDirectory() + @"\Media\Games\" + _currentConsole.ConsoleName + "\\" + game.Title + "_BoxFront.jpg"))
            {
                GamesTabImageBoxfront.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Media\Games\" + _currentConsole.ConsoleName + "\\" + game.Title + "_BoxFront.jpg"));
            }

            if (File.Exists(Directory.GetCurrentDirectory() + @"\Media\Games\" + _currentConsole.ConsoleName + "\\" + game.Title + "_BoxBack.jpg"))
            {
                GamesTabImageBoxback.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Media\Games\" + _currentConsole.ConsoleName + "\\" + game.Title + "_BoxBack.jpg"));
            }

            if (File.Exists(Directory.GetCurrentDirectory() + @"\Media\Games\" + _currentConsole.ConsoleName + "\\" + game.Title + "_Screenshot.jpg"))
            {
                GamesTabImageScreeshot.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Media\Games\" + _currentConsole.ConsoleName + "\\" + game.Title + "_Screenshot.jpg"));
            }
        }
        /// <summary>
        /// Refresh global favorites across all consoles and users
        /// </summary>
        public void RefreshGlobalFavs()
        {
            GlobalTabListboxGlobalFavorites.Items.Clear();
            var consoleList = Database.GetConsoleList();
            foreach (string consoleName in consoleList)
            {
                IConsole console = Database.GetConsole(consoleName);
                if (console.GetGameCount() > 0)
                {
                    var gameList = console.GetGameList();
                    foreach (string gameTitle in gameList)
                    {
                        IGame game = console.GetGame(gameTitle);
                        if (game != null)
                        {
                            if (game.Favorite)
                            {
                                GlobalTabListboxGlobalFavorites.Items.Add(game.Title + " (" + game.ConsoleName + ")");
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Placeholder method
        /// </summary>
        public void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        public string BrowseForImage()
        {
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            return null;
        }


        #endregion

        private void GamesTabButtonAddBoxfrontImage_Click(object sender, RoutedEventArgs e)
        {
            //Display the open file dialog
            string sourcePath = BrowseForImage();

            //Load the image
            System.Drawing.Image image = System.Drawing.Image.FromFile(sourcePath);

            //If the directory does not exist, create it
            string directoryPath = Directory.GetCurrentDirectory() + @"\Media\Games\" + _currentConsole.ConsoleName + "\\";
            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }

            string destFileName = directoryPath + _currentGame.Title + "_BoxFront.jpg";

            //Save the image as a jpg in the proper directory
            image.Save(destFileName, System.Drawing.Imaging.ImageFormat.Jpeg);

            //After saving the image as a jpg file, refresh the current game info
            RefreshGameInfo(_currentGame);
        }

        private void GamesTabButtonAddBoxbackImage_Click(object sender, RoutedEventArgs e)
        {
            //Display the open file dialog
            string sourcePath = BrowseForImage();

            //Load the image
            System.Drawing.Image image = System.Drawing.Image.FromFile(sourcePath);

            //If the directory does not exist, create it
            string directoryPath = Directory.GetCurrentDirectory() + @"\Media\Games\" + _currentConsole.ConsoleName + "\\";
            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }

            string destFileName = directoryPath + _currentGame.Title + "_BoxBack.jpg";

            //Save the image as a jpg in the proper directory
            image.Save(destFileName, System.Drawing.Imaging.ImageFormat.Jpeg);

            //After saving the image as a jpg file, refresh the current game info
            RefreshGameInfo(_currentGame);
        }

        private void GamesTabButtonAddscreenshotImage_Click(object sender, RoutedEventArgs e)
        {
            //Display the open file dialog
            string sourcePath = BrowseForImage();

            //Load the image
            System.Drawing.Image image = System.Drawing.Image.FromFile(sourcePath);

            //If the directory does not exist, create it
            string directoryPath = Directory.GetCurrentDirectory() + @"\Media\Games\" + _currentConsole.ConsoleName + "\\";
            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }

            string destFileName = directoryPath + _currentGame.Title + "_Screenshot.jpg";

            //Save the image as a jpg in the proper directory
            image.Save(destFileName, System.Drawing.Imaging.ImageFormat.Jpeg);

            //After saving the image as a jpg file, refresh the current game info
            RefreshGameInfo(_currentGame);
        }
    }
}
