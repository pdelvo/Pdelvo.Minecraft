using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdelvo.Minecraft.Protocol.Packets
{
    /// <summary>
    /// Defines a base interface for all packets that are entity related
    /// </summary>
    public interface IEntityPacket
    {
        /// <summary>
        /// The Entity ID
        /// </summary>
        int EntityId { get; set; }
    }
}
