using GamesMS.Api.Services;
using NUnit.Framework;

namespace GamesMS.Api.Tests.Services
{
    [TestFixture]
    [Category("Unit")]
    public class RandomNameGeneratorServiceTests
    {
        private RandomNameGeneratorService randomNameGeneratorService;

        [SetUp]
        public void Setup()
        {
            randomNameGeneratorService = new RandomNameGeneratorService();
        }

        [TestCase(280, 255)]
        [TestCase(0, 255)]
        [TestCase(2, 3)]
        [TestCase(-11, 255)]
        [TestCase(35, 35)]
        public void GenerateName_GeneratesName_Shorter_Or_Equal_255_characters(int length, int lengthLessThan)
        {
            var result = randomNameGeneratorService.GenerateName(length, GenerationMode.Mixed);

            Assert.That(result.Length, Is.LessThanOrEqualTo(lengthLessThan));
        }
    }
}