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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameEngine.SDL
{
    internal abstract class TextureManager
    {
        private static Dictionary<string, IntPtr> references = new Dictionary<string,IntPtr>();
        private static Dictionary<IntPtr, int> uses = new Dictionary<IntPtr,int>();
        internal static IntPtr LoadTexture(string file)
        {
            if (references.ContainsKey(file)) {
                IntPtr texture;
                references.TryGetValue(file, out texture);
                AddUse(texture);
                return texture;
            }
            IntPtr surfacePtr = IMG_Load(file);
            IntPtr texturePtr = SDL_CreateTextureFromSurface(Graphics.RendererPtr, surfacePtr);
            SDL_FreeSurface(surfacePtr);
            references.Add(file, texturePtr);
            uses.Add(texturePtr, 1);
            return texturePtr;
        }

        private static void AddUse(IntPtr texture) {
            int u = 0;
            uses.TryGetValue(texture, out u);
            u++;
            uses.Remove(texture);
            uses.Add(texture, u);
        }

        private static int RemoveUse(IntPtr texture)
        {
            int u = 0;
            uses.TryGetValue(texture, out u);
            u--;
            if (u > 0)
            {
                uses.Remove(texture);
                uses.Add(texture, u);
            }
            return u;
        }
 
        internal static void UnloadTexture(string file)
        {
            IntPtr texture;
            references.TryGetValue(file, out texture);
            if (RemoveUse(texture) <= 0)
            {
                references.Remove(file);
                uses.Remove(texture);
                SDL_DestroyTexture(texture);
            }
        }


        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SDL_CreateTextureFromSurface(IntPtr renderer, IntPtr surface);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_DestroyTexture(IntPtr texture);

        [DllImport(SDL.IMGLIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr IMG_Load([In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))] string file);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_FreeSurface(IntPtr surface);

    }
}
