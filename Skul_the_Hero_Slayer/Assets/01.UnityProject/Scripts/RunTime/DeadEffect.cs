using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEffect : MonoBehaviour
{
    private Animator deadAni;
    // Start is called before the first frame update
    void Start()
    {
        deadAni = gameObject.GetComponentMust<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //애니메이션 끝나면 자신파괴
        if (deadAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
