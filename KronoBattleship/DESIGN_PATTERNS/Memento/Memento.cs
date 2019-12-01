using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KronoBattleship.DESIGN_PATTERNS.Memento
{
    //https://www.dofactory.com/net/memento-design-pattern

    class MementoClient
    {
        private Originator o = new Originator();
        private Caretaker c = new Caretaker();

        public string SetStateFree()
        {
            o.State = "Waiting :("; //set player status to free
            c.Memento = o.CreateMemento(); //save players status safe
            return o.State;
        }
        public string SetStatePlaying()
        {
            o.State = "Playing :)";
            return o.State;
        }
        public string RestoreState()
        {
            // Restore saved state
            o.SetMemento(c.Memento);
            return o.State;
        }
        public string GetMementoState()
        {
            return o.State;
        }
    }

    public class Memento
    {
        private string state;

        /// <summary>
        /// Memento constructor
        /// </summary>
        /// <param name="state"></param>
        public Memento(string state)
        {
            this.state = state;
        }

        // Gets or sets state
        public string State
        {
            get
            {
                return state;
            }
        }

    }

    /// <summary>
    /// The Caretaker class- is responsible for the memento's safekeeping 
    /// </summary>
    public class Caretaker
    {
        private Memento memento;

        /// <summary>
        /// Gets or sets memento
        /// </summary>
        public Memento Memento
        {
            set
            {
                memento = value;
            }
            get
            {
                return memento;
            }
        }
    }

    /// <summary>
    /// The Originator class -creates a memento containing a snapshot of its current internal state
    /// </summary>
    class Originator
    {
        private string state;
        /// <summary>
        /// State property
        /// </summary>
        public string State
        {
            get { return state; }
            set
            {
                state = value;
                System.Diagnostics.Debug.WriteLine("State = " + state);
            }
        }

        /// <summary>
        /// Creates memento
        /// </summary>
        /// <returns></returns>
        public Memento CreateMemento()
        {
            return (new Memento(state));
        }

        /// <summary>
        /// Restores original state
        /// </summary>
        /// <param name="memento">Memento obj</param>
        public void SetMemento(Memento memento)
        {
            System.Diagnostics.Debug.WriteLine("Restoring state...");
            State = memento.State;
        }
    }
}