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
    /// Defines the point at which it is reasonable to assume linearly scaled advance widths on a glyph-by-glyph basis.
    /// </summary>
    public class FontTableLTSH
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableLTSH"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableLTSH(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
