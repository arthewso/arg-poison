private static (byte[] request, byte[] response) ModifyHttpPacket(byte[] packet)
{
    // Parse the packet to find the request and response parts
    // This is a simplified version and may not work for all packets

    var request = new byte[100];
    var response = new byte[100];

    // Modify the response to include a custom payload
    Array.Copy(packet, 0, request, 0, 100);
    Array.Copy(packet, 100, response, 0, 100);
    Array.Copy(Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\r\nContent-Length: 12\r\n\r\nFuck You"), 0, response, 0, 27);

    return (request, response);
}
