using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Create a form to enter information
            Console.WriteLine("Please enter your nickname:");
            Console.BackgroundColor = ConsoleColor.Red;
            string Nickname  = Console.ReadLine();
            Console.Title = "Client of " + Nickname;
            Console.WriteLine("Hello " + Nickname + "!"); 
            Console.BackgroundColor = ConsoleColor.Black;
            
            Console.WriteLine("Please enter your IP network:");
            string IPNetwork = Console.ReadLine();
            
            Console.WriteLine("Please enter your port network:");
            string port = Console.ReadLine();
            
            int PortNetwork = int.Parse(port);
            TcpClient clientSocket = new TcpClient();
            clientSocket.Connect(IPNetwork, PortNetwork);
            NetworkStream networkStream = clientSocket.GetStream();
            
                while (true)
            {
                Console.WriteLine("Enter message to send to server:");
                string ClientResponse = Console.ReadLine();
                byte[] sendbytes = Encoding.ASCII.GetBytes(ClientResponse + "$");
                networkStream.Write(sendbytes, 0, sendbytes.Length);
                networkStream.Flush();
                byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                int byteread = networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);
                string DataFromServer = Encoding.ASCII.GetString(bytesFrom,0, byteread);    
                Console.WriteLine("Server responses: " + DataFromServer);
            }
            clientSocket.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
