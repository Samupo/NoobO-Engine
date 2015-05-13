using NoobO_Engine.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobO_Engine.Components
{
    class SpriteRenderer : Renderer
    {
        public Texture texture;
        public double angle;
        public Flip flip;

        protected override double GetAngle()
        {
            return angle;
        }

        protected override Flip GetFlip()
        {
            return flip;
        }

        protected override Texture GetTexture()
        {
            return texture;
        }
    }
}
