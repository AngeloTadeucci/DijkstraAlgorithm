# This fork adds bi directional links

Check the original reposity here: <https://github.com/agabani/DijkstraAlgorithm>

## Dijkstra Algorithm

> Dijkstra's algorithm is an algorithm for finding the shortest paths between nodes in a graph. [wikipedia](https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm)

## Nuget Packages

| Nuget Package Name | Nuget Package URL                                                  |
|--------------------|--------------------------------------------------------------------|
| DijkstraAlgorithm  | <https://www.nuget.org/packages/DijkstraAlgorithm/> |

## Example Usage

``` csharp
// Create graph
var builder = new GraphBuilder();

builder
    .AddNode("A")
    .AddNode("B")
    .AddNode("C")
    .AddNode("D")
    .AddNode("E");

builder
    .AddBidirectionalLink("A", "B", 6)
    .AddBidirectionalLink("A", "D", 1);

builder
    .AddBidirectionalLink("B", "C", 5)
    .AddBidirectionalLink("B", "D", 2)
    .AddBidirectionalLink("B", "E", 2);

builder
    .AddBidirectionalLink("C", "E", 5);

// Example of uni directional links
builder
    .AddLink("D", "E", 1);

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
```
