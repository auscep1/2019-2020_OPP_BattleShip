using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.DESIGN_PATTERNS.Command
{
    public class Command
    {
        public class GetExtraLargeShipLocation : ICommand
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
        }
        public class GetLargeShipLocation : ICommand
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
        }
        public class GetNormalShipLocation : ICommand
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
        }
        public class GetSmallShipLocation : ICommand
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
        }
        public class GetExtraSmallShipLocation : ICommand
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
        }
    }
    
}