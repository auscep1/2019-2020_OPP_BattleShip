using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KronoBattleship.Models;
using KronoBattleship.DESIGN_PATTERNS.Prototype_pattern;

namespace KronoBattleship.DESIGN_PATTERNS.Factory
{
    public abstract class Unit : IPrototype
    {
        public abstract Unit Product { get; }
        public abstract string ProductName { get; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /**   protected List<ShipCoordinates> Coordinates;
  protected string PlayerName;
  protected int BattleId;
  protected User Player;
  protected Battle Battle;
  protected Unit()
  {
      Coordinates = new List<ShipCoordinates>();
  }
  protected Unit(User owner, Battle battle)
  {
      PlayerName = owner.UserName;
      Player = owner;
      BattleId = battle.BattleId;
      Battle = battle;
      Coordinates = new List<ShipCoordinates>();
  }**/
    }
}