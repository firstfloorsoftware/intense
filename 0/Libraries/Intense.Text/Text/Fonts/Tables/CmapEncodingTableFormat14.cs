using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Format 14: Unicode Variation Sequences
    /// </summary>
    public class CmapEncodingTableFormat14
        : CmapEncodingTable
    {
        internal CmapEncodingTableFormat14(byte[] buffer, int index, PlatformID platformID, UInt16 encodingID)
            : base(14, platformID, encodingID)
        {
            // TODO: implement
        }
    }
}
