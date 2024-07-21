using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class RawSocket
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
}
