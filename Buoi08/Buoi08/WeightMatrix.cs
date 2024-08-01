using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi08
{
    internal class WeightMatrix
    {
        public int n;
        public int[,] a;
        // Các array là biến toàn cục chỉ phục vụ cho giải thuật
        int[] pre;      // Lưu trữ đỉnh nằm ngay trước trên đường đi
        int[] dist;     // Lưu độ dài ngắn nhất đến các đỉnh
        bool[] visited; // Duyệt đỉnh ghé thăm
        int[,] p;       // Ma trận Lưu trữ đỉnh nằm ngay trước trên đường đi (Floy)
        int[,] d;       // Ma trận Lưu độ dài ngắn nhất đến các đỉnh (Floy)
        // propeties
        public int N { get => n; set => n = value; }
        public int[,] A { get => a; set => a = value; }
        public int[] Dist { get => dist; set => dist = value; }
        public int[] Pre { get => pre; set => pre = value; }
        public int[,] D { get => d; set => d = value; }
        public int[,] P { get => p; set => p = value; }

        // constructor không đối số
        public WeightMatrix() { }
        // constructor có đối số
        // Khởi tạo đồ thị có k đỉnh
        public WeightMatrix(int k)
        {
            n = k;
            a = new int[n, n];
        }
        // Đọc file ra ma trận kề có trọng số
        public void FileToWeightMatrix(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            n = int.Parse(sr.ReadLine());
            a = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                string[] s = sr.ReadLine().Split();
                for (int j = 0; j < n; j++)
                    if (i == j) a[i, j] = 0;
                    else
                        a[i, j] = int.Parse(s[j]) > 0 ? int.Parse(s[j]) : int.MaxValue;
            }
            sr.Close();
        }
        // Xuất Ma trận kề có trọng số lên màn hình
        public void Output()
        {
            Console.WriteLine("Đồ thị ma trận kề - số đỉnh : " + n);
            Console.WriteLine();
            Console.Write(" Đỉnh |");
            for (int i = 0; i < n; i++) Console.Write("    {0}", i);
            Console.WriteLine(); Console.WriteLine("  " + new string('-', 6 * n));
            for (int i = 0; i < n; i++)
            {
                Console.Write("    {0} |", i);
                for (int j = 0; j < n; j++)
                    if (a[i, j] < int.MaxValue) Console.Write("  {0, 3}", a[i, j]);
                    else Console.Write("  {0, 3}", "_");
                Console.WriteLine();
            }
        }
        // Dijkstra : tìm đường đi ngắn nhất đến các đỉnh từ đỉnh s
        public void Dijkstra(int s)
        {
            // pre : Lưu đỉnh nằm trước trên đường từ s đi qua
            pre = new int[n];
            // dist : Lưu độ dài ngán nhất từ s đến các đỉnh còn lại
            dist = new int[n];
            // visited : Đánh dấu đỉnh đã đi qua
            visited = new bool[n];
            // Khởi gán giá trị ban đầu
            for (int i = 0; i < n; i++)
            {
                dist[i] = int.MaxValue;
                visited[i] = false;
                pre[i] = s;
            }
            dist[s] = 0;
            // Duyệt các đỉnh của đồ thị
            for (int i = 0; i < n; i++)
            {
                int u = dmin();     // Gọi u là đỉnh sao cho dist[u] nhỏ nhất
                visited[u] = true;  // Đánh dấu u
                // Duyệt các đỉnh v thuộc Kề(u) (tồn tại cạnh (u, v) )
                for (int v = 0; v < n; v++)
                    // Nếu (v chưa đánh dấu) và (tồn tại cạnh (u,v))
                    if (!visited[v] && a[u, v] != int.MaxValue)
                        // Nếu (dist[u] đã xác định) và (dist[u] + a[u, v] < dist[v])
                        if (dist[u] != int.MaxValue && dist[u] + a[u, v] < dist[v])
                        {
                            dist[v] = dist[u] + a[u, v];    // Đặt dist[v] = dist[u] + a[u, v];
                            pre[v] = u;     // Đặt đỉnh u trước v trên đường đi
                        }
            }
            // Kết quả : xác định giá trị các phần tử của dist[] và pre[]
        }
        // Tìm đỉnh minIndex có dist[minIndex] là nhỏ nhất
        public int dmin()
        {
            int minIndex = 0;   // Giá trị trả về
            int min = int.MaxValue;
            // Tìm min tại các đỉnh chưa xét
            for (int i = 0; i < n; i++)
                if (visited[i] == false && dist[i] < min)
                {
                    min = dist[i];
                    minIndex = i;
                }
            return minIndex;
        }
        // Xuất kết quả dist[] và pre[]
        public void PrintDijkstra(int s)
        {
            Console.WriteLine("Đường đi ngắn nhất từ " + s + " đến các đỉnh còn lại :");
            // Xuất số hiệu các đỉnh
            Console.Write("  Đỉnh : ");
            for (int i = 0; i < n; i++)
                Console.Write("  " + i);
            Console.WriteLine();
            Console.WriteLine("          " + new string('-', 3 * n));
            // Xuất array pre[]
            Console.Write("   pre : ");
            for (int i = 0; i < n; i++)
                Console.Write("{0, 3}", pre[i]);
            Console.WriteLine();
            // Xuất array dist[]
            Console.Write("  dist : ");
            for (int i = 0; i < n; i++)
                if (dist[i] < int.MaxValue) Console.Write("{0, 3}", dist[i]);
                else Console.Write("{0, 3}", "x");
        }
        // Bài 1 : Tìm đường ngắn nhất từ x đến y
        public void MinRouteXY(int x, int y)
        {
            // Chọn x là đỉnh xuất phát
            Dijkstra(x);
            // Kết quả : 2 array pre[] và dist[] có giá trị
            // ?
            Stack<int> st = new Stack<int>();
            int k = y; st.Push(k);
            while (pre[k] != x)
            {
                k = pre[k];
                st.Push(k);
            }
            Console.WriteLine();
            Console.Write(" Đường đi ngắn nhất từ {0} đến {1} : {2} ->", x, y, x);
            while (st.Count > 0)
            {
                k = st.Pop(); Console.Write(" {0} ->", k);
            }
            Console.WriteLine(" độ dài : " + dist[y]);
        }
        // Bài 2: Đường đi ngắn nhất từ x đến z qua đỉnh trung gian y
        public void MinRouteXYZ(int x, int y, int z)
        {
            // Tìm đường đi ngắn nhất từ x đến y : dxy
            // tìm đường đi ngắn nhất từ y đến z : dyz
            // Đường đi ngắn nhất tư x đến z có đi qua y : dxz = dxy + dyz
        }
        // Bài 3 : Đường đi ngắn nhất giữa các cặp đỉnh - giải thuật Floyd
        public void Floyd()
        {
            // khởi tạo giá trị cho d[,], p[,]
            d = new int[n, n];
            p = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    d[i, j] = a[i, j];
                    p[i, j] = i;
                }
            for (int i = 0; i < n; i++)
                d[i, i] = 0;

            for (int k = 0; k < n; k++)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if ((d[i, j] > d[i, k] + d[k, j]) && d[i, k] < int.MaxValue && d[k, j] < int.MaxValue)
                        {
                            d[i, j] = d[i, k] + d[k, j];
                            p[i, j] = p[k, j];
                        }
            //Outdp();
        }
        //Xuất ma trận d và p
        public void Outdp()
        {
            Console.WriteLine(" dist : ");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write("  " + d[i, j]);
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine(" pre : ");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write("  " + p[i, j]);
                Console.WriteLine();
            }
        }
        // Sử dụng 2 ma trận d và p để xuất đường đi ngắn nhất từ x đến y
        public void Floyd_RouteXY(int x, int y)
        {
            // Độ dài ngắn nhất từ x -> y = d[x,y]
            Stack<int> st = new Stack<int>();
            int k = y; st.Push(k);
            while (p[x, k] != x)
            {
                k = p[x, k];
                st.Push(k);
            }
            Console.WriteLine();
            Console.Write(" Đường đi ngắn nhất từ {0} đến {1} : {2} ->", x, y, x);
            while (st.Count > 0)
            {
                k = st.Pop(); Console.Write(" {0} ->", k);
            }
            Console.WriteLine(" độ dài : " + d[x, y]);
        }
        public void ChonXaXayTruong(WeightMatrix g)
        {
            // Khởi tạo biến lưu chi phí tối thiểu và vị trí xã
            int CPmin = int.MaxValue;
            int location = -1;

            // Duyệt qua tất cả các xã
            for (int i = 0; i < g.N; i++)
            {
                // Tính tổng chi phí đưa đón học sinh nếu xây trường ở xã i
                int TongCP = 0;
                for (int j = 0; j < g.N; j++)
                {
                    TongCP += g.D[i, j];
                }

                // Cập nhật vị trí xã và chi phí tối thiểu nếu tìm được xã có chi phí thấp hơn
                if (TongCP < CPmin)
                {
                    CPmin = TongCP;
                    location = i;
                }
            }
            if(location == 0) 
                Console.WriteLine($"Xã được chọn xây trường là xã A và chi phí là {CPmin}");
            else if (location == 1)
                Console.WriteLine($"Xã được chọn xây trường là xã B và chi phí là {CPmin}");
            else if (location == 2)
                Console.WriteLine($"Xã được chọn xây trường là xã C và chi phí là {CPmin}");
            else if (location == 3)
                Console.WriteLine($"Xã được chọn xây trường là xã D và chi phí là {CPmin}");
            else if (location == 4)
                Console.WriteLine($"Xã được chọn xây trường là xã E và chi phí là {CPmin}");
            else if (location == 5)
                Console.WriteLine($"Xã được chọn xây trường là xã F và chi phí là {CPmin}");
            else if (location == 6)
                Console.WriteLine($"Xã được chọn xây trường là xã G và chi phí là {CPmin}");
        }
    }
}
