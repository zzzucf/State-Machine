using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine
{
    /// <summary>
    /// Finite state machine model.
    /// </summary>
    class StateMachine
    {
        /// <summary>
        /// State inventroy collection. 
        /// </summary>
        private List<StateInventory> Inventory;

        /// <summary>
        /// The current state for the state machine.
        /// </summary>
        public States CurrentState { get; private set; }

        /// <summary>
        /// The initial state for the state machine.
        /// </summary>
        public States StartState { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public StateMachine(States startState)
        {
            StartState      = startState; ;
            CurrentState    = startState;

            Inventory       = new List<StateInventory>();
            
            foreach (States state in Enum.GetValues(typeof(States)))
            {
                Inventory.Add(new StateInventory(state));
            }
        }

        /// <summary>
        /// Start invoking the invocation list of start state.
        /// </summary>
        /// <returns></returns>
        public bool Invoke()
        {
            return Inventory.Find(x => x.State.Equals(StartState)).Invoke();
        }

        /// <summary>
        /// Add event to a state.
        /// </summary>
        public void AddEventHandler(Func<bool> func, States state)
        {
            Inventory.Find(x => x.State.Equals(state)).AddEventHandlerBack(func);
        }

        /// <summary>
        /// Add condition to transfer from one state to the other state.
        /// </summary>
        /// <param name="func">Condition funciton.</param>
        /// <param name="startState">Start state.</param>
        /// <param name="endState">Destination state.</param>
        public void AddStateHandler(Func<bool> func, States startState, States endState)
        {
            AddEventHandler(() =>
                {
                    // If the condition is true, invoke next state.
                    if (func.Invoke())
                    {
                        Inventory.Find(x => x.State.Equals(endState)).Invoke();
                        CurrentState = endState;
                        return true;
                    }

                    return false;
                }, 
                startState);
        }
    }
}
