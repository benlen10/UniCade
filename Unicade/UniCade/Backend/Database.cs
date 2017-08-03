﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using UniCade.Constants;
using UniCade.Objects;
using Console = UniCade.Objects.Console;

namespace UniCade.Backend
{
    static class Database
    {
        #region Properties

        /// <summary>
        /// The current user object 
        /// </summary>
        public static IUser CurrentUser;

        /// <summary>
        /// The default user for the interface
        /// </summary>
        public static IUser DefaultUser;

        #endregion

        #region  Private Properties

        /// <summary>
        /// The current number of consoles present in the database
        /// </summary>
        private static int _consoleCount;

        /// <summary>
        /// The current list of consoles
        /// </summary>
        private static List<IConsole> _consoleList;

        /// <summary>
        /// The list of current users
        /// </summary>
        private static List<IUser> _userList;

        /// <summary>
        /// The current number of users in the database
        /// </summary>
        private static int _userCount;

        /// <summary>
        /// The current number of games across all game consoles
        /// </summary>
        private static int _totalGameCount;

        #endregion

        #region Public Methods

        /// <summary>
        /// Initalize the current properties
        /// </summary>
        public static void Initalize()
        {
            _totalGameCount = 0;
            _consoleList = new List<IConsole>();
            _userList = new List<IUser>();
            IUser uniCadeUser = new User("UniCade", "temp", 0, "unicade@unicade.com", 0, " ", Enums.ESRB.Null, "");
            _userList.Add(uniCadeUser);
            CurrentUser = uniCadeUser;
            DefaultUser = uniCadeUser;
        }

        /// <summary>
        /// Add a new console to the database
        /// </summary>
        /// <param name="console"></param>
        /// <returns>true if the console was sucuessfully added</returns>
        public static bool AddConsole(IConsole console)
        {
            //Return false if a console with a duplicate name already exists
            if (_consoleList.Find(e => e.ConsoleName.Equals(console.ConsoleName)) != null)
            {
                return false;
            }

            _consoleList.Add(console);
            _consoleCount++;
            return true;
        }

        /// <summary>
        /// Add a new console to the database
        /// </summary>
        /// <param name="consoleName">The name of the console to remove</param>
        /// <returns>true if the console was sucuessfully added</returns>
        public static bool RemoveConsole(string consoleName)
        {
            //Attempt to fetch the console from the current list
            IConsole console = _consoleList.Find(e => e.ConsoleName.Equals(consoleName));

            if (console != null)
            {
                _consoleList.Remove(console);
                _consoleCount--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Return the console object with the matching name
        /// </summary>
        /// <param name="consoleName">The name of the console to fetch</param>
        /// <returns>IConsole object with matching name</returns>
        public static IConsole GetConsole(string consoleName)
        {
            return _consoleList.Find(c => c.ConsoleName.Equals(consoleName));
        }

        /// <summary>
        /// Return a string list of all console names
        /// </summary>
        /// <returns></returns>
        public static List<string> GetConsoleList()
        {
            return _consoleList.Select(c => c.ConsoleName).ToList();
        }

        public static int GetConsoleCount()
        {
            return _consoleCount;
        }

        /// <summary>
        /// Add a new user to the currentUserList
        /// Return false if the username already exists
        /// </summary>
        /// <returns>true if the user was added sucuessfully</returns>
        public static bool AddUser(IUser user)
        {
            if (_userList.Find(u => u.Username.Equals(user.Username)) == null)
            {
                _userList.Add(user);
                _userCount++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove a user from the current _userList
        /// Return false if the user is not found
        /// </summary>
        /// <param name="username"></param>
        /// <returns>True if the user was removed sucuessfully</returns>
        public static bool RemoveUser(string username)
        {
            IUser user = _userList.Find(u => u.Username.Equals(username));
            if (user != null)
            {
                _userList.Remove(user);
                _userCount--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Return the IUser object with the matching username
        /// </summary>
        /// <param name="username">The username of the user to fetch</param>
        /// <returns>IUser object with matching username</returns>
        public static IUser GetUser(string username)
        {
            return _userList.Find(c => c.Username.Equals(username));
        }

        /// <summary>
        /// Return a string list of all usernames
        /// </summary>
        /// <returns>List of all usernames</returns>
        public static List<string> GetUserList()
        {
            return _userList.Select(u => u.Username).ToList();
        }

        /// <summary>
        /// Return the current number of users present in the database
        /// </summary>
        /// <returns>User count</returns>
        public static int GetUserCount()
        {
            return _userCount;
        }

        /// <summary>
        /// Refresh the total game count across all consoles
        /// </summary>
        /// <returns>Total game count</returns>
        public static int RefreshTotalGameCount()
        {
            var count = 0;
            _consoleList.ForEach(c => count += c.GetGameCount());
            _totalGameCount = count;
            return count;
        }

        /// <summary>
        /// Return the total number of games across all consoles
        /// </summary>
        /// <returns>Total game count</returns>
        public static int GetTotalGameCount()
        {
            return _totalGameCount;
        }

        #endregion
    }
}
