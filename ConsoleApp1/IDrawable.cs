using System.Runtime.CompilerServices;
using System.Text;


namespace FalloutUnderneath
{
    public interface IDrawable
    {
        public void DrawOnViewport(Viewport currentViewport);
    }
}