using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Lab05_22DH112315
{
    internal class Graph
    {
        public int N { get; set; }
        public int S { get; set; }
        public int E { get; set; }

        LinkedList<int>[] danhSachKe;

        bool[] duyet;

        int[] truoc;

        //Bài 1 
        internal void LietKeCacDinhLienThongDFS(string fname)
        {
            ReadDanhSachKeBangDFSBai1(fname);
            DFS();
            WriteSoDinhLienThong(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void DFS()
        {
            duyet = new bool[N + 1];
            truoc = new int[N + 1];
            DFS(S);
        }

        private void WriteSoDinhLienThong(string fname)
        {
            int count = 0;
            String s = "";
            for (int i = 1; i < truoc.Length; i++)
            {
                if (truoc[i] > 0)
                {
                    count++;
                    s += String.Format("{0,-3}", i);
                }
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(count);
                file.WriteLine(s);
            }
        }

        private void ReadDanhSachKeBangDFSBai1(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            S = Int32.Parse(line[1].Trim());
            Console.WriteLine($"So dinh do thi: {N}");
            danhSachKe = new LinkedList<int>[N];
            for (int i = 0; i < N; i++)
            {
                danhSachKe[i] = new LinkedList<int>();
                if (lines[i + 1].Length == 0)
                {
                    continue;
                }
                line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int v = Int32.Parse(line[j].Trim());
                    danhSachKe[i].AddLast(v);
                }
            }
        }

        //Bài 2
        internal void TimDuongDiDFS(string fname)
        {
            ReadDanhSachKeBangDFSBai2(fname);
            DuongDiDFS(E, S);
            WriteDuongDiDFS(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void DuongDiDFS(int t, int s)
        {
            TimLienThong(s);
        }

        private void TimLienThong(int s)
        {
            duyet = new bool[N + 1];
            truoc = new int[N + 1];
            DFS(S);
        }

        private void DFS(int s)
        {
            Stack<int> stack = new Stack<int>();
            duyet[s] = true;
            truoc[S] = -1;
            stack.Push(s);

            while (stack.Count != 0)
            {
                int u = stack.Pop();
                foreach (int v in danhSachKe[u - 1])
                {
                    if (duyet[v])
                        continue;
                    duyet[v] = true;
                    truoc[v] = u;
                    stack.Push(u);
                    stack.Push(v);
                }
            }
        }

        private void WriteDuongDiDFS(string fname)
        {
            List<int> path = TimDuongDFS(S, E);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                if (path.Count == 1)
                {
                    file.Write(String.Format("Khong ton tai duong di tu {0} den {1}", S, E));
                    return;
                }
                file.WriteLine(path.Count);
                foreach (int v in path)
                    file.Write(String.Format("{0,-3}", v));

            }
        }

        private List<int> TimDuongDFS(int s, int t)
        {
            List<int> path = new List<int>();
            path.Add(t);
            int v = truoc[t];
            if (v != 0)
            {
                while (v != -1)
                {
                    path.Insert(0, v);
                    v = truoc[v];
                }
            }
            return path;
        }

        private void ReadDanhSachKeBangDFSBai2(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            S = Int32.Parse(line[1].Trim());
            E = Int32.Parse(line[2].Trim());
            Console.WriteLine($"So dinh do thi: {N}");
            danhSachKe = new LinkedList<int>[N];

            for (int i = 0; i < N; i++)
            {
                danhSachKe[i] = new LinkedList<int>();
                if (lines[i + 1].Length == 0)
                {
                    continue;
                }
                line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int v = Int32.Parse(line[j].Trim());
                    danhSachKe[i].AddLast(v);
                }
            }
        }
    }
}