using System.Collections.Generic;
using UnityEngine;

public class StateMachineController
{
    public StateMachine currentStateMachine;
    public List<StateMachine> stateMachines;
    int stateMachineIndex;

    //StateMachine for each equipment
    public NormalStateMachine normalStateMachine;
    public SpearStateMachine spearStateMachine;
    public GunStateMachine gunStateMachine;

    public StateMachineController(StateController player)
    {
        stateMachines = new List<StateMachine>();
        normalStateMachine = new NormalStateMachine(player);
        spearStateMachine = new SpearStateMachine(player);
        gunStateMachine = new GunStateMachine(player);

        stateMachines.Add(normalStateMachine);
        stateMachines.Add(spearStateMachine);
        stateMachines.Add(gunStateMachine);
        stateMachineIndex = 0;
    }
    public void Initialize()
    {
        currentStateMachine = stateMachines[stateMachineIndex];
        currentStateMachine.Enter();
    }
    public void TransitionToNext()
    {
        stateMachineIndex++;
        if (stateMachineIndex >= stateMachines.Count)
        {
            stateMachineIndex = 0;
        }
        currentStateMachine = stateMachines[stateMachineIndex];
        currentStateMachine.Enter();
    }
}
