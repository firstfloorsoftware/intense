using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml.Media;

namespace Intense.Text.Fonts
{
    /// <summary>
    /// Represents a font typeface.
    /// </summary>
    public class Typeface
    {
        private FontFamily family;

        internal Typeface()
        {
        }

        /// <summary>
        /// Gets the full name of the typeface.
        /// </summary>
        public string FullName { get; internal set; }

        /// <summary>
        /// Gets the family name.
        /// </summary>
        public string FamilyName { get; internal set; }
        /// <summary>
        /// Gets the face name.
        /// </summary>
        public string FaceName { get; internal set; }
        /// <summary>
        /// Gets the font family.
        /// </summary>
        public FontFamily Family
        {
            get
            {
                if (this.family == null) {
                    this.family = new FontFamily(this.FamilyName);
                }
                return this.family;
            }
        }
        /// <summary>
        /// Gets the font style.
        /// </summary>
        public FontStyle Style { get; internal set; }
        /// <summary>
        /// Gets the font stretch value.
        /// </summary>
        public FontStretch Stretch { get; internal set; }
        /// <summary>
        /// Gets the font weight.
        /// </summary>
        public FontWeight Weight { get; internal set; }
        /// <summary>
        /// Gets a value indicating whether this is a pictorial font.
        /// </summary>
        public bool Pictorial { get; internal set; }
        
    }
}
