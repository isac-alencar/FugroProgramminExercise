using Geometry;
using PolylineChallenge;
using System.Collections.Generic;

namespace UnitTests.PolylineChallengeTests
{
    [TestClass]
    public class SearchHelperTests
    {
        private const double delta = 1e-9;

        [TestMethod]
        public void FindOffsetAndStation_PointWithNoValidProjection_Test()
        {
            // Arrange
            var polyline = new Polyline(new List<Point>
            {
                new Point(0, 0),
                new Point(10, 0)
            });

            var p = new Point(15, 5);

            // Act
            var result = SearchHelper.FindOffsetAndStation(polyline, p);

            // Assert
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void FindOffsetAndStation_PointAboveSingleSegment_Test()
        {
            // Arrange
            var polyline = new Polyline(new List<Point>
            {
                new Point(0, 0),
                new Point(10, 0)
            });

            var p = new Point(4, 3);

            // Act
            var result = SearchHelper.FindOffsetAndStation(polyline, p);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(3.0, result.Offset, delta);
            Assert.AreEqual(4.0, result.Station, delta);
        }

        [TestMethod]
        public void FindOffsetAndStation_PointOnSingleSegment_Test()
        {
            // Arrange
            var polyline = new Polyline(new List<Point>
            {
                new Point(0, 0),
                new Point(10, 0)
            });

            var p = new Point(6, 0);

            // Act
            var result = SearchHelper.FindOffsetAndStation(polyline, p);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(0.0, result.Offset, delta);
            Assert.AreEqual(6.0, result.Station, delta);
        }

        [TestMethod]
        public void FindOffsetAndStation_PointClosestToSecondSegment_Test()
        {
            // Arrange
            var polyline = new Polyline(new List<Point>
            {
                new Point(0, 0),
                new Point(5, 0),
                new Point(5, 5)
            });

            var p = new Point(7, 3);

            // Act
            var result = SearchHelper.FindOffsetAndStation(polyline, p);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(2.0, result.Offset, delta);
            Assert.AreEqual(8.0, result.Station, delta);
        }

        [TestMethod]
        public void FindOffsetAndStation_PointEquidistantToTwoSegments_Test()
        {
            // Arrange
            var polyline = new Polyline(new List<Point>
            {
                new Point(0, 0),
                new Point(10, 0),
                new Point(10, 10)
            });

            var p = new Point(8, 2);

            // Act
            var result = SearchHelper.FindOffsetAndStation(polyline, p);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(2.0, result.Offset, delta);
            Assert.AreEqual(8.0, result.Station, delta);
        }

        [TestMethod]
        public void FindOffsetAndStation_ProjectionAtSegmentStart_Test()
        {
            // Arrange
            var polyline = new Polyline(new List<Point>
            {
                new Point(0, 0),
                new Point(10, 0)
            });

            var p = new Point(0, 4);

            // Act
            var result = SearchHelper.FindOffsetAndStation(polyline, p);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(4.0, result.Offset, delta);
            Assert.AreEqual(0.0, result.Station, delta);
        }

        [TestMethod]
        public void FindOffsetAndStation_ProjectionAtSegmentEnd_ShouldReturnFullSegmentLength()
        {
            // Arrange
            var polyline = new Polyline(new List<Point>
            {
                new Point(0, 0),
                new Point(10, 0)
            });

            var p = new Point(10, -3);

            // Act
            var result = SearchHelper.FindOffsetAndStation(polyline, p);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(3.0, result.Offset, delta);
            Assert.AreEqual(10.0, result.Station, delta);
        }
    }
}