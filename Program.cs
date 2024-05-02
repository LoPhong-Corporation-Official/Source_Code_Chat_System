using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main(string[] args)
    {
        try
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.1.4");
            int port = 50795;

            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect(ipAddress, port);
            NetworkStream networkStream = clientSocket.GetStream();

            while (true)
            {
                Console.WriteLine("Enter data to send to Server:");
                string message = Console.ReadLine();

                byte[] sendBytes = Encoding.ASCII.GetBytes(message + "$");
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();

                byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                int bytesRead = networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);
                string dataFromServer = Encoding.ASCII.GetString(bytesFrom, 0, bytesRead);
                Console.WriteLine("Server response:" + dataFromServer);
            }

            clientSocket.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
