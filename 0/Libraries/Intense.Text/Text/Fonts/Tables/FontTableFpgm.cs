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
    /// This table is similar to the CVT Program, except that it is only run once, when the font is first used.
    /// </summary>
    public class FontTableFpgm
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableFpgm"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableFpgm(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            this.Instructions = buffer.ToImmutableArray();
        }

        /// <summary>
        /// Instructions
        /// </summary>
        public ImmutableArray<byte> Instructions { get; }
    }
}
