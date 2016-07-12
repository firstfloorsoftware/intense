using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Utility
{
    /// <summary>
    /// Defines settings for the <see cref="FontXmlWriter"/>.
    /// </summary>
    public class FontXmlWriterSettings
    {
        /// <summary>
        /// Writes named values instead of integers where possible.
        /// </summary>
        public bool WriteNamedValues { get; set; } = true;
        /// <summary>
        /// Whether or not to write metadata only, or entire font data.
        /// </summary>
        public bool MetadataOnly { get; set; } = true;
    }
}
