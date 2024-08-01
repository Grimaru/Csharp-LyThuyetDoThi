using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KiemTra01_22DH112315
{
    internal class Graph
    {
        //Số đỉnh của ma trận kề
        public int N { get; set; }

        //Danh sách kề
        LinkedList<int>[] DanhSachKe;

        //Số cạnh của ma trận kề
        public int M { get; set; }

        //Danh sách cạnh
        LinkedList<Tuple<int, int>> DanhSachCanh;

        public Graph()
        {
            N = 0;
        }


        //Bài 1
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

        //Bài 2
        internal void ChuyenDanhSachKeThanhDanhSachCanh(string fname)
        {
            ReadDanhSachKe(fname); // Đọc danh sách kề từ file

            ChuyenDanhSachKeThanhDanhSachCanh();

            WriteDanhSachCanh(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void WriteDanhSachCanh(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(String.Format("{0,-3} {1, -3}", N, M));
                foreach (Tuple<int, int> n in DanhSachCanh)
                {
                    file.WriteLine(String.Format("{0,-3} {1, -3}", n.Item1, n.Item2));
                }
            }
        }

        private void ChuyenDanhSachKeThanhDanhSachCanh()
        {
            DanhSachCanh = new LinkedList<Tuple<int, int>>();
            M = 0;

            for (int i = 0; i < DanhSachKe.Length; i++)
            {
                int v1 = i + 1;
                foreach (int t in DanhSachKe[i])
                {
                    int v2 = t + 1; 
                    if (v1 < v2)
                    {
                        DanhSachCanh.AddLast(new Tuple<int, int>(v1, v2));
                        M++;
                    }
                }
            }
        }

        //Bài 3
        //Danh sách kề
        List<List<int>> danhSachKe;

        //Hành động duyệt qua đồ thị
        bool[] duyet;

        internal void DemSoMienLienThong(string fname)
        {
            ReadDanhSachKeBFS(fname);
            List<List<int>> MienLienThong = TimSoMienLienThong();
            WriteSoMienLienThong(fname, MienLienThong);
        }

        private void WriteSoMienLienThong(string fname, List<List<int>> SoMien)
        {
            using (StreamWriter sw = new StreamWriter(fname.Replace(".INP", ".OUT")))
            {
                sw.WriteLine(SoMien.Count);
            }
        }

        private List<List<int>> TimSoMienLienThong()
        {
            int n = danhSachKe.Count - 1;
            duyet = new bool[n + 1];
            List<List<int>> MienLienThong = new List<List<int>>();

            for (int i = 1; i <= n; i++)
            {
                if (!duyet[i])
                {
                    List<int> mien = new List<int>();
                    BFS(i, ref mien);
                    MienLienThong.Add(mien);
                }
            }

            return MienLienThong;
        }

        private void BFS(int start, ref List<int> mien)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);
            duyet[start] = true;
            mien.Add(start);

            while (queue.Count > 0)
            {
                int current = queue.Dequeue();

                foreach (var n in danhSachKe[current])
                {
                    if (!duyet[n])
                    {
                        duyet[n] = true;
                        queue.Enqueue(n);
                        mien.Add(n);
                    }
                }
            }
        }

        private void ReadDanhSachKeBFS(string fname)
        {
            using (StreamReader sr = new StreamReader(fname))
            {
                int a = int.Parse(sr.ReadLine());
                danhSachKe = new List<List<int>>(a + 1);
                for (int i = 0; i <= a; i++)
                {
                    danhSachKe.Add(new List<int>());
                }

                for (int i = 1; i <= a; i++)
                {
                    string[] canh = sr.ReadLine().Split();
                    foreach (var e in canh)
                    {
                        int n = int.Parse(e);
                        danhSachKe[i].Add(n);
                        danhSachKe[n].Add(i);
                    }
                }
            }
        }
    }
}