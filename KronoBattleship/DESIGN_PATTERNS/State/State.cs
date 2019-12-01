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
        public abstract void Handle(Context context);
    }
    /// <summary>
    /// ConcreteStateConnected class
    /// </summary>
    class ConcreteStateConnected : State
    {
        public override void Handle(Context context)
        {
            context.State = new Waiting();
        }
    }
    /// <summary>
    /// Concrete sate class
    /// </summary>
    class Waiting : State
    {
        public override void Handle(Context context)
        {
            context.State = new Joined();
        }
    }
    /// <summary>
    /// Concrete sate class
    /// </summary>
    class Joined : State
    {
        public override void Handle(Context context)
        {
            context.State = new Ready();
        }
    }
    /// <summary>
    /// Concrete sate class
    /// </summary>
    class Ready : State
    {
        public override void Handle(Context context)
        {
            context.State = new Playing();
        }
    }
    /// <summary>
     /// Concrete sate class
    /// </summary>
    class Playing : State
    {
        public override void Handle(Context context)
        {
            context.State = new Waiting();
        }
    }
    /// <summary>
    /// Context class
    /// </summary>
    class Context
    {
        private State state;

        // Constructor
        public Context(State state)
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
                System.Diagnostics.Debug.WriteLine("State: " + state.GetType().Name);
            }
        }
        public void Request()
        {
            state.Handle(this);
        }
    }
}