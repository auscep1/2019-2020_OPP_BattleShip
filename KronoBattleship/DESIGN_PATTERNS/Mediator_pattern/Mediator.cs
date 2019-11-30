using KronoBattleship.Datalayer;
using KronoBattleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.DESIGN_PATTERNS.Mediator_pattern
{
    public class Mediator
    {
        PlayersManager playersManager { get; set; }
        BattleManager battleManager { get; set; }
        UserManager userManager { get; set; }
        public PlayersManager PlayersManager
        {
            set { playersManager = value; }
        }
        public BattleManager BattleManager
        {
            set { battleManager = value; }
        }
        public UserManager UserManager
        {
            set { userManager = value; }
        }
        public string GetPlayers(string user1, string user2, out string enemy)
        {
            return playersManager.Find(user1, user2, out enemy);
        }
        public Battle AddBattle(ApplicationDbContext db, string user1, string user2)
        {
            return battleManager.AddBattle(db, user1, user2);
        }
        public User FindUser(ApplicationDbContext db, string name)
        {
            return userManager.Find(db, name);
        }
    }
    public abstract class Manager
    {
        protected Mediator mediator;
        public Manager(Mediator mediator)
        {
            this.mediator = mediator;
        }
    }
    public class PlayersManager : Manager
    {
        public PlayersManager(Mediator mediator) : base(mediator) { }
        public string Find(string user1, string user2, out string enemy)
        {
            string player;
            if (user1.CompareTo(user2) < 0)
            {
                player = user1;
                enemy = user2;
            }
            else
            {
                player = user2;
                enemy = user1;
            }
            return player;
        }
    }
    public class BattleManager : Manager
    {
        public BattleManager(Mediator mediator) : base(mediator) { }
        public Battle AddBattle(ApplicationDbContext db, string user1, string user2)
        {
            string playerName, enemyName;
            playerName = mediator.GetPlayers(user1, user2, out enemyName);
            Battle battle = db.Battles.Where(b => b.PlayerName.Equals(playerName) && b.EnemyName.Equals(enemyName)).FirstOrDefault();
            if (battle == null)
            {
                User player = mediator.FindUser(db, playerName);
                User enemy = mediator.FindUser(db, enemyName);
                battle = new Battle(player, enemy);
                db.Battles.Add(battle);
                db.SaveChanges();
            }
            return battle;
        }
    }
    public class UserManager : Manager
    {
        public UserManager(Mediator mediator) : base(mediator) { }

        public User Find(ApplicationDbContext db, string name)
        {
            return db.Users.Where(n => n.UserName.Equals(name)).First();
        }
    }
}