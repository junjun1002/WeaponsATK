using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{
    public void Execute(StateBase state)
    {
        state.Execute();
    }
}
