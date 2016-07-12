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
    /// Specifies the y coordinate of the vertical origin of every glyph in the font.
    /// </summary>
    public class FontTableVORG
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableVORG"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableVORG(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
