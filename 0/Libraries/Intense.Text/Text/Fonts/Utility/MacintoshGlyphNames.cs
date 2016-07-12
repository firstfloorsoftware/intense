using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intense.Text.Fonts.Utility
{
    /// <summary>
    /// Represents the post table format 1 glyph names in the standard Macintosh ordering.
    /// </summary>
    public static class MacintoshGlyphNames
    {
        /// <summary>
        /// Gets the number of names.
        /// </summary>
        public const int Count = 258;

        private static string[] names;

        static MacintoshGlyphNames()
        {
            names = CreateNames();
        }

        private static string[] CreateNames()
        {
            var names = new string[Count];

            names[0] = ".notdef";
            names[1] = ".null";
            names[2] = "nonmarkingreturn";
            names[3] = "space";
            names[4] = "exclam";
            names[5] = "quotedbl";
            names[6] = "numbersign";
            names[7] = "dollar";
            names[8] = "percent";
            names[9] = "ampersand";
            names[10] = "quotesingle";
            names[11] = "parenleft";
            names[12] = "parenright";
            names[13] = "asterisk";
            names[14] = "plus";
            names[15] = "comma";
            names[16] = "hyphen";
            names[17] = "period";
            names[18] = "slash";
            names[19] = "zero";
            names[20] = "one";
            names[21] = "two";
            names[22] = "three";
            names[23] = "four";
            names[24] = "five";
            names[25] = "six";
            names[26] = "seven";
            names[27] = "eight";
            names[28] = "nine";
            names[29] = "colon";
            names[30] = "semicolon";
            names[31] = "less";
            names[32] = "equal";
            names[33] = "greater";
            names[34] = "question";
            names[35] = "at";
            names[36] = "A";
            names[37] = "B";
            names[38] = "C";
            names[39] = "D";
            names[40] = "E";
            names[41] = "F";
            names[42] = "G";
            names[43] = "H";
            names[44] = "I";
            names[45] = "J";
            names[46] = "K";
            names[47] = "L";
            names[48] = "M";
            names[49] = "N";
            names[50] = "O";
            names[51] = "P";
            names[52] = "Q";
            names[53] = "R";
            names[54] = "S";
            names[55] = "T";
            names[56] = "U";
            names[57] = "V";
            names[58] = "W";
            names[59] = "X";
            names[60] = "Y";
            names[61] = "Z";
            names[62] = "bracketleft";
            names[63] = "backslash";
            names[64] = "bracketright";
            names[65] = "asciicircum";
            names[66] = "underscore";
            names[67] = "grave";
            names[68] = "a";
            names[69] = "b";
            names[70] = "c";
            names[71] = "d";
            names[72] = "e";
            names[73] = "f";
            names[74] = "g";
            names[75] = "h";
            names[76] = "i";
            names[77] = "j";
            names[78] = "k";
            names[79] = "l";
            names[80] = "m";
            names[81] = "n";
            names[82] = "o";
            names[83] = "p";
            names[84] = "q";
            names[85] = "r";
            names[86] = "s";
            names[87] = "t";
            names[88] = "u";
            names[89] = "v";
            names[90] = "w";
            names[91] = "x";
            names[92] = "y";
            names[93] = "z";
            names[94] = "braceleft";
            names[95] = "bar";
            names[96] = "braceright";
            names[97] = "asciitilde";
            names[98] = "Adieresis";
            names[99] = "Aring";
            names[100] = "Ccedilla";
            names[101] = "Eacute";
            names[102] = "Ntilde";
            names[103] = "Odieresis";
            names[104] = "Udieresis";
            names[105] = "aacute";
            names[106] = "agrave";
            names[107] = "acircumflex";
            names[108] = "adieresis";
            names[109] = "atilde";
            names[110] = "aring";
            names[111] = "ccedilla";
            names[112] = "eacute";
            names[113] = "egrave";
            names[114] = "ecircumflex";
            names[115] = "edieresis";
            names[116] = "iacute";
            names[117] = "igrave";
            names[118] = "icircumflex";
            names[119] = "idieresis";
            names[120] = "ntilde";
            names[121] = "oacute";
            names[122] = "ograve";
            names[123] = "ocircumflex";
            names[124] = "odieresis";
            names[125] = "otilde";
            names[126] = "uacute";
            names[127] = "ugrave";
            names[128] = "ucircumflex";
            names[129] = "udieresis";
            names[130] = "dagger";
            names[131] = "degree";
            names[132] = "cent";
            names[133] = "sterling";
            names[134] = "section";
            names[135] = "bullet";
            names[136] = "paragraph";
            names[137] = "germandbls";
            names[138] = "registered";
            names[139] = "copyright";
            names[140] = "trademark";
            names[141] = "acute";
            names[142] = "dieresis";
            names[143] = "notequal";
            names[144] = "AE";
            names[145] = "Oslash";
            names[146] = "infinity";
            names[147] = "plusminus";
            names[148] = "lessequal";
            names[149] = "greaterequal";
            names[150] = "yen";
            names[151] = "mu";
            names[152] = "partialdiff";
            names[153] = "summation";
            names[154] = "product";
            names[155] = "pi";
            names[156] = "integral";
            names[157] = "ordfeminine";
            names[158] = "ordmasculine";
            names[159] = "Omega";
            names[160] = "ae";
            names[161] = "oslash";
            names[162] = "questiondown";
            names[163] = "exclamdown";
            names[164] = "logicalnot";
            names[165] = "radical";
            names[166] = "florin";
            names[167] = "approxequal";
            names[168] = "Delta";
            names[169] = "guillemotleft";
            names[170] = "guillemotright";
            names[171] = "ellipsis";
            names[172] = "nonbreakingspace";
            names[173] = "Agrave";
            names[174] = "Atilde";
            names[175] = "Otilde";
            names[176] = "OE";
            names[177] = "oe";
            names[178] = "endash";
            names[179] = "emdash";
            names[180] = "quotedblleft";
            names[181] = "quotedblright";
            names[182] = "quoteleft";
            names[183] = "quoteright";
            names[184] = "divide";
            names[185] = "lozenge";
            names[186] = "ydieresis";
            names[187] = "Ydieresis";
            names[188] = "fraction";
            names[189] = "currency";
            names[190] = "guilsinglleft";
            names[191] = "guilsinglright";
            names[192] = "fi";
            names[193] = "fl";
            names[194] = "daggerdbl";
            names[195] = "periodcentered";
            names[196] = "quotesinglbase";
            names[197] = "quotedblbase";
            names[198] = "perthousand";
            names[199] = "Acircumflex";
            names[200] = "Ecircumflex";
            names[201] = "Aacute";
            names[202] = "Edieresis";
            names[203] = "Egrave";
            names[204] = "Iacute";
            names[205] = "Icircumflex";
            names[206] = "Idieresis";
            names[207] = "Igrave";
            names[208] = "Oacute";
            names[209] = "Ocircumflex";
            names[210] = "apple";
            names[211] = "Ograve";
            names[212] = "Uacute";
            names[213] = "Ucircumflex";
            names[214] = "Ugrave";
            names[215] = "dotlessi";
            names[216] = "circumflex";
            names[217] = "tilde";
            names[218] = "macron";
            names[219] = "breve";
            names[220] = "dotaccent";
            names[221] = "ring";
            names[222] = "cedilla";
            names[223] = "hungarumlaut";
            names[224] = "ogonek";
            names[225] = "caron";
            names[226] = "Lslash";
            names[227] = "lslash";
            names[228] = "Scaron";
            names[229] = "scaron";
            names[230] = "Zcaron";
            names[231] = "zcaron";
            names[232] = "brokenbar";
            names[233] = "Eth";
            names[234] = "eth";
            names[235] = "Yacute";
            names[236] = "yacute";
            names[237] = "Thorn";
            names[238] = "thorn";
            names[239] = "minus";
            names[240] = "multiply";
            names[241] = "onesuperior";
            names[242] = "twosuperior";
            names[243] = "threesuperior";
            names[244] = "onehalf";
            names[245] = "onequarter";
            names[246] = "threequarters";
            names[247] = "franc";
            names[248] = "Gbreve";
            names[249] = "gbreve";
            names[250] = "Idotaccent";
            names[251] = "Scedilla";
            names[252] = "scedilla";
            names[253] = "Cacute";
            names[254] = "cacute";
            names[255] = "Ccaron";
            names[256] = "ccaron";
            names[257] = "dcroat";

            return names;
        }

        /// <summary>
        /// Gets the name at specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string Get(int index)
        {
            return names[index];
        }
    }
}
