using System;
using System.Collections.Generic;
using System.Text;

namespace Lab01_22DH112315
{
    class Graph
    {
        //Số đỉnh của ma trận kề
        public int N { get; set; }
        int[,] MaTranKe;

        LinkedList<int>[] DanhSachKe;

        //Số cạnh của đồ thị
        public int M { get; set; }
        List<Tuple<int, int>> DanhSachCanh;

        public Graph()
        {
            N = 0;
            M = 0;
        }

        //Bài 01
        internal void VertexDegree(string fname)
        {
            ReadMaTranKe(fname);
            WriteVertexDegreesAM(fname.Substring(0, fname.Length - 3) + "OUT");

        }

        private void WriteVertexDegreesAM(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(String.Format("{0,-3}", N));
                for (int i = 0; i < N; i++)
                {
                    int degree = 0;
                    for (int j = 0; j < N; j++)
                    {
                        degree += MaTranKe[i, j];
                    }
                    file.Write(String.Format("{0,-3}", degree));
                }
                file.WriteLine();
            }
        }

        private void ReadMaTranKe(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            N = Int32.Parse(lines[0].Trim());
            Console.WriteLine("So dinh cua ma tran ke: " + N);
            MaTranKe = new int[N, N];

            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int value = Int32.Parse(line[j].Trim());
                    MaTranKe[i - 1, j] = value;
                    Console.Write(String.Format("{0, -3}", MaTranKe[i - 1, j]));
                }
                Console.WriteLine();
            }

        }

        //Bài 02
        internal void InOutDegrees(string filename)
        {
            ReadMaTranKe(filename); //ReadMaTranKe áp dụng cho cả Bài 01 và Bài 02
            WriteInOutDegrees(filename.Substring(0, filename.Length - 3) + "OUT");
        }

        private void WriteInOutDegrees(string filename)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
            {
                file.WriteLine(String.Format("{0,-3}", N));
                for (int i = 0; i < N; i++)
                {
                    int inDegree = 0;
                    int outDegree = 0;
                    for (int j = 0; j < N; j++)
                    {
                        outDegree += MaTranKe[i, j];
                        inDegree += MaTranKe[j, i];
                    }
                    file.WriteLine(String.Format("{0,-3} {1,-3}", inDegree, outDegree));
                }
            }
        }

        //Bài 03
        internal void VertexDegreeAl(string fname)
        {
            ReadDanhSachKe(fname);
            WriteVertexDegreeAL(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void WriteVertexDegreeAL(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(N);
                for (int i = 0; i < N; i++)
                { file.Write(String.Format("{0,-3}", DanhSachKe[i].Count)); }
            }
        }

        private void ReadDanhSachKe(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            N = Int32.Parse(lines[0].Trim());
            Console.WriteLine("So Dinh Ma Tran Ke: " + N);
            DanhSachKe = new LinkedList<int>[N];
            for (int i = 0; i < N; i++)
            {
                DanhSachKe[i] = new LinkedList<int>();
                string[] line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int n = Int32.Parse(line[j].Trim()) - 1;
                    DanhSachKe[i].AddLast(n);
                }
            }
        }

        //Bài 04
        internal void VertexDegreesListEdge(string fname)
        {
            ReadDanhSachCanh(fname);
            WriteVertexDegreesListEdge(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void WriteVertexDegreesListEdge(string fname)
        {
            List<int> degree = new List<int>(N);
            foreach (Tuple<int, int> t in DanhSachCanh)
            {              
                while (t.Item1 >= degree.Count)
                    degree.Add(1);
                

                while (t.Item2 >= degree.Count)
                    degree.Add(0);
                

                degree[t.Item1]++;
                degree[t.Item2]++;
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(N);
                foreach (int d in degree)
                    file.Write(String.Format("{0,-3}", d));
            }
        }

        private void ReadDanhSachCanh(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            M = Int32.Parse(line[1].Trim());
            Console.WriteLine("So Dinh Cua Ma Tran Ke: " + N);
            DanhSachCanh = new List<Tuple<int, int>>(M);

            for (int i = 2; i < lines.Length; i++)
            {
                string[] line1 = lines[i].Split(' ');
                if (line1.Length == 2) 
                {
                    int n = Int32.Parse(line1[0].Trim()) - 1;
                    int m = Int32.Parse(line1[1].Trim()) - 1;
                    DanhSachCanh.Add(new Tuple<int, int>(n, m));
                }
            }
        }


    }
}
