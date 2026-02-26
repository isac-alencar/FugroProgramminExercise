using System;

namespace Geometry
{
    /// <summary>
    /// Represents a point in a two-dimensional Cartesian plane.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Gets the X coordinate of the point.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets the Y coordinate of the point.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class
        /// with the specified coordinates.
        /// </summary>
        /// <param name="xCoord">The X coordinate of the point.</param>
        /// <param name="yCoord">The Y coordinate of the point.</param>
        public Point(int xCoord, int yCoord)
        {
            X = xCoord;
            Y = yCoord;
        }

        /// <summary>
        /// Calculates the Euclidean distance between this point
        /// and another specified point.
        /// </summary>
        /// <param name="p">The target point used to calculate the distance.</param>
        /// <returns>
        /// The distance between the two points, computed using
        /// the Euclidean distance formula.
        /// </returns>
        public double GetDistanceToPoint(Point p)
        {
            int dx = p.X - X;
            int dy = p.Y - Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
