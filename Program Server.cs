using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Server
{
    static void Main()
    {
        Console.WriteLine("Please enter the IP network:");
        string IPNetwork = Console.ReadLine();
        
        IPAddress ipAddress = IPAddress.Parse(IPNetwork);
        Console.WriteLine("Please enter port network:");
        string PortNetwork = Console.ReadLine();
        string port = PortNetwork;
        int port1 = int.Parse(PortNetwork);
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.Title = "LoPhong Corporation - System chat";

         TcpListener serverSocket = new TcpListener(ipAddress, port1);
        serverSocket.Start();
        Console.WriteLine("Server started!");

        while (true)
        {
            TcpClient clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine("Client has been connected...");

            Thread clientThread = new Thread(() => HandleClient(clientSocket));
            clientThread.Start();
        }
    }

    static void HandleClient(TcpClient clientSocket)
    {
        try
        {
            NetworkStream networkStream = clientSocket.GetStream();
            byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];

            while (true)
            {
                int bytesRead = networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);
                string dataFromClient = Encoding.ASCII.GetString(bytesFrom, 0, bytesRead);
                dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                Console.WriteLine("Client send: " + dataFromClient);

                Console.WriteLine("Enter response to send to client:");
                string serverResponse = Console.ReadLine();

                byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse + "$");
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            clientSocket.Close();
        }
    }
    }

 
  
    
}
