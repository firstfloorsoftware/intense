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
    /// Contains a compact representation of a PostScript Type 1, or CIDFont 
    /// </summary>
    public class FontTableCFF
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableCFF"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableCFF(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
