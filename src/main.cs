static async Task Main(string[] args)
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

    Console.WriteLine("MitM attack started.");
    Console.ReadKey();
}
