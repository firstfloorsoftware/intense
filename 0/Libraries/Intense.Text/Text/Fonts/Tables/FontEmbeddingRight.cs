using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Describes font embedding permissions specified in an OpenType font file.
    /// </summary>
    [Flags]
    public enum FontEmbeddingRight : UInt16
    {
        /// <summary>
        /// Fonts with this setting indicate that they may be embedded and permanently installed on the remote system by an application. The user of the remote system acquires the identical rights, obligations and licenses for that font as the original purchaser of the font, and is subject to the same end-user license agreement, copyright, design patent, and/or trademark as was the original purchaser.  
        /// </summary>
        Installable = 0,
        /// <summary>
        /// Fonts with this setting must not be modified, embedded or exchanged in any manner without first obtaining permission of the legal owner.  
        /// </summary>
        RestrictedLicense = 0x0002,
        /// <summary>
        /// The font may be embedded, and temporarily loaded on the remote system. Documents containing Preview &amp; Print fonts must be opened “read-only;” no edits can be applied to the document.
        /// </summary>
        PreviewAndPrint = 0x0004,
        /// <summary>
        /// The font may be embedded but must only be installed temporarily on other systems.
        /// </summary>
        Editable = 0x0008,
        /// <summary>
        /// The font may not be subsetted prior to embedding.
        /// </summary>
        NoSubsetting = 0x0100,
        /// <summary>
        /// only bitmaps contained in the font may be embedded. No outline data may be embedded. If there are no bitmaps available in the font, then the font is considered unembeddable and the embedding services will fail.
        /// </summary>
        BitmapEmbeddingOnly = 0x0200
    }
}
