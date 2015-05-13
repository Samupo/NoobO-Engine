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
using NoobO_Engine;
using NoobO_Engine.SDL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoobO_Engine.Editor
{
    public partial class EditorForm : Form
    {
        public EditorForm()
        {
            InitializeComponent();
        }
        private void EditorForm_Load(object sender, EventArgs e)
        {
            foldersControl1.Restart();
            filesControl1.FoldersControl = foldersControl1;
            Thread thread = new Thread(new ParameterizedThreadStart(CreateSDLContext));
            thread.Start(this.Handle);
        }

        private static void CreateSDLContext(object handler)
        {
            SDL.SDL.RunFrom((IntPtr)handler, new GameThread());
        }

        private void splitContainer4_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            Control control = splitContainer4;
            while (control != this)
            {
                x += control.Left;
                y += control.Top;
                control = control.Parent;
            }
            NoobO_Engine.SDL.Graphics.ChangeWindowPosInEditor(this.Handle, x, y, splitContainer4.Panel1.Width, splitContainer4.Panel1.Height);
        }

    }
}
