using NoobO_Engine.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobO_Engine.Components
{
    class TextRenderer : Renderer
    {
        private TextRendererNative textRenderer = new TextRendererNative();
        public Texture Texture { get { return textRenderer.Texture; } }
        public string Text { get { return textRenderer.Text; } set { textRenderer.Text = value; } }
        public FontInfo FontInfo { get { return textRenderer.FontInfo; } set { textRenderer.FontInfo = value; } }

        protected override double GetAngle()
        {
            return 0;
        }

        protected override Flip GetFlip()
        {
            return Flip.NONE;
        }

        protected override Texture GetTexture()
        {
            return Texture;
        }
    }
}
