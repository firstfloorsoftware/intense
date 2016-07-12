using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Security.Cryptography.Certificates;
using Windows.Storage;

using FluentAssertions;
using Intense.Text.Fonts;
using Intense.Text.Fonts.Tables;

namespace Intense.Test.UnitTests
{
    [TestClass]
    public class SegoeMDL2FontReaderTests
    {
        private async Task<Stream> GetFontStreamAsync(string uriString)
        {
            var uri = new Uri("ms-appx:///segmdl2.ttf", UriKind.Absolute);
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);

            var fileStream = await file.OpenReadAsync();
            return fileStream.AsStreamForRead();
        }

        private async Task<FontFile> GetFontFileAsync(string uriString)
        {
            var stream = await GetFontStreamAsync(uriString);
            return FontReader.ReadFontFile(stream);
        }

        [TestMethod]
        public async Task SegoeMDL2_IsFontStream_should_return_true()
        {
            using (var stream = await GetFontStreamAsync("ms-appx:///segmdl2.ttf")) {
                FontReader.IsFontStream(stream).Should().BeTrue();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_FontFile_FontType_And_FontCount_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                fontFile.Should().NotBeNull();
                fontFile.FontCount.Should().Be(1);
                fontFile.FontType.Should().Be(FontType.TrueType);
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_access_stream_after_dispose_FontFile_should_throw_ObjectDisposedException()
        {
            var stream = await GetFontStreamAsync("ms-appx:///segmdl2.ttf");
            var fontFile = FontReader.ReadFontFile(stream);
            fontFile.Dispose();

            Action a = () => stream.Seek(0, SeekOrigin.Begin);
            a.ShouldThrowExactly<ObjectDisposedException>().WithMessage("Cannot access a closed Stream.");
        }

        [TestMethod]
        public async Task SegoeMDL2_FontFile_CreateReader_after_dispose_should_throw_ObjectDisposedException()
        {
            var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf");
            fontFile.Dispose();

            Action a = () => fontFile.CreateReader();
            a.ShouldThrowExactly<ObjectDisposedException>().WithMessage("Cannot access a disposed object.\r\nObject name: 'FontFile'.");
        }

        [TestMethod]
        public async Task SegoeMDL2_FontFile_CreateReader_fontIndex_1_should_throw_ArgumentOutOfRangeException()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                Action a = () => fontFile.CreateReader(1);
                a.ShouldThrowExactly<ArgumentOutOfRangeException>().WithMessage("Specified argument was out of the range of valid values.\r\nParameter name: fontIndex");
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_TryReadTable_foo_should_return_false()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                FontTable table;
                reader.TryReadTable("foo", out table).Should().BeFalse();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_TableNames_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.TableNames.Should().HaveCount(17);
                reader.TableNames.Should().ContainInOrder("DSIG", "OS/2", "VDMX", "cmap", "cvt", "fpgm", "gasp", "glyf", "head", "hhea", "hmtx", "loca", "maxp", "meta", "name", "post", "prep");
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_all_tables_should_have_valid_checksum()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                foreach (var name in reader.TableNames) {
                    FontTable table;
                    reader.TryReadTable(name, out table).Should().BeTrue();
                    table.IsChecksumValid().Should().BeTrue();
                }
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_FontTable_should_throw_NotSupportedException()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                Action a = () => reader.ReadTable<FontTable>();
                a.ShouldThrowExactly<NotSupportedException>().WithMessage("Specified method is not supported.");
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_TryReadTable_head_should_match_ReadTable_HeaderTable()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();

                FontTable table;
                reader.TryReadTable(FontTableNames.Head, out table).Should().BeTrue();

                table.ShouldBeEquivalentTo(reader.ReadTable<FontTableHead>());
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_table_maxp_NumGlyphs_should_match_post_NumberOfGlyphs_if_post_version_is_not_3()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var maxp = reader.ReadTable<FontTableMaxp>();
                var post = reader.ReadTable<FontTablePost>();

                if (post.Version.Major != 3) {
                    maxp.NumGlyphs.Should().Be(post.NumberOfGlyphs);
                }
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_table_post_NumberOfGlyphs_should_be_0_if_version_is_1_or_3()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var post = reader.ReadTable<FontTablePost>();

                if (post.Version.Major == 1 || post.Version.Major == 3) {
                    post.NumberOfGlyphs.Should().Be(0);
                }
                else {
                    post.NumberOfGlyphs.Should().NotBe(0);
                }
            }
        }


        [TestMethod]
        public async Task SegoeMDL2_ReadTable_BASE_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableBASE>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_CBDT_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableCBDT>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_CBLC_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableCBLC>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_CFF_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableCFF>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_cmap_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var cmap = reader.ReadTable<FontTableCmap>();
                cmap.TableName.Should().Be("cmap");
                cmap.TableChecksum.Should().Be(219081352);
                cmap.IsChecksumValid().Should().BeTrue();

                cmap.Version.Should().Be(0);
                cmap.NumTables.Should().Be(1);
                cmap.EncodingTables.Should().HaveCount(1);

                cmap.EncodingTables[0].Should().BeOfType<CmapEncodingTableFormat4>();
                var table = (CmapEncodingTableFormat4)cmap.EncodingTables[0];
                table.Format.Should().Be(4);
                table.PlatformID.Should().Be(PlatformID.Windows);
                table.EncodingID.Should().Be(1);
                table.Length.Should().Be(2986);
                table.SegCountX2.Should().Be(304);
                table.SearchRange.Should().Be(256);
                table.EntrySelector.Should().Be(7);
                table.RangeShift.Should().Be(48);
                table.EndCounts.Should().HaveCount(152);
                table.ReservedPad.Should().Be(0);
                table.StartCounts.Should().HaveCount(152);
                table.IdDeltas.Should().HaveCount(152);
                table.IdRangeOffsets.Should().HaveCount(152);
                table.GlyphIds.Should().HaveCount(877);
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_COLR_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableCOLR>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_CPAL_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableCPAL>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_cvt_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var cvt = reader.ReadTable<FontTableCvt>();
                cvt.TableName.Should().Be("cvt");
                cvt.TableChecksum.Should().Be(90047402);
                cvt.IsChecksumValid().Should().BeTrue();
                cvt.Values.Should().HaveCount(15);
                cvt.Values.Should().ContainInOrder(new short[] { 42, 85, 112, 128, 170, 256, 384, 85, 112, 128, 170, 256, 384, 0, 0 });
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_DSIG_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var dsig = reader.ReadTable<FontTableDSIG>();
                dsig.TableName.Should().Be("DSIG");
                dsig.TableChecksum.Should().Be(1991336922);
                dsig.IsChecksumValid().Should().BeTrue();
                dsig.Version.Should().Be(1);
                dsig.NumSigs.Should().Be(1);
                dsig.Flag.Should().Be(PermissionFlags.CannotBeResigned);

                dsig.Signatures.Should().HaveCount(1);
                var signature = dsig.Signatures[0];
                signature.Format.Should().Be(SignatureFormat.PKCS7or9);
                signature.Length.Should().Be(8286);

                // verify some parts of signature
                var certificate = new Certificate(signature.GetBuffer());
                certificate.FriendlyName.Should().BeEmpty();
                certificate.KeyAlgorithmName.Should().Be("RSA");
                certificate.Issuer.Should().Be("Microsoft Code Signing PCA");
                certificate.SignatureAlgorithmName.Should().Be("sha1RSA");
                certificate.SignatureHashAlgorithmName.Should().Be("SHA1");
                certificate.Subject.Should().Be("Microsoft Corporation");
                certificate.ValidFrom.Date.Should().Be(new DateTime(2015, 6, 4));
                certificate.ValidTo.Date.Should().Be(new DateTime(2016, 9, 4));
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_EBDT_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableEBDT>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_EBLC_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableEBLC>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_EBSC_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableEBSC>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_fpgm_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var fpgm = reader.ReadTable<FontTableFpgm>();
                fpgm.TableName.Should().Be("fpgm");
                fpgm.TableChecksum.Should().Be(4238272142);
                fpgm.IsChecksumValid().Should().BeTrue();
                fpgm.Instructions.Should().HaveCount(345);
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_gasp_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var gasp = reader.ReadTable<FontTableGasp>();
                gasp.TableName.Should().Be("gasp");
                gasp.TableChecksum.Should().Be(524315);
                gasp.IsChecksumValid().Should().BeTrue();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_GDEF_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableGDEF>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_glyf_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var glyf = reader.ReadTable<FontTableGlyf>();
                glyf.TableName.Should().Be("glyf");
                glyf.TableChecksum.Should().Be(3420143567);
                glyf.IsChecksumValid().Should().BeTrue();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_GPOS_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableGPOS>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_GSUB_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableGSUB>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_hdmx_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableHdmx>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_head_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var head = reader.ReadTable<FontTableHead>();
                head.TableName.Should().Be("head");
                head.TableChecksum.Should().Be(93413256);
                head.IsChecksumValid().Should().BeTrue();
                head.Version.Should().Be(new Version(1, 0));
                head.FontRevision.Should().Be(new Version(1, 28180));
                head.ChecksumAdjustment.Should().Be(0xA7CE6DF4);
                head.MagicNumber.Should().Be(0x5F0F3CF5);
                head.Flags.Should().Be(HeaderFlags.BaseLine | HeaderFlags.LeftSidebearingPoint | HeaderFlags.ForcePpemToInteger);
                head.UnitsPerEm.Should().Be(2048);
                head.Created.Should().Be(new DateTime(2006, 1, 26));
                head.Modified.Should().Be(new DateTime(2015, 8, 21, 14, 15, 20));
                head.XMin.Should().Be(-1);
                head.YMin.Should().Be(-2);
                head.XMax.Should().Be(5240);
                head.YMax.Should().Be(2048);
                head.MacStyle.Should().Be(MacStyle.None);
                head.LowestRecPPEM.Should().Be(9);
                head.FontDirectionHint.Should().Be(2);
                head.IndexToLocFormat.Should().Be(0);
                head.GlyphDataFormat.Should().Be(0);
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_hhea_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var hhea = reader.ReadTable<FontTableHhea>();
                hhea.TableName.Should().Be("hhea");
                hhea.TableChecksum.Should().Be(477697916);
                hhea.IsChecksumValid().Should().BeTrue();
                hhea.Version.Should().Be(new Version(1, 0));
                hhea.Ascender.Should().Be(2048);
                hhea.Descender.Should().Be(0);
                hhea.LineGap.Should().Be(0);
                hhea.AdvanceWidthMax.Should().Be(5240);
                hhea.MinLeftSideBearing.Should().Be(-1);
                hhea.MinRightSideBearing.Should().Be(-1);
                hhea.XMaxExtent.Should().Be(5240);
                hhea.CaretSlopeRise.Should().Be(1);
                hhea.CaretSlopeRun.Should().Be(0);
                hhea.CaretOffset.Should().Be(0);
                hhea.MetricDataFormat.Should().Be(0);
                hhea.NumberOfHMetrics.Should().Be(772);
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_hmtx_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var hmtx = reader.ReadTable<FontTableHmtx>();
                hmtx.TableName.Should().Be("hmtx");
                hmtx.TableChecksum.Should().Be(9677551);
                hmtx.IsChecksumValid().Should().BeTrue();
                hmtx.Metrics.Length.Should().Be(772);

                // check some metrics
                hmtx.Metrics[0].AdvanceWidth.Should().Be(1322);
                hmtx.Metrics[0].LeftSideBearing.Should().Be(166);
                hmtx.Metrics[1].AdvanceWidth.Should().Be(0);
                hmtx.Metrics[1].LeftSideBearing.Should().Be(0);

                hmtx.LeftSideBearings.Count().Should().Be(77);
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_JSTF_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableJSTF>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_Kern_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableKern>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_loca_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var loca = reader.ReadTable<FontTableLoca>();
                loca.TableName.Should().Be("loca");
                loca.TableChecksum.Should().Be(3823132652);
                loca.IsChecksumValid().Should().BeTrue();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_LTSH_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableLTSH>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_MATH_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableMATH>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_maxp_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var maxp = reader.ReadTable<FontTableMaxp>();
                maxp.TableName.Should().Be("maxp");
                maxp.TableChecksum.Should().Be(66454056);
                maxp.IsChecksumValid().Should().BeTrue();
                maxp.Version.Should().Be(new Version(1, 0));
                maxp.NumGlyphs.Should().Be(849);
                maxp.MaxPoints.Should().Be(350);
                maxp.MaxContours.Should().Be(33);
                maxp.MaxCompositePoints.Should().Be(135);
                maxp.MaxCompositeContours.Should().Be(20);
                maxp.MaxZones.Should().Be(1);
                maxp.MaxTwilightPoints.Should().Be(0);
                maxp.MaxStorage.Should().Be(0);
                maxp.MaxFunctionDefs.Should().Be(10);
                maxp.MaxInstructionDefs.Should().Be(0);
                maxp.MaxStackElements.Should().Be(100);
                maxp.MaxSizeOfInstructions.Should().Be(65);
                maxp.MaxComponentElements.Should().Be(1);
                maxp.MaxComponentDepth.Should().Be(1);
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_name_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var name = reader.ReadTable<FontTableName>();
                name.TableName.Should().Be("name");
                name.TableChecksum.Should().Be(3476704520);
                name.IsChecksumValid().Should().BeTrue();

                name.Format.Should().Be(0);
                name.Count.Should().Be(37);
                name.NameRecords.Length.Should().Be(37);
                name.NameRecords.Should().OnlyContain(n => n.PlatformID == PlatformID.Windows && n.EncodingID == 1);
                name.LangTagCount.Should().Be(0);
                name.LangTags.IsDefault.Should().BeTrue();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_OS_2_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var os2 = reader.ReadTable<FontTableOS2>();
                os2.TableName.Should().Be("OS/2");
                os2.TableChecksum.Should().Be(1247441991);
                os2.IsChecksumValid().Should().BeTrue();
                os2.Version.Should().Be(3);

                os2.XAvgCharWidth.Should().Be(2112);
                os2.WeightClass.Should().Be(400);
                os2.WidthClass.Should().Be(5);
                os2.Type.Should().Be(FontEmbeddingRight.Editable);
                os2.YSubscriptXSize.Should().Be(1434);
                os2.YSubscriptYSize.Should().Be(1331);
                os2.YSubscriptXOffset.Should().Be(0);
                os2.YSubscriptYOffset.Should().Be(283);
                os2.YSuperscriptXSize.Should().Be(1434);
                os2.YSuperscriptYSize.Should().Be(1331);
                os2.YSuperscriptXOffset.Should().Be(0);
                os2.YSuperscriptYOffset.Should().Be(977);
                os2.YStrikoutSize.Should().Be(102);
                os2.YStrikoutPosition.Should().Be(530);
                os2.FamilyClass.Should().Be(IBMFontClass.SansSerifNeoGrotesqueGothic);
                os2.Panose.Should().NotBeNull();
                os2.Panose.FamilyType.Should().Be(PanoseFamilyType.Pictorial);
                os2.Panose.SerifStyle.Should().Be(PanoseSerifStyle.Triangle);
                os2.Panose.Weight.Should().Be(PanoseWeight.NoFit);
                os2.Panose.Proportion.Should().Be(PanoseProportion.OldStyle);
                os2.Panose.Contrast.Should().Be(PanoseContrast.NoFit);
                os2.Panose.StrokeVariation.Should().Be(PanoseStrokeVariation.NoFit);
                os2.Panose.ArmStyle.Should().Be(PanoseArmStyle.NoFit);
                os2.Panose.LetterForm.Should().Be(PanoseLetterForm.NoFit);
                os2.Panose.Midline.Should().Be(PanoseMidline.NoFit);
                os2.Panose.XHeight.Should().Be(PanoseXHeight.NoFit);
                os2.UnicodeRange1.Should().Be(UnicodeRange1.None);
                os2.UnicodeRange2.Should().Be(UnicodeRange2.PrivateUseArea);
                os2.UnicodeRange3.Should().Be(UnicodeRange3.None);
                os2.UnicodeRange4.Should().Be(UnicodeRange4.None);
                os2.VendID.Should().Be("MS");
                os2.Selection.Should().Be(FontSelection.Regular);
                os2.FirstCharIndex.Should().Be(13);
                os2.LastCharIndex.Should().Be(60621);
                os2.TypoAscender.Should().Be(2048);
                os2.TypoDescender.Should().Be(0);
                os2.TypoLineGap.Should().Be(0);
                os2.WinAscent.Should().Be(2048);
                os2.WinDescent.Should().Be(0);
                os2.CodePageRange1.Should().Be(CodePageRange1.Latin1);
                os2.CodePageRange2.Should().Be(CodePageRange2.None);
                os2.XHeight.Should().Be(1024);
                os2.CapHeight.Should().Be(2048);
                os2.DefaultChar.Should().Be(0);
                os2.BreakChar.Should().Be(32);
                os2.MaxContext.Should().Be(0);
                os2.LowerOpticalPointSize.Should().Be(0);
                os2.UpperOpticalPointSize.Should().Be(0);
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_PCLT_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTablePCLT>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_post_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var post = reader.ReadTable<FontTablePost>();
                post.TableName.Should().Be("post");
                post.TableChecksum.Should().Be(4283498615);
                post.IsChecksumValid().Should().BeTrue();
                post.Version.Should().Be(new Version(3, 0));

                post.ItalicAngle.Should().Be(0f);
                post.UnderlinePosition.Should().Be(-178);
                post.UnderlineThickness.Should().Be(119);
                post.IsFixedPitch.Should().Be(0);
                post.MinMemType42.Should().Be(0);
                post.MaxMemType42.Should().Be(0);
                post.MinMemType1.Should().Be(0);
                post.MaxMemType1.Should().Be(0);
                post.NumberOfGlyphs.Should().Be(0);
                post.GlyphNameIndices.IsDefault.Should().BeTrue();
                post.GlyphNames.IsDefault.Should().BeTrue();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_prep_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var prep = reader.ReadTable<FontTablePrep>();
                prep.TableName.Should().Be("prep");
                prep.TableChecksum.Should().Be(2800037668);
                prep.IsChecksumValid().Should().BeTrue();
                prep.Instructions.Should().HaveCount(151);
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_SVG_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableSVG>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_VDMX_should_match()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                var vdmx = reader.ReadTable<FontTableVDMX>();
                vdmx.TableName.Should().Be("VDMX");
                vdmx.TableChecksum.Should().Be(2170456433);
                vdmx.IsChecksumValid().Should().BeTrue();

                vdmx.Version.Should().Be(0);
                vdmx.NumRecs.Should().Be(1);
                vdmx.NumRatios.Should().Be(1);
                vdmx.RatioRanges.Length.Should().Be(1);

                var range = vdmx.RatioRanges[0];
                range.CharSet.Should().Be(1);
                range.XRatio.Should().Be(1);
                range.YEndRatio.Should().Be(1);
                range.YStartRatio.Should().Be(1);
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_vhea_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableVhea>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_vmtx_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableVmtx>().Should().BeNull();
            }
        }

        [TestMethod]
        public async Task SegoeMDL2_ReadTable_VORG_should_return_null()
        {
            using (var fontFile = await GetFontFileAsync("ms-appx:///segmdl2.ttf")) {
                var reader = fontFile.CreateReader();
                reader.ReadTable<FontTableVORG>().Should().BeNull();
            }
        }
    }
}
