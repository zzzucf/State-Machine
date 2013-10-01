using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMachine
{
    class StateInventory
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public StateInventory(States state)
        {
            IsInvoking  = false;
            State       = state;
        }

        /// <summary>
        /// Add one event to the beginning of the invocation list.
        /// </summary>
        /// <param name="function"></param>
        public void AddEventHandlerFront(Func<bool> function)
        {
            EventHandlers = function + EventHandlers;
        }

        /// <summary>
        /// Add one event to the end of the invocation list.
        /// </summary>
        /// <param name="function"></param>
        public void AddEventHandlerBack(Func<bool> function)
        {
            EventHandlers += function;
        }
        
        /// <summary>
        /// Excecute all the functions in the invocation list.
        /// </summary>
        public bool Invoke()
        {
            if (EventHandlers == null)
            {
                throw new Exception("Method contains no function to be executed.");
            }

            // Add initialization.
            AddEventHandlerFront(Init);

            // Add end.
            AddEventHandlerBack(End);

            if (!IsInvoking)
            {
                EventHandlers.Invoke();
                IsInvoking = true;
            }

            return IsInvoking;
        }

        /// <summary>
        /// Initialization function to be called in the beginning.
        /// </summary>
        /// <returns></returns>
        protected virtual bool Init()
        {
            return true;
        }

        /// <summary>
        /// Initialization function to be called in the end.
        /// </summary>
        /// <returns></returns>
        protected virtual bool End()
        {
            return true;
        }

        /// <summary>
        /// The state for this state inventory.
        /// </summary>
        public States State { get; set; }

        /// <summary>
        /// Illustrate whether the invocation list being invoking for this state inventory.
        /// </summary>
        protected bool IsInvoking { get; set; }

        /// <summary>
        /// The delegate method for this state.
        /// </summary>
        protected Func<bool> EventHandlers { get; set; }
    }
}
