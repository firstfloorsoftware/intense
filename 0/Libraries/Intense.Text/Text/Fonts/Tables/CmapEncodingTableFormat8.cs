using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Format 8: mixed 16-bit and 32-bit coverage
    /// </summary>
    public class CmapEncodingTableFormat8
        : CmapEncodingTable
    {
        internal CmapEncodingTableFormat8(byte[] buffer, int index, PlatformID platformID, UInt16 encodingID)
            : base(8, platformID, encodingID)
        {
            // TODO: implement
        }
    }
}
