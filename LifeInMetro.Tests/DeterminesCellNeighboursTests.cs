using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace LifeInMetro.Tests
{
    [TestClass]
    public class DeterminesCellNeighboursTests
    {
        [TestMethod]
        public void CanDetermineNeighboursForOriginWithOneByteCell()
        {
            var sut = new DeterminesCellNeighbours(5, 5);

            var neighbours = sut.GetNeighbourIndexes(0, 0);

            Assert.AreEqual(24, neighbours[0]);
            Assert.AreEqual(20, neighbours[1]);
            Assert.AreEqual(21, neighbours[2]);
            Assert.AreEqual(4, neighbours[3]);
            Assert.AreEqual(1, neighbours[4]);
            Assert.AreEqual(9, neighbours[5]);
            Assert.AreEqual(5, neighbours[6]);
            Assert.AreEqual(6, neighbours[7]);
        }

        [TestMethod]
        public void CanDetermineNeighboursForUpperRightWithOneByteCell()
        {
            var sut = new DeterminesCellNeighbours(5, 5);

            var neighbours = sut.GetNeighbourIndexes(4, 0);

            Assert.AreEqual(23, neighbours[0]);
            Assert.AreEqual(24, neighbours[1]);
            Assert.AreEqual(20, neighbours[2]);
            Assert.AreEqual(3, neighbours[3]);
            Assert.AreEqual(0, neighbours[4]);
            Assert.AreEqual(8, neighbours[5]);
            Assert.AreEqual(9, neighbours[6]);
            Assert.AreEqual(5, neighbours[7]);
        }

        [TestMethod]
        public void CanDetermineNeighboursForLowerLeftWithOneByteCell()
        {
            var sut = new DeterminesCellNeighbours(5, 5);

            var neighbours = sut.GetNeighbourIndexes(0, 4);

            Assert.AreEqual(19, neighbours[0]);
            Assert.AreEqual(15, neighbours[1]);
            Assert.AreEqual(16, neighbours[2]);
            Assert.AreEqual(24, neighbours[3]);
            Assert.AreEqual(21, neighbours[4]);
            Assert.AreEqual(4, neighbours[5]);
            Assert.AreEqual(0, neighbours[6]);
            Assert.AreEqual(1, neighbours[7]);
        }

        [TestMethod]
        public void CanDetermineNeighboursForLowerRightWithOneByteCell()
        {
            var sut = new DeterminesCellNeighbours(5, 5);

            var neighbours = sut.GetNeighbourIndexes(4, 4);

            Assert.AreEqual(18, neighbours[0]);
            Assert.AreEqual(19, neighbours[1]);
            Assert.AreEqual(15, neighbours[2]);
            Assert.AreEqual(23, neighbours[3]);
            Assert.AreEqual(20, neighbours[4]);
            Assert.AreEqual(3, neighbours[5]);
            Assert.AreEqual(4, neighbours[6]);
            Assert.AreEqual(0, neighbours[7]);
        }
    }
}
