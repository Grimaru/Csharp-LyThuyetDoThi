using System;

namespace KiemTra02_22DH112315
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            Console.WriteLine("Bai 1: ");
            graph.CanhCauBangBFS("CanhCau.INP"); //Bài 01
            Console.WriteLine();
            Console.WriteLine("Bai 2: ");
            graph.TimDuongDiDFS("TimDuongDFS.INP"); //Bài 02
            Console.WriteLine();
            Console.WriteLine("Bai 3: ");
            graph.ChuTrinh("ChuTrinh.INP"); //Bài 03
            Console.WriteLine();
        }
    }
}
