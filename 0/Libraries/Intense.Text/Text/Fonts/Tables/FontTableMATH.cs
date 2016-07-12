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
    /// Provides font-specific information about font and individual glyphs that math handling engine uses to produce formula layout that matches font design and realizes full font potential by using available glyph stylistic alternates and stretching variants.
    /// </summary>
    public class FontTableMATH
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableMATH"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableMATH(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
