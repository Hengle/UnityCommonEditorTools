using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CommonTools
{
    public class TextureEditorTools
    {
        [MenuItem("CommonTools/Texture/Change Texture Type/Default")]
        static void ChangeTextureType_Default()
        {
            SelectedChangeTextureType(TextureImporterType.Default);
        }

        [MenuItem("CommonTools/Texture/Change Texture Type/NormalMap")]
        static void ChangeTextureType_NormalMap()
        {
            SelectedChangeTextureType(TextureImporterType.NormalMap);
        }

        [MenuItem("CommonTools/Texture/Change Texture Type/EditorGUIAndLegacyGui")]
        static void ChangeTextureType_Gui()
        {
            SelectedChangeTextureType(TextureImporterType.GUI);
        }

        [MenuItem("CommonTools/Texture/Change Texture Type/Sprite")]
        static void ChangeTextureType_Sprite()
        {
            SelectedChangeTextureType(TextureImporterType.Sprite);
        }

        [MenuItem("CommonTools/Texture/Change Texture Type/Cursor")]
        static void ChangeTextureType_Cursor()
        {
            SelectedChangeTextureType(TextureImporterType.Cursor);
        }

        [MenuItem("CommonTools/Texture/Change Texture Type/Cookie")]
        static void ChangeTextureType_Cookie()
        {
            SelectedChangeTextureType(TextureImporterType.Cookie);
        }

        [MenuItem("CommonTools/Texture/Change Texture Type/LightMap")]
        static void ChangeTextureType_LightMap()
        {
            SelectedChangeTextureType(TextureImporterType.Lightmap);
        }
        // ----------------------------------------------------------------------------

        [MenuItem("CommonTools/Texture/Change Texture Compression/None")]
        static void ChangeTextureCompression_None()
        {
            SelectedChangeTextureFormatSettings(TextureImporterCompression.Uncompressed);
        }

        [MenuItem("CommonTools/Texture/Change Texture Compression/LowQuality")]
        static void ChangeTextureCompression_LowQuality()
        {
            SelectedChangeTextureFormatSettings(TextureImporterCompression.CompressedLQ);
        }

        [MenuItem("CommonTools/Texture/Change Texture Compression/NormalQuality")]
        static void ChangeTextureCompression_NormalQuality()
        {
            SelectedChangeTextureFormatSettings(TextureImporterCompression.Compressed);
        }

        [MenuItem("CommonTools/Texture/Change Texture Compression/HighQuality")]
        static void ChangeTextureCompression_HighQuality()
        {
            SelectedChangeTextureFormatSettings(TextureImporterCompression.CompressedHQ);
        }
        // ----------------------------------------------------------------------------

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/32")]
        static void ChangeTextureSize_32()
        {
            SelectedChangeMaxTextureSize(32);
        }

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/64")]
        static void ChangeTextureSize_64()
        {
            SelectedChangeMaxTextureSize(64);
        }

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/128")]
        static void ChangeTextureSize_128()
        {
            SelectedChangeMaxTextureSize(128);
        }

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/256")]
        static void ChangeTextureSize_256()
        {
            SelectedChangeMaxTextureSize(256);
        }

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/512")]
        static void ChangeTextureSize_512()
        {
            SelectedChangeMaxTextureSize(512);
        }

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/1024")]
        static void ChangeTextureSize_1024()
        {
            SelectedChangeMaxTextureSize(1024);
        }

        [MenuItem("CommonTools/Texture/Change Texture Size/Change Max Texture Size/2048")]
        static void ChangeTextureSize_2048()
        {
            SelectedChangeMaxTextureSize(2048);
        }

        // ----------------------------------------------------------------------------

        [MenuItem("CommonTools/Texture/Change MipMap/Enable MipMap")]
        static void ChangeMipMap_On()
        {
            SelectedChangeMipMap(true);
        }

        [MenuItem("CommonTools/Texture/Change MipMap/Disable MipMap")]
        static void ChangeMipMap_Off()
        {
            SelectedChangeMipMap(false);
        }

        // ----------------------------------------------------------------------------


        [MenuItem("CommonTools/Texture/Change Non Power of 2/None")]
        static void ChangeNPOT_None()
        {
            SelectedChangeNonPowerOf2(TextureImporterNPOTScale.None);
        }

        [MenuItem("CommonTools/Texture/Change Non Power of 2/ToNearest")]
        static void ChangeNPOT_ToNearest()
        {
            SelectedChangeNonPowerOf2(TextureImporterNPOTScale.ToNearest);
        }

        [MenuItem("CommonTools/Texture/Change Non Power of 2/ToLarger")]
        static void ChangeNPOT_ToLarger()
        {
            SelectedChangeNonPowerOf2(TextureImporterNPOTScale.ToLarger);
        }

        [MenuItem("CommonTools/Texture/Change Non Power of 2/ToSmaller")]
        static void ChangeNPOT_ToSmaller()
        {
            SelectedChangeNonPowerOf2(TextureImporterNPOTScale.ToSmaller);
        }

        // ----------------------------------------------------------------------------

        [MenuItem("CommonTools/Texture/Change Is Readable/Enable")]
        static void ChangeIsReadable_Yes()
        {
            SelectedChangeIsReadable(true);
        }

        [MenuItem("CommonTools/Texture/Change Is Readable/Disable")]
        static void ChangeIsReadable_No()
        {
            SelectedChangeIsReadable(false);
        }

        [MenuItem("CommonTools/Texture/Change FilterMode/Point")]
        static void ChangeFilterModePoint()
        {
            SelectedFilterMode(FilterMode.Point);
        }

        [MenuItem("CommonTools/Texture/Change FilterMode/BiLinear")]
        static void ChangeFilterModeBilinear()
        {
            SelectedFilterMode(FilterMode.Bilinear);
        }

        [MenuItem("CommonTools/Texture/Change FilterMode/TriLinear")]
        static void ChangeFilterModeTRilinear()
        {
            SelectedFilterMode(FilterMode.Trilinear);
        }

        [MenuItem("CommonTools/Texture/Dither Optimize/RGBA16")]
        static void OptimizeRGBAWithDither()
        {
            foreach (var obj in Selection.objects)
            {
                Texture2D texture = obj as Texture2D;
                if (texture != null)
                {
                    string srcPath = AssetDatabase.GetAssetPath(texture);
                    string newPath = GetDitherTexturePath(srcPath,1);
                    AssetDatabase.CopyAsset(srcPath, newPath);
                    AssetDatabase.Refresh();
                }
            }
        }

        [MenuItem("CommonTools/Texture/Dither Optimize/RGB16")]
        static void OptimizeRGBWithDither()
        {
            foreach (var obj in Selection.objects)
            {
                Texture2D texture = obj as Texture2D;
                if (texture != null)
                {
                    string srcPath = AssetDatabase.GetAssetPath(texture);
                    string newPath = GetDitherTexturePath(srcPath, 2);
                    AssetDatabase.CopyAsset(srcPath, newPath);
                    AssetDatabase.Refresh();
                }
            }
        }


        public static void SelectedChangeIsReadable(bool enabled)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexChangeIsReadable(tex, enabled);
            }
        }

        public static void SetTexChangeIsReadable(Texture2D tex, bool enabled)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.isReadable = enabled;
            AssetDatabase.ImportAsset(path);
        }

        public static void SelectedChangeMaxTextureSize(int size)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexMaxTextureSize(tex, size);
            }
        }

        public static void SetTexMaxTextureSize(Texture2D tex, int size)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.maxTextureSize = size;
            AssetDatabase.ImportAsset(path);
        }

        public static void SelectedChangeNonPowerOf2(TextureImporterNPOTScale scale)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexNonPowerOf2(tex, scale);
            }
        }

        public static void SetTexNonPowerOf2(Texture2D tex, TextureImporterNPOTScale scale)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.npotScale = scale;
            AssetDatabase.ImportAsset(path);
        }

        public static void SelectedChangeMipMap(bool enabled)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexMipMap(tex, enabled);
            }
        }

        public static void SetTexMipMap(Texture2D tex, bool enabled)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.mipmapEnabled = enabled;
            AssetDatabase.ImportAsset(path);
        }

        public static void SelectedChangeTextureType(TextureImporterType type)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexTextureType(tex, type);
            }
        }

        public static void SetTexTextureType(Texture2D tex, TextureImporterType type)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.textureType = type;
            AssetDatabase.ImportAsset(path);
        }

        public static void SelectedChangeTextureFormatSettings(TextureImporterCompression format)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexTextureFormatSettings(tex, format);
            }
        }

        public static void SetTexTextureFormatSettings(Texture2D tex, TextureImporterCompression format)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.textureCompression = format;
            AssetDatabase.ImportAsset(path);
        }

        public static void SelectedFilterMode(FilterMode type)
        {
            Object[] textures = GetSelectedTextures();
            foreach (Texture2D tex in textures)
            {
                SetTexTextureFilterMod(tex, type);
            }
        }

        public static void SetTexTextureFilterMod(Texture2D tex, FilterMode format)
        {
            string path = AssetDatabase.GetAssetPath(tex);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.filterMode = format;
            AssetDatabase.ImportAsset(path);
        }

        static Object[] GetSelectedTextures()
        {
            return Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
        }

        public static void OptimizeRGBA(Texture2D texture)
        {
            var texw = texture.width;
            var texh = texture.height;

            var pixels = texture.GetPixels();
            var offs = 0;

            var k1Per15 = 1.0f / 15.0f;
            var k1Per16 = 1.0f / 16.0f;
            var k3Per16 = 3.0f / 16.0f;
            var k5Per16 = 5.0f / 16.0f;
            var k7Per16 = 7.0f / 16.0f;

            for (var y = 0; y < texh; y++)
            {
                for (var x = 0; x < texw; x++)
                {
                    float a = pixels[offs].a;
                    float r = pixels[offs].r;
                    float g = pixels[offs].g;
                    float b = pixels[offs].b;

                    var a2 = Mathf.Clamp01(Mathf.Floor(a * 16) * k1Per15);
                    var r2 = Mathf.Clamp01(Mathf.Floor(r * 16) * k1Per15);
                    var g2 = Mathf.Clamp01(Mathf.Floor(g * 16) * k1Per15);
                    var b2 = Mathf.Clamp01(Mathf.Floor(b * 16) * k1Per15);

                    var ae = a - a2;
                    var re = r - r2;
                    var ge = g - g2;
                    var be = b - b2;

                    pixels[offs].a = a2;
                    pixels[offs].r = r2;
                    pixels[offs].g = g2;
                    pixels[offs].b = b2;

                    var n1 = offs + 1;
                    var n2 = offs + texw - 1;
                    var n3 = offs + texw;
                    var n4 = offs + texw + 1;

                    if (x < texw - 1)
                    {
                        pixels[n1].a += ae * k7Per16;
                        pixels[n1].r += re * k7Per16;
                        pixels[n1].g += ge * k7Per16;
                        pixels[n1].b += be * k7Per16;
                    }

                    if (y < texh - 1)
                    {
                        pixels[n3].a += ae * k5Per16;
                        pixels[n3].r += re * k5Per16;
                        pixels[n3].g += ge * k5Per16;
                        pixels[n3].b += be * k5Per16;

                        if (x > 0)
                        {
                            pixels[n2].a += ae * k3Per16;
                            pixels[n2].r += re * k3Per16;
                            pixels[n2].g += ge * k3Per16;
                            pixels[n2].b += be * k3Per16;
                        }

                        if (x < texw - 1)
                        {
                            pixels[n4].a += ae * k1Per16;
                            pixels[n4].r += re * k1Per16;
                            pixels[n4].g += ge * k1Per16;
                            pixels[n4].b += be * k1Per16;
                        }
                    }

                    offs++;
                }
            }

            texture.SetPixels(pixels);
            EditorUtility.CompressTexture(texture, TextureFormat.RGBA4444, TextureCompressionQuality.Best);
        }

        public static void OptimizeRGB(Texture2D texture)
        {
            var texw = texture.width;
            var texh = texture.height;

            var pixels = texture.GetPixels();
            var offs = 0;

            var k1Per31 = 1.0f / 31.0f;

            var k1Per32 = 1.0f / 32.0f;
            var k5Per32 = 5.0f / 32.0f;
            var k11Per32 = 11.0f / 32.0f;
            var k15Per32 = 15.0f / 32.0f;

            var k1Per63 = 1.0f / 63.0f;

            var k3Per64 = 3.0f / 64.0f;
            var k11Per64 = 11.0f / 64.0f;
            var k21Per64 = 21.0f / 64.0f;
            var k29Per64 = 29.0f / 64.0f;

            var k_r = 32; //R&B压缩到5位，所以取2的5次方
            var k_g = 64; //G压缩到6位，所以取2的6次方

            for (var y = 0; y < texh; y++)
            {
                for (var x = 0; x < texw; x++)
                {
                    float r = pixels[offs].r;
                    float g = pixels[offs].g;
                    float b = pixels[offs].b;

                    var r2 = Mathf.Clamp01(Mathf.Floor(r * k_r) * k1Per31);
                    var g2 = Mathf.Clamp01(Mathf.Floor(g * k_g) * k1Per63);
                    var b2 = Mathf.Clamp01(Mathf.Floor(b * k_r) * k1Per31);

                    var re = r - r2;
                    var ge = g - g2;
                    var be = b - b2;

                    var n1 = offs + 1;
                    var n2 = offs + texw - 1;
                    var n3 = offs + texw;
                    var n4 = offs + texw + 1;

                    if (x < texw - 1)
                    {
                        pixels[n1].r += re * k15Per32;
                        pixels[n1].g += ge * k29Per64;
                        pixels[n1].b += be * k15Per32;
                    }

                    if (y < texh - 1)
                    {
                        pixels[n3].r += re * k11Per32;
                        pixels[n3].g += ge * k21Per64;
                        pixels[n3].b += be * k11Per32;

                        if (x > 0)
                        {
                            pixels[n2].r += re * k5Per32;
                            pixels[n2].g += ge * k11Per64;
                            pixels[n2].b += be * k5Per32;
                        }

                        if (x < texw - 1)
                        {
                            pixels[n4].r += re * k1Per32;
                            pixels[n4].g += ge * k3Per64;
                            pixels[n4].b += be * k1Per32;
                        }
                    }

                    pixels[offs].r = r2;
                    pixels[offs].g = g2;
                    pixels[offs].b = b2;

                    offs++;
                }
            }

            texture.SetPixels(pixels);
            EditorUtility.CompressTexture(texture, TextureFormat.RGB565, TextureCompressionQuality.Best);

            string path = AssetDatabase.GetAssetPath(texture);
            TextureImporter teximp = AssetImporter.GetAtPath(path) as TextureImporter;
            teximp.isReadable = false;
            AssetDatabase.ImportAsset(path);
        }

        static string GetDitherTexturePath(string srcPath, int type)
        {
            string srcRoot = Path.GetDirectoryName(srcPath);
            string srcExtension = Path.GetExtension(srcPath);
            string srcNameWithoutExtension = Path.GetFileNameWithoutExtension(srcPath);
            string newName = srcNameWithoutExtension + "_dither";
            if (type == 1)
                newName += "_RGBA";
            else
                newName += "_RGB";
            return Path.Combine(srcRoot, newName + srcExtension);
        }
    }
}