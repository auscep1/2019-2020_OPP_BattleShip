using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServer.Models
{
    public abstract class AbstractFactory
    {
        public abstract Plane GetPlane();
        public abstract Ship GetShip();
    }
    public class SinglePartFactory : AbstractFactory
    {
        public override Plane GetPlane()
        {
            return new Plane1();
        }

        public override Ship GetShip()
        {
            return new Ship1();
        }
    }
    public class TwoPartsFactory : AbstractFactory
    {
        public override Plane GetPlane()
        {
            return new Plane2();
        }

        public override Ship GetShip()
        {
            return new Ship2();
        }
    }
    public class ThreePartsFactory : AbstractFactory
    {
        public override Plane GetPlane()
        {
            return new Plane3();
        }

        public override Ship GetShip()
        {
            return new Ship3();
        }
    }
    public class FourPartsFactory : AbstractFactory
    {
        public override Plane GetPlane()
        {
            return new Plane4();
        }

        public override Ship GetShip()
        {
            return new Ship4();
        }
    }
}
