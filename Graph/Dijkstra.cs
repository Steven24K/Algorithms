public partial class Graph
{

    // Dijkstra's algorithm
    public Tuple<double[], int[]> SingleSourceShortestPath(int source) //distance and prev arrays
    {
        double[] distance = new double[Count];
        int[] prev = new int[Count];
        List<int> unvisitedNodes = new List<int>(Count);
        // initialization of distance, prev and unvisitedNodes
        for (int i = 0; i < Count; i++)
        {
            distance[i] = double.PositiveInfinity;
            prev[i] = -1;
            unvisitedNodes.Add(i);
        }
        // set distance of source
        distance[source] = 0;
        while (unvisitedNodes.Count > 0) // unvisitedNodes is not empty
        {
            int firstUnvisited = unvisitedNodes.First();
            double min = distance[firstUnvisited];
            int minIndex = firstUnvisited;
            for (int i = 0; i < unvisitedNodes.Count; i++)
            { // find closest node in unvisitedNodes
                if (distance[unvisitedNodes[i]] < min)
                {
                    minIndex = unvisitedNodes[i];
                    min = distance[unvisitedNodes[i]];
                }
            }
            // remove the closest node from unvisitedNodes
            unvisitedNodes.Remove(minIndex);
            List<int> neighbors = Neighbors(minIndex);

            for (int i = 0; i < neighbors.Count; i++)
            {
                if (unvisitedNodes.Contains(neighbors[i]))
                { // calculate distance and update if smaller
                    double alt = distance[minIndex] + AdjacencyMatrix[minIndex, neighbors[i]];
                    if (alt < distance[neighbors[i]])
                    {
                        distance[neighbors[i]] = alt;
                        prev[neighbors[i]] = minIndex;
                    }
                }
            }
        }

        return new Tuple<double[], int[]>(distance, prev);
    }

}

public class DebugDijkstra
{
    public static void Debug()
    {
        var inf = double.PositiveInfinity;
        var g = new Graph(
            new double[,]
            {
        { inf, 3, 3, inf },
        { inf, inf, -2, 2 },
        { 4, inf, inf, inf },
        { inf, inf, 5, inf },
            }
            );
        var path = g.SingleSourceShortestPath(0);
        Console.WriteLine($"Dijkstra path {string.Join("-->", path.Item1)}");
    }
}
