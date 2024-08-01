using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace test
{
    internal class graph
    {
        public int N { get; set; }
        public int E { get; set; }
        public int S { get; set; }
        public int T { get; set; }
        public int X { get; set; }

        protected const int INF = 1000;

        public int[,] WeightMatrix { get; set; }
        int[,] floydMatrix;
        int M;

        public int[,] DistanceMatrix { get; set; }



        LinkedList<Tuple<int, int, int>> wEList;
        LinkedList<int>[] adjList;
        List<Tuple<int, int, int>> edges;

        internal void InterVertexShortesPath(string fname)
        {
            ReadEdgeList(fname);
            Tuple<int, List<int>> path = IVShortestPath();
            WriteInterVertexShortestPath(path, fname.Substring(0, fname.Length - 3) + "OUT");
        }

        // Đọc danh sách cạnh từ tệp văn bản
        private void ReadEdgeList(string fname)
        {
            using (StreamReader sr = new StreamReader(fname))
            {
                string[] tokens = sr.ReadLine().Split(' ');
                N = int.Parse(tokens[0]);
                E = int.Parse(tokens[1]);
                S = int.Parse(tokens[2]);
                T = int.Parse(tokens[3]);
                X = int.Parse(tokens[4]);

                edges = new List<Tuple<int, int, int>>();
                for (int i = 0; i < E; i++)
                {
                    tokens = sr.ReadLine().Split(' ');
                    int u = int.Parse(tokens[0]);
                    int v = int.Parse(tokens[1]);
                    int w = int.Parse(tokens[2]);
                    edges.Add(new Tuple<int, int, int>(u, v, w));
                }
            }
        }

        // Thuật toán tìm đường đi ngắn nhất từ S đến T đi qua X
        private Tuple<int, List<int>> IVShortestPath()
        {
            Dictionary<int, List<Tuple<int, int>>> graph = new Dictionary<int, List<Tuple<int, int>>>();

            // Tạo đồ thị từ danh sách cạnh
            foreach (var edge in edges)
            {
                if (!graph.ContainsKey(edge.Item1))
                    graph[edge.Item1] = new List<Tuple<int, int>>();
                if (!graph.ContainsKey(edge.Item2))
                    graph[edge.Item2] = new List<Tuple<int, int>>();

                graph[edge.Item1].Add(new Tuple<int, int>(edge.Item2, edge.Item3));
                graph[edge.Item2].Add(new Tuple<int, int>(edge.Item1, edge.Item3));
            }

            // Sử dụng thuật toán Dijkstra để tìm đường đi ngắn nhất
            int[] distance = Enumerable.Repeat(int.MaxValue, N + 1).ToArray();
            bool[] visited = new bool[N + 1];
            int[] parent = new int[N + 1];
            distance[S] = 0;

            while (true)
            {
                int u = -1;
                int minDistance = int.MaxValue;
                // Tìm đỉnh có khoảng cách ngắn nhất chưa được xét
                for (int i = 1; i <= N; i++)
                {
                    if (!visited[i] && distance[i] < minDistance)
                    {
                        minDistance = distance[i];
                        u = i;
                    }
                }

                // Nếu không tìm thấy đỉnh u thì kết thúc
                if (u == -1 || u == T)
                    break;

                visited[u] = true;

                // Cập nhật khoảng cách từ đỉnh u đến các đỉnh kề v của nó
                if (graph.ContainsKey(u))
                {
                    foreach (var neighbor in graph[u])
                    {
                        int v = neighbor.Item1;
                        int w = neighbor.Item2;
                        if (!visited[v] && distance[u] + w < distance[v])
                        {
                            // Nếu đang xét đến đỉnh X, thì xét cả trường hợp đi qua đỉnh X
                            if (u == X)
                            {
                                // Kiểm tra nếu đỉnh tiếp theo cần đi là đỉnh T hoặc đỉnh X
                                if (v == T || v == X)
                                {
                                    distance[v] = distance[u] + w;
                                    parent[v] = u;
                                }
                            }
                            else
                            {
                                distance[v] = distance[u] + w;
                                parent[v] = u;
                            }
                        }
                    }
                }
            }

            // Tạo đường đi từ S đến T qua X
            List<int> path = new List<int>();
            int curr = T;
            while (curr != S)
            {
                path.Add(curr);
                curr = parent[curr];
            }
            path.Add(S);
            path.Reverse();

            return new Tuple<int, List<int>>(distance[T], path);
        }



        // Ghi kết quả ra tệp văn bản
        private void WriteInterVertexShortestPath(Tuple<int, List<int>> path, string fname)
        {
            using (StreamWriter sw = new StreamWriter(fname))
            {
                sw.WriteLine(path.Item1); // Độ dài đường đi ngắn nhất
                foreach (var vertex in path.Item2)
                {
                    sw.Write(vertex + " ");
                }
            }
        }
    }
}

