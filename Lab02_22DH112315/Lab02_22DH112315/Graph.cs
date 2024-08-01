using System;
using System.Collections.Generic;

namespace Lab02_22DH112315
{
    internal class Graph
    {
        //So dinh cua ma tran
        public int N { get; set; }

        //So canh cua ma tran
        public int M { get; set; }

        //Danh sach canh
        LinkedList<Tuple<int, int>> DanhSachCanh;

        //Danh sach ke
        LinkedList<int>[] DanhSachKe;

        //Bai 01
        internal void ChuyenDanhSachCanhThanhDanhSachKe(string fname)
        {
            ReadDanhSachCanh(fname);

            ChuyenDanhSachCanhThanhDanhSachKe();

            WriteDanhSachKe(fname.Substring(0,fname.Length - 3) + "OUT");
        }

        private void WriteDanhSachKe(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(String.Format("{0, -3}", N));
                for(int i = 0; i < N; i++)
                {
                    foreach (int v in DanhSachKe[i])
                    {
                        file.Write(String.Format("{0, -3}", v));
                    }
                    file.WriteLine();
                }
            }
        }

        private void ChuyenDanhSachCanhThanhDanhSachKe()
        {
            DanhSachKe = new LinkedList<int>[N];
            for(int i = 0; i < N; i++)
            {
                DanhSachKe[i] = new LinkedList<int>();
            }
            foreach(Tuple<int, int> e in DanhSachCanh)
            {
                DanhSachKe[e.Item1 - 1].AddLast(e.Item2);
                DanhSachKe[e.Item2 - 1].AddLast(e.Item1);
            }
        }

        private void ReadDanhSachCanh(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            M = Int32.Parse(line[1].Trim());
            Console.WriteLine($"So dinh cua ma tran: {N}" );
            DanhSachCanh = new LinkedList<Tuple<int, int>>();

            for(int i = 0; i < M; i++)
            {
                line = lines[i + 1].Split(' ');
                DanhSachCanh.AddLast(new Tuple<int, int>(Int32.Parse(line[0].Trim()), Int32.Parse(line[1].Trim())));
            }
        }

        //Bai 03
        int[,] MaTranKe;
        internal void BonChua(string fname)
        {
            ReadMaTranKe(fname);
            LinkedList<int> bonChua = TimBonChua();
            WriteBonChua(fname.Substring(0, fname.Length - 3) + "OUT", bonChua);
        }

        protected LinkedList<int> TimBonChua()
        {
            LinkedList<int> list = new LinkedList<int>();

            for(int i = 0; i < N; i++)
            {
                int bacVao = 0;
                int bacRa = 0;
                for(int j = 0; j < N; j++)
                {
                    bacVao += MaTranKe[j, i];
                    bacRa += MaTranKe[i, j];
                }
                if(bacVao > 0 && bacRa == 0)
                {
                    list.AddLast(i + 1);
                }
            }
            return list;
        }

        private void WriteBonChua(string fname, LinkedList<int> list)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                file.WriteLine(list.Count);
                if (list.Count < 1) return;
                foreach(int v in list) file.Write(String.Format("{0, -3}", v));
            }
        }

        private void ReadMaTranKe(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            N = Int32.Parse(lines[0].Trim());
            Console.WriteLine($"So dinh cua ma tran ke: {N}");
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

        //Bai 04
        internal void DoThiChuyenVi(string fname)
        {
            ReadDanhSachKe(fname);
            List<List<int>> chuyenVi = ChuyenVi();
            WriteDanhSachKe(fname.Substring(0, fname.Length - 3) + "OUT", chuyenVi);
        }

        protected void WriteDanhSachKe(string fname, List<List<int>> chuyenVi)
        {           
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
                {
                    file.WriteLine(N);
                    foreach(List<int> list in chuyenVi)
                    {
                        foreach (int v in list)
                        {
                            file.Write(String.Format("{0, -3}", v));
                        }
                        file.WriteLine();
                    }
                }
        }

        protected List<List<int>> ChuyenVi()
        {
            List<List<int>> DoThiChuyenVi = new List<List<int>>();
            for(int i = 0; i < N; i++)
            {
                DoThiChuyenVi.Add(new List<int>());
            }
            for(int i = 0; i < N; i++)
            {
                foreach (int v in DanhSachKe[i])
                {
                    DoThiChuyenVi[v - 1].Add(i + 1);
                }
            }
            return DoThiChuyenVi;
        }

        private void ReadDanhSachKe(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);
            N = Int32.Parse(lines[0].Trim());
            Console.WriteLine($"So dinh cua danh sach ke: {N}");
            DanhSachKe = new LinkedList<int>[N];
            for (int i = 0; i < N; i++)
            {
                DanhSachKe[i] = new LinkedList<int>();
                if (lines[i + 1].Length == 0) continue;
                string[] line = lines[i + 1].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int n = Int32.Parse(line[j].Trim());
                    DanhSachKe[i].AddLast(n);
                }
            }
        }


        //Bai 05
        LinkedList<Tuple<int, int, int>> DanhSachCanhCoTrongSo;
        internal void TrungBinhCanh(string fname)
        {
            ReadDanhSachCanhCoTrongSo(fname);
            WriteTrungBinhCanh(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void WriteTrungBinhCanh(string fname)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname))
            {
                int max = 0;
                double trungBinh = 0;
                foreach(Tuple<int, int, int> e in DanhSachCanhCoTrongSo)
                {
                    trungBinh += e.Item3;
                    if (e.Item3 > max)
                    {
                        max = e.Item3;
                    }
                }
                trungBinh /= DanhSachCanhCoTrongSo.Count;
                int count = 0;
                string list = "";
                foreach(Tuple<int, int, int> e in DanhSachCanhCoTrongSo)
                {
                    if (e.Item3 == max)
                    {
                        list += string.Format("\n{0, -3}{1, -3}{2, -3}", e.Item1, e.Item2, e.Item3);
                        count++;
                    }
                }
                file.WriteLine(String.Format($"{trungBinh:0.00}"));
                file.Write(String.Format("{0, -3}", count));
                file.Write(list);


            }
        }

        private void ReadDanhSachCanhCoTrongSo(string fname)
        {
            string[] lines = System.IO.File.ReadAllLines(fname);

            string[] line = lines[0].Split(' ');
            N = Int32.Parse(line[0].Trim());
            M = Int32.Parse(line[1].Trim());
            Console.WriteLine($"So dinh cua ma tran: {N} ");
            DanhSachCanhCoTrongSo = new LinkedList<Tuple<int, int, int>>();

            for (int i = 0; i < M; i++)
            {
                line = lines[i + 1].Split(' ');
                DanhSachCanhCoTrongSo.AddLast(new Tuple<int, int, int>(Int32.Parse(line[0].Trim()), Int32.Parse(line[1].Trim()), Int32.Parse(line[2].Trim())));
            }
        }

        //Bai tap them 01
        internal void ChuyenMaTranKeSangDanhSachKe(string fname)
        {
            ReadMaTranKe(fname);
            ChuyenMaTranKeSangDanhSachKe();
            WriteDanhSachKe(fname.Substring(0, fname.Length - 3) + "OUT");
        }

        private void ChuyenMaTranKeSangDanhSachKe()
        {
            DanhSachKe = new LinkedList<int>[N];

            for (int i = 0; i < N; i++)
            {
                DanhSachKe[i] = new LinkedList<int>();

                for (int j = 0; j < N; j++)
                {
                    if (MaTranKe[i, j] > 0)
                    {
                        DanhSachKe[i].AddLast(j + 1);
                    }
                }
            }
        }




    }
}