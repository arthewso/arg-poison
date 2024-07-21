private static bool IsHttpPacket(byte[] packet)
{
    // Check if the packet starts with "GET" or "POST" to identify HTTP packets
    // This is a simplified version and may not work for all packets

    return packet.Length >= 4 && Encoding.ASCII.GetString(packet, 0, 4) == "GET " || packet.Length >= 5 && Encoding.ASCII.GetString(packet, 0, 5) == "POST";
}
