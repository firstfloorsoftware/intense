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
    /// Stores the offsets to the locations of the glyphs in the font, relative to the beginning of the glyphData table
    /// </summary>
    public class FontTableLoca
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableLoca"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableLoca(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
