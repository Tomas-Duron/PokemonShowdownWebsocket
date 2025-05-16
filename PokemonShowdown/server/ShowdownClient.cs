using System;
using System.Net.WebSockets;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PokemonShowdown.Utils;

namespace PokemonShowdown.ShowdownClient
{
    public class PSClient
    {
        private static readonly Uri Uri = new Uri("wss://sim3.psim.us/showdown/websocket");
        private ClientWebSocket Socket;
        private bool IsLoggedIn;
        public PSClient(bool _IsLoggedIn)
        {
            this.IsLoggedIn = _IsLoggedIn;
            this.Socket = new ClientWebSocket();
        }

        public async Task Login()
        {
            await ConnectAsync(Socket);
            if (Socket.State == WebSocketState.Open)
            {
                await ReceiveLoop(Socket);
            }
            else { Console.WriteLine("Something Broke"); }
        }

        async Task ConnectAsync(ClientWebSocket socket)
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Console.WriteLine("Connecting to " + Uri.ToString());
                await Socket.ConnectAsync(Uri, CancellationToken.None);
                stopwatch.Stop();
                Console.WriteLine("Connected to " + Uri.ToString() + " after " + stopwatch.ElapsedMilliseconds + "ms\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        async Task ReceiveLoop(ClientWebSocket socket)
        {
            byte[] buffer = new byte[4096];

            while (Socket.State == WebSocketState.Open)
            {
                var res = await Socket.ReceiveAsync(buffer, CancellationToken.None);

                switch (res.MessageType)
                {
                    case WebSocketMessageType.Close:
                        Console.WriteLine("Websocket Closed.");
                        await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                        break;
                    case WebSocketMessageType.Text:
                        string msg = Encoding.UTF8.GetString(buffer, 0, res.Count);
                        Console.WriteLine(msg + "\n");
                        if (!IsLoggedIn)
                        {
                            Console.WriteLine("Need to log in");
                        }
                        break;
                    case WebSocketMessageType.Binary:
                        break;


                    default:
                        break;
                        
                }
            }
        }

        static async Task SendAsync(ClientWebSocket socket, string message)
        {

        }
    }
}
