using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab03_22DH112315
{
    internal class Graph
    {
        //Số đỉnh đồ thị
        public int N { get; set; }

        //Số cạnh
        public int A { get; set; }

        //Danh sách kề
        LinkedList<int>[] DanhSachKe;
        

        //Ma trận kề
        int[,] MaTranKe;

        //Biến cuối
        int end;

        //Hành động duyệt qua đồ thị
        bool[] duyet;

        //Biến trước
        int[] truoc;


        //Bài 1
        internal void LietKeCacDinhLienThong(string fname)
        {
            ReadDanhSachKe(fname);
            BFS();
            WriteBFS(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void WriteBFS(string fname)
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

        private void BFS()
        {
            duyet = new bool[N + 1];
            truoc = new int[N + 1];
            BFS(A);
        }

            private void BFS(int a)
            {
            Queue<int> queue = new Queue<int>();
            duyet[a] = true;
            truoc[A] = -1;
            queue.Enqueue(a);

            while (queue.Count != 0)
            {
                int u = queue.Dequeue();
                foreach (int v in DanhSachKe[u - 1])
                {
                    if (duyet[v]) continue;
                    duyet[v] = true;
                    truoc[v] = u;
                    queue.Enqueue(v);
                }
            }
        }

        private void ReadDanhSachKe(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            A = Int32.Parse(line[1].Trim());
            Console.WriteLine($"So dinh do thi: {N}");
            DanhSachKe = new LinkedList<int>[N];
            for (int i = 0; i < N; i++)
            {
                DanhSachKe[i] = new LinkedList<int>();
                if (lines[i + 1].Length == 0)
                {
                    continue;
                }
                line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int v = Int32.Parse(line[j].Trim());
                    DanhSachKe[i].AddLast(v);
                }
            }
        }

        //Bài 2

        //Số cạnh
        public int B { get; set; }

        internal void TimDuongDi(string fname)
        {
            ReadDanhSachKe2(fname); 
            DuongDiBFS();
            WriteDuongDiBFS(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void WriteDuongDiBFS(string fname)
        {
            List<int> duongdi = TimDuong(A, B);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                if (duongdi.Count == 1)
                {
                    file.Write(String.Format($"Khong ton tai duong di tu {A} den {B}"));
                    return;
                }
                file.WriteLine(duongdi.Count);
                foreach (int n in duongdi)
                    file.Write(String.Format("{0,-3}", n));

            }
        }

        private List<int> TimDuong(int a, int b)
        {
            List<int> duongdi = new List<int>();
            duongdi.Add(b);
            int v = truoc[b];
            if (v != 0)
            {
                while (v != -1)
                {
                    duongdi.Insert(0, v);
                    v = truoc[v];
                }
            }
            return duongdi;
        }

        private void DuongDiBFS()
        {
            BFS();
        }

        private void ReadDanhSachKe2(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            A = Int32.Parse(line[1].Trim());
            B = Int32.Parse(line[2].Trim());
            Console.WriteLine($"So dinh do thi: {N}");
            DanhSachKe = new LinkedList<int>[N];
            for (int i = 0; i < N; i++)
            {
                DanhSachKe[i] = new LinkedList<int>();
                if (lines[i + 1].Length == 0)
                {
                    continue;
                }
                line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int v = Int32.Parse(line[j].Trim());
                    DanhSachKe[i].AddLast(v);
                }
            }
        }

        //Bài 3

        //Biến đầu
        int start;

        internal void KiemTraDoThiLienThong(string fname)
        {
            ReadDanhSachKe3(fname);
            start = 0;
            bool lienthong = DoThiLienThong();
            WriteKetQuaLienThongCuaDoThi(fname.Substring(0, fname.Length - 3) + "OUT", lienthong);
        }

        private void WriteKetQuaLienThongCuaDoThi(string fname, bool lienthong)
        {
            using (StreamWriter sw = new StreamWriter(fname))
            {
                sw.WriteLine(lienthong? "YES" : "NO");
            }
        }

        private bool DoThiLienThong()
        {
            duyet = new bool[N];
            truoc = new int[N];

            BFS2(start);

            return duyet.All(v => v);
        }

        private void BFS2(int Start)
        {
            Queue<int> queue = new Queue<int>();
            duyet[Start] = true;
            queue.Enqueue(Start);

            while (queue.Count != 0)
            {
                int DinhHienTai = queue.Dequeue();

                foreach (var DinhLienKe in DanhSachKe[DinhHienTai])
                {
                    if (!duyet[DinhLienKe])
                    {
                        duyet[DinhLienKe] = true;
                        queue.Enqueue(DinhLienKe);
                    }
                }
            }
        }

        private void ReadDanhSachKe3(string fname)
        {
            using (StreamReader sr = new StreamReader(fname))
            {
                N = int.Parse(sr.ReadLine());
                Console.WriteLine($"So dinh do thi: {N}");
                DanhSachKe = new LinkedList<int>[N];

                for (int i = 0; i < N; i++)
                {
                    DanhSachKe[i] = new LinkedList<int>();
                    string[] line = sr.ReadLine().Split();
                    foreach (var vertex in line.Skip(1).Select(int.Parse))
                    {
                        DanhSachKe[i].AddLast(vertex - 1); 
                    }
                }
            }
        }

        //Bài 4

        //Danh sách kề
        List<List<int>> danhSachKe;

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

        //Bài tập thêm 1
        internal void MaTranKeSangDanhSachKe(string fname)
        {
            ReadMaTranKe(fname);
            ChuyenMaTranKeSangDanhSachKe();
            WriteDanhSachKe(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void WriteDanhSachKe(string fname)
        {
            using (StreamWriter sw = new StreamWriter(fname.Substring(0, fname.Length - 3) + "OUT"))
            {
                sw.WriteLine(N);

                for (int i = 0; i < N; i++)
                {
                    sw.WriteLine(string.Join(" ", DanhSachKe[i]));
                }
            }
        }

        private void ChuyenMaTranKeSangDanhSachKe()
        {
            DanhSachKe = new LinkedList<int>[N];
            for (int i = 0; i < N; i++)
            {
                DanhSachKe[i] = new LinkedList<int>();
                for (int j = 0; j < N; j++)
                {
                    if (MaTranKe[i, j] == 1)
                    {
                        DanhSachKe[i].AddLast(j + 1);
                    }
                }
            }
        }

        private void ReadMaTranKe(string fname)
        {
            using (StreamReader sr = new StreamReader(fname))
            {
                N = int.Parse(sr.ReadLine());
                Console.WriteLine($"So dinh do thi: {N}");
                MaTranKe = new int[N, N];

                for (int i = 0; i < N; i++)
                {
                    string[] line = sr.ReadLine().Split();
                    for (int j = 0; j < N; j++)
                    {
                        MaTranKe[i, j] = int.Parse(line[j]);
                    }
                }
            }
        }
    }
}