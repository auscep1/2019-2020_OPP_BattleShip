using KronoBattleship.DESIGN_PATTERNS.Builder;

namespace KronoBattleship.DESIGN_PATTERNS.Chain
{
    public abstract class ShipChain
    {
        protected ShipChain next;
        protected AbstractShipBuilder builder;
        
        public ShipChain()
        {
        }

        public abstract void SetNextChain(ShipChain next);
        public abstract void Build();
    }

    public class BuildReset : ShipChain
    {

        public BuildReset(AbstractShipBuilder builder)
        {
            this.builder = builder;
        }
        public override void Build()
        {
            builder.Reset();
            next.Build();
        }

        public override void SetNextChain(ShipChain next)
        {
            this.next = next;
        }
    }

    public class BuildBase : ShipChain
    {
        public BuildBase(AbstractShipBuilder builder)
        {
            this.builder = builder;
        }
        public override void Build()
        {
            builder.BuildBase();
            next.Build();
        }

        public override void SetNextChain(ShipChain next)
        {
            this.next = next;
        }
    }

    public class BuildCoordinates : ShipChain
    {
        public BuildCoordinates(AbstractShipBuilder builder)
        {
            this.builder = builder;
        }
        public override void Build()
        {
            builder.BuildCoordinates();
            next.Build();
        }

        public override void SetNextChain(ShipChain next)
        {
            this.next = next;
        }
    }

    public class BuildSize : ShipChain
    {
        public BuildSize(AbstractShipBuilder builder)
        {
            this.builder = builder;
        }
        public override void Build()
        {
            builder.BuildSize();
        }

        public override void SetNextChain(ShipChain next)
        {
            this.next = next;
        }
    }
}