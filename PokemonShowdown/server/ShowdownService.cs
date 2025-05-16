using System;
using System.Net.WebSockets;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PokemonShowdown.Utils;

class ShowdownService
{
    private static readonly Uri ServerUri = new Uri("wss://sim3.psim.us/showdown/websocket");
    private bool isLoggedIn;
    public ShowdownService()
    {
        
    }

    static async Task Main(string[] args)
    {
        var socket = new ClientWebSocket();
        
        await ConnectAsync(socket);
        await ReceiveLoop(socket);
    }

    static async Task ConnectAsync(ClientWebSocket socket)
    {
        try
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Connecting to " + ServerUri.ToString());
            await socket.ConnectAsync(ServerUri, CancellationToken.None);
            stopwatch.Stop();
            Console.WriteLine("Connected to " + ServerUri.ToString() + " after " + stopwatch.ElapsedMilliseconds + "ms");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    static async Task ReceiveLoop(ClientWebSocket socket)
    {
        byte[] buffer = new byte[4096];

        while (socket.State == WebSocketState.Open)
        {
            var res = await socket.ReceiveAsync(buffer, CancellationToken.None);

            switch (res.MessageType)
            {
                case WebSocketMessageType.Close:
                    Console.WriteLine("Websocket Closed.");
                    await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                    break;

                default:
                    string msg = Encoding.UTF8.GetString(buffer, 0, res.Count);
                    Console.WriteLine(msg);
                    //if (isLoggedIn && msg.Contains("|challstr|"))
                    //{

                    //}
                    break;
            }
        }
    }

    static async Task SendAsync(ClientWebSocket socket, string message)
    {
        
    }
}
