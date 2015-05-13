using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NoobO_Engine.SDL
{
    public class FontInfo
    {
        public string Name { get; private set; }
        public int Size { get; private set; }

        public FontInfo(string name, int size)
        {
            this.Name = name;
            this.Size = size;
        }
    }

    internal class Font
    {
        private static Dictionary<FontInfo, Font> cachedFonts = new Dictionary<FontInfo, Font>();

        internal FontInfo Info { get; private set; }
        internal IntPtr Pointer { get; private set; }
        private int uses;

        private Font(FontInfo info)
        {
            this.Info = info;
            Pointer = TTF_OpenFont(info.Name, info.Size);
        }

        public static Font LoadFont(string name, int size=12)
        {
            FontInfo finfo = new FontInfo(name, size);
            Font font;
            cachedFonts.TryGetValue(finfo, out font);
            if (font == null)
            {
                font = new Font(finfo);
            }
            font.uses++;
            return font;
        }

        public static void UnloadFont(Font font)
        {
            font.uses--;
            if (font.uses <= 0)
            {
                cachedFonts.Remove(font.Info);
                TTF_CloseFont(font.Pointer);
            }
        }

        #region NATIVE CODE
        [DllImport(SDL.TTFLIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr TTF_OpenFont([In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))] string file, int size);

        [DllImport(SDL.TTFLIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern void TTF_CloseFont(IntPtr font);
        #endregion
    }

    class TextRendererNative
    {
        private Font font;
        public FontInfo FontInfo
        { 
            get {
                return font.Info;
            }
            set {
                if (font != null) Font.UnloadFont(font);
                font = Font.LoadFont(value.Name, value.Size);
                RecreateTexture();
            }
        }
        public Texture Texture { get; private set; }
        private string _text;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                RecreateTexture();
            }
        }

        public TextRendererNative()
        {
            _text = "Text";
        }

        ~TextRendererNative()
        {
            Font.UnloadFont(font);
        }

        private void RecreateTexture() {
            Texture newTexture = new Texture(TTF_RenderText_Blended(font.Pointer, Text, new Color(255, 255, 255)));
            if (Texture != null) {
                newTexture.BlendMode = Texture.BlendMode;
                newTexture.Color = Texture.Color;
            }
            Texture = newTexture;
        }

        [DllImport(SDL.TTFLIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr TTF_RenderText_Blended(IntPtr font, [In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))] string text, Color color);
    }
}
