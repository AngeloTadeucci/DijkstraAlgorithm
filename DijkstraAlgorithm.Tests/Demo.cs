using System.Linq;
using DijkstraAlgorithm.Graphing;
using DijkstraAlgorithm.Pathing;
using Xunit;

namespace DijkstraAlgorithm.Tests
{
    public class Demo
    {
        [Fact]
        public void Example()
        {
            // Create graph
            var builder = new GraphBuilder();

            builder
                .AddNode("A")
                .AddNode("B")
                .AddNode("C")
                .AddNode("D")
                .AddNode("E");

            builder
                .AddLink("A", "B", 6)
                .AddLink("A", "D", 1);

            builder
                .AddLink("B", "A", 6)
                .AddLink("B", "C", 5)
                .AddLink("B", "D", 2)
                .AddLink("B", "E", 2);

            builder
                .AddLink("C", "B", 5)
                .AddLink("C", "E", 5);

            builder
                .AddLink("D", "A", 1)
                .AddLink("D", "B", 2)
                .AddLink("D", "E", 1);

            builder
                .AddLink("E", "B", 2)
                .AddLink("E", "C", 5)
                .AddLink("E", "D", 1);

            var graph = builder.Build();

            // Create path finder
            var pathFinder = new PathFinder(graph);

            // Find path
            const string origin = "A", destination = "C";

            var path = pathFinder.FindShortestPath(
                graph.Nodes.Single(node => node.Id == origin),
                graph.Nodes.Single(node => node.Id == destination));

            // Assert results
            Assert.Equal(origin, path.Origin.Id);
            Assert.Equal(destination, path.Destination.Id);
            Assert.Equal(3, path.Segments.Count);
            Assert.Equal(7, path.Segments.Sum(s => s.Weight));

            Assert.Equal("A", path.Segments.ElementAt(0).Origin.Id);
            Assert.Equal(1, path.Segments.ElementAt(0).Weight);
            Assert.Equal("D", path.Segments.ElementAt(0).Destination.Id);

            Assert.Equal("D", path.Segments.ElementAt(1).Origin.Id);
            Assert.Equal(1, path.Segments.ElementAt(1).Weight);
            Assert.Equal("E", path.Segments.ElementAt(1).Destination.Id);

            Assert.Equal("E", path.Segments.ElementAt(2).Origin.Id);
            Assert.Equal(5, path.Segments.ElementAt(2).Weight);
            Assert.Equal("C", path.Segments.ElementAt(2).Destination.Id);
        }
    }
}