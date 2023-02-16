using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MonsterState
{
    void StateEnter();
    void StateFixedUpdate();
    void StateUpdate();
    void StateExit();
}

