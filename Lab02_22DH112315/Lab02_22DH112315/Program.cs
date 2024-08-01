using System;

namespace Lab02_22DH112315
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();

            Console.WriteLine("Bai 01");
            graph.ChuyenDanhSachCanhThanhDanhSachKe("Canh2Ke.INP"); //Bai 01
            Console.WriteLine();

            Console.WriteLine("Bai 03");
            graph.BonChua("BonChua.INP"); //Bai 03
            Console.WriteLine();

            Console.WriteLine("Bai 04");
            graph.DoThiChuyenVi("ChuyenVi.INP"); //Bai 04
            Console.WriteLine();

            Console.WriteLine("Bai 05");
            graph.TrungBinhCanh("TrungBinhCanh.INP"); //Bai 05
            Console.WriteLine();

            Console.WriteLine("Bai tap them 01");
            graph.ChuyenMaTranKeSangDanhSachKe("MaTranKeThanhDanhSachKe.INP"); //Bai tap them 01
            Console.ReadKey();
        }
    }
}
