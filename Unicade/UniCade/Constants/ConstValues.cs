﻿
namespace UniCade.Constants
{
    internal static class ConstValues
    {
        #region Constants 

        /// <summary>
        /// The maximum number of consoles allowed 
        /// </summary>
        internal const int MaxConsoleCount = 100;

        /// <summary>
        /// The maximum number of games allowed per console
        /// </summary>
        internal const int MaxGameCount = 5000;

        /// <summary>
        /// The maximum number of users allowed 
        /// </summary>
        internal const int MaxUserCount = 50;

        /// <summary>
        /// The max length for console
        /// </summary>
        internal const int MaxConsoleNameLength = 35;

        /// <summary>
        /// The max length for directory paths
        /// </summary>
        internal const int MaxPathLength = 1000;

        /// <summary>
        /// The min length for directory paths
        /// </summary>
        internal const int MinPathLength = 4;

        /// <summary>
        /// The max length a ROM file extension
        /// </summary>
        internal const int MaxFileExtLength = 1000;

        /// <summary>
        /// The max length a the console info
        /// </summary>
        internal const int MaxConsoleInfoLength = 1000;

        /// <summary>
        /// The max length a the console info
        /// </summary>
        internal const int MaxLaunchParamsLength = 1000;

        /// <summary>
        /// The max char length for a username
        /// </summary>
        internal const int MaxUsernameLength = 30;

        /// <summary>
        /// The min char length for a username
        /// </summary>
        internal const int MinUsernameLength = 4;

        /// <summary>
        /// The max char length for user info descriptions
        /// </summary>
        internal const int MaxUserInfoLength = 200;

        /// <summary>
        /// The max char length for user email addresses
        /// </summary>
        internal const int MaxEmailLength = 200;

        /// <summary>
        /// The max char length for game filenames
        /// </summary>
        internal const int MaxGameFilenameLength = 200;

        /// <summary>
        /// The max char length for game titles
        /// </summary>
        internal const int MaxGameTitleLength = 200;

        /// <summary>
        /// The max char length for game descriptions
        /// </summary>
        internal const int MaxGameDescriptionLength = 500;

        /// <summary>
        /// The max char length for game publisher names
        /// </summary>
        internal const int MaxPublisherDeveloperLength = 500;

        /// <summary>
        /// The max char length for game generes
        /// </summary>
        internal const int MaxGameGenreLength = 500;

        /// <summary>
        /// The max char length for game tags
        /// </summary>
        internal const int MaxGameTagsLength = 500;

        /// <summary>
        /// The max char length for game user/critic review scores
        /// </summary>
        internal const int MaxGameReviewScoreLength = 20;

        /// <summary>
        /// The max char length for game trivia
        /// </summary>
        internal const int MaxGameTriviaLength = 500;

        /// <summary>
        /// The max char length for game trivia
        /// </summary>
        internal const int MaxGamePlayercountLength = 500;

        /// <summary>
        /// The max char length for game trivia
        /// </summary>
        internal const int MaxGameEsrbDescriptorsLength = 500;

        /// <summary>
        /// The max char length for game trivia
        /// </summary>
        internal const int MaxGameEsrbSummaryLength = 1000;

        /// <summary>
        /// The min length for a User password
        /// </summary>
        internal const int MinUserPasswordLength = 4;

        /// <summary>
        /// The max length for a User password
        /// </summary>
        internal const int MaxUserPasswordLength = 30;

        #endregion

        #region  Static Readonly Fields

        /// <summary>
        /// Global invalid characters 
        /// </summary>
        internal static readonly char[] InvalidChars = new char[] { '|', '*' };

        #endregion
    }
}
