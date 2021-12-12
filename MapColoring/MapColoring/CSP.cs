using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapColoring
{
    public class CSP
    {
        // Number of vertices in the graph
        int V = 0;

        #region allMaps

        bool[,] pakistanGraph = {
                { false, true, true, false, false },    // prov0
                { true, false, true, true, false },     // prov1
                { true, true, false, true, true },      // prov2
                { false, true, true, false, true },     // prov3
                { false, false, true, true, false },    // prov4
            };

        bool[,] saGraph = {
                { false, true, true, true, false, false, false, false, false },     // prov0
                { true, false, true, false, true, true, false, false, false },      // prov1
                { true, true, false, true, false, true, false, false, false },      // prov2
                { true, false, true, false, false, false, true, false, false },     // prov3
                { false, true, false, false, false, true, false, true, false },     // prov4
                { false, true, true, false, true, false, true, false, true },       // prov5
                { false, false, false, true, false, true, false, false, true },     // prov6
                { false, false, false, false, true, false, false, false, true },    // prov7
                { false, false, false, false, false, true, true, true, false },     // prov8
            };

        bool[,] ausGraph = {
                { false, true, false, true, false, false, false },      // prov0
                { true, false, true, true, false, false, false },       // prov1
                { false, true, false, true, true, false, false },       // prov2
                { true, true, true, false, true, true, false },         // prov3
                { false, false, true, true, false, true, false },       // prov4
                { false, false, false, true, true, false, false },      // prov5
                { false, false, false, false, false, false, false },    // prov6
            };

        bool[,] canadaGraph = {
                { false, true, false, true, false, false, false, false, false, false, false, false },       // prov0
                { true, false, true, true, true, true, false, false, false, false, false, false },          // prov1
                { false, true, false, false, false, false, true, false, false, false, false, false },       // prov2
                { true, true, false, false, true, false, false, false, false, false, false, false },        // prov3
                { false, true, false, true, false, true, false, false, false, false, false, false },        // prov4
                { false, true, false, false, true, false, true, false, false, false, false, false },        // prov5
                { false, false, true, false, false, true, false, true, false, false, false, false },        // prov6
                { false, false, false, false, false, false, true, false, true, false, false, false },       // prov7
                { false, false, false, false, false, false, false, true, false, true, true, false },        // prov8
                { false, false, false, false, false, false, false, false, true, false, false, true },       // prov9
                { false, false, false, false, false, false, false, false, true, false, false, false },      // prov10
                { false, false, false, false, false, false, false, false, false, true, false, false },     // prov11
            };


        #endregion


        /* A utility function to return color solution */
        string GetSolution(int[] color)
        {
            string str = "";

            if (color != null && color.Length > 0)
                str = color[0].ToString();

            for (int i = 1; i < color.Length; i++)
                str += "," + color[i];

            return str;
        }

        // check if the colored
        // graph is safe or not
        bool IsSafe(bool[,] graph, int[] color)
        {
            // check for every edge
            for (int i = 0; i < V; i++)
                for (int j = i + 1; j < V; j++)
                    if (graph[i, j] && color[j] == color[i])
                        return false;
            return true;
        }

        // a utility method to be called by the api
        // also sets the graph selected by the user
        public Result GetResult(string mapId, int colorsCount)
        {
            Result result = new Result();

            bool[,] graph;

            // set graph based on user selection
            switch (mapId)
            {
                case "1":
                    graph = pakistanGraph;
                    break;
                case "2":
                    graph = saGraph;
                    break;
                case "3":
                    graph = ausGraph;
                    break;
                case "4":
                    graph = canadaGraph;
                    break;
                default:
                    graph = pakistanGraph;
                    break;
            }

            V = graph.GetLength(0);

            int m = colorsCount; // Number of colors

            // Initialize all color values as 0.
            // This initialization is needed
            // correct functioning of isSafe()
            int[] color = new int[V];
            for (int i = 0; i < V; i++)
                color[i] = 0;

            if (!GraphColoring(graph, m, 0, color))
            {
                result.result = false;
            }
            else
            {
                result.result = true;
                result.colors = GetSolution(color);
            }

            return result;
        }



        /* This function solves the map Coloring
		problem using recursion. It returns
		false if the m colours cannot be assigned,
		otherwise, return true and prints
		assignments of colours to all vertices.
		Please note that there may be more than
		one solutions, this function prints one
		of the feasible solutions.*/
        bool GraphColoring(bool[,] graph, int m,
                                int i, int[] color)
        {
            // if current index reached end
            if (i == V)
            {

                // if coloring is safe
                if (IsSafe(graph, color))
                {

                    // Print the solution
                    // printSolution(color);
                    return true;
                }
                return false;
            }

            // Assign each color from 1 to m
            for (int j = 1; j <= m; j++)
            {
                color[i] = j;

                // Recur of the rest vertices
                if (GraphColoring(graph, m, i + 1, color))
                    return true;

                color[i] = 0;
            }

            return false;
        }
    }
}