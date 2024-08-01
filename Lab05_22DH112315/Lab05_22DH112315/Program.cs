using System;

namespace Lab05_22DH112315
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            graph.LietKeCacDinhLienThongDFS("LienThongDFS.INP"); //Bài 1
            graph.TimDuongDiDFS("TimDuongDFS.INP"); //Bài 2
        }
    }
}
