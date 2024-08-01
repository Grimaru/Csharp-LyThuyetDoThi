using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab07_22DH112315
{
    internal class graph
    {
        protected const int INF = 1000;

        protected int trongSoLonNhat = 0;

        int[,] maTranKe;

        int[,] maTranFloyd;

        List<List<Tuple<int, int>>> danhSachKe;

        public int N;

        public int E;

        public int start;

        public int end;

        public int intermediary;

        public int[] duyet;

        public int[] truoc;

        public int[] duongDi;

        //Bài 02
        internal void DuongDiNganNhatQuaTrungGian(string fname)
        {
            ReadDanhSachKeBai02(fname);
            List<int> danhsach = DuongDiNganNhatQuaTrungGian();
            VietDuongDiNganNhatQuaTrungGian(fname, danhsach);

        }

        private void VietDuongDiNganNhatQuaTrungGian(string fname, List<int> res)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                if (res.Count == 3)
                {
                    file.WriteLine(String.Format("Khong co duong di tu {0, -3} đến {1, -3}", start + 1, end + 1));
                    System.Console.WriteLine(String.Format("Khong co duong di tu {0, -3} đến {1, -3}", start + 1, end + 1));
                }
                else
                {
                    file.WriteLine(String.Format("{0, -3}", res[res.Count - 1]));
                    System.Console.WriteLine(String.Format("{0, -3}", res[res.Count - 1]));
                    int index = 0;
                    foreach (int v in res)
                    {
                        if (index == (res.Count - 1)) break;
                        file.Write(String.Format("0,-3", v + 1));
                        System.Console.Write(String.Format("0,-3", v + 1));
                        index++;
                    }
                    file.WriteLine();
                    System.Console.WriteLine();
                }
            }
        }

        protected List<int> DuongDiNganNhatQuaTrungGian()
        {
            int tam = end;
            end = intermediary;
            Dijkstra();
            int khoangCach = duongDi[end];
            List<int> res = TimDuongDi();
            end = tam;
            tam = start;
            start = intermediary;
            for (int i = 0; i < N; i++)
            { 
                if (!res.Contains(i+1))
                {
                    duyet[i] = 0;
                    truoc[i] = -2;
                    duongDi[i] = INF;
                }
            }
            duyet[start] = 0;
            SubDijkstra();
            List<int> luot2 = TimDuongDi();
            khoangCach += duongDi[end];
            intermediary = start;
            start = tam;
            res.RemoveAt(res.Count - 1);
            res.AddRange(luot2);
            res.Add(khoangCach);
            return res;
        }

        protected void SubDijkstra()
        {
            int g = start;
            duongDi[g] = 0;
            do
            {
                g = start;
                for (int i = 0; i < N; i++)
                {
                    if ((duyet[i] == 0) && (duongDi[g] > duongDi[i])) g = i;
                }
                duyet[g] = 1;
                if ((duongDi[g] == INF) || g == end) break;
                foreach (Tuple<int, int> v2 in danhSachKe[g])
                {
                    if (duyet[v2.Item1] == 0)
                    {
                        int d = duongDi[g] + v2.Item2;
                        if (duongDi[v2.Item1] > d)
                        {
                            duongDi[v2.Item1] = d;
                            truoc[v2.Item1] = g;
                        }
                    }
                }
            } while (true);
        }

        protected List<int> TimDuongDi()
        {
            int y = end;
            int x = start;

            List<int> res = new List<int>();
            res.Add(y);

            int v = truoc[y];
            if (v != -2)
            {
                while (v != x)
                {
                    res.Insert(0, v);
                    v = truoc[v];
                }
                if (v == x) res.Insert(0, v);
            }
            return res;
        }

        protected void Dijkstra()
        {
            InitIntArray(-1);
            int g = start;
            duongDi[g] = 0;
            truoc[g] = -1;
            do
            {
                g = end;
                for (int i = 0; i < N; i++)
                {
                    if ((duyet[i] == 0) && (duongDi[g] > duongDi[i])) g = i;
                }
                duyet[g] = 1;
                if ((duongDi[g] == INF) || g == end) break;
                foreach (Tuple<int, int> v2 in danhSachKe[g])
                {
                    if (duyet[v2.Item1] == 0)
                    {
                        int d = duongDi[g] + v2.Item2;
                        if (duongDi[v2.Item1] > d)
                        {
                            duongDi[v2.Item1] = d;
                            truoc[v2.Item1] = g;
                        }
                    }
                }
            } while (true);
        }

        private void InitIntArray(int value = -2)
        {
            duyet = new int[N];
            truoc = new int[N];
            duongDi = new int[N];
            for(int i = 0; i < N; i++)
            {
                duyet[i] = 0;
                truoc[i] = value;
                duongDi[i] = INF;
            }
        }

        private void ReadDanhSachKeBai02(string fname)
        {
            string[] dong = System.IO.File.ReadAllLines(fname);
            string[] soDong = dong[0].Split();
            N = Int32.Parse(soDong[0].Trim());
            E = Int32.Parse(soDong[1].Trim());
            start = Int32.Parse(soDong[2].Trim()) - 1;
            end = Int32.Parse(soDong[3].Trim()) - 1;
            intermediary = Int32.Parse(soDong[4].Trim()) - 1;
            Console.WriteLine($"So dinh do thi: {N}");
            danhSachKe = new List<List<Tuple<int, int>>>();
            for(int i = 0; i < N; i++)
            {
                danhSachKe.Add(new List<Tuple<int, int>>());
            }
            for (int i = 0; i < E; i++)
            {
                soDong = dong[i + 1].Trim().Split(' ');
                int dinhThuNhat = Int32.Parse(soDong[0].Trim()) - 1;
                int dinhThuHai = Int32.Parse(soDong[1].Trim()) - 1;
                int trungGian = Int32.Parse(soDong[2].Trim());

                danhSachKe[dinhThuNhat].Add(new Tuple<int, int>(dinhThuHai, trungGian));
                danhSachKe[dinhThuHai].Add(new Tuple<int, int>(dinhThuNhat, trungGian));
            }
        }
    }
}