﻿using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCade.Backend;
using UniCade.Constants;
using UniCade.Network;
using UniCade.Objects;

namespace UnitTests.NetworkTests
{
    [TestClass]
    public class ApiTests
    {
        #region Properties

        /// <summary>
        /// The current game object for API testing
        /// </summary>
        private Game _game;

        /// <summary>
        /// The current consoleName for API testing
        /// </summary>
        private const string ConsoleName = "Sony Playstation";

        #endregion 

        /// <summary>
        /// Initalize the database and create a new game
        /// </summary>
        [TestInitialize]
        public void Initalize()
        {
            //Initalize the program
            Database.Initalize();

            //Create a new game 
            _game = new Game("Resident Evil 2.bin", ConsoleName);
        }

        #region API Tests

        /// <summary>
        /// Verify that the GamesDB API properly fetches info for games
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void VerifyGamesDbApiScraping()
        {
            //Attempt to fetch info from GameDB API
            Assert.IsTrue(GamesdbApi.UpdateGameInfo(_game), "Connection error");

            //Verify that the game info has been properly scraped
            Assert.IsTrue(_game.Description.Contains("Suspense"), "Verify that the game desription is properly fetched");
            Assert.AreEqual(Enums.EsrbRatings.Mature, _game.EsrbRating, "Verify that the ESRB rating is properly fetched");
            Assert.AreEqual("Capcom", _game.PublisherName, "Verify that the publisher is properly fetched");

            //Verify that game images have been properly scraped
            string mediaDirectoryPath = Directory.GetCurrentDirectory() + @"\Media\Games\" + _game.ConsoleName + "\\";
            Assert.IsTrue(File.Exists(mediaDirectoryPath + _game.Title + "_BoxBack.jpg"));
            Assert.IsTrue(File.Exists(mediaDirectoryPath + _game.Title + "_BoxFront.jpg"));
            Assert.IsTrue(File.Exists(mediaDirectoryPath + _game.Title + "_Screenshot.jpg"));
        }

        /// <summary>
        /// Verify that the GamesDB API properly fetches info for consoles
        /// </summary>
        [TestMethod]
        [Priority(1)]
        public void VerifyGamesDbApiConsoleScraping()
        {
            //Attempt to fetch info from GameDB API
            Console console = new Console("Nintendo Gamecube");
            GamesdbApi.UpdateConsoleInfo(console);

            //Verify that the console info has been properly updated
            Assert.AreEqual("Nintendo", console.Developer, "Verify that the developer is properly fetched");
            Assert.AreEqual("486 MHz IBM \"Gekko\" PowerPC CPU", console.Cpu, "Verify that the cpu is properly fetched");
            Assert.AreEqual("43 MB total non-unified RAM", console.Ram, "Verify that the ram is properly fetched");
            Assert.AreEqual("162 MHz \"Flipper\" LSI (co-developed by Nintendo and ArtX, acquired by ATI)", console.Graphics, "Verify that the graphics card info is properly fetched");
            Assert.AreEqual("640×480 interlaced (480i) or progressive scan (480p) - 60 Hz", console.DisplayResolution, "Verify that the display resolution is properly fetched");
            Assert.IsTrue(console.ConsoleInfo.Contains("first Nintendo console"), "Verify that the console info is properly fetched");
            Assert.IsNotNull(console.ConsoleRating, "Verify that the avg user rating is properly fetched");
        }

        /// <summary>
        /// Verify that the MobyGames API properly fetches info for games
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [SuppressMessage("ReSharper", "UnusedVariable")]
        public void VerifyMobyGamesApiScraping()
        {

            //Attempt to fetch info from MobyGames API
            var result = MobyGamesApi.FetchGameInfo(_game).Result;
            Assert.IsTrue(result, "Connection error");

            //Verify that the game info has been properly scraped
            Assert.IsNotNull(_game.MobyGamesUrl, "Verify that the mobygames url is properly fetched");
            Assert.IsNotNull(_game.MobygamesApiId, "Verify that the mobygames API id is properly fetched");
            Assert.IsNotNull(_game.Description, "Verify that the mobygames desription is properly fetched");
            Assert.IsNotNull(_game.Genres, "Verify that the mobygames url geners are properly fetched");
            Assert.IsNotNull(_game.OtherPlatforms, "Verify that the list of other platforms is properly fetched");
            Assert.IsNotNull(_game.UserReviewScore, "Verify that the user review score is properly fetched");

        }

        #endregion

        /// <summary>
        /// Cleanup and delete the temp media directory
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            string mediaDirectoryPath = Directory.GetCurrentDirectory() + @"\Media\";
            if (Directory.Exists(mediaDirectoryPath))
            {
                Directory.Delete(mediaDirectoryPath, true);
            }
        }
    }
}
