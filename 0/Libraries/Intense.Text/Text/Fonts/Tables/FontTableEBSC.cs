using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Provides a mechanism for describing embedded bitmaps which are created by scaling other embedded bitmaps
    /// </summary>
    public class FontTableEBSC
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableEBSC"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableEBSC(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
