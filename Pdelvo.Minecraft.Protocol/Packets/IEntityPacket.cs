namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    ///   Defines a base interface for all packets that are entity related
    /// </summary>
    public interface IEntityPacket
    {
        /// <summary>
        ///   The Entity ID
        /// </summary>
        int EntityId { get; set; }
    }
}