using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistSlam : MonoBehaviour
{
    private Animator fistSlamAni;
    // Start is called before the first frame update
    void Start()
    {
        fistSlamAni = gameObject.GetComponentMust<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fistSlamAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            gameObject.SetActive(false);
        }
    }
}
