using System;
using System.Collections.Generic;
using System.Linq;

public class Solution
{
    public static void Main(string[] args)
    {
        int q = Convert.ToInt32(Console.ReadLine().Trim());
        
        for (int qItr = 0; qItr < q; qItr++)
        {
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');
            
            int n = Convert.ToInt32(firstMultipleInput[0]);
            int m = Convert.ToInt32(firstMultipleInput[1]);
            int c_lib = Convert.ToInt32(firstMultipleInput[2]);
            int c_road = Convert.ToInt32(firstMultipleInput[3]);
            
            List<List<int>> cities = new List<List<int>>();
            
            for (int i = 0; i < m; i++)
            {
                cities.Add(Console.ReadLine().TrimEnd().Split(' ').Select(citiesTemp => Convert.ToInt32(citiesTemp)).ToList());
            }
            
            long result = roadsAndLibraries(n, c_lib, c_road, cities);
            Console.WriteLine(result);
        }
    }
    
    public static long roadsAndLibraries(int n, int c_lib, int c_road, List<List<int>> cities)
    {
        
        if (c_lib <= c_road)
        {
            return (long)n * c_lib;
        }

       
        List<int>[] graph = new List<int>[n + 1];
        for (int i = 1; i <= n; i++)
        {
            graph[i] = new List<int>();
        }

       
        foreach (var connection in cities)
        {
            int city1 = connection[0];
            int city2 = connection[1];
            graph[city1].Add(city2);
            graph[city2].Add(city1);
        }

        bool[] visited = new bool[n + 1];
        long totalCost = 0;

        
        for (int i = 1; i <= n; i++)
        {
            if (!visited[i])
            {
                
                int cityCount = BFS(i, graph, visited);
                
                
                totalCost += c_lib + (long)(cityCount - 1) * c_road;
            }
        }

        return totalCost;
    }

    private static int BFS(int start, List<int>[] graph, bool[] visited)
    {
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(start);
        visited[start] = true;
        int count = 0;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            count++;

            foreach (int neighbor in graph[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return count;
    }
}
