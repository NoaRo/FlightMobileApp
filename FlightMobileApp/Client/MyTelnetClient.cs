using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;





using System.Net.Sockets;
using System.Text;

namespace FlightMobileApp.Client
{
    public class MyTelnetClient:ITelnetClient
    {
        private TcpClient telnetClient;

        public void connect(string ip, int port)
        {
            // Try to create TcpClient.
            try
            {
                telnetClient = new TcpClient();
            }
            catch
            {
                // If something got wrong- dont do anything (the model will send a message).
            }

            // Define timeout
            telnetClient.SendTimeout = 10000;
            telnetClient.ReceiveTimeout = 10000;

            // Connect to server.
            telnetClient.Connect(ip, port);
        }

        public void write(string command)
        {
            // Convert the message to byte array and send it to server.
            byte[] message;
            message = Encoding.ASCII.GetBytes(command);
            telnetClient.GetStream().Write(message, 0, message.Length);
        }

        public string read()
        {
            // Read the message from the server to byte array and Convert it to string.
            int numberOfBytesRead;
            byte[] myReadBuffer = new byte[1024];
            string myCompleteMessage;
            numberOfBytesRead = telnetClient.GetStream().Read(myReadBuffer, 0, myReadBuffer.Length);
            myCompleteMessage = Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead);
            return myCompleteMessage;
        }

        public void disconnect()
        {
            // Close the client.
            telnetClient.Close();
        }
    }
}
