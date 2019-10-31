using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.DESIGN_PATTERNS.Decorator
{
    //https://www.codeproject.com/Articles/479635/UnderstandingplusandplusImplementingplusDecoratorp

    public interface ISizeComponent
    {
        string GetName();
        int GetResizerX(int x);
    }
    public class SizeBase : ISizeComponent
    {
        int resizerBy = 1;
        string name = "SizerBase";

        public string GetName()
        {
            return name;
        }
        public int GetResizerX(int x)
        {
            return x+resizerBy;
        }
    }

    public abstract class Decorator : ISizeComponent
    {
        ISizeComponent baseSizeComponent = null;
        protected int resizerBy = 0;
        protected string name = "UnderfinedDcorator";

        protected Decorator(ISizeComponent sizeComponent)
        {
            this.baseSizeComponent = sizeComponent;
        }

        string ISizeComponent.GetName()
        {
            return string.Format("{0}, {1}", baseSizeComponent.GetName(), name);
        }
        int ISizeComponent.GetResizerX(int x)
        {
            return resizerBy + baseSizeComponent.GetResizerX(x);
        }
    }
    public class SizeDecorator : Decorator
    {
        public SizeDecorator(ISizeComponent baseSizeComponent, int x) : base(baseSizeComponent)
        {
            this.name = "Size decorator";
            this.resizerBy = baseSizeComponent.GetResizerX(x);
        }
        public int GetResizer()
        {
            return this.resizerBy;
        }
        public string GetName()
        {
            return this.name;
        }
    }
}