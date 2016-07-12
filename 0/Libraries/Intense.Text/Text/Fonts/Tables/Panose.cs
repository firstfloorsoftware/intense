using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// The PANOSE classification.
    /// </summary>
    public class Panose
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Panose"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        internal Panose(byte[] value, int index)
        {
            this.FamilyType = (PanoseFamilyType)value[index];
            this.SerifStyle = (PanoseSerifStyle)value[index + 1];
            this.Weight = (PanoseWeight)value[index + 2];
            this.Proportion = (PanoseProportion)value[index + 3];
            this.Contrast = (PanoseContrast)value[index + 4];
            this.StrokeVariation = (PanoseStrokeVariation)value[index + 5];
            this.ArmStyle = (PanoseArmStyle)value[index + 6];
            this.LetterForm = (PanoseLetterForm)value[index + 7];
            this.Midline = (PanoseMidline)value[index + 8];
            this.XHeight = (PanoseXHeight)value[index + 9];
        }

        /// <summary>
        /// Family type.
        /// </summary>
        public PanoseFamilyType FamilyType { get; }
        /// <summary>
        /// Serif style.
        /// </summary>
        public PanoseSerifStyle SerifStyle { get; }
        /// <summary>
        /// Weight.
        /// </summary>
        public PanoseWeight Weight { get; }
        /// <summary>
        /// Proportion.
        /// </summary>
        public PanoseProportion Proportion { get; }
        /// <summary>
        /// Contrast.
        /// </summary>
        public PanoseContrast Contrast { get; }
        /// <summary>
        /// Stroke variation.
        /// </summary>
        public PanoseStrokeVariation StrokeVariation { get; }
        /// <summary>
        /// Arm style.
        /// </summary>
        public PanoseArmStyle ArmStyle { get; }
        /// <summary>
        /// Letter form.
        /// </summary>
        public PanoseLetterForm LetterForm { get; }
        /// <summary>
        /// Midline.
        /// </summary>
        public PanoseMidline Midline { get; }
        /// <summary>
        /// X height.
        /// </summary>
        public PanoseXHeight XHeight { get; }
    }
}
