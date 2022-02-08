using Microsoft.AspNetCore.Mvc;
using Spiracle.NETCore.Models;
using System;
using System.Net;
using System.Net.Sockets;

namespace Spiracle.NETCore.Controllers
{
    public class socketController : Controller
    {
        public IActionResult Index()
        {
            Spiracle.NETCore.Models.SocketModel socketModel = new Spiracle.NETCore.Models.SocketModel();
            return View(socketModel);
        }

        public IActionResult ConnectToServer(SocketModel socketModel)
        {
            string serverIP = socketModel.ServerIP;
            string remotePort = socketModel.ServerPort;
            string result = "";
            Socket sender = null;

            if (String.IsNullOrEmpty(serverIP) || String.IsNullOrEmpty(remotePort))
            {
                socketModel.TextData = "Please enter valid IPv4 IP address and port number to connect to";
                return View("Index", socketModel);
            }

            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(serverIP);  
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, Int32.Parse(remotePort));   
                sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(remoteEP);  
                result += "Socket connected to " + sender.RemoteEndPoint.ToString();      
            }

            catch (Exception e)
            {
                result += e;
            }

            finally
            {
                if (sender != null)
                {
                    sender.Close();
                }
            }
            socketModel.TextData = result;
            return View("Index", socketModel);
        }

        public IActionResult StartServer(SocketModel socketModel)
        {
            string result = "";
            string localServerListeningPort = socketModel.LocalServerListeningPort;
            
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());  
                IPAddress ipAddress = ipHostInfo.AddressList[0];  
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Int32.Parse(localServerListeningPort));  
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);  
    
                listener.Bind(localEndPoint);  
                listener.Listen(10);  
                listener.AcceptAsync(new SocketAsyncEventArgs());

                result += ("Server bound and listening on " + localEndPoint.ToString());
            }
            
            catch (System.Net.Sockets.SocketException socketException)
            {
                result += socketException + System.Environment.NewLine;
                if (!socketException.Message.Contains("Unknown socket error"))
                {
                    result += System.Environment.NewLine + "Try a different port number, or restart Spiracle application if 'Address already in use' SocketException occurs";
                }
            }
            
            catch (Exception e)
            {
                result += e;
            }

            socketModel.TextDataServer = result;
            return View("Index", socketModel);
        }
    }
}