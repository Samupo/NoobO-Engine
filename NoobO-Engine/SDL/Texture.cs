#region License
/* NoobOSDL License
 *
 * Copyright (c) 2014 Sergio Alonso
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 * 
 * Thanks to Ethan Lee for his work on SDL2-C# port.
 *
 * Permission is granted to anyone to use this software for any non-commercial
 * project as long as the following requirements are met:
 *
 * 1. Sergio Alonso must be credited as the original author of NoobOSDL even
 * if you modify it's source files. Thanks must be given to Ethan Lee for his
 * work on SDL2-C# port.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 * 
 * If you want to use this software for a commercial project you need to ask
 * for permission to do so at samupo@noobogames.com
 *
 * Sergio "Samupo" Alonso <samupo@noobogames.com>
 * 
 * 
 * Ethan "flibitijibibo" Lee <flibitijibibo@flibitijibibo.com>
 *
 */
#endregion
using System;
using System.Runtime.InteropServices;
namespace _2DGameEngine.SDL
{

    public enum BlendMode
    {
        NONE = 0x00000000,
        BLEND = 0x00000001,
        ADD = 0x00000002,
        MOD = 0x00000004
    }

    /// <summary>
    /// Represents a texture that is stored in memory.
    /// </summary>
    public class Texture
    {
        #region SDL ENUMS
        private enum TextureAccess
        {
            STATIC, STREAMING, TARGET
        }

        private enum PixelFormat
        {
            ARGB8888,
            RGBA8888
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SDL_Texture
        {
            IntPtr magic;
            UInt32 format;
            int access;
            internal int w;
            internal int h;
            int modMode;
            uint blendMode;
            byte r;
            byte g;
            byte b;
            byte a;
            IntPtr renderer;
            IntPtr native;
            int pitch;
            Rect lockedRect;
            IntPtr driverData;
            IntPtr prev;
            IntPtr next;

            public static SDL_Texture FromPointer(IntPtr data)
            {
                return (SDL_Texture)Marshal.PtrToStructure(data, typeof(SDL_Texture));
            }
        }
        #endregion
        internal IntPtr TexturePtr { get; private set; }
        public int Width { get { return texture.w; } }
        public int Height { get { return texture.h; } }
        private string file;
        private SDL_Texture texture;
        private bool disposed;

        private BlendMode _blendMode = BlendMode.BLEND;
        public BlendMode BlendMode { get { return _blendMode; } set { _blendMode = value; } }
        private Color _color = new Color();
        public Color Color { get { return _color; } set { _color = value; } }

        public Texture(int width, int height)
        {
            TexturePtr = SDL_CreateTexture(Graphics.RendererPtr, (uint)PixelFormat.ARGB8888, (int)TextureAccess.TARGET, width, height);
            texture = SDL_Texture.FromPointer(TexturePtr);
        }

        public Texture(string file)
        {
            this.file = file;
            TexturePtr = TextureManager.LoadTexture(file);
            texture = SDL_Texture.FromPointer(TexturePtr);
        }

        public void Draw()
        {
            SDL_SetTextureBlendMode(TexturePtr, (uint)BlendMode);
            SDL_SetTextureAlphaMod(TexturePtr, Color.A);
            SDL_SetTextureColorMod(TexturePtr, Color.R, Color.G, Color.B);
            SDL_RenderCopy(Graphics.RendererPtr, TexturePtr, IntPtr.Zero, IntPtr.Zero);
        }

        ~Texture()
        {
            if (file != null) TextureManager.UnloadTexture(file);
            else SDL_DestroyTexture(TexturePtr);
            Dispose(false);
        }


        /// <summary>
        /// Disposes the texture and frees its memory
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                if (file != null) TextureManager.UnloadTexture(file);
                else SDL_DestroyTexture(TexturePtr);
            }
            disposed = true;
        }

        #region NATIVE CODE

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_RenderCopy(IntPtr renderer, IntPtr texture, IntPtr srcRect, IntPtr dstRect);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_RenderCopy(IntPtr renderer, IntPtr texture, ref Rect srcRect, ref Rect dstRect);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SDL_CreateTexture(IntPtr renderer, UInt32 format, int access, int w, int h);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_SetTextureAlphaMod(IntPtr texture, byte alpha);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_GetTextureAlphaMod(IntPtr texture, out byte alpha);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_SetTextureBlendMode(IntPtr texture, uint mode);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_GetTextureBlendMode(IntPtr texture, out uint mode);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_GetTextureColorMod(IntPtr texture, out byte r, out byte g, out byte b);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_SetTextureColorMod(IntPtr texture, byte r, byte g, byte b);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_RenderCopyEx(IntPtr renderer, IntPtr texture, ref Rect srcRect, ref Rect dstRect, double angle, IntPtr center, uint flip);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_DestroyTexture(IntPtr texture);
        #endregion
    }
}
