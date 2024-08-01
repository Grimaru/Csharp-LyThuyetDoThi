using System;

namespace KiemTra01_22DH112315
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            Console.WriteLine("Bai 1");
            graph.VertexDegreeAl("DanhSachKe.INP"); //Bài 1
            Console.WriteLine();
            Console.WriteLine("Bai 2");
            graph.ChuyenDanhSachKeThanhDanhSachCanh("DSKe2Canh.INP"); //Bài 2
            Console.WriteLine();
            Console.WriteLine("Bai 3");
            graph.DemSoMienLienThong("DemLienThong.INP"); //Bài 3
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
