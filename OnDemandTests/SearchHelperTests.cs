using Geometry;
using PolylineChallenge;
using System;
using System.Collections.Generic;
using System.IO;

namespace OnDemandTests
{
    [TestClass]
    public sealed class SearchHelperTests
    {
        [TestMethod]
        public void FindOffsetAndStation_OnDemandTest()
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var csvPath = Path.Combine(basePath, "TestFiles", "polyline_sample.csv");

            var vertices = FileHelper.ReadPointsFromCsv(csvPath);
            var polyline = new Polyline(vertices);
            var point = new Point(160, -50);

            var result = SearchHelper.FindOffsetAndStation(polyline, point);

            Assert.IsTrue(result.IsValid);
        }
    }
}
