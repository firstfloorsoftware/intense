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
    /// Consists of a set of TrueType instructions that will be executed whenever the font or point size or transformation matrix change and before each glyph is interpreted
    /// </summary>
    public class FontTablePrep
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTablePrep"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTablePrep(Stream stream, FontTableRecord record)
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
