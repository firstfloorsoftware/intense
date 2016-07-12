using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts
{
    /// <summary>
    /// Represents an Open Type font file.
    /// </summary>
    public class FontFile
        : IDisposable
    {
        private Stream stream;

        internal FontFile(Stream stream)
        {
            this.stream = stream;
        }

        /// <summary>
        /// Gets the type of the font.
        /// </summary>
        /// <value>The type of the font.</value>
        public FontType FontType { get; internal set; }
        /// <summary>
        /// Gets the number of fonts contained in the font file.
        /// </summary>
        /// <value>The font count.</value>
        public int FontCount { get; internal set; }

        /// <summary>
        /// Creates a font reader for the font at given font index.
        /// </summary>
        /// <param name="fontIndex"></param>
        /// <returns></returns>
        public FontReader CreateReader(int fontIndex = 0)
        {
            return new FontReader(this, fontIndex);
        }

        internal Stream Stream
        {
            get
            {
                if (this.stream == null) {
                    throw new ObjectDisposedException(nameof(FontFile));
                }
                return this.stream;
            }
        }

        /// <summary>
        /// Releases all resources currently used by this font file.
        /// </summary>
        public void Dispose()
        {
            if (this.stream != null) {
                this.stream.Dispose();
                this.stream = null;
            }
        }
    }
}
