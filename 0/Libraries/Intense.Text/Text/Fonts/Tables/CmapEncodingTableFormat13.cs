using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Format 13: Many-to-one range mappings.
    /// </summary>
    public class CmapEncodingTableFormat13
        : CmapEncodingTable
    {
        internal CmapEncodingTableFormat13(byte[] buffer, int index, PlatformID platformID, UInt16 encodingID)
            : base(13, platformID, encodingID)
        {
            // TODO: implement
        }
    }
}
