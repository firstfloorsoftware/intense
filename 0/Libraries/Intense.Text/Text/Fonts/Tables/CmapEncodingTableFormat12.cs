using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Format 12: Segmented coverage
    /// </summary>
    public class CmapEncodingTableFormat12
        : CmapEncodingTable
    {
        internal CmapEncodingTableFormat12(byte[] buffer, int index, PlatformID platformID, UInt16 encodingID)
            : base(12, platformID, encodingID)
        {
            // TODO: implement
        }
    }
}
