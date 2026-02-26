using Geometry;

namespace UnitTests.GeometryTests
{
    [TestClass]
    public sealed class PointTests
    {
        private const double delta = 0.0001;

        [TestMethod]
        public void Constructor_ShouldInitializeCoordinatesCorrectly_Test()
        {
            // Arrange
            const int x = -1;
            const int y = 10;

            // Act
            var point = new Point(x, y);

            // Assert
            Assert.AreEqual(x, point.X);
            Assert.AreEqual(y, point.Y);
        }

        [TestMethod]
        public void GetDistanceToPoint_WhenPointsAreTheSame_Test()
        {
            // Arrange
            Point p = new Point(1, 1);

            // Act
            double d = p.GetDistanceToPoint(p);

            // Assert
            Assert.AreEqual(0.0, d, delta);
        }

        [TestMethod]
        public void GetDistanceToPoint_HorizontalDistance_Test()
        {
            // Arrange
            var p1 = new Point(1, 1);
            var p2 = new Point(10, 1);

            // Act
            double distance = p1.GetDistanceToPoint(p2);

            // Assert
            Assert.AreEqual(p2.X - p1.X, distance, 0.0001);
        }

        [TestMethod]
        public void GetDistanceToPoint_VerticalDistance_Test()
        {
            // Arrange
            var p1 = new Point(5, 10);
            var p2 = new Point(5, 15);

            // Act
            double distance = p1.GetDistanceToPoint(p2);

            // Assert
            Assert.AreEqual(p2.Y - p1.Y, distance, 0.0001);
        }

        [TestMethod]
        public void GetDistanceToPoint_DiagonalDistance_Test()
        {
            // Arrange
            Point p1 = new Point(1, 1);
            Point p2 = new Point(5, 4);

            // Act
            double d = p1.GetDistanceToPoint(p2);

            // Assert
            Assert.AreEqual(5.0, d, delta);
        }
    }
}
