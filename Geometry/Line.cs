using System;

namespace Geometry
{
    /// <summary>
    /// Represents a line segment defined by two points in a
    /// two-dimensional Cartesian plane.
    /// </summary>
    public class Line
    {
        /// <summary>
        /// Represents an axis-aligned bounding box that fully contains
        /// the line segment.
        /// </summary>
        private struct BoundingBox
        {
            public int xMin;
            public int xMax;
            public int yMin;
            public int yMax;

            /// <summary>
            /// Initializes a new bounding box that encloses two points.
            /// </summary>
            /// <param name="p1">The first point.</param>
            /// <param name="p2">The second point.</param>
            public BoundingBox(Point p1, Point p2)
            {
                xMin = Math.Min(p1.X, p2.X);
                xMax = Math.Max(p1.X, p2.X);
                yMin = Math.Min(p1.Y, p2.Y);
                yMax = Math.Max(p1.Y, p2.Y);
            }

            /// <summary>
            /// Determines whether the specified point lies inside
            /// the bounding box (inclusive).
            /// </summary>
            /// <param name="p">The point to test.</param>
            /// <returns>
            /// <c>true</c> if the point is inside the bounding box;
            /// otherwise, <c>false</c>.
            /// </returns>
            public bool Contains(Point p)
            {
                return (p.X >= xMin && p.X <= xMax) && (p.Y >= yMin && p.Y <= yMax);
            }

            /// <summary>
            /// Computes the squared distance from a point to the bounding box.
            /// </summary>
            /// <param name="p">The point from which the distance is calculated.</param>
            /// <returns>
            /// The squared distance from the point to the bounding box.
            /// Returns <c>0</c> if the point lies inside the bounding box.
            /// </returns>
            public int GetSquareDistance(Point p)
            {
                int dx = Math.Max(0, Math.Max(xMin - p.X, p.X - xMax));
                int dy = Math.Max(0, Math.Max(yMin - p.Y, p.Y - yMax));
                return dx * dx + dy * dy;
            }
        }

        private readonly double _length = -1;
        private BoundingBox _boundingBox;

        /// <summary>
        /// Gets the starting point of the line segment.
        /// </summary>
        public Point StartPoint { get; private set; }

        /// <summary>
        /// Gets the ending point of the line segment.
        /// </summary>
        public Point EndPoint { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Line"/> class
        /// using the specified start and end points.
        /// </summary>
        /// <param name="startPoint">The starting point of the line segment.</param>
        /// <param name="endPoint">The ending point of the line segment.</param>
        public Line(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;

            _length = StartPoint.GetDistanceToPoint(EndPoint);
            _boundingBox = new BoundingBox(StartPoint, EndPoint);
        }

        /// <summary>
        /// Gets the length of the line segment.
        /// </summary>
        /// <returns>The Euclidean length of the line segment.</returns>
        public double GetLength()
        {
            return _length;
        }

        /// <summary>
        /// Determines whether the orthogonal projection of a point
        /// onto the infinite line lies within the bounds of the line segment.
        /// </summary>
        /// <param name="p">The point whose projection is tested.</param>
        /// <returns>
        /// <c>true</c> if the projected point lies between the start and end
        /// points of the segment; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsPointProjection(Point p)
        {
            int abx = EndPoint.X - StartPoint.X;
            int aby = EndPoint.Y - StartPoint.Y;
            int apx = p.X - StartPoint.X;
            int apy = p.Y - StartPoint.Y;

            double lengthSquared = abx * abx + aby * aby;

            if (lengthSquared == 0)
            {
                return false;
            }

            double t = (apx * abx + apy * aby) / lengthSquared;

            return t >= 0.0 && t <= 1.0;
        }

        /// <summary>
        /// Computes the squared distance from a point to the line segment's
        /// bounding box.
        /// </summary>
        /// <param name="p">The point from which the distance is calculated.</param>
        /// <returns>
        /// <c>0</c> if the point lies inside the bounding box; otherwise,
        /// the squared distance to the nearest edge of the bounding box.
        /// </returns>
        public double GetSquareDistanceToBoundBox(Point p)
        {
            if (_boundingBox.Contains(p))
            {
                return 0;
            }

            return _boundingBox.GetSquareDistance(p);
        }

        /// <summary>
        /// Calculates the perpendicular distance from a point
        /// to the infinite line defined by the line segment.
        /// </summary>
        /// <param name="p">The point from which the distance is measured.</param>
        /// <returns>
        /// The perpendicular distance from the point to the line.
        /// </returns>
        public double GetDistanceToPoint(Point p)
        {
            // Line coefficients Ax + By + C = 0
            int A = EndPoint.Y - StartPoint.Y;
            int B = StartPoint.X - EndPoint.X;
            int C = EndPoint.X * StartPoint.Y - StartPoint.X * EndPoint.Y;

            return Math.Abs(A * p.X + B * p.Y + C) / Math.Sqrt(A * A + B * B);
        }
    }
}
