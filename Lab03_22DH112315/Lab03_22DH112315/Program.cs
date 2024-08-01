using System;

namespace Lab03_22DH112315
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            Console.WriteLine("Bai 1");
            graph.LietKeCacDinhLienThong("BFS.INP"); //Bài 1
            Console.WriteLine();
            Console.WriteLine("Bai 2");
            graph.TimDuongDi("TimDuong.INP"); //Bài 2
            Console.WriteLine();
            Console.WriteLine("Bai 3");
            graph.KiemTraDoThiLienThong("LienThong.INP"); //Bài 3
            Console.WriteLine();
            Console.WriteLine("Bai 4");
            graph.DemSoMienLienThong("DemLienThong.INP"); //Bài 4
            Console.WriteLine();
            Console.WriteLine("Bai tap them 1");
            graph.MaTranKeSangDanhSachKe("MaTranKeToDanhSachKe.INP"); //Bài tập thêm 1
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
