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
    /// A set of one or more palettes, each containing a predefined number of RGBA values arranged consecutively by palette index value.
    /// </summary>
    public class FontTableCPAL
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableCPAL"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableCPAL(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
