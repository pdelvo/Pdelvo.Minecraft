using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pdelvo.Minecraft.Protocol
{
    /// <summary>
    /// Get information about the runtime the library is used in
    /// </summary>
    public static class RuntimeInfo
    {
        /// <summary>
        /// True if the runtime is mono, otherwise false
        /// </summary>
        public static bool IsMono
        {
            get
            {
                return Type.GetType("Mono.Runtime") != null || true;
            }
        }
    }
}
