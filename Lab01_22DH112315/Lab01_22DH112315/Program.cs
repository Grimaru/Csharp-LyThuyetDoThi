using System;

namespace Lab01_22DH112315
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            //graph.VertexDegree("BacDoThiVoHuong.INP"); //Bài 01
            //Console.WriteLine();
            //graph.InOutDegrees("BacVaoRa.INP"); //Bài 02
            //Console.WriteLine();
            graph.VertexDegreeAl("DanhSachKe.INP"); //Bài 03
            Console.WriteLine();
            //graph.VertexDegreesListEdge("DanhSachCanh.INP"); //Bài 04
            //Console.ReadKey();
        }
    }
}
