﻿using KronoBattleship.DESIGN_PATTERNS.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.Models
{
    public class Plane 
    {
        public Plane()
        {
            Coordinates = new List<ShipCoordinates>();
        }
        public Plane(User owner, Battle battle)
        {
            PlayerName = owner.UserName;
            Player = owner;
            BattleId = battle.BattleId;
            Battle = battle;
            Coordinates = new List<ShipCoordinates>();
        }
        public int PlaneId { get; set; }
        public int Size { get; set; }
        public List<ShipCoordinates> Coordinates { get; set; }
        public string PlayerName { get; set; }
        public virtual User Player { get; set; }
        public int BattleId { get; set; }
        public virtual Battle Battle { get; set; }
    }
}