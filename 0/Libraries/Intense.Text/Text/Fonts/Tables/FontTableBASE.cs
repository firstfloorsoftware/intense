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
    /// Provides information used to align glyphs of different scripts and sizes in a line of text, whether the glyphs are in the same font or in different fonts
    /// </summary>
    public class FontTableBASE
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableBASE"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableBASE(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
