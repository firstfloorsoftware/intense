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
    /// Contains SVG descriptions for some or all of the glyphs in the font
    /// </summary>
    public class FontTableSVG
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableSVG"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableSVG(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
