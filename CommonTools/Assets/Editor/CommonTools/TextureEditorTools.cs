using System.Collections;
using System.Collections.Generic;
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
    }
}