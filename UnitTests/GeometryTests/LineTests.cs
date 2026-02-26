using Geometry;
using System;

namespace UnitTests.GeometryTests
{
    [TestClass]
    public class LineTests
    {
        private const double delta = 1e-9;

        [TestMethod]
        public void Constructor_ShouldInitializePointsAndLength_Test()
        {
            // Arrange
            var start = new Point(0, 0);
            var end = new Point(3, 4);

            // Act
            var line = new Line(start, end);

            // Assert
            Assert.AreEqual(start, line.StartPoint);
            Assert.AreEqual(end, line.EndPoint);
            Assert.AreEqual(5.0, line.GetLength(), delta);
        }

        [TestMethod]
        public void GetLength_ShouldReturnCorrectLength_Test()
        {
            // Arrange
            var line = new Line(new Point(1, 1), new Point(4, 5));

            // Act and Assert
            Assert.AreEqual(5.0, line.GetLength(), delta);
        }

        [TestMethod]
        public void ContainsPointProjection_ProjectionInsideSegment_Test()
        {
            // Arrange
            var line = new Line(new Point(0, 0), new Point(10, 0));
            var p = new Point(5, 3);

            // Act and Assert
            Assert.IsTrue(line.ContainsPointProjection(p));
        }

        [TestMethod]
        public void ContainsPointProjection_ProjectionAtStartPoint_Test()
        {
            // Arrange
            var line = new Line(new Point(0, 0), new Point(10, 0));
            var p = new Point(0, 5);

            // Act and Assert
            Assert.IsTrue(line.ContainsPointProjection(p));
        }

        [TestMethod]
        public void ContainsPointProjection_ProjectionAtEndPoint_Test()
        {
            // Arrange
            var line = new Line(new Point(0, 0), new Point(10, 0));
            var p = new Point(10, -2);

            // Act and Assert
            Assert.IsTrue(line.ContainsPointProjection(p));
        }

        [TestMethod]
        public void ContainsPointProjection_ProjectionOutsideSegment_Test()
        {
            // Arrange
            var line = new Line(new Point(0, 0), new Point(10, 0));
            var p = new Point(15, 4);

            // Act and Assert
            Assert.IsFalse(line.ContainsPointProjection(p));
        }

        [TestMethod]
        public void ContainsPointProjection_DegenerateSegment_Test()
        {
            // Arrange
            var line = new Line(new Point(2, 2), new Point(2, 2));
            var p = new Point(3, 3);

            // Act and Assert
            Assert.IsFalse(line.ContainsPointProjection(p));
        }

        [TestMethod]
        public void GetSquareDistanceToBoundBox_PointInsideBoundingBox_Test()
        {
            // Arrange
            var line = new Line(new Point(0, 0), new Point(10, 10));
            var p = new Point(5, 3);

            // Act
            double distance = line.GetSquareDistanceToBoundBox(p);

            // Assert
            Assert.AreEqual(0.0, distance, delta);
        }

        [TestMethod]
        public void GetSquareDistanceToBoundBox_PointOutsideBoundingBoxOnX_Test()
        {
            // Arrange
            var line = new Line(new Point(0, 0), new Point(10, 10));
            var p = new Point(15, 5);

            // Act
            double distance = line.GetSquareDistanceToBoundBox(p);

            // Assert
            Assert.AreEqual(25.0, distance, delta);
        }

        [TestMethod]
        public void GetSquareDistanceToBoundBox_PointOutsideBoundingBoxOnDiagonal_Test()
        {
            // Arrange
            var line = new Line(new Point(0, 0), new Point(10, 10));
            var p = new Point(15, 15);

            // Act
            double distance = line.GetSquareDistanceToBoundBox(p);

            // Assert
            Assert.AreEqual(50.0, distance, delta);
        }

        [TestMethod]
        public void GetDistanceToPoint_PointOnLine_Test()
        {
            // Arrange
            var line = new Line(new Point(0, 0), new Point(10, 0));
            var p = new Point(5, 0);

            // Act
            double distance = line.GetDistanceToPoint(p);

            // Assert
            Assert.AreEqual(0.0, distance, delta);
        }

        [TestMethod]
        public void GetDistanceToPoint_PointAboveHorizontalLine_Test()
        {
            // Arrange
            var line = new Line(new Point(0, 0), new Point(10, 0));
            var p = new Point(5, 3);

            // Act
            double distance = line.GetDistanceToPoint(p);

            // Assert
            Assert.AreEqual(3.0, distance, delta);
        }

        [TestMethod]
        public void GetDistanceToPoint_PointToDiagonalLine_Test()
        {
            // Arrange
            var line = new Line(new Point(0, 0), new Point(10, 10));
            var p = new Point(10, 0);
            double expected = 10 / Math.Sqrt(2);

            // Act
            double distance = line.GetDistanceToPoint(p);

            // Assert
            Assert.AreEqual(expected, distance, delta);
        }
    }
}