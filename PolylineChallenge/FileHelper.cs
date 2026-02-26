using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Geometry;

namespace PolylineChallenge
{
    /// <summary>
    /// Provides helper methods for reading geometry-related data
    /// from external files.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Reads a collection of <see cref="Point"/> objects from a CSV file.
        /// </summary>
        /// <param name="filePath">
        /// The full path to the CSV file containing point coordinates.
        /// Each line is expected to contain two integer values
        /// representing the X and Y coordinates, separated by a comma.
        /// </param>
        /// <returns>
        /// A collection of <see cref="Point"/> instances parsed from the file.
        /// If the file cannot be read or contains no valid entries,
        /// an empty collection is returned.
        /// </returns>
        /// <remarks>
        /// Lines that are empty, improperly formatted, or contain non-numeric
        /// values are skipped. Informational warnings are written to the
        /// console for each skipped line.
        ///
        /// The method is resilient to common file-related errors and does not
        /// throw exceptions to the caller; instead, it logs errors and returns
        /// the points successfully parsed up to the failure point.
        /// </remarks>
        public static ICollection<Point> ReadPointsFromCsv(string filePath)
        {
            ICollection<Point> points = new List<Point>();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Error: CSV file path is null or empty.");
                return points;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: CSV file not found -> {filePath}");
                return points;
            }

            try
            {
                using var reader = new StreamReader(filePath);
                int lineNumber = 0;

                while (!reader.EndOfStream)
                {
                    lineNumber++;
                    string? line = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        Console.WriteLine($"Warning: line {lineNumber} is empty and was skipped.");
                        continue;
                    }

                    string[] values = line.Split(',');

                    if (values.Length < 2)
                    {
                        Console.WriteLine(
                            $"Warning: invalid format at line {lineNumber}. Expected 2 values, got {values.Length}. Line content: \"{line}\"");
                        continue;
                    }

                    if (!int.TryParse(values[0].Trim(), NumberStyles.Integer,
                                      CultureInfo.InvariantCulture, out int x) ||
                        !int.TryParse(values[1].Trim(), NumberStyles.Integer,
                                      CultureInfo.InvariantCulture, out int y))
                    {
                        Console.WriteLine(
                            $"Warning: non-numeric values at line {lineNumber}. Line content: \"{line}\"");
                        continue;
                    }

                    points.Add(new Point(x, y));
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Error: access to the CSV file was denied.");
                Console.WriteLine(ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error: I/O error occurred while reading the CSV file.");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: unexpected error occurred while processing the CSV file.");
                Console.WriteLine(ex.Message);
            }

            return points;
        }
    }
}
