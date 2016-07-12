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
    /// Provides extra information in the HP PCL 5 Printer Language.
    /// </summary>
    public class FontTablePCLT
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTablePCLT"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTablePCLT(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            // TODO: implement
        }
    }
}
