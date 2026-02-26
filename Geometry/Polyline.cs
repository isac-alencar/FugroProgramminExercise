using System;
using System.Collections.Generic;
using System.Linq;

namespace Geometry
{
    /// <summary>
    /// Represents a polyline composed of one or more connected
    /// line segments in a two-dimensional Cartesian plane.
    /// </summary>
    public class Polyline
    {
        private readonly Line[] _lines;
        private readonly double[] _lengths;

        /// <summary>
        /// Initializes a new instance of the <see cref="Polyline"/> class
        /// from a collection of vertices.
        /// </summary>
        /// <param name="vertices">
        /// An ordered collection of points defining the vertices of the polyline.
        /// Each consecutive pair of points forms a line segment.
        /// </param>
        /// <remarks>
        /// If the collection contains fewer than two points, the polyline
        /// will contain no line segments.
        /// </remarks>
        public Polyline(ICollection<Point> vertices)
        {
            int size = vertices.Count;
            if (size > 1)
            {
                _lines = new Line[size - 1];
                _lengths = new double[size - 1];
                double totalLength = 0;
                for (int i = 0; i < size - 1; i++)
                {
                    _lines[i] = new Line(vertices.ElementAt(i), vertices.ElementAt(i + 1));
                    totalLength += _lines[i].GetLength();
                    _lengths[i] = totalLength;
                }
            }
            else
            {
                _lines = Array.Empty<Line>();
                _lengths = Array.Empty<double>();
            }
        }

        /// <summary>
        /// Gets the line segment at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the line segment.</param>
        /// <returns>
        /// The <see cref="Line"/> at the specified index,
        /// or <c>null</c> if the index is out of range.
        /// </returns>
        public Line GetLineAt(int index)
        {
            if (index < 0 || index >= _lines.Length)
            {
                return null;
            }
            return _lines[index];
        }

        /// <summary>
        /// Gets the number of line segments in the polyline.
        /// </summary>
        /// <returns>The number of line segments.</returns>
        public int GetNumberOfLines()
        {
            return _lines.Length;
        }

        /// <summary>
        /// Gets the cumulative length of the polyline
        /// up to and including the line segment at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the line segment.</param>
        /// <returns>
        /// The cumulative length up to the specified segment,
        /// or <c>-1</c> if the index is out of range.
        /// </returns>
        /// <remarks>
        /// This method returns the sum of the lengths of all line segments
        /// from the start of the polyline up to the specified index.
        /// </remarks>
        public double GetCumulativeLengthAt(int index)
        {
            if (index < 0 || index >= _lengths.Length)
            {
                return -1;
            }
            return _lengths[index];
        }
    }
}
