﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using UniCade.Windows;

namespace UniCade
{
    class FileOps
    {
        #region Properties

        public static bool processActive;
        public static Process process;
        public static bool urlLaunch;

        #endregion

        #region Public Methods

        /// <summary>
        /// Load the database file from the specified path
        /// </summary>
        public static bool LoadDatabase(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }

            string rawLine;
            int consoleCount = 0;
            IConsole console = new Console("newConsole");
            char[] seperatorChar = { '|' };
            string[] spaceChar = { " " };
            StreamReader file = new StreamReader(path);

            while ((rawLine = file.ReadLine()) != null)
            {
                spaceChar = rawLine.Split(seperatorChar);
                if (rawLine.Substring(0, 5).Contains("***"))
                {
                    if (consoleCount > 0)
                    {
                        Database.ConsoleList.Add(console);
                    }
                    console = new Console(spaceChar[0].Substring(3), spaceChar[1], spaceChar[2], spaceChar[3], spaceChar[4], Int32.Parse(spaceChar[5]), spaceChar[6], spaceChar[7], spaceChar[8]);
                    consoleCount++;
                }
                else
                {
                    console.GameList.Add(new Game(spaceChar[0], spaceChar[1], Int32.Parse(spaceChar[2]), spaceChar[3], spaceChar[4], spaceChar[5], spaceChar[6], spaceChar[7], spaceChar[8], spaceChar[9], spaceChar[10], spaceChar[11], spaceChar[12], spaceChar[13], spaceChar[14], spaceChar[15], Int32.Parse(spaceChar[16])));
                }
            }
            if (consoleCount > 0)
            {
                Database.ConsoleList.Add(console);
            }

            if (consoleCount < 1)
            {
                MessageBox.Show("Fatal Error: Database File is corrupt");
                return false;
            }
            file.Close();
            return true;
        }

        /// <summary>
        /// Save the database to the specified path. Delete any preexisting database files
        /// </summary>
        public static void SaveDatabase(string path)
        {
            //Delete any preexisting database files 
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            try
            {
                using (StreamWriter streamWriter = File.CreateText(path))
                {
                    foreach (IConsole console in Database.ConsoleList)
                    {
                        streamWriter.WriteLine(string.Format("***{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|", console.ConsoleName, console.EmulatorPath, console.RomPath, console.PreferencesPath, console.RomExtension, console.GameCount, "Console Info", console.LaunchParams, console.ReleaseDate));
                        if (console.GameCount > 0)
                        {
                            foreach (IGame game in console.GameList)
                            {
                                streamWriter.WriteLine(string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}|{15}|{16}", game.FileName, game.ConsoleName, game.LaunchCount, game.ReleaseDate, game.Publisher, game.Developer, game.UserScore, game.CriticScore, game.Players, "Trivia", game.Esrb, game.EsrbDescriptor, game.EsrbSummary, game.Description, game.Genres, game.Tags, game.Favorite));
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Error saving database\n" + Program._databasePath + "\n"+ e.Message);
                return;
            }
        }

        /// <summary>
        /// Load preferences from the specified file path
        /// </summary>
        public static bool LoadPreferences(String path)
        {
            //Delete any preexisting preference files 
            if (!File.Exists(path))
            {
                return false;
            }

            string[] tmp = { "tmp" };
            char[] sep = { '|' };
            string[] tokenString = { " " };
            StreamReader file = new StreamReader(path);
            string line = file.ReadLine();

            tokenString = line.Split(sep);
            String currentUser = tokenString[1];

            line = file.ReadLine();
            tokenString = line.Split(sep);
            Program._databasePath = tokenString[1];

            line = file.ReadLine();
            tokenString = line.Split(sep);
            Program._emuPath = tokenString[1];

            line = file.ReadLine();
            tokenString = line.Split(sep);
            Program._mediaPath = tokenString[1];

            line = file.ReadLine();
            tokenString = line.Split(sep);
            if (tokenString[1].Contains("1"))
            {
                SettingsWindow.ShowSplashScreen = 1;
            }
            else
            {
                SettingsWindow.ShowSplashScreen = 0;
            }

            line = file.ReadLine();
            tokenString = line.Split(sep);
            if ((tokenString[1].Contains("1")))
            {
                SettingsWindow.ScanOnStartup = 1;
            }
            else
            {
                SettingsWindow.ScanOnStartup = 0;
            }

            line = file.ReadLine();
            tokenString = line.Split(sep);
            SettingsWindow.RestrictESRB = Int32.Parse(tokenString[1]);

            file.ReadLine();
            tokenString = line.Split(sep);
            if (tokenString[1].Contains("1"))
            {
                SettingsWindow.RequireLogin = 1;
            }
            else
            {
                SettingsWindow.RequireLogin = 0;
            }

            line = file.ReadLine();
            tokenString = line.Split(sep);
            if (tokenString[1].Contains("1"))
            {
                SettingsWindow.DisplayEsrbWhileBrowsing = 1;
            }
            else
            {
                SettingsWindow.DisplayEsrbWhileBrowsing = 0;
            }

            line = file.ReadLine();
            tokenString = line.Split(sep);
            if (tokenString[1].Contains("1"))
            {
                SettingsWindow.ShowLoadingScreen = 1;
            }
            else
            {
                SettingsWindow.ShowLoadingScreen = 0;
            }

            line = file.ReadLine();
            tokenString = line.Split(sep);
            if (tokenString[1].Contains("1"))
            {
                SettingsWindow.PayPerPlayEnabled = 1;
            }
            else
            {
                SettingsWindow.PayPerPlayEnabled = 0;
            }

            if (tokenString[2].Contains("1"))
            {
                SettingsWindow.LaunchOptions = 1;
            }
            else
            {
                SettingsWindow.LaunchOptions = 0;
            }

            //Parse coin count
            SettingsWindow.CoinsRequired = Int32.Parse(tokenString[3]);
            SettingsWindow.Playtime = Int32.Parse(tokenString[4]);

            //Parse user license key
            line = file.ReadLine();
            tokenString = line.Split(sep);
            Program._userLicenseName = tokenString[1];
            Program._userLicenseKey = tokenString[2];

            //Skip ***Users*** line
            file.ReadLine();

            //Parse user data
            while ((line = file.ReadLine()) != null)
            {
                tokenString = line.Split(sep);
                IUser user = new User(tokenString[0], tokenString[1], Int32.Parse(tokenString[2]), tokenString[3], Int32.Parse(tokenString[4]), tokenString[5], tokenString[6], "null");
                if (tokenString[6].Length > 0)
                {
                    string[] rawString = tokenString[7].Split('#');
                    String string1 = "";
                    int iterator = 1;

                    foreach (string s in rawString)
                    {
                        if ((iterator % 2 == 0) && (iterator > 1))
                        {
                            user.Favorites.Add(new Game(string1, s, 0));

                        }
                        string1 = s + ".zip";
                        iterator++;
                    }
                }
                Database.UserList.Add(user);
            }
            foreach (IUser user in Database.UserList)
            {
                if (user.Username.Equals(currentUser))
                {
                    SettingsWindow.CurrentUser = user;
                }
            }
            file.Close();
            return true;
        }

        /// <summary>
        /// Save preferences file to the specified path
        /// </summary>
        public static void SavePreferences(String path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            foreach (IUser user in Database.UserList)
            {
                if (SettingsWindow.CurrentUser.Username.Equals(user.Username))
                {
                    Database.UserList.Remove(user);
                    Database.UserList.Add(SettingsWindow.CurrentUser);
                    break;
                }
            }

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("CurrentUser|" + SettingsWindow.CurrentUser.Username);
                sw.WriteLine("_databasePath|" + Program._databasePath);
                sw.WriteLine("EmulatorFolderPath|" + Program._emuPath);
                sw.WriteLine("MediaFolderPath|" + Program._mediaPath);
                sw.WriteLine("ShowSplash|" + SettingsWindow.ShowSplashScreen);
                sw.WriteLine("ScanOnStartup|" + SettingsWindow.ScanOnStartup);
                sw.WriteLine("RestrictESRB|" + SettingsWindow.RestrictESRB);
                sw.WriteLine("RequireLogin|" + SettingsWindow.RequireLogin);
                sw.WriteLine("CmdOrGui|" + SettingsWindow.PerferCmdInterface);
                sw.WriteLine("LoadingScreen|" + SettingsWindow.ShowLoadingScreen);
                sw.WriteLine("PaySettings|" + SettingsWindow.PayPerPlayEnabled + "|" + SettingsWindow.LaunchOptions + "|" + SettingsWindow.CoinsRequired + "|" + SettingsWindow.Playtime);
                sw.WriteLine("License Key|" + Program._userLicenseName + "|" + Program._userLicenseKey);
                sw.WriteLine("***UserData***");
                foreach (IUser user in Database.UserList)
                {
                    string favs = "";
                    foreach (IGame g in user.Favorites)
                    {
                        favs += (g.Title + "#" + g.ConsoleName + "#");
                    }
                    sw.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|", user.Username, user.Pass, user.LoginCount, user.Email, user.TotalLaunchCount, user.UserInfo, user.AllowedEsrb, favs);
                }
            }
        }

        /// <summary>
        /// Scan the target directory for new ROM files and add them to the active database
        /// </summary>
        public static bool Scan(string targetDirectory)
        {
            string[] subdirectoryEntries = null;
            try
            {
                subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            }
            catch
            {
                MessageBox.Show("Directory Not Found: " + targetDirectory);
                return false;
            }
            foreach (string subdirectory in subdirectoryEntries)
            {
                ScanDirectory(subdirectory, targetDirectory);
            }

            return true;
        }

        /// <summary>
        /// Scan the specied folder for games within a single console
        /// Note: This is a helper function called multiple times by the primary scan function
        /// </summary>
        public static bool ScanDirectory(string path, string directory)
        {
            string emuName = new DirectoryInfo(path).Name;
            bool foundConsole = false;
            string[] extension;
            bool duplicate = false;

            IConsole currentConsole = null;
            foreach (IConsole console in Database.ConsoleList)
            {
                if (console.ConsoleName.Equals(emuName))
                {
                    currentConsole = console;
                    foundConsole = true;
                    break;
                }
            }
            if (!foundConsole)
            {
                return false;
            }

            string[] fileEntries = null;
            string[] exs = currentConsole.RomExtension.Split('*');
            try
            {
                fileEntries = Directory.GetFiles(path);
            }
            catch
            {
                MessageBox.Show("Directory Not Found: " + path);
                return false;
            }
            foreach (string fileName in fileEntries)
            {
                if (SettingsWindow.EnforceFileExtensions > 0)
                {
                    extension = fileName.Split('.');
                    foreach (string s in exs)
                    {
                        if (extension[1].Equals(s))
                        {
                            duplicate = false;
                            foreach (IGame game in currentConsole.GameList)
                            {
                                if (game.Title.Equals(Path.GetFileName(fileName)))
                                {
                                    duplicate = true;
                                    break;
                                }
                            }
                            if (!duplicate)
                            {
                                currentConsole.GameList.Add(new Game(Path.GetFileName(fileName), currentConsole.ConsoleName, 0));
                            }
                        }
                    }
                }
                else
                {
                    duplicate = false;
                    foreach (IGame game in currentConsole.GameList)
                    {
                        if (game.Title.Equals(fileName.Split('.')[0]))
                        {
                            duplicate = true;
                            break;
                        }
                    }
                    if (!duplicate)
                    {
                        currentConsole.GameList.Add(new Game(Path.GetFileName(fileName), currentConsole.ConsoleName, 0));
                    }
                }
            }

            //Delete nonexistent games
            bool found = false;
            IGame foundGame = null;
            foreach (IGame game in currentConsole.GameList)
            {
                found = false;
                foreach (string fileName in fileEntries)
                {
                    if (game.Title.Equals(Path.GetFileName(fileName)))
                    {
                        found = true;
                    }
                }
                if (found)
                {
                    currentConsole.GameList.Remove(foundGame);
                    found = false;
                    foundGame = null;
                }
            }
            RefreshGameCount();
            return true;
        }

        public static void LoadConsoles()
        {
            string line;
            char[] sep = { '|' };
            string[] r = { " " };
            StreamReader file = new StreamReader(@"C:\UniCade\ConsoleList.txt");
            while ((line = file.ReadLine()) != null)
            {
                r = line.Split(sep);
                Database.ConsoleList.Add(new Console(r[0], r[1], r[2], r[3], r[4], Int32.Parse(r[5]), r[6], r[8], " "));
            }
            file.Close();
        }

        /// <summary>
        /// Launch the specified ROM file using the paramaters specified by the console
        /// </summary>
        public static void Launch(IGame game)
        {
            if (SettingsWindow.CurrentUser.AllowedEsrb.Length > 1)
            {
                if (SettingsWindow.CalcEsrb(game.Esrb) >= SettingsWindow.CalcEsrb(SettingsWindow.CurrentUser.AllowedEsrb))
                {
                    ShowNotification("NOTICE", "ESRB " + game.Esrb + " Is Restricted for" + SettingsWindow.CurrentUser.Username);
                    return;
                }
            }

            else if (SettingsWindow.RestrictESRB > 0)
            {
                if (SettingsWindow.CalcEsrb(game.Esrb) >= SettingsWindow.RestrictESRB)
                {
                    ShowNotification("NOTICE", "ESRB " + game.Esrb + " Is Restricted\n");
                    return;
                }
            }

            game.LaunchCount++;
            SettingsWindow.CurrentUser.TotalLaunchCount++;
            process = new Process();

            //Fetch the console object
            IConsole console = Database.ConsoleList.Find(e => e.ConsoleName.Equals(game.ConsoleName));

            string gamePath = ("\"" + console.RomPath + game.FileName + "\"");
            string testGamePath = (console.RomPath + game.FileName);
            if (!File.Exists(testGamePath))
            {
                ShowNotification("System", "ROM does not exist. Launch Failed");
                return;
            }
            string args = "";
            if (console.ConsoleName.Equals("MAME"))
            {
                args = console.LaunchParams.Replace("%file", game.Title);
            }
            else
            {
                args = console.LaunchParams.Replace("%file", gamePath);
            }
            process.EnableRaisingEvents = true;
            process.Exited += new EventHandler(ProcessExited);
            if (console.ConsoleName.Equals("PC"))
            {
                process.StartInfo.FileName = args;
                if (args.Contains("url"))
                {
                    urlLaunch = true;
                }

            }
            else
            {
                if (!File.Exists(console.EmulatorPath))
                {
                    ShowNotification("System", "Emulator does not exist. Launch Failed");
                    return;
                }
                process.StartInfo.FileName = console.EmulatorPath;
                process.StartInfo.Arguments = args;
            }
            ShowNotification("System", "Loading ROM File");
            process.Start();
            processActive = true;
            MainWindow._gameRunning = true;
        }

        private static void ProcessExited(object sender, System.EventArgs e)
        {
            MainWindow._gameRunning = false;
            processActive = false;
        }

        /// <summary>
        /// Kill the currently running process and toggle flags
        /// </summary>
        public static void KillProcess()
        {
            if (urlLaunch)
            {
                SendKeys.SendWait("^%{F4}");
                ShowNotification("UniCade System", "Attempting Force Close");
                MainWindow._gameRunning = false;
                processActive = false;
                MainWindow.ReHookKeys();
                return;
            }
            else if (process.HasExited)
            {
                return;
            }

            process.Kill();
            MainWindow.ReHookKeys();
            MainWindow._gameRunning = false;
            processActive = false;
        }

        /// <summary>
        /// Restore the default consoles. These changes will take effect Immediately. 
        /// </summary>
        public static void RestoreDefaultConsoles()
        {
            Database.ConsoleList.Add(new Console("Sega Genisis", @"C:\UniCade\Emulators\Fusion\Fusion.exe", @"C:\UniCade\ROMS\Sega Genisis\", "prefPath", ".bin*.iso*.gen*.32x", 0, "consoleInfo", "%file -gen -auto -fullscreen", "1990"));
            Database.ConsoleList.Add(new Console("Wii", @"C:\UniCade\Emulators\Dolphin\dolphin.exe", @"C:\UniCade\ROMS\Wii\", "prefPath", ".gcz*.iso", 0, "consoleInfo", "/b /e %file", "2006"));
            Database.ConsoleList.Add(new Console("NDS", @"C:\UniCade\Emulators\NDS\DeSmuME.exe", @"C:\UniCade\ROMS\NDS\", "prefPath", ".nds", 0, "consoleInfo", "%file", "2005"));
            Database.ConsoleList.Add(new Console("GBC", @"C:\UniCade\Emulators\GBA\VisualBoyAdvance.exe", @"C:\UniCade\ROMS\GBC\", "prefPath", ".gbc", 0, "consoleInfo", "%file", "1998"));
            Database.ConsoleList.Add(new Console("MAME", @"C:\UniCade\Emulators\MAME\mame.bat", @"C:\UniCade\Emulators\MAME\roms\", "prefPath", ".zip", 0, "consoleInfo", "", "1980")); //%file -skip_gameinfo -nowindow
            Database.ConsoleList.Add(new Console("PC", @"C:\Windows\explorer.exe", @"C:\UniCade\ROMS\PC\", "prefPath", ".lnk*.url", 0, "consoleInfo", "%file", "1980"));
            Database.ConsoleList.Add(new Console("GBA", @"C:\UniCade\Emulators\GBA\VisualBoyAdvance.exe", @"C:\UniCade\ROMS\GBA\", "prefPath", ".gba", 0, "consoleInfo", "%file", "2001"));
            Database.ConsoleList.Add(new Console("Gamecube", @"C:\UniCade\Emulators\Dolphin\dolphin.exe", @"C:\UniCade\ROMS\Gamecube\", "prefPath", ".iso*.gcz", 0, "consoleInfo", "/b /e %file", "2001"));
            Database.ConsoleList.Add(new Console("NES", @"C:\UniCade\Emulators\NES\Jnes.exe", @"C:\UniCade\ROMS\NES\", "prefPath", ".nes", 0, "consoleInfo", "%file", "1983"));
            Database.ConsoleList.Add(new Console("SNES", @"C:\UniCade\Emulators\ZSNES\zsnesw.exe", @"C:\UniCade\ROMS\SNES\", "prefPath", ".smc", 0, "consoleInfo", "%file", "1990"));
            Database.ConsoleList.Add(new Console("N64", @"C:\UniCade\Emulators\Project64\Project64.exe", @"C:\UniCade\ROMS\N64\", "prefPath", ".n64*.z64", 0, "consoleInfo", "%file", "1996"));
            Database.ConsoleList.Add(new Console("PS1", @"C:\UniCade\Emulators\ePSXe\ePSXe.exe", @"C:\UniCade\ROMS\PS1\", "prefPath", ".iso*.bin*.img", 0, "consoleInfo", "-nogui -loadbin %file", "1994"));
            Database.ConsoleList.Add(new Console("PS2", @"C:\UniCade\Emulators\PCSX2\pcsx2.exe", @"C:\UniCade\ROMS\PS2\", "prefPath", ".iso*.bin*.img", 0, "consoleInfo", "%file", "2000"));
            Database.ConsoleList.Add(new Console("Atari 2600", @"C:\UniCade\Emulators\Stella\Stella.exe", @"C:\UniCade\ROMS\Atari 2600\", "prefPath", ".iso*.bin*.img", 0, "consoleInfo", "file", "1977"));
            Database.ConsoleList.Add(new Console("Dreamcast", @"C:\UniCade\Emulators\NullDC\nullDC_Win32_Release-NoTrace.exe", @"C:\UniCade\ROMS\Dreamcast\", "prefPath", ".iso*.bin*.img", 0, "consoleInfo", "-config ImageReader:defaultImage=%file", "1998"));
            Database.ConsoleList.Add(new Console("PSP", @"C:\UniCade\Emulators\PPSSPP\PPSSPPWindows64.exe", @"C:\UniCade\ROMS\PSP\", "prefPath", ".iso*.cso", 0, "consoleInfo", "%file", "2005"));
            Database.ConsoleList.Add(new Console("Wii U", @"C:\UniCade\Emulators\WiiU\cemu.exe", @"C:\UniCade\ROMS\Atari 2600\", "prefPath", ".iso*.bin*.img", 0, "consoleInfo", "file", "2012"));
            Database.ConsoleList.Add(new Console("Xbox 360", @"C:\UniCade\Emulators\X360\x360.exe", @"C:\UniCade\ROMS\X360\", "prefPath", ".iso*.bin*.img", 0, "consoleInfo", "%file", "2005"));
            Database.ConsoleList.Add(new Console("PS3", @"C:\UniCade\Emulators\PS3\ps3.exe", @"C:\UniCade\ROMS\PS3\", "prefPath", ".iso", 0, "consoleInfo", "%file", "2009"));
            Database.ConsoleList.Add(new Console("3DS", @"C:\UniCade\Emulators\PS3\3ds.exe", @"C:\UniCade\ROMS\3DS\", "prefPath", ".iso", 0, "consoleInfo", "%file", "2014"));
        }

        /// <summary>
        /// Refresh the total game count across all consoles
        /// </summary>
        public static void RefreshGameCount()
        {
            Database.TotalGameCount = 0; ;
            foreach (IConsole console in Database.ConsoleList)
            {
                foreach (IGame g in console.GameList)
                {
                    Database.TotalGameCount++;
                }
            }
        }

        /// <summary>
        /// Validate the integrity of the Media folder located in the current working directory
        /// </summary>
        public static bool VerifyMediaDirectory()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Media"))
            {
                MessageBox.Show("Media directory does not exist. Please reinstall or download UniCade Media Package to the current working directory");
                return false;
            }
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Media\Consoles"))
            {
                MessageBox.Show("Media (Consoles) directory does not exist. Please reinstall or download UniCade Media Package to the current working directory");
                return false;
            }
            if (Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Media\Consoles").Length < 4)
            {
                MessageBox.Show("Media (Consoles) directory is corrupt. Please reinstall or download UniCade Media Package to the current working directory");
                return false;
            }
            if (Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Media\Consoles\Logos").Length < 4)
            {
                MessageBox.Show("Media (Console Logos) directory is corrupt. Please reinstall or download UniCade Media Package to the current working directory");
                return false;
            }
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Media\Consoles\Logos"))
            {
                MessageBox.Show("Media (Console Logos) directory does not exist. Please reinstall or download UniCade Media Package to the current working directory");
                return false;
            }
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Media\Games"))
            {
                MessageBox.Show("Media (Games) directory does not exist. Please reinstall or download UniCade Media Package to the current working directory");
                return false;
            }
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Media\Backgrounds"))
            {
                MessageBox.Show("Media (Backgrounds) directory does not exist. Please reinstall or download UniCade Media Package to the current working directory");
                return false;
            }
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\Media\Esrb"))
            {
                MessageBox.Show("Media (ESRB) directory does not exist. Please reinstall or download UniCade Media Package to the current working directory");
                return false;
            }
            if (!File.Exists(Directory.GetCurrentDirectory() + @"\Media\Backgrounds\UniCade Logo.png") || !File.Exists(Directory.GetCurrentDirectory() + @"\Media\Backgrounds\Interface Background.png") || !File.Exists(Directory.GetCurrentDirectory() + @"\Media\Backgrounds\UniCade Marquee.png") || !File.Exists(Directory.GetCurrentDirectory() + @"\Media\Backgrounds\UniCade Icon.ico"))
            {
                MessageBox.Show("Media (Backgrounds) directory is corrupt. Please reinstall or download UniCade Media Package to the current working directory");
                return false;
            }

            if (!File.Exists(Directory.GetCurrentDirectory() + @"\Media\Esrb\Everyone.png") || !File.Exists(Directory.GetCurrentDirectory() + @"\Media\Esrb\Everyone 10+.png") || !File.Exists(Directory.GetCurrentDirectory() + @"\Media\Esrb\Teen.png") || !File.Exists(Directory.GetCurrentDirectory() + @"\Media\Esrb\Mature.png") || !File.Exists(Directory.GetCurrentDirectory() + @"\Media\Esrb\Adults Only (AO).png"))
            {
                MessageBox.Show("Media (ESRB) directory is corrupt. Please reinstall or download UniCade Media Package to the current working directory");
                return false;
            }
            return true;
        }

        public static void CreateNewRomDirectory()
        {
            Directory.CreateDirectory(Program._romPath + @"\Sega Genisis");
            Directory.CreateDirectory(Program._romPath + @"\Wii");
            Directory.CreateDirectory(Program._romPath + @"\NDS");
            Directory.CreateDirectory(Program._romPath + @"\GBC");
            Directory.CreateDirectory(Program._romPath + @"\MAME");
            Directory.CreateDirectory(Program._romPath + @"\PC");
            Directory.CreateDirectory(Program._romPath + @"\GBA");
            Directory.CreateDirectory(Program._romPath + @"\Gamecube");
            Directory.CreateDirectory(Program._romPath + @"\NES");
            Directory.CreateDirectory(Program._romPath + @"\SNES");
            Directory.CreateDirectory(Program._romPath + @"\N64");
            Directory.CreateDirectory(Program._romPath + @"\PS1");
            Directory.CreateDirectory(Program._romPath + @"\PS2");
            Directory.CreateDirectory(Program._romPath + @"\PS3");
            Directory.CreateDirectory(Program._romPath + @"\Atari 2600");
            Directory.CreateDirectory(Program._romPath + @"\Dreamcast");
            Directory.CreateDirectory(Program._romPath + @"\PSP");
            Directory.CreateDirectory(Program._romPath + @"\Wii U");
            Directory.CreateDirectory(Program._romPath + @"\Xbox 360");
            Directory.CreateDirectory(Program._romPath + @"\3DS");

        }

        /// <summary>
        /// Generate a new emulator directory with folders for all default emulators
        /// </summary>
        public static void CreateNewEmuDirectory()
        {
            Directory.CreateDirectory(Program._emuPath + @"\Sega Genisis");
            Directory.CreateDirectory(Program._emuPath + @"\Wii");
            Directory.CreateDirectory(Program._emuPath + @"\NDS");
            Directory.CreateDirectory(Program._emuPath + @"\GBC");
            Directory.CreateDirectory(Program._emuPath + @"\MAME");
            Directory.CreateDirectory(Program._emuPath + @"\GBA");
            Directory.CreateDirectory(Program._emuPath + @"\Gamecube");
            Directory.CreateDirectory(Program._emuPath + @"\NES");
            Directory.CreateDirectory(Program._emuPath + @"\SNES");
            Directory.CreateDirectory(Program._emuPath + @"\N64");
            Directory.CreateDirectory(Program._emuPath + @"\PS1");
            Directory.CreateDirectory(Program._emuPath + @"\PS2");
            Directory.CreateDirectory(Program._emuPath + @"\PS3");
            Directory.CreateDirectory(Program._emuPath + @"\Atari 2600");
            Directory.CreateDirectory(Program._emuPath + @"\Dreamcast");
            Directory.CreateDirectory(Program._emuPath + @"\PSP");
            Directory.CreateDirectory(Program._emuPath + @"\Wii U");
            Directory.CreateDirectory(Program._emuPath + @"\Xbox 360");
            Directory.CreateDirectory(Program._emuPath + @"\3DS");
        }

        /// <summary>
        /// Restore default preferences. These updated preferences will take effect immediatly.
        /// NOTE: These changes are not automatically saved to the database file.
        /// </summary>
        public static void RestoreDefaultPreferences()
        {
            SettingsWindow.CurrentUser = new User("UniCade", "temp", 0, "unicade@unicade.com", 0, " ", "", "");
            Database.UserList.Add(SettingsWindow.CurrentUser);
            SettingsWindow.ShowSplashScreen = 0;
            SettingsWindow.ScanOnStartup = 0;
            SettingsWindow.RestrictESRB = 0;
            SettingsWindow.RequireLogin = 0;
            SettingsWindow.PerferCmdInterface = 0;
            SettingsWindow.ShowLoadingScreen = 0;
            SettingsWindow.PayPerPlayEnabled = 0;
            SettingsWindow.CoinsRequired = 1;
            SettingsWindow.Playtime = 15;
            SettingsWindow.LaunchOptions = 0;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Display a timed popup notification in the lower right corner of the interface
        /// </summary>
        private static void ShowNotification(string title, string body)
        {
            NotificationWindow notification = new NotificationWindow(title, body);
            notification.Show();
        }

        #endregion
    }
}

