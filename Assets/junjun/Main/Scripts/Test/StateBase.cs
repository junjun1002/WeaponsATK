using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBase
{
    public delegate void ExecuteState();
    public ExecuteState execDelegate;

    public virtual void Execute()
    {
        if (execDelegate != null)
        {
            execDelegate();
        }
    }

    public abstract string GetStateName();
}
