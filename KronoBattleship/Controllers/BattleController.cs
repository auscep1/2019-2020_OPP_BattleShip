using KronoBattleship.Datalayer;
using KronoBattleship.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System;
using KronoBattleship.DESIGN_PATTERNS.Builder;
using KronoBattleship.DESIGN_PATTERNS.Facade;

namespace KronoBattleship.Controllers
{
    [System.Web.Mvc.Authorize]
    public class BattleController : Controller
    {
        // GET: Battle/id
        public ActionResult Index(int battleId)
        {
            var db = ApplicationDbContext.GetInstance();
            Battle battle = db.Battles.Find(battleId);
            var currentUserName = getCurrentUserName();
            if (battle != null && (currentUserName.Equals(battle.PlayerName) || currentUserName.Equals(battle.EnemyName)))
            {
                return View(new BattleViewModel(battle, currentUserName));
            }
            return Redirect("/Chat/Index");
        }

        // POST: Battle/Create
        [HttpPost]
        public ActionResult Create(string user1, string user2)
        {

            string playerName, enemyName;

            getPlayers(user1, user2, out playerName, out enemyName);
            var db = ApplicationDbContext.GetInstance();
            Battle battle = db.Battles.Where(b => b.PlayerName.Equals(playerName) && b.EnemyName.Equals(enemyName)).FirstOrDefault();
            if (battle == null)
            {
                User player = db.Users.Where(n => n.UserName.Equals(playerName)).First();
                User enemy = db.Users.Where(n => n.UserName.Equals(enemyName)).First();
                battle = new Battle(player, enemy);
                db.Battles.Add(battle);
                db.SaveChanges();
            }
            var context = getContext();
            context.Clients.Group(getEnemyName(battle)).answer(User.Identity.Name, battle.BattleId);
            return RedirectToAction("Index", new { battleId = battle.BattleId });
        }


        // POST: Battle/Attack/5
        [HttpPost]
        public ActionResult Attack(int battleId, int attack)
        {
            bool hit;
            bool gameOver = false;
            string boardAfterAttack;
            var playerName = getCurrentUserName();
            var db = ApplicationDbContext.GetInstance();
            Battle battle = db.Battles.Find(battleId);

            // ====== PRIDETA LOGIKA ====== 
            var enemyName = battle.PlayerName == playerName ? battle.EnemyName : battle.PlayerName;
            var enemyShips = db.Ships.Where(x => x.BattleId == battle.BattleId && x.PlayerName == enemyName);
            int Xattack = attack % 10;
            int Yattack = attack / 10;
            var hittedShipCoor = enemyShips.SelectMany(xx => xx.Coordinates.Where(c => c.Alive == true && c.x == Xattack && c.y == Yattack).ToList()).FirstOrDefault();
            if(hittedShipCoor != null)
            {
                //var coorToChange = hittedShip.Coordinates.Where(c => c.Alive == true && c.x == Xattack && c.y == Yattack).FirstOrDefault();//enemyShips.Select(xx => xx.Coordinates.Where(c => c.Alive == true && c.x == Xattack && c.y == Yattack).FirstOrDefault()).FirstOrDefault();
                hittedShipCoor.Alive = false;
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
                foreach(var ship in shipsToRemove)
                {
                    db.Ships.Remove(ship);
                }
                // ====== end PRIDETA LOGIKA ======
            }
            else
            {
                battle.ActivePlayer = getEnemyName(battle);
            }
            db.SaveChanges();
            return Json(new { Hit = hit, GameOver = gameOver });

        }


        // GET: Battle/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public void GameOver(int battleId)
        {
            var db = ApplicationDbContext.GetInstance();
            Battle battle = db.Battles.Find(battleId);
            var currentUserName = getCurrentUserName();

            battle.PlayerBoard = "";
            battle.EnemyBoard = "";
            battle.ActivePlayer = "";
            if (battle.PlayerName.Equals(currentUserName))
            {
                battle.Player.Losses++;
                battle.Enemy.Wins++;
            }
            else
            {
                battle.Player.Wins++;
                battle.Enemy.Losses++;
            }
            // -------- PRIDETA LOGIKA ------
            var shipsToRemove = db.Ships.Where(x => x.BattleId == battle.BattleId);
            foreach (var ship in shipsToRemove)
            {
                db.Ships.Remove(ship);
            }
            // -------- end PRIDETA LOGIKA ------
            db.SaveChanges();
        }

        [HttpPost]
        public ActionResult Ready(int battleId, string playerBoard)
        {
            var db = ApplicationDbContext.GetInstance();
            Battle battle = db.Battles.Find(battleId);
            var currentUserName = getCurrentUserName();

            // ====== PRIDETA LOGIKA ======
            var currentPlayer = db.Users.FirstOrDefault(x => x.UserName == currentUserName);
            //LAIVAI: a5, m3, q3, i2, e2, LEKTUVAI:o1, s2 ----- coskg (viena grupe vertikali kita horizontali
            List<List<char>> list = new List<List<char>>();
            var copy = playerBoard;
            var ships = "acegikmoqs";
            for (int i = 0; i < copy.Length; i++)
            {
                if (ships.Contains(copy[i]) && ships.Length > 0)
                {
                    var x = i % 10;
                    var y = i / 10;
                    int endx = 0;
                    int endy = 0;

                    bool isHorizontal = i < copy.Length - 1 && copy[i + 1] == copy[i] ? true : false;
                    switch (copy[i])
                    {
                        case 'a':
                            endx = isHorizontal ? x + 4 : x;
                            endy = isHorizontal ? y : y + 4;
                            break;
                        case 'c':
                            endx = isHorizontal ? x + 4 : x;
                            endy = isHorizontal ? y : y + 4;
                            break;
                        case 'm':
                            endx = isHorizontal ? x + 2 : x;
                            endy = isHorizontal ? y : y + 2;
                            break;
                        case 'q':
                            endx = isHorizontal ? x + 2 : x;
                            endy = isHorizontal ? y : y + 2;
                            break;
                        case 'i':
                            endx = isHorizontal ? x + 1 : x;
                            endy = isHorizontal ? y : y + 1;
                            break;
                        case 'k':
                            endx = isHorizontal ? x + 1 : x;
                            endy = isHorizontal ? y : y + 1;
                            break;
                        case 'e':
                            endx = isHorizontal ? x + 1 : x;
                            endy = isHorizontal ? y : y + 1;
                            break;
                        case 'g':
                            endx = isHorizontal ? x + 1 : x;
                            endy = isHorizontal ? y : y + 1;
                            break;
                        case 'o':  /**vienvietis**/
                            isHorizontal = true;
                            endx = x;
                            endy = y;
                            break;
                        case 's':
                            endx = isHorizontal ? x + 1 : x;
                            endy = isHorizontal ? y : y + 1;
                            break;
                    }
                    ships = ships.Remove(ships.IndexOf(copy[i]), 1);
                    //Ship ship = new Ship(currentPlayer, battle);

                    //while (x <= endx && y <= endy)
                    //{
                    //    ship.Coordinates.Add(new ShipCoordinates(x, y, ship));
                    //    if (isHorizontal)
                    //    {
                    //        x++;
                    //    }
                    //    else
                    //    {
                    //        y++;
                    //    }
                    //}
                    //ship.Size = ship.Coordinates.Count;

                    Facade facade = new Facade(currentPlayer, battle, x, endx, y, endy, isHorizontal);
                    Ship ship = facade.GetShip();
                    
                    //var builder = new ShipBuilder(currentPlayer, battle, x, endx, y, endy, isHorizontal);
                    //builder.BuildBase();
                    //builder.BuildCoordinates();
                    //builder.BuildSize();
                    //Ship ship = builder.GetShip();

                    db.Ships.Add(ship);
                }
            }
            // ====== end PRIDETA LOGIKA ======
            

            //var db = new ApplicationDbContext();
            //Battle battle = db.Battles.Find(battleId);
            //var currentUserName = getCurrentUserName();
            string enemyBoard;
            if (currentUserName.Equals(battle.PlayerName))
            {
                battle.PlayerBoard = playerBoard;
                enemyBoard = battle.EnemyBoard;
                if (battle.EnemyBoard == "")
                    battle.ActivePlayer = battle.PlayerName;
            }
            else
            {
                battle.EnemyBoard = playerBoard;
                enemyBoard = battle.PlayerBoard;
                if (battle.PlayerBoard == "")
                    battle.ActivePlayer = battle.EnemyName;
            }
            db.SaveChanges();
            return Json(new { EnemyBoard = enemyBoard });
        }


        private IHubContext getContext()
        {
            return GlobalHost.ConnectionManager.GetHubContext<ConnectionHub>();
        }
        private string getCurrentUserName()
        {
            return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).UserName;
        }
        private string getEnemyName(Battle battle)
        {
            return battle.PlayerName.Equals(User.Identity.Name) ? battle.EnemyName : battle.PlayerName;
        }

        // Helpers
        private void getPlayers(string user1, string user2, out string player, out string enemy)
        {
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
        }

        private void shipHit(int attack, out bool hit, ref string enemyBoard)
        {
            // if the enemyboard contains any letter that indicates is a ship
            hit = "acegikmoqs".Contains(enemyBoard[attack]);
            char[] tempBoard = enemyBoard.ToCharArray();
            // increase the letter at the possition of the attack
            tempBoard[attack] = (char)(enemyBoard[attack] + 1);
            enemyBoard = new string(tempBoard);
        }

        private bool isTheGameOver(string enemyboard, Battle battle)
        {
            // ====== PRIDETA LOGIKA ======
            var db = ApplicationDbContext.GetInstance();
            var player1IsDead = db.Ships.Where(x => x.BattleId == battle.BattleId && x.PlayerName == battle.PlayerName).All(x => x.Coordinates.All(xx => xx.Alive == false));
            var player2IsDead = db.Ships.Where(x => x.BattleId == battle.BattleId && x.PlayerName == battle.EnemyName).All(x => x.Coordinates.All(xx => xx.Alive == false));
            /*UZKOMENTUOTA NES NU VA APACIOJ KITAIP 
             * return player1IsDead || player2IsDead;*/
            // ====== end PRIDETA LOGIKA ======

            // check if all the ships have been hit
            return enemyboard.FirstOrDefault(c => c != 'y' && c != 'z' && ("acegikmoqs".Contains(c)))
                             .Equals('\0');
        }
    }
}
