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
    /// Embed bitmaps in OpenType™ fonts
    /// </summary>
    public class FontTableEBDT
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableEBDT"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableEBDT(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
