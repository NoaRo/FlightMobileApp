using FlightMobileApp.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FlightMobileApp.Models
{
    public class CommandManager
    {
        ITelnetClient telnetClient;

        public CommandManager(ITelnetClient telnetClient)
        {
            this.telnetClient = telnetClient;
            try
            {
                telnetClient.connect("127.0.0.1", 5402);
            }
            catch
            {

            }

        }
        public void SendCommand(Command command)
        {
            string messageFromSimulator;
            telnetClient.write("set /controls/flight/aileron "+command.Aileron.ToString()+"\r\n");
            try
            {
                telnetClient.write("get /controls/flight/aileron\r\n");
                try
                {
                    messageFromSimulator = telnetClient.read();
                    if (messageFromSimulator.Contains("ERR") || Double.Parse(messageFromSimulator)!=command.Aileron)
                    {

                    }
                }
                catch
                {
                }
            }
            catch
            {
            }
            telnetClient.write("set /controls/engines/current-engine/throttle "+command.Throttle.ToString()+"\r\n");
            try
            {
                telnetClient.write("get /controls/engines/current-engine/throttle\r\n");
                try
                {
                    messageFromSimulator = telnetClient.read();
                    if (messageFromSimulator.Contains("ERR") || Double.Parse(messageFromSimulator) != command.Throttle)
                    {

                    }
                }
                catch
                {
                }
            }
            catch
            {
            }
            telnetClient.write("set /controls/flight/rudder " + command.Rudder.ToString() + "\r\n");
            try
            {
                telnetClient.write("get /controls/flight/rudder\r\n");
                try
                {
                    messageFromSimulator = telnetClient.read();
                    if (messageFromSimulator.Contains("ERR") || Double.Parse(messageFromSimulator) != command.Rudder)
                    {

                    }
                }
                catch
                {
                }
            }
            catch
            {
            }
            telnetClient.write("set /controls/flight/elevator " + command.Elevator.ToString() + "\r\n");
            try
            {
                telnetClient.write("get /controls/flight/elevator\r\n");
                try
                {
                    messageFromSimulator = telnetClient.read();
                    if (messageFromSimulator.Contains("ERR") || Double.Parse(messageFromSimulator) != command.Elevator)
                    {

                    }
                }
                catch
                {
                }
            }
            catch
            {
            }

        }
        protected virtual async Task<Byte[]> GetScreenShot()
        {
            // Create the request.
            string strurl = string.Format("get HOST:5000/screenshot");
            HttpWebRequest lxRequest = (HttpWebRequest)WebRequest.Create(strurl);
            lxRequest.Timeout = 10000;
            lxRequest.Method = "GET";
            // returned values are returned as a stream, then read into a string
            String lsResponse = string.Empty;
            Byte[] lnByte;
            // Get the response from server.
            using (HttpWebResponse lxResponse = (HttpWebResponse) await lxRequest.GetResponseAsync())
            {
                // Return response to image.
                using (BinaryReader reader = new BinaryReader(lxResponse.GetResponseStream()))
                {
                    lnByte = reader.ReadBytes(1 * 1024 * 1024 * 10);
                    using (FileStream lxFS = new FileStream("screenshot.jpg", FileMode.Create))
                    {
                        lxFS.Write(lnByte, 0, lnByte.Length);
                    }
                }
            }
            return lnByte;
        }
    }
}
