using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Represents a signature defined in the <see cref="FontTableDSIG"/> table.
    /// </summary>
    public class Signature
    {
        private byte[] signature;

        internal Signature(SignatureFormat format, byte[] signature)
        {
            this.Format = format;
            this.Length = signature.Length;
            this.signature = signature;
        }

        /// <summary>
        /// Gets the format of the signature.
        /// </summary>
        public SignatureFormat Format { get; }
        /// <summary>
        /// Length of signature in bytes
        /// </summary>
        public Int32 Length { get; }
        /// <summary>
        /// Gets the signature packet data.
        /// </summary>
        /// <returns></returns>
        public IBuffer GetBuffer()
        {
            return this.signature.AsBuffer();
        }
    }
}
