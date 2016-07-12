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
    /// Provides font developers with additional control over glyph substitution and positioning in justified text.
    /// </summary>
    public class FontTableJSTF
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableJSTF"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableJSTF(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
