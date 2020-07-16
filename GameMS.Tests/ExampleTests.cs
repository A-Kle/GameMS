using GamesMS.Records;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace GameMS.Tests
{
    [TestFixture]
    public class ExampleTests
    {
        private IBoardGameRepository gameRepository;

        [SetUp]
        public void Setup()
        {
            var gameRepo = new Mock<IBoardGameRepository>();
            gameRepo.Setup(r => r.QueryOver(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<BoardGameRecord>() { new BoardGameRecord() });
            gameRepository = gameRepo.Object;
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}