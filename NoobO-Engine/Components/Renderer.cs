using NoobO_Engine.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobO_Engine.Components
{
    public abstract class Renderer : Component
    {

        protected abstract Texture GetTexture();
        protected virtual Rect GetSrcRect() { if (!UsesSrcRect()) throw new NotImplementedException(); return new Rect(); }
        protected virtual bool UsesSrcRect() { return false; }
        protected abstract double GetAngle();
        protected abstract Flip GetFlip();
        protected Transform transform;

        public override void Awake()
        {
            transform = GameObject.GetComponents<Transform>()[0];
            if (transform == null)
            {
                Debug.Error("Renderer component requires a Transform component");
            }
        }

        internal override void Render(Rect viewport)
        {
            Rect dstRect = new Rect((int)(transform.Position.X - viewport.X), (int)(transform.Position.Y - viewport.Y), (int)(transform.Size.X), (int)(transform.Size.Y));
            if (!UsesSrcRect()) {
                GetTexture().Draw(dstRect, GetAngle(), GetFlip());
            }
            else
            {
                GetTexture().Draw(GetSrcRect(), dstRect, GetAngle(), GetFlip());
            }
            
        }
    }
}
