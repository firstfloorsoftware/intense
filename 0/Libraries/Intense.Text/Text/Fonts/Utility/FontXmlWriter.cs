using Intense.Text.Fonts.Tables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Intense.Text.Fonts.Utility
{
    /// <summary>
    /// Writes font data to XML.
    /// </summary>
    public class FontXmlWriter
    {
        class FontXmlWriterImpl
        {
            private static readonly DateTime Epoch = new DateTime(1904, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            private FontFile fontFile;
            private XmlWriter writer;
            private FontXmlWriterSettings settings;

            public FontXmlWriterImpl(FontFile fontFile, XmlWriter writer, FontXmlWriterSettings settings)
            {
                this.fontFile = fontFile;
                this.writer = writer;
                this.settings = settings;
            }

            public void Write()
            {
                StartElem("fontFile");
                Attr("type", this.fontFile.FontType.ToString());    // always write named value
                Attr("fontCount", this.fontFile.FontCount);

                for (var i = 0; i < this.fontFile.FontCount; i++) {
                    var reader = this.fontFile.CreateReader(i);

                    StartElem("font");
                    Attr("index", i);
                    Attr("tableCount", reader.TableNames.Length);

                    foreach (var name in reader.TableNames) {
                        FontTable table;
                        if (!reader.TryReadTable(name, out table)) {
                            continue;
                        }
                        StartElem("table");
                        Attr("name", table.TableName);

                        if (table.TableName == FontTableNames.Cmap) {
                            Table(writer, (FontTableCmap)table);
                        }
                        else if (table.TableName == FontTableNames.Head) {
                            Table(writer, (FontTableHead)table);
                        }
                        else if (table.TableName == FontTableNames.Hhea) {
                            Table(writer, (FontTableHhea)table);
                        }
                        else if (table.TableName == FontTableNames.Hmtx) {
                            Table(writer, (FontTableHmtx)table);
                        }
                        else if (table.TableName == FontTableNames.Maxp) {
                            Table(writer, (FontTableMaxp)table);
                        }
                        else if (table.TableName == FontTableNames.Name) {
                            Table(writer, (FontTableName)table);
                        }
                        else if (table.TableName == FontTableNames.OS2) {
                            Table(writer, (FontTableOS2)table);
                        }
                        else if (table.TableName == FontTableNames.Post) {
                            Table(writer, (FontTablePost)table);
                        }

                        EndElem();
                    }
                    EndElem();
                }

                EndElem();
            }

            private void Table(XmlWriter writer, FontTableCmap cmap)
            {
                Attr("version", cmap.Version);
                Attr("numTables", cmap.NumTables);

                foreach(var table in cmap.EncodingTables) {
                    StartElem("subtable");
                    Attr("format", table.Format);
                    Attr("platformID", table.PlatformID);
                    Attr("encodingID", table.EncodingID);

                    if (table.Format == 0) {
                        EncodingTable(writer, (CmapEncodingTableFormat0)table);
                    }
                    else if (table.Format == 2) {
                        EncodingTable(writer, (CmapEncodingTableFormat2)table);
                    }
                    else if (table.Format == 4) {
                        EncodingTable(writer, (CmapEncodingTableFormat4)table);
                    }
                    else if (table.Format == 6) {
                        EncodingTable(writer, (CmapEncodingTableFormat6)table);
                    }
                    else if (table.Format == 8) {
                        EncodingTable(writer, (CmapEncodingTableFormat8)table);
                    }
                    else if (table.Format == 10) {
                        EncodingTable(writer, (CmapEncodingTableFormat10)table);
                    }
                    else if (table.Format == 12) {
                        EncodingTable(writer, (CmapEncodingTableFormat12)table);
                    }
                    else if (table.Format == 13) {
                        EncodingTable(writer, (CmapEncodingTableFormat13)table);
                    }
                    else if (table.Format == 14) {
                        EncodingTable(writer, (CmapEncodingTableFormat14)table);
                    }
                    EndElem();
                }
            }

            private void EncodingTable(XmlWriter writer, CmapEncodingTableFormat0 table)
            {
                Attr("length", table.Length);
                Attr("language", table.Language);

                if (!this.settings.MetadataOnly) {
                    // TODO: implement
                }
            }

            private void EncodingTable(XmlWriter writer, CmapEncodingTableFormat2 table)
            {
                // TODO: implement
            }

            private void EncodingTable(XmlWriter writer, CmapEncodingTableFormat4 table)
            {
                Attr("length", table.Length);
                Attr("language", table.Language);
                Attr("segCountX2", table.SegCountX2);
                Attr("searchRange", table.SearchRange);
                Attr("entrySelector", table.EntrySelector);
                Attr("rangeShift", table.RangeShift);

                if (!this.settings.MetadataOnly) {
                    StartElem("segments");
                    Attr("count", table.EndCounts.Length);
                    for(var i = 0; i < table.SegCountX2 /2; i++) {
                        StartElem("segment");
                        Attr("startCount", table.StartCounts[i]);
                        Attr("endCount", table.EndCounts[i]);
                        Attr("idDelta", table.IdDeltas[i]);
                        Attr("idRangeOffset", table.IdRangeOffsets[i]);
                        EndElem();
                    }
                    EndElem();

                    StartElem("glyphIdArray");
                    Attr("count", table.GlyphIds.Length);
                    foreach (var glyphId in table.GlyphIds) {
                        Elem("glyphId", glyphId);
                    }
                    EndElem();
                }
            }

            private void EncodingTable(XmlWriter writer, CmapEncodingTableFormat6 table)
            {
                // TODO: implement
            }

            private void EncodingTable(XmlWriter writer, CmapEncodingTableFormat8 table)
            {
                // TODO: implement
            }

            private void EncodingTable(XmlWriter writer, CmapEncodingTableFormat10 table)
            {
                // TODO: implement
            }

            private void EncodingTable(XmlWriter writer, CmapEncodingTableFormat12 table)
            {
                // TODO: implement
            }

            private void EncodingTable(XmlWriter writer, CmapEncodingTableFormat13 table)
            {
                // TODO: implement
            }

            private void EncodingTable(XmlWriter writer, CmapEncodingTableFormat14 table)
            {
                // TODO: implement
            }

            private void Table(XmlWriter writer, FontTableHead head)
            {
                Attr("version", head.Version);
                Attr("fontRevision", head.FontRevision);
                Attr("checkSumAdjustment", head.ChecksumAdjustment, "x", "0x");
                Attr("magicNumber", head.MagicNumber, "x", "0x");
                Attr("flags", head.Flags);
                Attr("unitsPerEm", head.UnitsPerEm);
                Attr("created", head.Created);
                Attr("modified", head.Modified);
                Attr("xMin", head.XMin);
                Attr("yMin", head.YMin);
                Attr("xMax", head.XMax);
                Attr("yMax", head.YMax);
                Attr("macStyle", head.MacStyle);
                Attr("lowestRecPPEM", head.LowestRecPPEM);
                Attr("fontDirectionHint", head.FontDirectionHint);
                Attr("indexToLocFormat", head.IndexToLocFormat);
                Attr("glyphDataFormat", head.GlyphDataFormat);
            }

            private void Table(XmlWriter writer, FontTableHhea hhea)
            {
                Attr("version", hhea.Version);
                Attr("Ascender", hhea.Ascender);
                Attr("Descender", hhea.Descender);
                Attr("LineGap", hhea.LineGap);
                Attr("advanceWidthMax", hhea.AdvanceWidthMax);
                Attr("minLeftSideBearing", hhea.MinLeftSideBearing);
                Attr("minRightSideBearing", hhea.MinRightSideBearing);
                Attr("xMaxExtent", hhea.XMaxExtent);
                Attr("caretSlopeRise", hhea.CaretSlopeRise);
                Attr("caretSlopeRun", hhea.CaretSlopeRun);
                Attr("metricDataFormat", hhea.MetricDataFormat);
                Attr("numberOfHMetrics", hhea.NumberOfHMetrics);
            }

            private void Table(XmlWriter writer, FontTableHmtx hmtx)
            {
                Attr("hMetricCount", hmtx.Metrics.Length);
                Attr("leftSideBearingCount", hmtx.LeftSideBearings.Length);

                if (!this.settings.MetadataOnly) {
                    if (!hmtx.Metrics.IsDefaultOrEmpty) {
                        foreach (var metric in hmtx.Metrics) {
                            StartElem("hMetric");
                            Attr("advanceWidth", metric.AdvanceWidth);
                            Attr("lsb", metric.LeftSideBearing);
                            EndElem();
                        }
                    }
                    if (!hmtx.LeftSideBearings.IsDefaultOrEmpty) {
                        foreach (var lsb in hmtx.LeftSideBearings) {
                            Elem("leftSideBearing", lsb);
                        };
                    }
                }
            }

            private void Table(XmlWriter writer, FontTableMaxp maxp)
            {
                Attr("version", maxp.Version);
                Attr("numGlyphs", maxp.NumGlyphs);

                if (maxp.Version.Major >= 1) {
                    Attr("maxPoints", maxp.MaxPoints);
                    Attr("maxContours", maxp.MaxContours);
                    Attr("maxCompositePoints", maxp.MaxCompositePoints);
                    Attr("maxCompositeContours", maxp.MaxCompositeContours);
                    Attr("maxZones", maxp.MaxZones);
                    Attr("maxTwilightPoints", maxp.MaxTwilightPoints);
                    Attr("maxStorage", maxp.MaxStorage);
                    Attr("maxFunctionDefs", maxp.MaxFunctionDefs);
                    Attr("maxInstructionDefs", maxp.MaxInstructionDefs);
                    Attr("maxStackElements", maxp.MaxStackElements);
                    Attr("maxSizeOfInstructions", maxp.MaxSizeOfInstructions);
                    Attr("maxComponentElements", maxp.MaxComponentElements);
                    Attr("maxComponentDepth", maxp.MaxComponentDepth);
                }
            }

            private void Table(XmlWriter writer, FontTableName name)
            {
                Attr("format", name.Format);
                Attr("count", name.Count);
                Attr("langTagCount", name.LangTagCount);

                if (!name.NameRecords.IsDefaultOrEmpty) {
                    foreach (var record in name.NameRecords) {
                        StartElem("nameRecord");
                        Attr("platformID", record.PlatformID);
                        Attr("encodingID", record.EncodingID);
                        Attr("languageID", record.LanguageID);
                        Attr("nameID", record.NameID);
                        Text(record.Value);
                        EndElem();
                    }
                }
                if (!name.LangTags.IsDefaultOrEmpty) {
                    foreach (var tag in name.LangTags) {
                        Elem("langTagRecord", tag);
                    }
                }
            }

            private void Table(XmlWriter writer, FontTableOS2 os2)
            {
                Attr("version", os2.Version);
                Attr("xAvgCharWidth", os2.XAvgCharWidth);
                Attr("usWeightClass", os2.WeightClass);
                Attr("usWidthClass", os2.WidthClass);
                Attr("fsType", os2.Type);
                Attr("ySubscriptXSize", os2.YSubscriptXSize);
                Attr("ySubscriptYSize", os2.YSubscriptYSize);
                Attr("ySubscriptXOffset", os2.YSubscriptXOffset);
                Attr("ySubscriptYOffset", os2.YSubscriptYOffset);
                Attr("ySuperscriptXSize", os2.YSuperscriptXSize);
                Attr("ySuperscriptYSize", os2.YSuperscriptYSize);
                Attr("ySuperscriptXOffset", os2.YSuperscriptXOffset);
                Attr("ySuperscriptYOffset", os2.YSuperscriptYOffset);
                Attr("yStrikoutSize", os2.YStrikoutSize);
                Attr("yStrikoutPosition", os2.YStrikoutPosition);
                Attr("sFamilyClass", os2.FamilyClass);
                // panose written as element (see below)
                Attr("ulUnicodeRange1", os2.UnicodeRange1);
                Attr("ulUnicodeRange2", os2.UnicodeRange2);
                Attr("ulUnicodeRange3", os2.UnicodeRange3);
                Attr("ulUnicodeRange4", os2.UnicodeRange4);
                Attr("achVendID", os2.VendID);
                Attr("fsSelection", os2.Selection);
                Attr("usFirstCharIndex", os2.FirstCharIndex);
                Attr("usLastCharIndex", os2.LastCharIndex);
                Attr("sTypoAscender", os2.TypoAscender);
                Attr("sTypoDescender", os2.TypoDescender);
                Attr("sTypoLineGap", os2.TypoLineGap);
                Attr("usWinAscent", os2.WinAscent);
                Attr("usWinDescent", os2.WinDescent);

                if (os2.Version > 0) {
                    Attr("ulCodePageRange1", os2.CodePageRange1);
                    Attr("ulCodePageRange2", os2.CodePageRange2);
                }
                if (os2.Version > 1) {
                    Attr("sxHeight", os2.XHeight);
                    Attr("sCapHeight", os2.CapHeight);
                    Attr("usDefaultChar", os2.DefaultChar);
                    Attr("usMaxContext", os2.MaxContext);
                }
                if (os2.Version > 4) {
                    Attr("usLowerOpticalPointSize", os2.LowerOpticalPointSize);
                    Attr("usUpperOpticalPointSize", os2.UpperOpticalPointSize);
                }

                if (os2.Panose != null) {
                    StartElem("panose");
                    Attr("bFamilyType", os2.Panose.FamilyType);
                    Attr("bSerifStyle", os2.Panose.SerifStyle);
                    Attr("bWeight", os2.Panose.Weight);
                    Attr("bProportion", os2.Panose.Proportion);
                    Attr("bContrast", os2.Panose.Contrast);
                    Attr("bStrokeVariation", os2.Panose.StrokeVariation);
                    Attr("bArmStyle", os2.Panose.ArmStyle);
                    Attr("bLetterForm", os2.Panose.LetterForm);
                    Attr("bXHeight", os2.Panose.XHeight);
                    EndElem();
                }
            }

            private void Table(XmlWriter writer, FontTablePost post)
            {
                Attr("version", post.Version);
                Attr("italicAngle", post.ItalicAngle);
                Attr("underlinePosition", post.UnderlinePosition);
                Attr("underlineThickness", post.UnderlineThickness);
                Attr("isFixedPitch", post.IsFixedPitch);
                Attr("minMemType42", post.MinMemType42);
                Attr("maxMemType42", post.MaxMemType42);
                Attr("minMemType1", post.MinMemType1);
                Attr("maxMemType1", post.MaxMemType1);

                if (post.Version.Major == 2) {
                    Attr("numberOfGlyphs", post.NumberOfGlyphs);

                    if (post.Version.Minor == 0) {
                        // TODO: implement
                    }
                    else if (post.Version.Minor == 5) {
                        // TODO: implement
                    }
                }
            }

            private void StartElem(string name)
            {
                this.writer.WriteStartElement(name);
            }

            private void EndElem()
            {
                this.writer.WriteEndElement();
            }

            private void Elem(string name, string value)
            {
                this.writer.WriteElementString(name, value);
            }

            private void Elem(string name, IFormattable value, string format = "g", string prefix = null)
            {
                Elem(name, prefix + value.ToString(format, CultureInfo.InvariantCulture));
            }

            private void Text(string value)
            {
                this.writer.WriteString(value);
            }

            private void Attr(string name, string value)
            {
                this.writer.WriteAttributeString(name, value);
            }

            private void Attr(string name, Version value)
            {
                Attr(name, value.ToString(2));
            }

            private void Attr(string name, DateTime value)
            {
                if (this.settings.WriteNamedValues) {
                    Attr(name, value.ToString("s", CultureInfo.InvariantCulture));
                }
                else {
                    var seconds = (value - Epoch).TotalSeconds;
                    Attr(name, (Int64)seconds);
                }
            }

            private void Attr(string name, Enum value)
            {
                if (this.settings.WriteNamedValues) {
                    Attr(name, value.ToString("G"));
                }
                else {
                    Attr(name, value.ToString("D"));
                }
            }

            private void Attr(string name, IFormattable value, string format = "g", string prefix = null)
            {
                Attr(name, prefix + value.ToString(format, CultureInfo.InvariantCulture));
            }
        }

        private FontFile fontFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="FontXmlWriter"/>.
        /// </summary>
        /// <param name="fontFile"></param>
        public FontXmlWriter(FontFile fontFile)
        {
            if (fontFile == null) {
                throw new ArgumentNullException(nameof(fontFile));
            }
            this.fontFile = fontFile;
        }

        /// <summary>
        /// Writes the font file to specified output.
        /// </summary>
        /// <param name="output"></param>
        /// <param name="settings"></param>
        public void Write(Stream output, FontXmlWriterSettings settings = null)
        {
            using (var writer = XmlWriter.Create(output)) {
                Write(writer, settings);
            }
        }

        /// <summary>
        /// Writes the font file using specified writer.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="settings"></param>
        public void Write(XmlWriter writer, FontXmlWriterSettings settings = null)
        {
            var w = new FontXmlWriterImpl(this.fontFile, writer, settings ?? new FontXmlWriterSettings());
            w.Write();
        }
    }
}
