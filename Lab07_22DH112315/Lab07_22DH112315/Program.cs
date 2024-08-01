using System;

namespace Lab07_22DH112315
{
    class Program
    {
        static void Main(string[] args)
        {
            graph Graph = new graph();
            Console.WriteLine("Bai 02");
            Graph.DuongDiNganNhatQuaTrungGian("NganNhatX.INP"); //Bài 02
            Console.ReadKey();
        }
    }
}
