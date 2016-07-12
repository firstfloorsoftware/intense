using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Format 10: Trimmed array
    /// </summary>
    public class CmapEncodingTableFormat10
        : CmapEncodingTable
    {
        internal CmapEncodingTableFormat10(byte[] buffer, int index, PlatformID platformID, UInt16 encodingID)
            : base(10, platformID, encodingID)
        {
            // TODO: implement
        }
    }
}
