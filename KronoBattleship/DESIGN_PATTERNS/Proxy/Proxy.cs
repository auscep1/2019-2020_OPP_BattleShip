using KronoBattleship.Datalayer;
using KronoBattleship.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.DESIGN_PATTERNS.Proxy
{
    // https://www.geeksforgeeks.org/proxy-design-pattern/
    public abstract class IProxy
    {
        public abstract Tuple<bool, bool> Attack(int attack, int battleId);
    }

    public class AttackProxy : IProxy
    {
        public override Tuple<bool, bool> Attack(int attack, int battleId)
        {
            System.Diagnostics.Debug.WriteLine("AttackProxy: called attack method");
            IProxy attackObj = new AttackObject();
            var result = attackObj.Attack(attack, battleId);
            System.Diagnostics.Debug.WriteLine("AttackProxy: returnting attack method result");
            return result;
        }
    }
    public class AttackObject : IProxy
    {
        public override Tuple<bool, bool> Attack(int attack, int battleId)
        {
            System.Diagnostics.Debug.WriteLine("AttackObject: called attack method");
            bool hit;
            bool gameOver = false;
            string boardAfterAttack;
            var playerName = getCurrentUserName();
            var db = ApplicationDbContext.GetInstance();
            Battle battle = db.Battles.Find(battleId);

            // ====== PRIDETA LOGIKA ====== 
            var enemyName = battle.PlayerName == playerName ? battle.EnemyName : battle.PlayerName;
            var enemyShips = db.Ships.Where(x => x.BattleId == battle.BattleId && x.PlayerName == enemyName);
            var enemyPlanes = db.Planes.Where(x => x.BattleId == battle.BattleId && x.PlayerName == enemyName);
            int Xattack = attack % 10;
            int Yattack = attack / 10;
            var hittedShipCoor = enemyShips.SelectMany(xx => xx.Coordinates.Where(c => c.Alive == true && c.x == Xattack && c.y == Yattack).ToList()).FirstOrDefault();
            var hittedPlaneCoor = enemyPlanes.SelectMany(xx => xx.Coordinates.Where(c => c.Alive == true && c.x == Xattack && c.y == Yattack).ToList()).FirstOrDefault();
            if (hittedShipCoor != null)
            {
                //var coorToChange = hittedShip.Coordinates.Where(c => c.Alive == true && c.x == Xattack && c.y == Yattack).FirstOrDefault();//enemyShips.Select(xx => xx.Coordinates.Where(c => c.Alive == true && c.x == Xattack && c.y == Yattack).FirstOrDefault()).FirstOrDefault();
                hittedShipCoor.Alive = false;
            }
            if (hittedPlaneCoor != null)
            {
                hittedPlaneCoor.Alive = false;
            }
            // ====== end PRIDETA LOGIKA ======

            if (playerName.Equals(battle.PlayerName))
            {
                boardAfterAttack = battle.EnemyBoard;
                shipHit(attack, out hit, ref boardAfterAttack);
                battle.EnemyBoard = boardAfterAttack;
            }
            else
            {
                boardAfterAttack = battle.PlayerBoard;
                shipHit(attack, out hit, ref boardAfterAttack);
                battle.PlayerBoard = boardAfterAttack;
            }

            if (isTheGameOver(boardAfterAttack, battle))
            {
                gameOver = true;
                battle.PlayerBoard = "";
                battle.EnemyBoard = "";
                battle.ActivePlayer = "";
                if (battle.PlayerName.Equals(playerName))
                {
                    battle.Player.Wins++;
                    battle.Enemy.Losses++;
                }
                else
                {
                    battle.Player.Losses++;
                    battle.Enemy.Wins++;
                }
                // ====== PRIDETA LOGIKA ======
                var shipsToRemove = db.Ships.Where(x => x.BattleId == battle.BattleId);
                foreach (var ship in shipsToRemove)
                {
                    db.Ships.Remove(ship);
                }
                var planesToRemove = db.Planes.Where(x => x.BattleId == battle.BattleId);
                foreach (var plane in planesToRemove)
                {
                    db.Planes.Remove(plane);
                }

                //>>>>>>>>>>>>>>>>>>---------State & Memento 2019-12-01-------------------
                battle.Player.RestoreMementoState();
                battle.Enemy.RestoreMementoState();
                battle.Player.ChangeState();
                battle.Enemy.ChangeState();
                db.SaveChanges();
                //------------------------------------------------------------------
                // ====== end PRIDETA LOGIKA ======
            }
            else
            {
                battle.ActivePlayer = enemyName;
            }
            db.SaveChanges();
            return new Tuple<bool, bool>(hit, gameOver);
        }
        private string getCurrentUserName()
        {
            System.Diagnostics.Debug.WriteLine("AttackObject: getCurrentUserName method");
            return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).UserName;
        }
        private void shipHit(int attack, out bool hit, ref string enemyBoard)
        {
            System.Diagnostics.Debug.WriteLine("AttackObject: shipHit method");
            // if the enemyboard contains any letter that indicates is a ship
            hit = "acegikmoqs".Contains(enemyBoard[attack]);
            char[] tempBoard = enemyBoard.ToCharArray();
            // increase the letter at the possition of the attack
            tempBoard[attack] = (char)(enemyBoard[attack] + 1);
            enemyBoard = new string(tempBoard);
        }
        private bool isTheGameOver(string enemyboard, Battle battle)
        {
            System.Diagnostics.Debug.WriteLine("AttackObject: isTheGameOver method");
            // ====== PRIDETA LOGIKA ======
            var db = ApplicationDbContext.GetInstance();
            var player1IsDead = db.Ships.Where(x => x.BattleId == battle.BattleId && x.PlayerName == battle.PlayerName).All(x => x.Coordinates.All(xx => xx.Alive == false));
            var player2IsDead = db.Ships.Where(x => x.BattleId == battle.BattleId && x.PlayerName == battle.EnemyName).All(x => x.Coordinates.All(xx => xx.Alive == false));

            var player1IsDeadPlane = db.Planes.Where(x => x.BattleId == battle.BattleId && x.PlayerName == battle.PlayerName).All(x => x.Coordinates.All(xx => xx.Alive == false));
            var player2IsDeadPlane = db.Planes.Where(x => x.BattleId == battle.BattleId && x.PlayerName == battle.EnemyName).All(x => x.Coordinates.All(xx => xx.Alive == false));

            /*UZKOMENTUOTA NES NU VA APACIOJ KITAIP 
             * return player1IsDead || player2IsDead;*/
            // ====== end PRIDETA LOGIKA ======

            // check if all the ships have been hit
            return enemyboard.FirstOrDefault(c => c != 'y' && c != 'z' && ("acegikmoqs".Contains(c)))
                             .Equals('\0');
        }
    }
}