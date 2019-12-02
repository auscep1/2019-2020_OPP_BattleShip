using KronoBattleship.DESIGN_PATTERNS.Visitor;
using System;
using System.Collections.Generic;

namespace KronoBattleship.DESIGN_PATTERNS.Command
{
    public class Command
    {
        public class GetExtraLargeShipLocation : ICommand, IVisitable
        {
            public List<int> Execute(bool isHorizontal, int iteration)
            {
                var x = iteration % 10;
                var y = iteration / 10;

                int endx = isHorizontal ? x + 4 : x;
                int endy = isHorizontal ? y : y + 4;

                return new List<int>() { endx, endy };
            }

            public List<int> Undo()
            {
                return new List<int> { 0, 0 };
            }

            List<int> IVisitable.accept(IVisitor visitor, bool isHorizontal, int iteration)
            {
                return visitor.visit(this, isHorizontal, iteration);
            }
        }
        public class GetLargeShipLocation : ICommand, IVisitable
        {
            public List<int> Execute(bool isHorizontal, int iteration)
            {
                var x = iteration % 10;
                var y = iteration / 10;

                int endx = isHorizontal ? x + 2 : x;
                int endy = isHorizontal ? y : y + 2;

                return new List<int>() { endx, endy };
            }

            public List<int> Undo()
            {
                return new List<int> { 0, 0 };
            }

            List<int> IVisitable.accept(IVisitor visitor, bool isHorizontal, int iteration)
            {
                return visitor.visit(this, isHorizontal, iteration);
            }
        }
        public class GetNormalShipLocation : ICommand, IVisitable
        {
            public List<int> Execute(bool isHorizontal, int iteration)
            {
                var x = iteration % 10;
                var y = iteration / 10;

                int endx = isHorizontal ? x + 2 : x;
                int endy = isHorizontal ? y : y + 2;

                return new List<int>() { endx, endy };
            }

            public List<int> Undo()
            {
                return new List<int> { 0, 0 };
            }

            List<int> IVisitable.accept(IVisitor visitor, bool isHorizontal, int iteration)
            {
                return visitor.visit(this, isHorizontal, iteration);
            }
        }
        public class GetSmallShipLocation : ICommand, IVisitable
        {
            public List<int> Execute(bool isHorizontal, int iteration)
            {
                var x = iteration % 10;
                var y = iteration / 10;

                int endx = isHorizontal ? x + 1 : x;
                int endy = isHorizontal ? y : y + 1;

                return new List<int>() { endx, endy };
            }

            public List<int> Undo()
            {
                return new List<int> { 0, 0 };
            }

            List<int> IVisitable.accept(IVisitor visitor, bool isHorizontal, int iteration)
            {
                return visitor.visit(this, isHorizontal, iteration);
            }
        }
        public class GetExtraSmallShipLocation : ICommand, IVisitable
        {
            public List<int> Execute(bool isHorizontal, int iteration)
            {
                var x = iteration % 10;
                var y = iteration / 10;

                int endx = isHorizontal ? x + 1 : x;
                int endy = isHorizontal ? y : y + 1;

                return new List<int>() { endx, endy };
            }

            public List<int> Undo()
            {
                return new List<int> { 0, 0 };
            }

            List<int> IVisitable.accept(IVisitor visitor, bool isHorizontal, int iteration)
            {
                return visitor.visit(this, isHorizontal, iteration);
            }
        }
    }
    
}