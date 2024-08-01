using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab06_22DH112315
{
    internal class Graph
    {
        LinkedList<int>[] MaTranKe;

        int N;

        int[] toMau;

        //Bài 01
        internal void DoThiPhanDoi(string fname)
        {
            ReadMaTranKeBai01(fname);
            bool co = KiemTraPhanDoi();
            WriteDoThiPhanDoi(fname.Substring(0, fname.Length - 3) + "OUT", co);
        }

        private void ReadMaTranKeBai01(string fname)
        {
            using (StreamReader reader = new StreamReader(fname))
            {
                N = int.Parse(reader.ReadLine());
                MaTranKe = new LinkedList<int>[N + 1];
                Console.WriteLine($"So dinh do thi: {N}");
                toMau = new int[N + 1];
                for (int i = 1; i <= N; i++)
                {
                    MaTranKe[i] = new LinkedList<int>();
                    string[] tokens = reader.ReadLine().Split(' ');
                    foreach (var token in tokens)
                    {
                        int neighbor = int.Parse(token);
                        MaTranKe[i].AddLast(neighbor);
                    }
                }
            }
        }
        private bool KiemTraPhanDoi()
        {
            Array.Fill(toMau, -1);

            for (int i = 1; i <= N; i++)
            {
                if (toMau[i] == -1)
                {
                    if (!ToMauBangDFS(i))
                        return false;
                }
            }
            return true;
        }
        private bool ToMauBangDFS(int vertex)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(vertex);
            toMau[vertex] = 0;

            while (stack.Count > 0)
            {
                int currentVertex = stack.Pop();


                foreach (int neighbor in MaTranKe[currentVertex])
                {
                    if (toMau[neighbor] == -1)
                    {
                        stack.Push(neighbor);
                        toMau[neighbor] = 1 - toMau[currentVertex];
                    }
                    else if (toMau[neighbor] == toMau[currentVertex])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void WriteDoThiPhanDoi(string fname, bool isBipartite)
        {
            using (StreamWriter writer = new StreamWriter(fname))
            {
                if (isBipartite)
                    writer.WriteLine("YES");
                else
                    writer.WriteLine("NO");
            }
        }

        //Bài 02
        internal void ChuTrinh(string fname)
        {
            ReadMaTranKeBai02(fname);
            bool flag = KiemTraChuTrinh();
            WriteKetQuaChuTrinh(fname.Substring(0, fname.Length - 3) + "OUT", flag);
        }
        private void ReadMaTranKeBai02(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = int.Parse(line[0].Trim());

            MaTranKe = new LinkedList<int>[N + 1];
            Console.WriteLine($"So dinh do thi: {N}");
            for (int i = 1; i <= N; i++)
            {
                MaTranKe[i] = new LinkedList<int>();
                if (lines[i].Length == 0)
                    continue;
                line = lines[i].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int n = int.Parse(line[j].Trim());
                    MaTranKe[i].AddLast(n);
                }
            }
        }

        private bool KiemTraChuTrinh()
        {
            bool[] visited = new bool[N + 1];
            for (int i = 1; i <= N; i++)
            {
                if (!visited[i])
                {
                    if (XetChuTrinh(i, -1, visited))
                        return true;
                }
            }
            return false;
        }

        private bool XetChuTrinh(int current, int parent, bool[] visited)
        {
            visited[current] = true;

            foreach (int neighbor in MaTranKe[current])
            {
                if (!visited[neighbor])
                {
                    if (XetChuTrinh(neighbor, current, visited))
                        return true;
                }
                else if (neighbor != parent)
                {
                    return true;
                }
            }
            return false;
        }

        private void WriteKetQuaChuTrinh(string fname, bool flag)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(flag ? "YES" : "NO");
            }
            Console.WriteLine(flag ? "YES" : "NO");
        }
        
        //Bài 03 
        internal void SapXepKieuTopo(string fname)
        {
            ReadMaTranKeBai03(fname);
            List<int> list = SapXepTopo();
            WriteDoThiKieuTopo(fname.Substring(0, fname.Length - 3) + "OUT", list);
        }
        private void ReadMaTranKeBai03(string fname)
        {
            using (StreamReader reader = new StreamReader(fname))
            {
                N = int.Parse(reader.ReadLine());
                MaTranKe = new LinkedList<int>[N + 1];
                toMau = new int[N + 1];

                for (int i = 1; i <= N; i++)
                {
                    MaTranKe[i] = new LinkedList<int>();
                    string[] tokens = reader.ReadLine().Split(' ');
                    foreach (var token in tokens)
                    {
                        int neighbor;
                        if (int.TryParse(token, out neighbor))
                        {
                            MaTranKe[i].AddLast(neighbor);
                        }
                        else
                        {
                            Console.WriteLine($"Khong co du lieu tu dong {i}");
                        }
                    }
                }
            }
        }
        private List<int> SapXepTopo()
        {
            List<int> result = new List<int>();
            bool[] visited = new bool[N + 1];
            Stack<int> stack = new Stack<int>();
            for (int i = 1; i <= N; i++)
            {
                if (!visited[i])
                {
                    DFS(i, visited, stack);
                }
            }
            while (stack.Count > 0)
            {
                result.Add(stack.Pop());
            }

            return result;
        }
        private void DFS(int current, bool[] visited, Stack<int> stack)
        {
            visited[current] = true;

            foreach (int neighbor in MaTranKe[current])
            {
                if (!visited[neighbor])
                {
                    DFS(neighbor, visited, stack);
                }
            }
            stack.Push(current);
        }
        private void WriteDoThiKieuTopo(string fname, List<int> list)
        {
            using (StreamWriter writer = new StreamWriter(fname))
            {
                foreach (int vertex in list)
                {
                    writer.Write(vertex + " ");
                }
            }
        }
    }
}