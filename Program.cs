using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

public class Client
{

    private const int BUFFER_SIZE = 1024;
    private const int PORT_NUMBER = 8080;

    static ASCIIEncoding encoding = new ASCIIEncoding();

    public static void Main()
    {

        try
        {
            TcpClient client = new TcpClient();

            // 1. connect
            client.Connect("127.0.0.1", PORT_NUMBER);
            Stream stream = client.GetStream();

            Console.WriteLine("Đã kết nối với Máy chủ.");
            
            while (true)
            {

                
                var reader = new StreamReader(stream);
                var writer = new StreamWriter(stream);
                writer.AutoFlush = true;

                Console.WriteLine("Please enter the number (0-10) / dig[domain.com] / curl[domain.com/myfile.htm] : ");

                // 2. send
                string str = Console.ReadLine();
                writer.WriteLine(str);
                writer.AutoFlush = true;

                // 3. receive
                str = reader.ReadLine();
                Console.WriteLine(str);
                if (str.ToUpper() == "BYE")
                    break;
            }                                                                          
            // 4. close
            stream.Close();
            client.Close();
        }

        catch (Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(";Ngắt kết nối tại: " + DateTime.Now + ";" + "Lý do: Đóng bởi máy chủ\n");
            File.AppendAllText("D://Access.log", sb.ToString());
            sb.Clear();
            Console.WriteLine("Lỗi: " + ex);
        }

        Console.Read();
    }
}
