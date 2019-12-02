using KronoBattleship.DESIGN_PATTERNS.Flyweight_pattern;
using KronoBattleship.DESIGN_PATTERNS.Memento;
using KronoBattleship.DESIGN_PATTERNS.State;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace KronoBattleship.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public static Random random = new Random();
        public User()
        {
            Losses = 0;
            Wins = 0;
            Picture = (1 + random.Next(22)) + ".png";
            State = new MementoClient();
            //Flyweight naudojimas:
            FlyweightFactory ff = new FlyweightFactory();
            State2 = new ContextState(ff.GetFlyweight('W'));
            System.Diagnostics.Debug.WriteLine("Flyweight: Waiting");
            //State naudojimas be flyweight:
            State2 = new ContextState(new Waiting());
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("Picture", this.Picture));
            return userIdentity;
        }
        public User Copy() => (User)this.MemberwiseClone();

        public int Wins { get; set; }
        public int Losses { get; set; }
        public string Picture { get; set; }
        private MementoClient State { get; set; }
        private ContextState State2 { get; set; }

        public void ChangeState()
        {
            this.State2.Request();
        }
        public string GetState()
        {
            return this.State2.GetUserState();
        }
        public void RestoreState()
        {
            //Flyweight naudojimas:
            FlyweightFactory ff = new FlyweightFactory();
            this.State2 = new ContextState(ff.GetFlyweight('W'));
            System.Diagnostics.Debug.WriteLine("Flyweight: Waiting");
            //State naudojimas be flyweight:
            this.State2 = new ContextState(new Waiting());
        }
        public void SetMementoStateFree()
        {
            this.State.SetStateFree();
        }
        public void SetMementoStatePlaying()
        {
            this.State.SetStatePlaying();
        }
        public void RestoreMementoState()
        {
            this.State.RestoreState();
        }
        public string GetMementoState()
        {
            return this.State.GetMementoState();
        }
    }
}