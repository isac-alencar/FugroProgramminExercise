using Geometry;
using System;

namespace PolylineChallenge
{
    /// <summary>
    /// Provides helper methods for searching spatial relationships
    /// between a point and a polyline.
    /// </summary>
    public static class SearchHelper
    {
        /// <summary>
        /// Represents the result of a spatial search operation
        /// against a polyline.
        /// </summary>
        public struct SearchResult
        {
            /// <summary>
            /// Gets or sets the perpendicular distance from the point
            /// to the closest line segment of the polyline.
            /// </summary>
            public double Offset;

            /// <summary>
            /// Gets or sets the station value, representing the cumulative
            /// distance along the polyline to the orthogonal projection
            /// of the point.
            /// </summary>
            public double Station;

            /// <summary>
            /// Gets or sets a value indicating whether the search
            /// produced a valid result.
            /// </summary>
            public bool IsValid;
        }

        /// <summary>
        /// Finds the offset and station of a point relative to a polyline.
        /// </summary>
        /// <param name="polyline">
        /// The polyline used as the reference geometry.
        /// </param>
        /// <param name="p">
        /// The point for which the offset and station are calculated.
        /// </param>
        /// <returns>
        /// A <see cref="SearchResult"/> containing the offset and station
        /// if a valid projection is found; otherwise, a result with
        /// <see cref="SearchResult.IsValid"/> set to <c>false</c>.
        /// </returns>
        /// <remarks>
        /// The algorithm iterates over each line segment of the polyline
        /// and considers only those segments where the orthogonal
        /// projection of the point lies within the segment bounds.
        ///
        /// A bounding box squared-distance check is used as a fast
        /// pre-filter to reduce unnecessary distance calculations.
        /// The final offset is computed as the perpendicular distance
        /// to the closest valid line segment.
        ///
        /// The station value represents the cumulative length of the
        /// polyline up to the projected point.
        /// </remarks>
        public static SearchResult FindOffsetAndStation(Polyline polyline, Point p)
        {
            double minDist = double.PositiveInfinity;
            double minSquareDist = double.PositiveInfinity;
            int bestLineIndex = -1;
            bool isMinDistCalculationPending = false;

            for (int i = 0; i < polyline.GetNumberOfLines(); i++)
            {
                Line currentLine = polyline.GetLineAt(i);
                if (!currentLine.ContainsPointProjection(p)) 
                {
                    continue;
                }

                double currentSquareDistance = currentLine.GetSquareDistanceToBoundBox(p);
                if (currentSquareDistance > minSquareDist)
                {
                    continue;
                }

                if (currentSquareDistance < minSquareDist)
                {
                    minSquareDist = currentSquareDistance;
                    bestLineIndex = i;
                    isMinDistCalculationPending = true;
                    continue;
                }

                if (currentSquareDistance == 0)
                {                    
                    if (isMinDistCalculationPending)
                    {
                        minDist = polyline.GetLineAt(bestLineIndex).GetDistanceToPoint(p);
                        isMinDistCalculationPending = false;
                    }

                    double currentMinDist = currentLine.GetDistanceToPoint(p);
                    if (currentMinDist < minDist)
                    {
                        minSquareDist = 0;
                        minDist = currentMinDist;
                        bestLineIndex = i;
                    }
                }
            }

            return CreateResultFromSearchOutputs(polyline.GetLineAt(bestLineIndex), minDist, isMinDistCalculationPending, polyline.GetCumulativeLengthAt(bestLineIndex - 1), p);
        }

        /// <summary>
        /// Creates a <see cref="SearchResult"/> from the intermediate
        /// outputs of the search process.
        /// </summary>
        /// <param name="bestLine">
        /// The line segment identified as the closest valid segment.
        /// </param>
        /// <param name="minDist">
        /// The minimum perpendicular distance from the point to the line.
        /// </param>
        /// <param name="isMinDistCalculationPending">
        /// Indicates whether the minimum distance still needs to be computed.
        /// </param>
        /// <param name="length">
        /// The cumulative length of the polyline up to the start of
        /// the selected line segment.
        /// </param>
        /// <param name="p">
        /// The point being evaluated.
        /// </param>
        /// <returns>
        /// A populated <see cref="SearchResult"/> instance.
        /// </returns>
        private static SearchResult CreateResultFromSearchOutputs(Line bestLine, double minDist, bool isMinDistCalculationPending, double length, Point p)
        {
            SearchResult result = new SearchResult();

            if (bestLine is null)
            {
                result.IsValid = false;
                return result;
            }

            if (isMinDistCalculationPending)
            {
                minDist = bestLine.GetDistanceToPoint(p);
            }

            double distancePointToLineOrigin = p.GetDistanceToPoint(bestLine.StartPoint);
            double lastStationLength = Math.Sqrt(distancePointToLineOrigin * distancePointToLineOrigin - minDist * minDist);

            result.IsValid = true;
            result.Offset = minDist;
            result.Station = length > 0 ? length + lastStationLength : lastStationLength;

            return result;
        }
    }
}
