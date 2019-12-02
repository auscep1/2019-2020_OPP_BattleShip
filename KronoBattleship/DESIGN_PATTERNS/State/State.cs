using KronoBattleship.Datalayer;
using KronoBattleship.DESIGN_PATTERNS.Flyweight_pattern;
using KronoBattleship.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.DESIGN_PATTERNS.State
{
    //https://www.dofactory.com/net/state-design-pattern


    /// <summary>
    /// The 'State' abstract class
    /// </summary>
    abstract class State
    {
        public abstract void Handle(ContextState context);
    }
    /// <summary>
    /// ConcreteStateConnected class
    /// </summary>
    class ConcreteStateConnected : State
    {
        public override void Handle(ContextState context)
        {
            context.State = new Waiting();
        }
    }
    /// <summary>
    /// Concrete sate class
    /// </summary>
    class Waiting : State
    {
        public override void Handle(ContextState context)
        {
            context.State = new Joined();
        }
    }
    /// <summary>
    /// Concrete sate class
    /// </summary>
    class Joined : State
    {
        public override void Handle(ContextState context)
        {
            context.State = new Ready();
        }
    }
    /// <summary>
    /// Concrete sate class
    /// </summary>
    class Ready : State
    {
        public override void Handle(ContextState context)
        {
            context.State = new Playing();
        }
    }
    /// <summary>
    /// Concrete sate class
    /// </summary>
    class Playing : State
    {
        public override void Handle(ContextState context)
        {
            context.State = new Waiting();
        }
    }
    /// <summary>
    /// Context class
    /// </summary>
    class ContextState
    {
        private State state;
        private string userState;
        // Constructor
        public ContextState(State state)
        {
            this.State = state;
        }
        // Gets or sets the state
        public State State
        {
            get { return state; }
            set
            {
                state = value;
                userState = state.GetType().Name;
                System.Diagnostics.Debug.WriteLine("StatePattern: " + state.GetType().Name);
            }
        }
        public void Request()
        {
            state.Handle(this);
        }
        public string GetUserState()
        {
           return this.userState;
        }
    }

    ///// <summary>
    ///// The 'State' abstract class
    ///// </summary>
    //abstract class State
    //{
    //    public abstract void Handle(ContextState context, ApplicationDbContext db, Battle battle, User user);
    //}
    ///// <summary>
    ///// ConcreteStateConnected class
    ///// </summary>
    //class ConcreteStateConnected : State
    //{
    //    public override void Handle(ContextState context, ApplicationDbContext db, Battle battle, User user)
    //    {
    //        context.Db = db;
    //        context.Battle = battle;
    //        context.User = user;
    //        context.State = new Waiting(db, battle, user);
    //    }
    //}
    ///// <summary>
    ///// Concrete sate class
    ///// </summary>
    //class Waiting : State
    //{
    //    private ApplicationDbContext db { get; set; }
    //    private User user { get; set; }
    //    private Battle battle { get; set; }

    //    public Waiting(ApplicationDbContext db, Battle battle, User user)
    //    {
    //        this.db = db;
    //        this.battle = battle;
    //        this.user = user;
    //    }

    //    public override void Handle(ContextState context, ApplicationDbContext db, Battle battle, User user)
    //    {
    //        context.Db = db;
    //        context.Battle = battle;
    //        context.User = user;
    //        context.State = new Joined(db, battle, user);
    //    }
    //}
    ///// <summary>
    ///// Concrete sate class
    ///// </summary>
    //class Joined : State
    //{
    //    private ApplicationDbContext db { get; set; }
    //    private User user { get; set; }
    //    private Battle battle { get; set; }

    //    public Joined(ApplicationDbContext db, Battle battle, User user)
    //    {
    //        this.db = db;
    //        this.battle = battle;
    //        this.user = user;
    //    }

    //    public override void Handle(ContextState context, ApplicationDbContext db, Battle battle, User user)
    //    {
    //        context.Db = db;
    //        context.Battle = battle;
    //        context.User = user;
    //        context.State = new Ready(db, battle, user);
    //    }
    //}
    ///// <summary>
    ///// Concrete sate class
    ///// </summary>
    //class Ready : State
    //{
    //    private ApplicationDbContext db { get; set; }
    //    private User user { get; set; }
    //    private Battle battle { get; set; }

    //    public Ready(ApplicationDbContext db, Battle battle, User user)
    //    {
    //        this.db = db;
    //        this.battle = battle;
    //        this.user = user;
    //    }

    //    public override void Handle(ContextState context, ApplicationDbContext db, Battle battle, User user)
    //    {
    //        context.Db = db;
    //        context.Battle = battle;
    //        context.User = user;
    //        context.State = new Playing(db, battle, user);
    //    }
    //}
    ///// <summary>
    ///// Concrete sate class
    ///// </summary>
    //class Playing : State
    //{
    //    private ApplicationDbContext db { get; set; }
    //    private User user { get; set; }
    //    private Battle battle { get; set; }

    //    public Playing(ApplicationDbContext db, Battle battle, User user)
    //    {
    //        this.db = db;
    //        this.battle = battle;
    //        this.user = user;
    //    }

    //    public override void Handle(ContextState context, ApplicationDbContext db, Battle battle, User user)
    //    {
    //        context.Db = db;
    //        context.Battle = battle;
    //        context.User = user;
    //        context.State = new Waiting(db, battle, user);
    //    }
    //}
    ///// <summary>
    ///// Context class
    ///// </summary>
    //class ContextState
    //{
    //    private ApplicationDbContext db;
    //    private User user;
    //    private Battle battle;
    //    private State state;
    //    // Constructor
    //    public ContextState(State state, ApplicationDbContext db, Battle battle, User user)
    //    {
    //        this.State = state;
    //        this.Db = db;
    //        this.User = user;
    //        this.attle = battle;
    //    }
    //    // Gets or sets the state
    //    public State State
    //    {
    //        get { return state; }
    //        set
    //        {
    //            state = value;
    //            System.Diagnostics.Debug.WriteLine("State: " + state.GetType().Name);
    //            user.State2 = "State: " + state.GetType().Name;
    //            db.SaveChanges();
    //        }
    //    }
    //    public Battle Battle
    //    {
    //        get { return battle; }
    //        set
    //        {
    //            battle = value;
    //        }
    //    }
    //    public User User
    //    {
    //        get { return user; }
    //        set
    //        {
    //            user = value;
    //        }
    //    }
    //    public ApplicationDbContext Db
    //    {
    //        get { return db; }
    //        set
    //        {
    //            db = value;
    //        }
    //    }
    //    public void Request()
    //    {
    //        state.Handle(this, db, battle, user);
    //    }
    //}
}