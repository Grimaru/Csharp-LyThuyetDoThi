using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KiemTra02_22DH112315
{
    internal class Graph
    {
        //Số đỉnh đồ thị
        public int N { get; set; }

        //Số cạnh
        public int M { get; set; }

        //Số cạnh
        public int A { get; set; }

        //Danh sách kề
        List<List<int>> danhSachKe;

        //Biến duyệt qua đồ thị
        int[] duyet;

        //Biến trước
        int[] truoc;

        //Biến đầu
        int start;

        //Biến cuối
        int end;

        //Bài 01
        //Biến bỏ đỉnh 1
        int removeDinh1;

        //Biến bỏ đỉnh 2
        int removeDinh2;
        internal void CanhCauBangBFS(string fname)
        {
            ReadDanhSachKeBFSBai01(fname);
            string result = TimCanhCauBangBFS() ? "YES" : "NO";
            WriteCanhCauBangBFS(fname, result);
        }

        private void WriteCanhCauBangBFS(string fname, string result)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname.Substring(0, fname.Length - 3) + "OUT"))
            {
                file.WriteLine(String.Format("{0,-3}", result));
                System.Console.WriteLine(String.Format("{0,-3}", result));
            }
        }

        private bool TimCanhCauBangBFS()
        {
            bool res = false;
            int SoMienLienThongTruocKhiXoaCanh = DemSoMienLienThongBangBFS().Count;
            danhSachKe[removeDinh1].Remove(removeDinh2);
            danhSachKe[removeDinh2].Remove(removeDinh1);
            int SoMienLienThongSauKhiXoaCanh = DemSoMienLienThongBangBFS().Count;
            danhSachKe[removeDinh1].Add(removeDinh2);
            danhSachKe[removeDinh2].Add(removeDinh1);
            if (SoMienLienThongTruocKhiXoaCanh < SoMienLienThongSauKhiXoaCanh)
            {
                res = true;
            }
            return res;
        }

        protected List<List<int>> DemSoMienLienThongBangBFS()
        {
            List<List<int>> danhsach = new List<List<int>>();
            InitIntArray(-2);
            for (int i = 0; i < N; i++)
            {
                if (duyet[i] == 0)
                {
                    List<int> res = CanhCauBangBFS(i);
                    danhsach.Add(res);
                }
            }
            return danhsach;
        }

        private void InitIntArray(int value = -2)
        {
            duyet = new int[N];
            truoc = new int[N];
            for (int i = 0; i < duyet.Length; i++)
            {
                duyet[i] = 0;
                truoc[i] = value;
            }
        }

        private List<int> CanhCauBangBFS(int s)
        {
            List<int> res = new List<int>();
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(s);
            duyet[s] = 1;
            while (queue.Count > 0)
            {
                int v = queue.Dequeue();
                foreach (int u in danhSachKe[v])
                {
                    if (duyet[u] == 0)
                    {
                        queue.Enqueue(u);
                        res.Add(u);
                        duyet[u] = 1;
                    }
                }
            }
            return res;
        }

        private void ReadDanhSachKeBFSBai01(string fname)
        {
            string[] soDong = File.ReadAllLines(fname);
            string[] dong = soDong[0].Split(' ');
            N = int.Parse(dong[0].Trim());
            removeDinh1 = int.Parse(dong[1].Trim()) - 1;
            removeDinh2 = int.Parse(dong[2].Trim()) - 1;

            Console.WriteLine($"So dinh do thi: {N}");

            danhSachKe = new List<List<int>>();
            for (int i = 0; i < N; i++)
            {
                if (i + 1 < soDong.Length)
                {
                    dong = soDong[i + 1].Trim().Split(' ');
                    List<int> danhsach = new List<int>();
                    for (int j = 0; j < dong.Length; j++)
                    {
                        if (dong[j].Trim().Length > 0)
                        {
                            danhsach.Add(int.Parse(dong[j].Trim()) - 1);
                        }
                    }
                    danhSachKe.Add(danhsach);
                }
            }
        }

        //Bài 02
        public int S { get; set; }
        public int E { get; set; }

        LinkedList<int>[] danhSachKeBai2;

        bool[] duyetBai2;

        int[] truocBai2;

        internal void TimDuongDiDFS(string fname)
        {
            ReadDanhSachKeBangDFSBai02(fname);
            DuongDiDFS(E, S);
            WriteDuongDiDFS(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void DuongDiDFS(int t, int s)
        {
            TimLienThong(s);
        }

        private void TimLienThong(int s)
        {
            duyetBai2 = new bool[N + 1];
            truocBai2 = new int[N + 1];
            DFS(S);
        }

        private void DFS(int s)
        {
            Stack<int> stack = new Stack<int>();
            duyetBai2[s] = true;
            truocBai2[S] = -1;
            stack.Push(s);

            while (stack.Count != 0)
            {
                int u = stack.Pop();
                foreach (int v in danhSachKeBai2[u - 1])
                {
                    if (duyetBai2[v])
                        continue;
                    duyetBai2[v] = true;
                    truocBai2[v] = u;
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
            int v = truocBai2[t];
            if (v != 0)
            {
                while (v != -1)
                {
                    path.Insert(0, v);
                    v = truocBai2[v];
                }
            }
            return path;
        }

        private void ReadDanhSachKeBangDFSBai02(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            S = Int32.Parse(line[1].Trim());
            E = Int32.Parse(line[2].Trim());
            Console.WriteLine($"So dinh do thi: {N}");
            danhSachKeBai2 = new LinkedList<int>[N];

            for (int i = 0; i < N; i++)
            {
                danhSachKeBai2[i] = new LinkedList<int>();
                if (lines[i + 1].Length == 0)
                {
                    continue;
                }
                line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int v = Int32.Parse(line[j].Trim());
                    danhSachKeBai2[i].AddLast(v);
                }
            }
        }

        //Bài 03
        LinkedList<int>[] MaTranKe;

        internal void ChuTrinh(string fname)
        {
            ReadMaTranKeBai03(fname);
            bool flag = KiemTraChuTrinh();
            WriteKetQuaChuTrinh(fname.Substring(0, fname.Length - 3) + "OUT", flag);
        }
        private void ReadMaTranKeBai03(string fname)
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
    }
}