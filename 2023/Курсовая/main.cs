using System;
using System.Collections.Generic;
using System.IO;

public class Dijkstra
{
    public static void DijkstraAlgorithm(List<List<int[]>> graph, int startNode)
    {
        int numNodes = graph.Count;
        int[] distance = new int[numNodes];
        bool[] visited = new bool[numNodes];
        Array.Fill(distance, int.MaxValue);
        distance[startNode] = 0;

        for (int count = 0; count < numNodes - 1; count++)
        {
            int u = GetMinDistanceNode(distance, visited);
            visited[u] = true;

            foreach (var edge in graph[u])
            {
                int v = edge[0];
                int weight = edge[1];

                if (!visited[v] && distance[u] != int.MaxValue && distance[u] + weight < distance[v])
                {
                    distance[v] = distance[u] + weight;
                }
            }
        }

        PrintShortestPaths(distance, startNode);
    }

    private static int GetMinDistanceNode(int[] distance, bool[] visited)
    {
        int minDistance = int.MaxValue;
        int minNode = -1;

        for (int node = 0; node < distance.Length; node++)
        {
            if (!visited[node] && distance[node] < minDistance)
            {
                minDistance = distance[node];
                minNode = node;
            }
        }

        return minNode;
    }

public static List<List<int[]>> ReadGraphFromFile()
    {
        List<List<int[]>> graph = new List<List<int[]>>();
        try
        {
            string[] lines = File.ReadAllLines("graph.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                int node1 = int.Parse(parts[0]);
                int node2 = int.Parse(parts[1]);
                if (Convert.ToInt32(parts[2]) < 0){
                    Console.WriteLine("Ошибка чтения графа: Отрицательный вес");
                    Environment.Exit(0);
                }
                int weight = int.Parse(parts[2]);
                while (graph.Count <= Math.Max(node1, node2))
                {
                    graph.Add(new List<int[]>());
                }
                
                graph[node1].Add(new int[] { node2, weight });
                graph[node2].Add(new int[] { node1, weight });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Ошибка чтения файла: " + e.Message);
            Environment.Exit(0);
        }

        return graph;
    }

    private static void PrintShortestPaths(int[] distance, int startNode)
    {

        for (int node = 0; node < distance.Length;node++)
        {
            Console.WriteLine($"Расстояние от вершины {startNode} до вершины {node}: {(distance[node] == int.MaxValue ? "недостижимо": distance[node].ToString())}");
        }
    }

    public static void Main()
    {
        List<List<int[]>> graph = ReadGraphFromFile();
            
        try{
            int startNode = 0;
            DijkstraAlgorithm(graph, startNode);
            
        }
        catch(Exception e)
        {
                        Console.WriteLine("Ошибка обработки graph: " + e.Message);
                        Environment.Exit(0);
        }
    }
}