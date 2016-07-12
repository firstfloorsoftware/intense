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
    /// Contains the digital signature of the OpenType™ font.
    /// </summary>
    public class FontTableDSIG
        : FontTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FontTableDSIG"/>.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="record"></param>
        internal FontTableDSIG(Stream stream, FontTableRecord record)
            : base(stream, record)
        {
            var buffer = ReadTable();

            this.Version = buffer.ReadUInt32(0);
            this.NumSigs = buffer.ReadUInt16(4);
            this.Flag = (PermissionFlags)buffer.ReadUInt16(6);

            var signatures = ImmutableArray.CreateBuilder<Signature>();
            for (var i = 0; i < this.NumSigs; i++) {
                var format = (SignatureFormat)buffer.ReadUInt32(i * 12 + 8);
                var length = buffer.ReadUInt32(i * 12 + 12);
                var offset = (int)buffer.ReadUInt32(i * 12 + 16);

                var signatureLength = (int)buffer.ReadUInt32(offset + 4);
                var signature = new byte[signatureLength];
                Array.Copy(buffer, offset + 8, signature, 0, signatureLength);

                signatures.Add(new Signature(format, signature));
            }
            this.Signatures = signatures.ToImmutable();
        }

        /// <summary>
        /// Version number of the DSIG table
        /// </summary>
        public UInt32 Version { get; }
        /// <summary>
        /// Number of signatures in the table
        /// </summary>
        public UInt16 NumSigs { get; }
        /// <summary>
        /// Permission flags
        /// </summary>
        public PermissionFlags Flag { get; }
        /// <summary>
        /// Gets the signatures.
        /// </summary>
        public ImmutableArray<Signature> Signatures { get; }
    }
}
