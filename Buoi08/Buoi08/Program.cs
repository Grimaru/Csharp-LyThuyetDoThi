
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Xuất text theo Unicode (có dấu tiếng Việt)
            Console.OutputEncoding = Encoding.Unicode;
            // Nhập text theo Unicode (có dấu tiếng Việt)
            Console.InputEncoding = Encoding.Unicode;

            /* Tạo menu */
            Menu menu = new Menu();
            string title = "ĐƯỜNG ĐI NGẮN NHẤT";   // Tiêu đề menu
            // Danh sách các mục chọn
            string[] ms = { "1. Bài 1: Bài toán đi ra biên",
                "2. Bài 2: Chọn thành phố để họp",
                "3. Bài 3 : Công trình.",
                "4. Bài 4: Xây dựng trường học",
                "0. Thoát" };
            int chon;
            do
            {
                Console.Clear();
                // Xuất menu
                menu.ShowMenu(title, ms);
                Console.Write("     Chọn : ");
                chon = int.Parse(Console.ReadLine());
                switch (chon)
                {
                    case 1:
                        {
                            // Bài 5 : Bài toán đi ra biên.
                            // Có các hàm trình bày trong Program
                            int[,] mt;
                            Console.WriteLine();
                            // Đọc file --> ma trận a[,]
                            string fileName = "../../TextFile/Matran.txt";
                            mt = FileToMatrix(fileName);
                            // Xuất ma trận mt
                            Console.WriteLine("  Ma trận a :"); PrintMatrix(mt);

                            // Đọc ma trận mt[,] --> đồ thị ma trận kề g
                            WeightMatrix g = new WeightMatrix();
                            g = MatrixToWeightMatrix(mt);
                            Console.WriteLine();
                            //g.Output();

                            // Nhập tọa độ ô xuất phát s(x,y)
                            Console.WriteLine("  Nhập tọa độ ô xuất phát s(x,y) :");
                            Console.Write("  x = ");
                            int x = int.Parse(Console.ReadLine());
                            Console.Write("  y = ");
                            int y = int.Parse(Console.ReadLine());

                            if (x == 0 || y == 0 || x == mt.GetLength(0) - 1 || y == mt.GetLength(1) - 1)
                                Console.WriteLine("  ({0},{1}) là đỉnh nằm trên biên.", x, y);
                            else
                            {
                                // Chuyển đổi tọa độ (x,y) --> đỉnh s
                                int s = x * mt.GetLength(1) + y;
                                // Gọi Dijkstra từ đỉnh s
                                g.Dijkstra(s);
                                // Tìm đường đi ra biên
                                ToBorderline(g, s);
                            }
                            break;
                        }
                    case 2:
                        {
                            // Bài 6 : Chọn thành phố để họp
                            string filePath = "../../TextFile/SelectCity.txt";
                            WeightMatrix g = new WeightMatrix();
                            g.FileToWeightMatrix(filePath); g.Output();
                            Console.WriteLine();
                            // city : thành phố được chọn, Select(g) : tình thành phố
                            int city;
                            city = SelectCity(g);
                            Console.WriteLine("   Thành phố được chọn : {0}", city);
                            break;
                        }
                    case 3:
                        {
                            // Bài 7 : Công trình
                            // Đọc file Congtrinh.txt ra đồ thị ma trận kề g, xuất g
                            string filePath = "../../TextFile/Congtrinh.txt";
                            WeightMatrix g = new WeightMatrix();
                            g.FileToWeightMatrix(filePath); g.Output();
                            Console.WriteLine();
                            // thay đổi giá trị g.A[i,j] = -g.A[i,j] nếu g.A[i,j] khác vô cùng
                            for (int i = 0; i < g.N; i++)
                                for (int j = 0; j < g.N; j++)
                                    if (g.A[i, j] < int.MaxValue)
                                        g.A[i, j] = -g.A[i, j];
                            // Cho g.A[7, 9] = g.A[8, 9] = 0;
                            g.A[7, 9] = g.A[8, 9] = 0;
                            // Chạy thuật toán Floyd
                            g.Floyd();
                            // Xuất kết quả : -g.D[0,9] 
                            Console.WriteLine();
                            Console.WriteLine("  Thời gian sớm nhất hoàn thành công trình : {0}", -g.D[0, 9]);
                            break;
                        }
                    case 4:
                        {
                            // Chọn xã
                            // Đọc file Congtrinh.txt ra đồ thị ma trận kề g, xuất g
                            string filePath = "../../TextFile/ChonXa.txt";
                            WeightMatrix g = new WeightMatrix();
                            g.FileToWeightMatrix(filePath); g.Output();
                            Console.WriteLine();
                            g.Floyd();
                            g.ChonXaXayTruong(g);
                            Console.WriteLine();
                            
                            break;
                        }
                }
                Console.ReadKey();
            } while (chon != 0);
        }
        static int[,] FileToMatrix(string fileName)
        {
            int[,] mt;
            // Doc file Matran.txt -> Ma tran a
            StreamReader sr = new StreamReader(fileName);
            string[] s = sr.ReadLine().Split();
            int row = int.Parse(s[0]);
            int col = int.Parse(s[1]);

            mt = new int[row, col];
            for (int i = 0; i < row; i++)
            {
                s = sr.ReadLine().Split();
                for (int j = 0; j < col; j++)
                    mt[i, j] = int.Parse(s[j]);
            }
            sr.Close();
            return mt;
        }
        // Xuất ma trận a -> màn hình
        static void PrintMatrix(int[,] a)
        {
            Console.WriteLine("      0   1   2   3   4   5");
            Console.WriteLine("    -------------------------");
            for (int i = 0; i < a.GetLength(0); i++)
            {
                Console.Write(" " + i + "|");
                for (int j = 0; j < a.GetLength(1); j++)
                    Console.Write("{0, 4}", a[i, j]);
                Console.WriteLine("  |");
                Console.WriteLine("  |");
            }
            Console.WriteLine("    -------------------------");
        }
        // Đọc ma trận a --> WeightList g. Hàm trả về một đồ thị danh sách kề
        static WeightMatrix MatrixToWeightMatrix(int[,] mt)
        {
            // Khai báo đồ thị ma trận kề
            WeightMatrix g = new WeightMatrix();
            // Xác định số dòng (row), số cột (col)của ma trận mt
            int row = mt.GetLength(0);
            int col = mt.GetLength(1);
            // Xác định số đỉnh (g.N) của đồ thị và khở tạo g.A
            g.N = row * col;
            g.A = new int[g.N, g.N];
            // Khởi tạo giá trị ban đầu cho g.A với ô (i,i) = 0, còn lại = int.MaxValue
            for (int i = 0; i < g.N; i++)
                for (int j = 0; j < g.N; j++)
                    if (i == j) g.A[i, j] = 0;
                    else g.A[i, j] = int.MaxValue;
            // Duyệt từng ô của ma trận mt : i:0 -> <row, j:0 -> < col
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    // với mỗi ô (x,y) xác định đỉnh k tương ứng
                    int k = i * col + j;
                    // Mỗi một ô có tối đa 4 ô kề -> đỉnk k có tối đa 4 đỉnh kề -> tìm các đỉnh kề cho k
                    // Có 4 trường hợp như sau : i > 0, j > 0, j < col-1, i < row-1
                    if (i > 0)
                        g.A[k, k - col] = mt[i - 1, j];

                    if (j > 0)
                        g.A[k, k - 1] = mt[i, j - 1];

                    if (j < col - 1)
                        g.A[k, k + 1] = mt[i, j + 1];

                    if (i < row - 1)
                        g.A[k, k + col] = mt[i + 1, j];
                }
            // trả về g
            return g;
        }
        // Xuất đường đi ra biên từ ô s(x,y) ngắn nhất. Ô nằm trên biên có bậc < 4 (có dưới 4 đỉnh kề
        static void ToBorderline(WeightMatrix g, int s)
        {
            // Sử dụng Stack<int> st -> khởi tạo st
            Stack<int> st = new Stack<int>();
            // Biến bien : lưu trữ đỉnh nằm trên biên cần tìm, ban đầu = 0
            int bien = 0;
            // min : lưu giá trị độ dài đường đi ra biên ngắn nhất, ban đầu = int.MaxValue
            int min = int.MaxValue;
            // Xét tất cả các đường từ (x,y) ra biên để chọn đường ngắn nhất.
            // (Đỉnh ở biên có Deg < 4 -> viết thêm hàm DegV(i) tính bậc đỉnh i)
            // g.Dist : chứa các độ dài đến các đỉnh, g.Pre : chứa nội dung -> đường đi
            for (int i = 0; i < g.N; i++)
                if (DegV(i, g) < 4)     // Chỉ xét các đỉnh i có dưới 4 đỉnh kề -> bậc < 4
                    if (g.Dist[i] < min)
                    {
                        min = g.Dist[i];
                        bien = i;
                    }
            Console.WriteLine();
            // Đến đây biến bien lưu trữ đỉnh nằm trên biên gần nhất
            // Xuất đường đi ra biên : từ dinhS --> đỉnh bien và độ dài là bài toán xuất đường đi
            while (bien != s)
            {
                st.Push(bien);
                bien = g.Pre[bien];
            }
            Console.Write("Đường ra biên gần nhất, xuất phát từ đỉnh s({0}) : ", s);
            Console.ForegroundColor = ConsoleColor.Cyan;
            while (st.Count > 0) Console.Write(" -> " + st.Pop());
            Console.WriteLine("  Có độ dài = " + min);
            Console.ForegroundColor = ConsoleColor.White;
        }
        // Hàm Tính bậc của đỉnh v,lưu ý loại trừ các giá trị đặc biệt trong g.A
        static int DegV(int v, WeightMatrix g)
        {
            int deg = 0;
            for (int i = 0; i < g.N; i++)
                if (g.A[v, i] < int.MaxValue && g.A[v, i] != 0) deg++;
            return deg;
        }
        // Bài 6 : chọn thành phố
        static int SelectCity(WeightMatrix g)
        {
            // Chạy thuật toán Floyd -> ma trận d
            g.Floyd();
            // Biến cty : lưu thành phố được chọn, khởi tạo = -1
            int cty = -1;
            // Biến min tham gia tìm, khởi tạo = int.MaxValue;
            int min = int.MaxValue;
            // Duyệt (i : 0 -> < g.N)
            for (int i = 0; i < g.N; i++)
            {
                //Biến max : đường dài nhất khi chọn họp thành phố i
                int max = -int.MaxValue;
                // Duyệt (j : 0 -> g.N)
                for (int j = 0; j < g.N; j++)
                    // Nếu g.D[i, j] > max thì max = g.D[i, j]
                    if (g.D[i, j] > max)
                        max = g.D[i, j];
                // Nếu max < min
                if (max < min)
                {
                    // cty = i và min = max;
                    cty = i;
                    min = max;
                }
            }
            // Trả về cty
            return cty;
        }
    }
}