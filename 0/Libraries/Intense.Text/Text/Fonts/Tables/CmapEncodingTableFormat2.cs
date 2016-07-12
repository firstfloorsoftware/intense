using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Format 2: High-byte mapping through table
    /// </summary>
    public class CmapEncodingTableFormat2
        : CmapEncodingTable
    {
        internal CmapEncodingTableFormat2(byte[] buffer, int index, PlatformID platformID, UInt16 encodingID)
            : base(2, platformID, encodingID)
        {
            // TODO: implement
        }
    }
}
