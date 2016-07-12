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
    /// Contains information for substituting glyphs to render the scripts and language systems supported in a font.
    /// </summary>
    public class FontTableGSUB
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableGSUB"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableGSUB(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
