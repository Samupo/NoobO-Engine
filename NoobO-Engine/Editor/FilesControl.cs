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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2DGameEngine.Editor
{
    class FileItem : Control
    {
        private string dir;
        public const int WIDTH = 100;
        public const int HEIGHT = 100;
        private FoldersControl fc;

        public void AssignFoldersControl(FoldersControl fc)
        {
            this.fc = fc;
        }

        public FileItem(string dir, string img)
        {
            this.dir = dir;
            PictureBox pbox = new PictureBox();
            Controls.Add(pbox);
            if (dir.EndsWith(".bmp") || dir.EndsWith(".jpg") || dir.EndsWith(".jpeg") || dir.EndsWith(".png"))
            {
                pbox.BackgroundImage = Image.FromFile(dir);
            }
            else
            {
                try
                {
                    pbox.BackgroundImage = Icon.ExtractAssociatedIcon(dir).ToBitmap();
                }
                catch
                {
                    pbox.BackgroundImage = Image.FromFile(img);
                }
            }
            pbox.BackgroundImageLayout = ImageLayout.Zoom;
            Label label = new Label();
            Controls.Add(label);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Text = dir.Substring(dir.LastIndexOf("\\") + 1);

            this.Width = WIDTH;
            this.Height = HEIGHT;

            pbox.Height = 64;
            pbox.Width = 64;
            pbox.Left = (WIDTH - pbox.Width) / 2;
            pbox.Top = 0;

            label.Height = HEIGHT - pbox.Height;
            label.Top = pbox.Height;
            label.Left = 0;
            label.Width = WIDTH;

            pbox.DoubleClick += FileItem_DoubleClick;
            label.DoubleClick += FileItem_DoubleClick;
        }

        void FileItem_DoubleClick(object sender, EventArgs e)
        {
            if (fc == null)
            {
                Process.Start(@"" + dir);
            }
            else
            {
                fc.SelectedDirectory = dir;
            }
        }
    }
    class FilesControl : Control
    {
        private FoldersControl _fc;
        private int index = 0;

        public FoldersControl FoldersControl
        {
            get { return _fc; }
            set
            {
                if (_fc != null && _fc != value) throw new Exception("Folders Control already set!");
                _fc = value;
                FoldersControl.OnSelectionChanged += FoldersControl_OnSelectionChanged;
            }
        }

        void FoldersControl_OnSelectionChanged(object sender, EventArgs e)
        {
            Restart(((FoldersControl)FoldersControl).SelectedDirectory);
        }
        public void Restart(string dir = null)
        {
            this.Controls.Clear();
            AddControls(dir);
        }

        public void AddItem(string dir, bool directory = false)
        {
            int cols = Width / FileItem.WIDTH;
            int x = index % cols;
            int y = index / cols;

            FileItem item;
            if (directory)
            {
                item = new FileItem(dir, "Editor\\Standard\\Img\\folder.png");
                item.AssignFoldersControl(FoldersControl);
            }
            else
            {
                item = new FileItem(dir, "Editor\\Standard\\Img\\file.png");
            }
            item.Left = x * FileItem.WIDTH;
            item.Top = y * FileItem.HEIGHT;
            Controls.Add(item);
            index++;
        }

        private void AddControls(string dir)
        {
            index = 0;
            if (dir == null) return;
            foreach (string d in Directory.EnumerateDirectories(dir, "*", SearchOption.TopDirectoryOnly))
            {
                AddItem(d, true);
            }
            foreach (string f in Directory.EnumerateFiles(dir, "*", SearchOption.TopDirectoryOnly))
            {
                AddItem(f);
            }
        }
    }
}
