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
    /// Contains a list of values that can be referenced by instructions
    /// </summary>
    public class FontTableCvt
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableCvt"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableCvt(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            var values = ImmutableArray.CreateBuilder<Int16>();
            for (var i = 0; i < buffer.Length; i += 2) {
                values.Add(buffer.ReadInt16(i));
            }
            this.Values = values.ToImmutable();
        }

        /// <summary>
        /// List of values referenceable by instructions
        /// </summary>
        public ImmutableArray<Int16> Values { get; }
    }
}
