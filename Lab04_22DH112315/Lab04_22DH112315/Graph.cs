using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Lab04_22DH112315
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

        //Bài 1
        internal void LietKeCacMienLienThong(string fname)
        {
            ReadDanhSachKeBFSBai01(fname);
            List<List<int>> DanhSachMienLienThong = TimSoMienLienThong();
            WriteSoMienLienThong(fname, DanhSachMienLienThong);
        }

        private void WriteSoMienLienThong(string fname, List<List<int>> res)
        {
            using (StreamWriter file = new StreamWriter(fname.Substring(0, fname.Length - 3) + "OUT"))
            {
                file.WriteLine(String.Format("{0,-3}", res.Count));
                Console.WriteLine(String.Format("{0,-3}", res.Count));
                foreach (List<int> DanhSachMienLienThong in res)
                {
                    foreach (int mien in DanhSachMienLienThong)
                    {
                        file.Write(String.Format("{0,-3}", mien));
                        Console.Write(String.Format("{0,-3}", mien));
                    }
                    file.WriteLine();
                    Console.WriteLine();
                }
            }
        }

        protected List<List<int>> TimSoMienLienThong()
        {
            List<List<int>> DanhSachMienLienThong = new List<List<int>>();
            //InitIntArray(-2);
            for (int i = 0; i < N; i++)
            {
                if (duyet[i] == 0)
                {
                    List<int> res = BFS(i);
                    if (res.Count > 0)
                    {
                        DanhSachMienLienThong.Add(res);
                    }
                }
            }
            return DanhSachMienLienThong;
        }

        protected List<int> BFS(int s)
        {
            List<int> res = new List<int>();
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(s);
            res.Add(s + 1);
            duyet[s] = 1;

            while (queue.Count > 0)
            {
                int v = queue.Dequeue();
                foreach (int u in danhSachKe[v])
                {
                    if (duyet[u] == 0)
                    {
                        queue.Enqueue(u);
                        res.Add(u + 1);
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
            Console.WriteLine($"So dinh do thi: {N}");

            danhSachKe = new List<List<int>>(N);
            duyet = new int[N];
            for (int i = 0; i < N; i++)
            {
                danhSachKe.Add(new List<int>());
            }

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
                    danhSachKe[i].AddRange(danhsach);
                }
            }

            Console.WriteLine("Da doc xong!");
            Console.WriteLine("Ket qua cua bai 1 la");
        }

        //Bài 2
        //Biến bỏ đỉnh 1
        int removeDinh1;

        //Biến bỏ đỉnh 2
        int removeDinh2;
        internal void CanhCauBangBFS(string fname)
        {
            ReadDanhSachKeBFSBai02(fname);
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

        private void ReadDanhSachKeBFSBai02(string fname)
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

            Console.WriteLine("Da doc xong!");
            Console.WriteLine("Ket qua cua bai 2 la");
        }


        //Bài 3
        internal void TimDinhKhopBangBFS(string fname)
        {
            ReadMaTranKeBai3(fname);
            bool flag = DinhKhopBangBFS();
            WriteDinhKhopBangBFS(fname.Substring(0, fname.Length - 3) + "OUT", flag);
        }
        internal void WriteDinhKhopBangBFS(string fname, bool flag)
        {
            File.WriteAllText(fname, flag ? "YES" : "NO");
        }

        internal void ReadMaTranKeBai3(string fname)
        {
            string[] lines = File.ReadAllLines(fname);
            string[] firstLine = lines[0].Split(' ');
            N = int.Parse(firstLine[0]);
            M = int.Parse(firstLine[1]);

            danhSachKe = new List<List<int>>();
            for (int i = 0; i <= N; i++)
            {
                danhSachKe.Add(new List<int>());
            }

            for (int i = 1; i <= M; i++)
            {
                string[] vertices = lines[i].Split(' ');
                foreach (string vertex in vertices)
                {
                    danhSachKe[i].Add(int.Parse(vertex));
                }
            }

            start = M;
            end = 0;
        }

        internal bool DinhKhopBangBFS()
        {
            int[] num = new int[N + 1];
            int[] low = new int[N + 1];
            duyet = new int[N + 1];
            truoc = new int[N + 1];
            int counter = 0;
            bool isCutVertex = false;

            void DFS(int u)
            {
                duyet[u] = 1;
                counter++;
                num[u] = counter;
                low[u] = num[u];

                foreach (int v in danhSachKe[u])
                {
                    if (duyet[v] == 0)
                    {
                        truoc[v] = u;
                        DFS(v);

                        low[u] = Math.Min(low[u], low[v]);

                        if (low[v] >= num[u])
                        {
                            isCutVertex = true;
                            if (u != start || end != 0)
                            {
                                break;
                            }
                        }
                    }
                    else if (v != truoc[u])
                    {
                        low[u] = Math.Min(low[u], num[v]);
                    }
                }
            }

            for (int i = 1; i <= N; i++)
            {
                duyet[i] = 0;
                truoc[i] = 0;
            }

            for (int i = 1; i <= N; i++)
            {
                if (duyet[i] == 0)
                {
                    counter = 0;
                    DFS(i);
                    if (isCutVertex)
                    {
                        break;
                    }
                }
            }

            return isCutVertex;
        }

        //Bài 4       
        //Mảng lưới
        List<List<int>> Luoi;

        //Tạo biến cho i và j
        public int A1 { get; set; }
        public int B1 { get; set; }

        public int A2 { get; set; }
        public int B2 { get; set; }

        internal void DiTrenLuoiBangBFS(string fname)
        {
            ReadLuoiBangBFS(fname);
            List<string> danhsachluoi = TimLuoiBangBFS();
            WriteDanhSachLuoiBangBFS(fname.Substring(0, fname.Length - 3) + "OUT", danhsachluoi);
        }
        internal void ReadLuoiBangBFS(string fname)
        {
            string[] soDong = File.ReadAllLines(fname);
            string[] dongDauTien = soDong[0].Split(' ');
            N = int.Parse(dongDauTien[0]);
            M = int.Parse(dongDauTien[1]);

            string[] dongThuHai = soDong[1].Split(' ');
            A1 = int.Parse(dongThuHai[0]) - 1;
            B1 = int.Parse(dongThuHai[1]) - 1;
            A2 = int.Parse(dongThuHai[2]) - 1;
            B2 = int.Parse(dongThuHai[3]) - 1;

            Luoi = new List<List<int>>();
            for (int i = 0; i < N; i++)
            {
                string[] dong = soDong[i + 2].Split(' ');
                List<int> DataCuaDong = new List<int>();
                for (int j = 0; j < M; j++)
                {
                    DataCuaDong.Add(int.Parse(dong[j]));
                }
                Luoi.Add(DataCuaDong);
            }
        }

        internal List<string> TimLuoiBangBFS()
        {
            danhSachKe = new List<List<int>>();
            for (int i = 0; i < N * M; i++)
            {
                danhSachKe.Add(new List<int>());
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (Luoi[i][j] == 1)
                    {
                        int u = i * M + j;
                        if (i > 0 && Luoi[i - 1][j] == 1)
                        {
                            int v = (i - 1) * M + j;
                            danhSachKe[u].Add(v);
                        }
                        if (j > 0 && Luoi[i][j - 1] == 1)
                        {
                            int v = i * M + (j - 1);
                            danhSachKe[u].Add(v);
                        }
                        if (i < N - 1 && Luoi[i + 1][j] == 1)
                        {
                            int v = (i + 1) * M + j;
                            danhSachKe[u].Add(v);
                        }
                        if (j < M - 1 && Luoi[i][j + 1] == 1)
                        {
                            int v = i * M + (j + 1);
                            danhSachKe[u].Add(v);
                        }
                    }
                }
            }

            duyet = new int[N * M];
            truoc = new int[N * M];
            for (int i = 0; i < N * M; i++)
            {
                duyet[i] = 0;
                truoc[i] = -1;
            }

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(A2 * M + B2);
            duyet[A2 * M + B2] = 1;

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                for (int i = 0; i < danhSachKe[u].Count; i++)
                {
                    int v = danhSachKe[u][i];
                    if (duyet[v] == 0)
                    {
                        queue.Enqueue(v);
                        duyet[v] = 1;
                        truoc[v] = u;
                    }
                }
            }

            List<string> result = new List<string>();

            if (duyet[A1 * M + B1] == 1)
            {
                int u = A1 * M + B1;
                int count = 0;
                while (u != -1)
                {
                    int i = u / M + 1;
                    int j = u % M + 1;
                    result.Add($"{i} {j}");
                    u = truoc[u];
                    count++;
                }
                result.Insert(0, count.ToString());
            }
            else
            {
                result.Add("0");
            }

            return result;
        }

        internal void WriteDanhSachLuoiBangBFS(string fname, List<string> danhsachluoi)
        {
            File.WriteAllLines(fname, danhsachluoi);
        }
    }
}