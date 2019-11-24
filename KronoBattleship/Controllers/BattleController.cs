using KronoBattleship.Datalayer;
using KronoBattleship.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KronoBattleship.DESIGN_PATTERNS.Template;
using KronoBattleship.DESIGN_PATTERNS.Proxy;
using KronoBattleship.DESIGN_PATTERNS.Memento;

namespace KronoBattleship.Controllers
{
    [System.Web.Mvc.Authorize]
    public class BattleController : Controller
    {
        //>>>>>>>>>>--Memento logic for players state 20191124------------------

        private MementoClient memento = new MementoClient();
        //-----------------------------------------------------------<<<<<<<<<<<

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
                //>>>>>>>>>>>>-Memento logic for players state 20191124------------------
                player.State = memento.SetStateFree();
                //---------------------------------------------------------<<<<<<<<<<<<<<
                User enemy = db.Users.Where(n => n.UserName.Equals(enemyName)).First();
                //>>>>>>>>>>>>-Memento logic for players state 20191124------------------
                enemy.State = memento.SetStateFree();
                //---------------------------------------------------------<<<<<<<<<<<<<<
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
            #region IŠKELTA LOGIKA
            /*bool hit;
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
                foreach(var ship in shipsToRemove)
                {
                    db.Ships.Remove(ship);
                }
				var planesToRemove = db.Planes.Where(x => x.BattleId == battle.BattleId);
				foreach (var plane in planesToRemove)
				{
					db.Planes.Remove(plane);
				}
				// ====== end PRIDETA LOGIKA ======
			}
            else
            {
                battle.ActivePlayer = getEnemyName(battle);
            }
            db.SaveChanges();*/
            #endregion
            IProxy attackProxy = new AttackProxy();
            var result = attackProxy.Attack(attack, battleId);
            //return Json(new { Hit = hit, GameOver = gameOver });
            return Json(new { Hit = result.Item1, GameOver = result.Item2 });
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
            //>>>>>>>>>>>>>>-Memento logic for players state 20191124-----------
            //-------------------------------------------------------<<<<<<<<<<<
            battle.Player.State = memento.SetStateFree();
            battle.Player.State = memento.SetStatePlaying();
            battle.Player.State = memento.RestoreState();
            battle.Enemy.State = memento.SetStateFree();
            battle.Enemy.State = memento.SetStatePlaying();
            battle.Enemy.State = memento.RestoreState();
            //-------------------------------------------------------<<<<<<<<<<<
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
            db.SaveChanges();
            var planesToRemove = db.Planes.Where(x => x.BattleId == battle.BattleId);
            foreach (var plane in planesToRemove)
            {
                db.Planes.Remove(plane);
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
            //LAIVAI: a5, m3, q3, i2, e2, LEKTUVAI:o1, s2 ----- coskg (viena grupe vertikali kita horizontali
            // List<List<char>> list = new List<List<char>>();


            //>>>>>>>>>>>>>>>>>---iskelta i Template 2019-11-12
            Client.ClientCode(new TemplateShips(), battleId, playerBoard);
            Client.ClientCode(new TemplatePlanes(), battleId, playerBoard);
            //------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            // ====== end PRIDETA LOGIKA ======


            //var db = new ApplicationDbContext();
            //Battle battle = db.Battles.Find(battleId);
            //var currentUserName = getCurrentUserName();
            string enemyBoard;
            if (currentUserName.Equals(battle.PlayerName))
            {
                battle.PlayerBoard = playerBoard;
                enemyBoard = battle.EnemyBoard;
                //>>>>>>>>>>>>-Memento logic for players state 20191124------------------
                battle.Player.State = memento.SetStatePlaying();
                //---------------------------------------------------------<<<<<<<<<<<<<<
                if (battle.EnemyBoard == "")
                {
                    battle.ActivePlayer = battle.PlayerName;
                }
            }
            else
            {
                battle.EnemyBoard = playerBoard;
                enemyBoard = battle.PlayerBoard;
                //>>>>>>>>>>>>-Memento logic for players state 20191124------------------
                battle.Enemy.State = memento.SetStatePlaying();
                //---------------------------------------------------------<<<<<<<<<<<<<<
                if (battle.PlayerBoard == "")
                {
                    battle.ActivePlayer = battle.EnemyName;
                }
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
        #region iškelta į proxy, nes atakos logika
        //private void shipHit(int attack, out bool hit, ref string enemyBoard)
        //{
        //    // if the enemyboard contains any letter that indicates is a ship
        //    hit = "acegikmoqs".Contains(enemyBoard[attack]);
        //    char[] tempBoard = enemyBoard.ToCharArray();
        //    // increase the letter at the possition of the attack
        //    tempBoard[attack] = (char)(enemyBoard[attack] + 1);
        //    enemyBoard = new string(tempBoard);
        //}

        //     private bool isTheGameOver(string enemyboard, Battle battle)
        //     {
        //         // ====== PRIDETA LOGIKA ======
        //         var db = ApplicationDbContext.GetInstance();
        //         var player1IsDead = db.Ships.Where(x => x.BattleId == battle.BattleId && x.PlayerName == battle.PlayerName).All(x => x.Coordinates.All(xx => xx.Alive == false));
        //         var player2IsDead = db.Ships.Where(x => x.BattleId == battle.BattleId && x.PlayerName == battle.EnemyName).All(x => x.Coordinates.All(xx => xx.Alive == false));

        //var player1IsDeadPlane = db.Planes.Where(x => x.BattleId == battle.BattleId && x.PlayerName == battle.PlayerName).All(x => x.Coordinates.All(xx => xx.Alive == false));
        //var player2IsDeadPlane = db.Planes.Where(x => x.BattleId == battle.BattleId && x.PlayerName == battle.EnemyName).All(x => x.Coordinates.All(xx => xx.Alive == false));

        ///*UZKOMENTUOTA NES NU VA APACIOJ KITAIP 
        //          * return player1IsDead || player2IsDead;*/
        //// ====== end PRIDETA LOGIKA ======

        //// check if all the ships have been hit
        //return enemyboard.FirstOrDefault(c => c != 'y' && c != 'z' && ("acegikmoqs".Contains(c)))
        //                          .Equals('\0');
        //     }
        #endregion
    }
}
