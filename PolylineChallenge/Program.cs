using Geometry;
using System;
using System.Collections.Generic;

namespace PolylineChallenge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo cki;

            Console.WriteLine("Enter CSV file path containing the polyline vertices:");
            string filePath = Console.ReadLine();
            
            ICollection<Point> vertices = FileHelper.ReadPointsFromCsv(filePath);

            if (vertices.Count == 0)
            {
                Console.WriteLine("Error: can't find any point in the CSV file.");
                return;
            }

            Polyline polyline = new Polyline(vertices);

            do
            {
                Console.WriteLine("Enter the point P coordinates (x,y) separated by a comma:");
                string[] coordinates = Console.ReadLine().Split(',');

                int x = Convert.ToInt32(coordinates[0]);
                int y = Convert.ToInt32(coordinates[1]);
                Point p = new Point(x, y);

                SearchHelper.SearchResult result = SearchHelper.FindOffsetAndStation(polyline, p);
                if (result.IsValid)
                {
                    Console.WriteLine($"Result: Offset = {result.Offset:0.0000}, Station = {result.Station:0.0000}");
                }
                else
                {
                    Console.WriteLine("Can't find Offset and Station");
                }

                Console.WriteLine("Press the Escape (Esc) key to quit, or any other key to continue\n");
                cki = Console.ReadKey(true);
            } while (cki.Key != ConsoleKey.Escape);
        }
    }
}
