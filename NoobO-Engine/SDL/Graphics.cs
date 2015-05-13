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
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace NoobO_Engine.SDL
{
    internal abstract class Graphics
    {
        #region Ethan Lee's code
        [StructLayout(LayoutKind.Sequential)]
        public struct SDL_version
        {
            public byte major;
            public byte minor;
            public byte patch;
        }
        public enum SDL_SYSWM_TYPE
        {
            SDL_SYSWM_UNKNOWN,
            SDL_SYSWM_WINDOWS,
            SDL_SYSWM_X11,
            SDL_SYSWM_DIRECTFB,
            SDL_SYSWM_COCOA,
            SDL_SYSWM_UIKIT
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct INTERNAL_windows_wminfo
        {
            public IntPtr window; // Refers to an HWND
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INTERNAL_x11_wminfo
        {
            public IntPtr display; // Refers to a Display*
            public IntPtr window; // Refers to a Window (XID, use ToInt64!)
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INTERNAL_directfb_wminfo
        {
            public IntPtr dfb; // Refers to an IDirectFB*
            public IntPtr window; // Refers to an IDirectFBWindow*
            public IntPtr surface; // Refers to an IDirectFBSurface*
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INTERNAL_cocoa_wminfo
        {
            public IntPtr window; // Refers to an NSWindow*
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INTERNAL_uikit_wminfo
        {
            public IntPtr window; // Refers to a UIWindow*
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct INTERNAL_SysWMDriverUnion
        {
            [FieldOffset(0)]
            public INTERNAL_windows_wminfo win;
            [FieldOffset(0)]
            public INTERNAL_x11_wminfo x11;
            [FieldOffset(0)]
            public INTERNAL_directfb_wminfo dfb;
            [FieldOffset(0)]
            public INTERNAL_cocoa_wminfo cocoa;
            [FieldOffset(0)]
            public INTERNAL_uikit_wminfo uikit;
            // private int dummy;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SDL_SysWMinfo
        {
            public SDL_version version;
            public SDL_SYSWM_TYPE subsystem;
            public INTERNAL_SysWMDriverUnion info;


            public static SDL_SysWMinfo FromPointer(IntPtr data)
            {
                return (SDL_SysWMinfo)Marshal.PtrToStructure(data, typeof(SDL_SysWMinfo));
            }
        }
        #endregion

        public static VectorF WindowSize
        {
            get
            {
                int w, h;
                SDL_GetWindowSize(WindowPtr, out w, out h);
                return new VectorF(w, h);
            }
            set
            {
                SDL_SetWindowSize(WindowPtr, (int)value.X, (int)value.Y);
            }
        }
        public static VectorF WindowPosition
        {
            get
            {
                int x, y;
                SDL_GetWindowPosition(WindowPtr, out x, out y);
                return new VectorF(x, y);
            }
            set
            {
                SDL_SetWindowPosition(WindowPtr, (int)value.X, (int)value.Y);
            }
        }

        private enum WindowFlags
        {
            SDL_WINDOW_FULLSCREEN = 0x00000001,
            SDL_WINDOW_OPENGL = 0x00000002,
            SDL_WINDOW_SHOWN = 0x00000004,
            SDL_WINDOW_HIDDEN = 0x00000008,
            SDL_WINDOW_BORDERLESS = 0x00000010,
            SDL_WINDOW_RESIZABLE = 0x00000020,
            SDL_WINDOW_MINIMIZED = 0x00000040,
            SDL_WINDOW_MAXIMIZED = 0x00000080,
            SDL_WINDOW_INPUT_GRABBED = 0x00000100,
            SDL_WINDOW_INPUT_FOCUS = 0x00000200,
            SDL_WINDOW_MOUSE_FOCUS = 0x00000400,
            SDL_WINDOW_FULLSCREEN_DESKTOP =
            (SDL_WINDOW_FULLSCREEN | 0x00001000),
            SDL_WINDOW_FOREIGN = 0x00000800,
            SDL_WINDOW_ALLOW_HIGHDPI = 0x00002000
        }
        private enum RendererFlags
        {
            SDL_RENDERER_SOFTWARE = 0x00000001,
            SDL_RENDERER_ACCELERATED = 0x00000002,
            SDL_RENDERER_PRESENTVSYNC = 0x00000004,
            SDL_RENDERER_TARGETTEXTURE = 0x00000008
        }

        const WindowFlags EDITOR_FLAGS = WindowFlags.SDL_WINDOW_SHOWN | WindowFlags.SDL_WINDOW_OPENGL | WindowFlags.SDL_WINDOW_BORDERLESS;
        const WindowFlags WINDOWS_FLAGS = WindowFlags.SDL_WINDOW_SHOWN | WindowFlags.SDL_WINDOW_OPENGL | WindowFlags.SDL_WINDOW_RESIZABLE;
        const RendererFlags RENDERER_FLAGS = RendererFlags.SDL_RENDERER_ACCELERATED | RendererFlags.SDL_RENDERER_TARGETTEXTURE;
        const int WINDOW_POS_UNDEFINED = 0x1FFF0000;

        internal static IntPtr WindowPtr { get; private set; }
        internal static IntPtr RendererPtr { get; private set; }

        private static double aspectRatio;

        internal static void Initialize(int width, int height)
        {
            aspectRatio = width / height;
#if EDITOR
            WindowPtr = SDL_CreateWindow("NoobO Engine", WINDOW_POS_UNDEFINED, WINDOW_POS_UNDEFINED, width, height, (UInt32)EDITOR_FLAGS);
#else
            WindowPtr = SDL_CreateWindow("NoobO Engine", WINDOW_POS_UNDEFINED, WINDOW_POS_UNDEFINED, width, height, (UInt32)WINDOWS_FLAGS);
#endif
            RendererPtr = SDL_CreateRenderer(WindowPtr, -1, (UInt32)RENDERER_FLAGS);
        }

        internal static void InitializeFrom(IntPtr handler, int width, int height)
        {
            aspectRatio = width / height;
            WindowPtr = SDL_CreateWindow("NoobO Engine", WINDOW_POS_UNDEFINED, WINDOW_POS_UNDEFINED, width, height, (UInt32)EDITOR_FLAGS);

            SDL_SysWMinfo info;
            SDL_GetWindowWMInfo(WindowPtr, out info);
            IntPtr m_SdlWindowHandle = info.info.win.window;

            // Set the Window Position to 0/0
            SetWindowPos(m_SdlWindowHandle, handler, 100, 100, 100, 100, (SetWindowPosFlags.SWP_SHOWWINDOW));

            // Make the SDL Window the child of our Panel
            SetParent(m_SdlWindowHandle, handler);
            ShowWindow(m_SdlWindowHandle, ShowWindowCommand.SW_SHOWNORMAL);


            RendererPtr = SDL_CreateRenderer(WindowPtr, -1, (UInt32)RENDERER_FLAGS);
        }

        public static void ChangeWindowPosInEditor(IntPtr handler, int x, int y, int w, int h)
        {
            SDL_SysWMinfo info;
            SDL_GetWindowWMInfo(WindowPtr, out info);
            IntPtr m_SdlWindowHandle = info.info.win.window;

            if (h < (int)(w * aspectRatio))
            {
                MoveWindow(m_SdlWindowHandle, x + (w - (int)(h / aspectRatio)) / 2, y, (int)(h / aspectRatio), h, true);
            }
            else
            {
                MoveWindow(m_SdlWindowHandle, x, y, w, (int)(w * aspectRatio), true);
            }
        }

        internal static void Render()
        {
            SDL_RenderPresent(RendererPtr);
            /*SDL_SetRenderDrawColor(RendererPtr, 255, 128, 128, 255);
            SDL_RenderClear(RendererPtr)^;*/
        }

        public static void SetRenderTarget(IntPtr texturePtr)
        {
            SDL_SetRenderTarget(Graphics.RendererPtr, texturePtr);
        }


        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_RenderClear(IntPtr renderer);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern int SDL_SetRenderTarget(IntPtr renderer, IntPtr texture);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SDL_RenderPresent(IntPtr renderer);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SDL_CreateWindow([In()] [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(LPUtf8StrMarshaler))] string title, int x, int y, int w, int h, UInt32 flags);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SDL_CreateWindowFrom(ref IntPtr data);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr SDL_CreateRenderer(IntPtr window, int index, UInt32 flags);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_SetWindowSize(IntPtr window, int w, int h);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_GetWindowSize(IntPtr window, out int w, out int h);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_SetWindowPosition(IntPtr window, int x, int y);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SDL_GetWindowPosition(IntPtr window, out int x, out int y);

        [DllImport(SDL.NATIVELIB, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        private static extern int SDL_GetWindowWMInfo(IntPtr window, out SDL_SysWMinfo info);

        #region Native Win32 Functions
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr child, IntPtr newParent);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ShowWindow(IntPtr handle, ShowWindowCommand command);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr handle, IntPtr handleAfter, int x, int y, int cx, int cy, SetWindowPosFlags flags);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool MoveWindow(IntPtr handle, int x, int y, int w, int h, bool repaint);

        private enum ShowWindowCommand : int
        {
            SW_HIDE = 0,
            SW_SHOW = 5,
            SW_SHOWNA = 8,
            SW_SHOWNORMAL = 1,
            SW_SHOWMAXIMIZED = 3
        }

        private enum SetWindowPosFlags : uint
        {
            SWP_SHOWWINDOW = 0x0400,
            SWP_NOSIZE = 0x0001
        }
        #endregion
    }
}
