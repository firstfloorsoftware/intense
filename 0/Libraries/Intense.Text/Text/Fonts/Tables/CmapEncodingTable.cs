using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Represents an encoding table in <see cref="FontTableCmap"/>.
    /// </summary>
    public class CmapEncodingTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CmapEncodingTable"/>.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="platformID"></param>
        /// <param name="encodingID"></param>
        internal CmapEncodingTable(UInt16 format, PlatformID platformID, UInt16 encodingID)
        {
            this.Format = format;
            this.PlatformID = platformID;
            this.EncodingID = encodingID;
        }

        /// <summary>
        /// Table format.
        /// </summary>
        public UInt16 Format { get; }
        /// <summary>
        /// Platform ID.
        /// </summary>
        public PlatformID PlatformID { get; }
        /// <summary>
        /// Platform-specific encoding ID.
        /// </summary>
        public UInt16 EncodingID { get; }
    }
}
