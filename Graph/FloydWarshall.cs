public class FloydWarshall
{
    public static double inf = double.PositiveInfinity;
    public double[,] AdjacencyMatrix { get; set; }
    public int Count { get { return AdjacencyMatrix.GetLength(0); } }

    public FloydWarshall(double[,] matrix)
    {
        if (matrix.GetLength(0) != matrix.GetLength(1))
            throw new System.ArgumentException("The adjacency matrix must be a square matrix");
        AdjacencyMatrix = matrix;
    }

    public Tuple<double[,], int[,]> AllPairShortestPath()
    {
        var numVertices = Count;
        double[,] dist = new double[numVertices, numVertices]; // matrix of minimum distances
        int[,] next = new int[numVertices, numVertices]; // matrix of vertex indices

        // initialization of dist and next
        for (int i = 0; i < numVertices; i++)
            for (int j = 0; j < numVertices; j++)
            {
                if (i == j)
                    dist[i, j] = 0;
                else
                    dist[i, j] = AdjacencyMatrix[i, j];

                if (AdjacencyMatrix[i, j] != inf)
                    next[i, j] = j;
                else
                    next[i, j] = -1;
            }

        for (int k = 0; k < numVertices; k++)
            for (int i = 0; i < numVertices; i++)
                for (int j = 0; j < numVertices; j++)
                {
                    if (dist[i, j] > dist[i, k] + dist[k, j])
                    {
                        dist[i, j] = dist[i, k] + dist[k, j];
                        next[i, j] = next[i, k];
                    }
                }

        return new Tuple<double[,], int[,]>(dist, next);
    }
}

public class DebugFloydWarshall
{
    public static void Debug()
    {
        var inf = FloydWarshall.inf;
        var g = new FloydWarshall(
          new double[,]
          {
          { inf, 3, 3, inf },
          { inf, inf, -2, 2 },
          { 4, inf, inf, inf },
          { inf, inf, 5, inf },
          }
          );
        var allPaths = g.AllPairShortestPath();
    }
}