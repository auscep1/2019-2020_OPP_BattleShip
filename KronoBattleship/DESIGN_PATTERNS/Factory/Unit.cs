using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KronoBattleship.Models;

namespace KronoBattleship.DESIGN_PATTERNS.AbstractFactory
{
    public abstract class Unit
    {
        protected List<ShipCoordinates> Coordinates;
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
        }
    }
}