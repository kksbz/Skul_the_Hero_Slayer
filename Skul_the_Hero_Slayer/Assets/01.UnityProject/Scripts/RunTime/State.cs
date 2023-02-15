using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State
{
    void Action();
}

public class Move : MonoBehaviour, State
{
    public void Action()
    {
        
    }
}

public class Attack : MonoBehaviour, State
{
    public void Action()
    {

    }
}
