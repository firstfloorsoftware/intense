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
    /// Classifies the different types of glyphs in the font
    /// </summary>
    public class FontTableGDEF
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableGDEF"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableGDEF(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
