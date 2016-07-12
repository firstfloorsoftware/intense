using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Tables
{
    /// <summary>
    /// Defines the IBM Font Class and the IBM Font Subclass parameter values to be used in the classification of font designs by the font designer or supplier.
    /// </summary>
    [Flags]
    public enum IBMFontClass : Int16
    {
        /// <summary>
        /// This class ID is used to indicate that the associated font has no design classification or that the design classification is not of significance to the creator or user of the font resource.
        /// </summary>
        NoClassification = 0,
        /// <summary>
        /// This style is generally based upon the Latin printing style of the 15th to 17th century.
        /// </summary>
        OldstyleSerifs = 256,
        /// <summary>
        /// This style is generally characterized by a large x-height, with short ascenders and descenders. 
        /// </summary>
        OldstyleSerifsIBMRoundedLegibility = 257,
        /// <summary>
        /// This style is generally characterized by a medium x-height, with tall ascenders. 
        /// </summary>
        OldstyleSerifsGaralde = 258,
        /// <summary>
        /// This style is generally characterized by a medium x-height, with a relatively monotone appearance and sweeping tails based on the designs of the early Venetian printers.
        /// </summary>
        OldstyleSerifsVenetian = 259,
        /// <summary>
        /// This style is generally characterized by a large x-height, with a relatively monotone appearance and sweeping tails based on the designs of the early Venetian printers.
        /// </summary>
        OldstyleSerifsModifiedVenetian = 260,
        /// <summary>
        /// This style is generally characterized by a large x-height, with wedge shaped serifs and a circular appearance to the bowls similar to the Dutch Traditional Subclass, but with lighter stokes. 
        /// </summary>
        OldstyleSerifsDutchModern = 261,
        /// <summary>
        /// This style is generally characterized by a large x-height, with wedge shaped serifs and a circular appearance of the bowls.
        /// </summary>
        OldstyleSerifsDutchTraditional = 262,
        /// <summary>
        /// This style is generally characterized by a small x-height, with light stokes and serifs.
        /// </summary>
        OldstyleSerifsContemporary = 263,
        /// <summary>
        /// This style is generally characterized by the fine hand writing style of calligraphy.
        /// </summary>
        OldstyleSerifsCalligraphic = 264,
        /// <summary>
        /// Miscellaneous designs.
        /// </summary>
        OldstyleSerifsMiscellaneous = 271,
        /// <summary>
        /// This style is generally based upon the Latin printing style of the 18th to 19th century.
        /// </summary>
        TransitionalSerifs = 512,
        /// <summary>
        /// This style is generally characterized by a medium x-height, with fine serifs, noticeable contrast, and capitol letters of approximately the same width. 
        /// </summary>
        TransitionalSerifsDirectLine = 513,
        /// <summary>
        /// This style is generally characterized by a hand written script appearance while retaining the Transitional Direct Line style. 
        /// </summary>
        TransitionalSerifsScript = 514,
        /// <summary>
        /// Miscellaneous designs.
        /// </summary>
        TransitionalSerifsMiscellaneous = 527,
        /// <summary>
        /// This style is generally based upon the Latin printing style of the 20th century.
        /// </summary>
        ModernSerifs = 768,
        /// <summary>
        /// This style is generally characterized by a medium x-height, with thin hairline serifs. 
        /// </summary>
        ModernSerifsItalian = 769,
        /// <summary>
        /// This style is generally characterized by a hand written script appearance while retaining the Modern Italian style.
        /// </summary>
        ModernSerifsScript = 770,
        /// <summary>
        /// Miscellaneous designs.
        /// </summary>
        ModernSerifsMiscellaneous = 783,
        /// <summary>
        /// This style is a variation of the Oldstyle Serifs and the Transitional Serifs, with a mild vertical stroke contrast and bracketed serifs.
        /// </summary>
        ClarendonSerifs = 1024,
        /// <summary>
        /// This style is generally characterized by a large x-height, with serifs and strokes of equal weight. 
        /// </summary>
        ClarendonSerifsClarendon = 1025,
        /// <summary>
        /// This style is generally characterized by a large x-height, with serifs of a lighter weight than the strokes and the strokes of a lighter weight than the Traditional.
        /// </summary>
        ClarendonSerifsModern = 1026,
        /// <summary>
        /// This style is generally characterized by a large x-height, with serifs of a lighter weight than the strokes.
        /// </summary>
        ClarendonSerifsTraditional = 1027,
        /// <summary>
        /// This style is generally characterized by a large x-height, with a simpler style of design and serifs of a lighter weight than the strokes. 
        /// </summary>
        ClarendonSerifsNewspaper = 1028,
        /// <summary>
        /// This style is generally characterized by a large x-height, with short stub serifs and relatively bold stems.
        /// </summary>
        ClarendonSerifsStubSerif = 1029,
        /// <summary>
        /// This style is generally characterized by a large x-height, with monotone stems. 
        /// </summary>
        ClarendonSerifsMonotone = 1030,
        /// <summary>
        /// This style is generally characterized by a large x-height, with moderate stroke thickness characteristic of a typewriter.
        /// </summary>
        ClarendonSerifsTypewriter = 1031,
        /// <summary>
        /// Miscellaneous designs.
        /// </summary>
        ClarendonSerifsMiscellaneous = 1039,
        /// <summary>
        /// This style is characterized by serifs with a square transition between the strokes and the serifs.
        /// </summary>
        SlabSerifs = 1280,
        /// <summary>
        /// This style is generally characterized by a large x-height, with serifs and strokes of equal weight.
        /// </summary>
        SlabSerifsMonotone = 1281,
        /// <summary>
        /// This style is generally characterized by a medium x-height, with serifs of lighter weight that the strokes. 
        /// </summary>
        SlabSerifsHumanist = 1282,
        /// <summary>
        /// This style is generally characterized by a large x-height, with serifs and strokes of equal weight and a geometric (circles and lines) design.
        /// </summary>
        SlabSerifsGeometric = 1283,
        /// <summary>
        /// This style is generally characterized by a large x-height, with serifs and strokes of equal weight and an emphasis on the white space of the characters. 
        /// </summary>
        SlabSerifsSwiss = 1284,
        /// <summary>
        /// This style is generally characterized by a large x-height, with serifs and strokes of equal but moderate thickness, and a geometric design. 
        /// </summary>
        SlabSerifsTypewriter = 1285,
        /// <summary>
        /// Miscellaneous designs.
        /// </summary>
        SlabSerifsMiscellaneous = 1295,
        /// <summary>
        /// This style includes serifs, but which expresses a design freedom that does not generally fit within the other serif design classifications.
        /// </summary>
        FreeformSerifs = 1792,
        /// <summary>
        /// This style is generally characterized by a medium x-height, with light contrast in the strokes and a round full design.
        /// </summary>
        FreeformSerifsModern = 1793,
        /// <summary>
        /// Miscellaneous designs.
        /// </summary>
        FreeformSerifsMiscellaneous = 1807,
        /// <summary>
        /// This style includes most basic letter forms (excluding Scripts and Ornamentals) that do not have serifs on the strokes. 
        /// </summary>
        SansSerif = 2048,
        /// <summary>
        /// This style is generally characterized by a large x-height, with uniform stroke width and a simple one story design distinguished by a medium resolution, hand tuned, bitmap rendition of the more general Neo-grotesque Gothic Subclass. 
        /// </summary>
        SansSerifIBMNeoGrotesqueGothic = 2049,
        /// <summary>
        /// This style is generally characterized by a medium x-height, with light contrast in the strokes and a classic Roman letterform. 
        /// </summary>
        SansSerifHumanist = 2050,
        /// <summary>
        /// This style is generally characterized by a low x-height, with monotone stroke weight and a round geometric design. 
        /// </summary>
        SansSerifLowXRoundGeometric = 2051,
        /// <summary>
        /// This style is generally characterized by a high x-height, with uniform stroke weight and a round geometric design.
        /// </summary>
        SansSerifHighXRoundGeometric = 2052,
        /// <summary>
        /// This style is generally characterized by a high x-height, with uniform stroke width and a simple one story design. 
        /// </summary>
        SansSerifNeoGrotesqueGothic = 2053,
        /// <summary>
        /// This style is similar to the Neo-grotesque Gothic style, with design variations to the G and Q. 
        /// </summary>
        SansSerifModifiedNeoGrotesqueGothic = 2054,
        /// <summary>
        /// This style is similar to the Neo-grotesque Gothic style, with moderate stroke thickness characteristic of a typewriter. 
        /// </summary>
        SansSerifTypewriterGothic = 2057,
        /// <summary>
        /// This style is generally a simple design characteristic of a dot matrix printer.
        /// </summary>
        SansSerifTypewriterMatrix = 2058,
        /// <summary>
        /// Miscellaneous designs.
        /// </summary>
        SansSerifMiscellaneous = 2063,
        /// <summary>
        /// This style includes highly decorated or stylized character shapes that are typically used in headlines. 
        /// </summary>
        Ornamentals = 2304,
        /// <summary>
        /// This style is characterized by fine lines or lines engraved on the stems.
        /// </summary>
        OrnamentalsEngraver = 2305,
        /// <summary>
        /// This style is generally based upon the printing style of the German monasteries and printers of the 12th to 15th centuries. 
        /// </summary>
        OrnamentalsBlackLetter = 2306,
        /// <summary>
        /// This style is characterized by ornamental designs (typically from nature, such as leaves, flowers, animals, etc.) incorporated into the stems and strokes of the characters. 
        /// </summary>
        OrnamentalsDecorative = 2307,
        /// <summary>
        /// This style is characterized by a three dimensional (raised) appearance of the characters created by shading or geometric effects. 
        /// </summary>
        OrnamentalsThreeDimensional = 2308,
        /// <summary>
        /// Miscellaneous designs.
        /// </summary>
        OrnamentalsMiscellaneous = 2319,
        /// <summary>
        /// This style includes those typefaces that are designed to simulate handwriting. 
        /// </summary>
        Scripts = 2560,
        /// <summary>
        /// This style is characterized by unjoined (nonconnecting) characters that are generally based on the hand writing style of Europe in the 6th to 9th centuries.
        /// </summary>
        ScriptsUncial = 2561,
        /// <summary>
        /// This style is characterized by joined (connecting) characters that have the appearance of being painted with a brush, with moderate contrast between thick and thin strokes. 
        /// </summary>
        ScriptsBrushJoined = 2562,
        /// <summary>
        /// This style is characterized by joined (connecting) characters that have a printed (or drawn with a stiff brush) appearance with extreme contrast between the thick and thin strokes. 
        /// </summary>
        ScriptsFormalJoined = 2563,
        /// <summary>
        /// This style is characterized by joined (connecting) characters that have a uniform appearance with little or no contrast in the strokes.
        /// </summary>
        ScriptsMonotoneJoined = 2564,
        /// <summary>
        /// This style is characterized by beautifully hand drawn, unjoined (non-connecting) characters that have an appearance of being drawn with a broad edge pen. 
        /// </summary>
        ScriptsCalligraphic = 2565,
        /// <summary>
        /// This style is characterized by unjoined (non-connecting) characters that have the appearance of being painted with a brush, with moderate contrast between thick and thin strokes. 
        /// </summary>
        ScriptsBrushUnjoined = 2566,
        /// <summary>
        /// This style is characterized by unjoined (non-connecting) characters that have a printed (or drawn with a stiff brush) appearance with extreme contrast between the thick and thin strokes.
        /// </summary>
        ScriptsFormalUnjoined = 2567,
        /// <summary>
        /// This style is characterized by unjoined (non-connecting) characters that have a uniform appearance with little or no contrast in the strokes.
        /// </summary>
        ScriptsMonotoneUnjoined = 2568,
        /// <summary>
        /// Miscellaneous designs.
        /// </summary>
        ScriptsMiscellaneous = 2575,
        /// <summary>
        /// This style is generally design independent, making it suitable for Pi and special characters (icons, dingbats, technical symbols, etc.) that may be used equally well with any font.
        /// </summary>
        Symbolic = 3072,
        /// <summary>
        /// This style is characterized by either both or a combination of serif and sans serif designs on those characters of the font for which design is important (e.g., superscript and subscript characters, numbers, copyright or trademark symbols, etc.). 
        /// </summary>
        SymbolicMixedSerif = 3075,
        /// <summary>
        /// This style is characterized by a Oldstyle Serif IBM Class design on those characters of the font for which design is important.
        /// </summary>
        SymbolicOldStyleSerif = 3078,
        /// <summary>
        /// This style is characterized by a Neo-grotesque Sans Serif IBM Font Class and Subclass design on those characters of the font for which design is important.
        /// </summary>
        SymbolicNeoGrotesqueSansSerif = 3079,
        /// <summary>
        /// Miscellaneous designs.
        /// </summary>
        SymbolicMiscellaneous = 3087
    }
}
