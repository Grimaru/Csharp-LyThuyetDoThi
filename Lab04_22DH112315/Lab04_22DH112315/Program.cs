using System;

namespace Lab04_22DH112315
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            Console.WriteLine("Bai 1");
            graph.LietKeCacMienLienThong("MienLienThong.INP"); //Bài 1
            Console.WriteLine();
            Console.WriteLine("Bai 2");
            graph.CanhCauBangBFS("CanhCau.INP"); //Bài 2
            Console.WriteLine();
            Console.WriteLine("Bai 3");
            graph.TimDinhKhopBangBFS("DinhKhop.INP"); //Bài 3
            Console.WriteLine();
            Console.WriteLine("Bai 4");
            graph.DiTrenLuoiBangBFS("Grid.INP"); //Bài 4
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
