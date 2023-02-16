using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonsterState
{
    void StateEnter(Monster monster);
    void StateFixedUpdate(Monster monster);
    void StateUpdate(Monster monster);
    void StateExit(Monster monster);
}
