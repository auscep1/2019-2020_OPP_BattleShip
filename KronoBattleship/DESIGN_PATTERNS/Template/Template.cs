using KronoBattleship.Datalayer;
using KronoBattleship.DESIGN_PATTERNS.Adapter;
using KronoBattleship.DESIGN_PATTERNS.Command;
using KronoBattleship.DESIGN_PATTERNS.Visitor;
using KronoBattleship.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static KronoBattleship.DESIGN_PATTERNS.Command.Command;

namespace KronoBattleship.DESIGN_PATTERNS.Template
{
    //https://refactoring.guru/design-patterns/template-method/csharp/example

    /// <summary>
    /// Template class
    /// </summary>
    abstract class Template
    {
        public void TemplateMethod(int battleId, string playerBoard)
        {
            this.BaseOperation();
            this.BattleShipObjects(battleId, playerBoard);
        }
        protected void BaseOperation()
        {
            System.Diagnostics.Debug.WriteLine("AbstractTempate : Base method");
        }
        protected abstract void BattleShipObjects(int battleId, string playerBoard);
    }

    /// <summary>
    /// Client class implements Template method
    /// </summary>
    class Client
    {
        public static void ClientCode(Template abstractTemplate, int battleId, string playerBoard)
        {
            abstractTemplate.TemplateMethod(battleId, playerBoard);
        }
    }

    /// <summary>
    /// TemplateShips creates ships for battleship game
    /// </summary>
    class TemplateShips : Template
    {
        protected override void BattleShipObjects(int battleId, string playerBoard)
        {
            var db = ApplicationDbContext.GetInstance();
            Battle battle = db.Battles.Find(battleId);
            var currentUserName = getCurrentUserName();

            var currentPlayer = db.Users.FirstOrDefault(x => x.UserName == currentUserName);

            var copy = playerBoard;
            var ships = "acegikmq";

            //Code for Command design patter

            List<ICommand> commands = new List<ICommand>();

            ICommand extraLargeShip = new GetExtraLargeShipLocation();
            ICommand largeShip = new GetLargeShipLocation();
            ICommand normalShip = new GetNormalShipLocation();
            ICommand smallShip = new GetSmallShipLocation();
            ICommand extraSmallShip = new GetExtraSmallShipLocation();

            commands.Add(extraLargeShip);
            commands.Add(largeShip);
            commands.Add(normalShip);
            commands.Add(smallShip);
            commands.Add(extraSmallShip);

            //Code for Visitor design patter

            IVisitor visitor = new ShipVisitor();

            IVisitable getExtraLargeShip = new GetExtraLargeShipLocation();
            IVisitable getLargeShip = new GetLargeShipLocation();
            IVisitable getNormalShip = new GetNormalShipLocation();
            IVisitable getSmallShip = new GetSmallShipLocation();
            IVisitable getExtraSmallShip = new GetExtraSmallShipLocation();

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
                            //This line is used for command design patter
                            List<int> extraLargeShipLocation = commands[0].Execute(isHorizontal, i);
                            //This line is used for visitor design patter
                            List<int> getExtraLargeShipLocation = getExtraLargeShip.accept(visitor, isHorizontal, i);
                            //endx = extraLargeShipLocation[0];
                            //endy = extraLargeShipLocation[1];
                            endx = getExtraLargeShipLocation[0];
                            endy = getExtraLargeShipLocation[1];
                            //System.Diagnostics.Debug.WriteLine("Command: Location of extra large ship was found");
                            break;
                        case 'm':
                            //This line is used for command design patter
                            List<int> largeShipLocation = commands[1].Execute(isHorizontal, i);
                            //This line is used for visitor design patter
                            List<int> getLargeShipLocation = getLargeShip.accept(visitor, isHorizontal, i);
                            //endx = largeShipLocation[0];
                            //endy = largeShipLocation[1];
                            endx = getLargeShipLocation[0];
                            endy = getLargeShipLocation[1];
                            //System.Diagnostics.Debug.WriteLine("Command: Location of large ship was found");
                            break;
                        case 'q':
                            //This line is used for command design patter
                            List<int> normalShipLocation = commands[2].Execute(isHorizontal, i);
                            //This line is used for visitor design patter
                            List<int> getNormalShipLocation = getNormalShip.accept(visitor, isHorizontal, i);
                            //endx = normalShipLocation[0];
                            //endy = normalShipLocation[1];
                            endx = getNormalShipLocation[0];
                            endy = getNormalShipLocation[1];
                            //System.Diagnostics.Debug.WriteLine("Command: Location of normal ship was found");
                            break;
                        case 'i':
                            //This line is used for command design patter
                            List<int> smallShipLocation = commands[3].Execute(isHorizontal, i);
                            //This line is used for visitor design patter
                            List<int> getSmallShipLocation = getSmallShip.accept(visitor, isHorizontal, i);
                            //endx = smallShipLocation[0];
                            //endy = smallShipLocation[1];
                            endx = getSmallShipLocation[0];
                            endy = getSmallShipLocation[1];
                            //System.Diagnostics.Debug.WriteLine("Command: Location of small ship was found");
                            break;
                        case 'e':
                            //This line is used for command design patter
                            List<int> extraSmallShipLocation = commands[4].Execute(isHorizontal, i);
                            //This line is used for visitor design patter
                            List<int> getExtraSmallShipLocation = getExtraSmallShip.accept(visitor, isHorizontal, i);
                            //endx = extraSmallShipLocation[0];
                            //endy = extraSmallShipLocation[1];
                            endx = getExtraSmallShipLocation[0];
                            endy = getExtraSmallShipLocation[1];
                            //System.Diagnostics.Debug.WriteLine("Command: Location of extra small ship was found");
                            break;
                        case 'c':
                            endx = isHorizontal ? x + 4 : x;
                            endy = isHorizontal ? y : y + 4;
                            break;
                        case 'k':
                            endx = isHorizontal ? x + 1 : x;
                            endy = isHorizontal ? y : y + 1;
                            break;
                        case 'g':
                            endx = isHorizontal ? x + 1 : x;
                            endy = isHorizontal ? y : y + 1;
                            break;
                    }
                    ships = ships.Remove(ships.IndexOf(copy[i]), 1);
                    //Facade facade = new Facade(currentPlayer, battle, x, endx, y, endy, isHorizontal);
                    IAdapter shipAdapter = new ShipAdapter(currentPlayer, battle, x, endx, y, endy, isHorizontal);
                    //Ship ship = facade.GetShip();
                    Ship ship = (Ship)shipAdapter.GetObject(0);
                    db.Ships.Add(ship);
                }
            }
            System.Diagnostics.Debug.WriteLine("TemplateShips : Template. Required operation- BattleShipObjects");
        }
        private string getCurrentUserName()
        {
            return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).UserName;
        }
    }
    /// <summary>
    /// TemplatePlanes creates planes for battleship game
    /// </summary>
    class TemplatePlanes : Template
    {
        protected override void BattleShipObjects(int battleId, string playerBoard)
        {
            var db = ApplicationDbContext.GetInstance();
            Battle battle = db.Battles.Find(battleId);
            var currentUserName = getCurrentUserName();

            var currentPlayer = db.Users.FirstOrDefault(x => x.UserName == currentUserName);

            var copy = playerBoard;
            var planes = "os";

            for (int i = 0; i < copy.Length; i++)
            {
                if (planes.Contains(copy[i]) && planes.Length > 0)
                {
                    var x = i % 10;
                    var y = i / 10;
                    int endx = 0;
                    int endy = 0;

                    bool isHorizontal = i < copy.Length - 1 && copy[i + 1] == copy[i] ? true : false;
                    int type = 0;
                    switch (copy[i])
                    {
                        case 'o':  /**vienvietis**/
                            type = 1;
                            isHorizontal = true;
                            endx = x;
                            endy = y;
                            break;
                        case 's':
                            type = 2;
                            endx = isHorizontal ? x + 1 : x;
                            endy = isHorizontal ? y : y + 1;
                            break;
                    }
                    planes = planes.Remove(planes.IndexOf(copy[i]), 1);

                    var facade = new Facade.Facade(currentPlayer, battle, x, endx, y, endy, isHorizontal);
                    IAdapter planeAdapter = new PlaneAdapter(currentPlayer, battle, x, endx, y, endy, isHorizontal);
                    Plane plane = (Plane)planeAdapter.GetObject(0);

                    db.Planes.Add(plane as Plane);
                }
            }
            System.Diagnostics.Debug.WriteLine("TemplatePlanes : Template. Required operation- BattleShipObjects");
        }
        private string getCurrentUserName()
        {
            return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId()).UserName;
        }
    }
}