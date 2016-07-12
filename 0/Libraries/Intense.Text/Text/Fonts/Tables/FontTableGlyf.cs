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
    /// Describes the glyphs in the font in the TrueType outline format
    /// </summary>
    public class FontTableGlyf
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableGlyf"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableGlyf(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
