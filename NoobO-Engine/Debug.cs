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
using System.Text;
using System.Threading.Tasks;

namespace _2DGameEngine
{
    public abstract class Debug
    {
        const ConsoleColor INFORMATION_COLOR = ConsoleColor.White;
        const ConsoleColor WARNING_COLOR = ConsoleColor.Yellow;
        const ConsoleColor ERROR_COLOR = ConsoleColor.Red;

        /// <summary>
        /// Logs information on output.
        /// </summary>
        /// <param name="info">Information text</param>
        public static void Log(string info)
        {
            Console.ForegroundColor = INFORMATION_COLOR;
            Console.WriteLine("[INFO] " + info);
        }

        /// <summary>
        /// Logs a warning on output.
        /// </summary>
        /// <param name="warning">Warning text</param>
        public static void Warn(string warning)
        {
            Console.ForegroundColor = WARNING_COLOR;
            Console.WriteLine("[WARNING] " + warning);
        }

        /// <summary>
        /// Logs an error on output. Does not stop execution.
        /// </summary>
        /// <param name="error">Error text</param>
        public static void Error(string error)
        {
            Console.ForegroundColor = ERROR_COLOR;
            Console.WriteLine("[ERROR] " + error);
        }
    }
}
