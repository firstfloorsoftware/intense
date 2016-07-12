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
    /// Provides precise control over glyph placement for sophisticated text layout and rendering in each script and language system that a font supports.
    /// </summary>
    public class FontTableGPOS
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableGPOS"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableGPOS(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
