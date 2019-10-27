using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KronoBattleship.Models;

namespace KronoBattleship.DESIGN_PATTERNS.Factory
{
     public abstract class Unit
     {
         public abstract Unit UnitObj { get; } //temporrary
         public abstract string ProductName { get; } //temporrary

         //public abstract int UnitId { get; set; }
         public abstract int Size { get; set; }
         public abstract List<ShipCoordinates> Coordinates { get; set; }
         public abstract string PlayerName { get; set; }
         public abstract int BattleId { get; set; }

         public virtual User Player { get; set; }
         public virtual Battle Battle { get; set; }
     }
}