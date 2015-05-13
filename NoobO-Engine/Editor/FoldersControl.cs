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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoobO_Engine.Editor
{
    class FolderPictureBox : PictureBox
    {
        public string Directory { get; private set; }

        public FolderPictureBox(string dir)
        {
            this.Directory = dir;
        }
    }

    class FolderLabel : Label
    {
        public string Directory { get; private set; }
        public FolderLabel(string dir)
        {
            this.Directory = dir;
        }
    }

    class FoldersControl : Control
    {
        public delegate void OnSelectionEventHandler(object sender, EventArgs e);
        public event OnSelectionEventHandler OnSelectionChanged;
        private FolderLabel selected = null;
        [Browsable(false)]
        public string SelectedDirectory
        {
            get { return selected.Directory; }

            set
            {
                label_Click(new FolderLabel(value), null);
            }
        }
        public string CurrentDirectory { get; set; }
        private int height = 0;
        private List<string> openDirectories = new List<string>();

        public FoldersControl()
        {
            Restart(false);
        }

        public void Restart(bool drawImg = true)
        {
            this.Controls.Clear();
            CurrentDirectory = Directory.GetCurrentDirectory();
            AddControls(drawImg);
        }

        private void AddControls(bool drawImg)
        {
            height = 0;
            foreach (string dir in Directory.GetDirectories(CurrentDirectory, "*", SearchOption.TopDirectoryOnly))
            {
                PaintDirectory(dir, drawImg);
            }
        }

        private void PaintDirectory(string dir, bool drawImg, int tab = 0)
        {
            FolderLabel label = new FolderLabel(dir);
            this.Controls.Add(label);
            label.Text = dir.Substring(dir.LastIndexOf("\\"));
            label.Top = height;
            label.Width = this.Width - tab;
            label.Left = tab + 16;
            label.Click += label_Click;
            if (selected != null && selected.Directory == dir)
            {
                selected = label;
                selected.BackColor = Color.LightBlue;
            }

            if (Directory.GetDirectories(dir, "*", SearchOption.TopDirectoryOnly).Length > 0 && drawImg)
            {
                FolderPictureBox pbox = new FolderPictureBox(dir);
                this.Controls.Add(pbox);
                if (openDirectories.Contains(dir))
                {
                    pbox.BackgroundImage = Image.FromFile("Editor\\Standard\\Img\\Open.png");
                }
                else
                {
                    pbox.BackgroundImage = Image.FromFile("Editor\\Standard\\Img\\Closed.png");
                }
                pbox.Left = tab;
                pbox.Top = height;
                pbox.Width = pbox.BackgroundImage.Width;
                pbox.Height = pbox.BackgroundImage.Height;

                pbox.Click += pbox_Click;
            }

            height += label.Height;

            if (openDirectories.Contains(dir))
            {
                foreach (string dir2 in Directory.GetDirectories(dir, "*", SearchOption.TopDirectoryOnly))
                {
                    PaintDirectory(dir2, drawImg, tab + 16);
                }
            }
        }

        void label_Click(object sender, EventArgs e)
        {
            FolderLabel label = (FolderLabel)sender;
            label.BackColor = Color.LightBlue;
            if (selected != null) selected.BackColor = Color.Transparent;
            this.selected = label;
            OnSelectionChanged(this, null);
        }

        void pbox_Click(object sender, EventArgs e)
        {
            FolderPictureBox fpb = (FolderPictureBox)sender;
            string dir = (fpb).Directory;
            if (openDirectories.Contains(dir))
            {
                openDirectories.Remove(dir);
                fpb.BackgroundImage = Image.FromFile("Editor\\Standard\\Img\\Closed.png");
            }
            else
            {
                openDirectories.Add(dir);
                fpb.BackgroundImage = Image.FromFile("Editor\\Standard\\Img\\Open.png");
            }
            Restart();
        }
    }
}
