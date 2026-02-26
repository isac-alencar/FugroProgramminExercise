using Geometry;
using System.Collections.Generic;

namespace UnitTests.GeometryTests
{
    [TestClass]
    public class PolylineTests
    {
        private const double delta = 1e-9;

        [TestMethod]
        public void Constructor_WithLessThanTwoPoints_Test()
        {
            // Arrange
            var vertices = new List<Point>
            {
                new Point(0, 0)
            };

            // Act
            var polyline = new Polyline(vertices);

            // Assert
            Assert.AreEqual(0, polyline.GetNumberOfLines());
        }

        [TestMethod]
        public void Constructor_WithTwoPoints_Test()
        {
            // Arrange
            var vertices = new List<Point>
            {
                new Point(0, 0),
                new Point(3, 4)
            };

            // Act
            var polyline = new Polyline(vertices);

            // Assert
            Assert.AreEqual(1, polyline.GetNumberOfLines());
        }

        [TestMethod]
        public void Constructor_WithMultiplePoints_Test()
        {
            // Arrange
            var vertices = new List<Point>
            {
                new Point(0, 0),
                new Point(3, 4),
                new Point(6, 4),
                new Point(6, 0)
            };

            // Act
            var polyline = new Polyline(vertices);

            // Assert
            Assert.AreEqual(3, polyline.GetNumberOfLines());
        }

        [TestMethod]
        public void GetLineAt_ValidIndex_Test()
        {
            // Arrange
            var vertices = new List<Point>
            {
                new Point(0, 0),
                new Point(3, 4),
                new Point(6, 4)
            };
            var polyline = new Polyline(vertices);

            // Act
            Line line = polyline.GetLineAt(1);

            // Assert
            Assert.IsNotNull(line);
            Assert.AreEqual(vertices[1], line.StartPoint);
            Assert.AreEqual(vertices[2], line.EndPoint);
        }

        [TestMethod]
        public void GetLineAt_InvalidIndex_Test()
        {
            var vertices = new List<Point>
            {
                new Point(0, 0),
                new Point(1, 1)
            };
            var polyline = new Polyline(vertices);

            Assert.IsNull(polyline.GetLineAt(-1));
            Assert.IsNull(polyline.GetLineAt(1));
            Assert.IsNull(polyline.GetLineAt(100));
        }

        [TestMethod]
        public void GetCumulativeLengthAt_ValidIndex_Test()
        {
            // Arrange
            var vertices = new List<Point>
            {
                new Point(0, 0),
                new Point(3, 4),
                new Point(6, 4)
            };
            var polyline = new Polyline(vertices);

            // Act
            double length0 = polyline.GetCumulativeLengthAt(0);
            double length1 = polyline.GetCumulativeLengthAt(1);

            // Assert
            Assert.AreEqual(5.0, length0, delta);
            Assert.AreEqual(8.0, length1, delta);
        }

        [TestMethod]
        public void GetCumulativeLengthAt_InvalidIndex_Test()
        {
            var vertices = new List<Point>
            {
                new Point(0, 0),
                new Point(3, 4)
            };
            var polyline = new Polyline(vertices);

            Assert.AreEqual(-1, polyline.GetCumulativeLengthAt(-1));
            Assert.AreEqual(-1, polyline.GetCumulativeLengthAt(1));
            Assert.AreEqual(-1, polyline.GetCumulativeLengthAt(100));
        }

        [TestMethod]
        public void Polyline_LengthsShouldMatchSumOfLineLengths_Test()
        {
            // Arrange
            var vertices = new List<Point>
            {
                new Point(0, 0),
                new Point(0, 3),
                new Point(4, 3)
            };

            var polyline = new Polyline(vertices);

            // Assert
            Assert.AreEqual(2, polyline.GetNumberOfLines());
            Assert.AreEqual(3.0, polyline.GetCumulativeLengthAt(0), delta);
            Assert.AreEqual(7.0, polyline.GetCumulativeLengthAt(1), delta);
        }
    }
}