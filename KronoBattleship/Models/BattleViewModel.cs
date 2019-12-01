using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.Models
{
    public class BattleViewModel
    {
        public BattleViewModel(Battle battle, string currentUsername)
        {
            if (battle.PlayerName.Equals(currentUsername))
            {
                PlayerName = battle.PlayerName;
                EnemyName = battle.EnemyName;
                PlayerBoard = battle.PlayerBoard;
                EnemyBoard = battle.EnemyBoard;
                PlayerPicture = battle.Player.Picture;
                EnemyPicture = battle.Enemy.Picture;
                PlayerState = battle.Player.GetMementoState();
                EnemyState = battle.Enemy.GetMementoState();
                PlayerState2 = battle.Player.GetState();
                EnemyState2 = battle.Enemy.GetState();

            }
            else
            {
                PlayerName = battle.EnemyName;
                EnemyName = battle.PlayerName;
                PlayerBoard = battle.EnemyBoard;
                EnemyBoard = battle.PlayerBoard;
                PlayerPicture = battle.Enemy.Picture;
                EnemyPicture = battle.Player.Picture;
                PlayerState = battle.Enemy.GetMementoState();
                EnemyState = battle.Player.GetMementoState();
                PlayerState2 = battle.Enemy.GetState();
                EnemyState2 = battle.Player.GetState();
            }
            BattleId = battle.BattleId;
            ActivePlayer = battle.ActivePlayer;
        }
        public int BattleId { get; set; }
        public string PlayerName { get; set; }
        public string EnemyName { get; set; }
        public string PlayerBoard { get; set; }
        public string EnemyBoard { get; set; }
        public string ActivePlayer { get; set; }
        public string PlayerPicture { get; set; }
        public string EnemyPicture { get; set; }
        public string PlayerState { get; set; }
        public string EnemyState { get; set; }
        public string PlayerState2 { get; set; }
        public string EnemyState2 { get; set; }
    }
}