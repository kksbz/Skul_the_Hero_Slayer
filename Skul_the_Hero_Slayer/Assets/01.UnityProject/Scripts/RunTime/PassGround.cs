using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassGround : MonoBehaviour
{
    private bool checkPlayer = false;
    private PlatformEffector2D passGroundObj;
    // Start is called before the first frame update
    void Start()
    {
        passGroundObj = gameObject.GetComponentMust<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.C) && checkPlayer == true)
        {
            Debug.Log("들어옴?");
            passGroundObj.rotationalOffset = 180f;
        }

        if (Input.GetKeyDown(KeyCode.C) && !Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log($"C키입력{checkPlayer}");
            passGroundObj.rotationalOffset = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == GData.PLAYER_LAYER_MASK)
        {
            checkPlayer = true;
            Debug.Log(checkPlayer);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == GData.PLAYER_LAYER_MASK)
        {
            checkPlayer = false;
            Debug.Log($"나갈때{checkPlayer}");
        }
    }
}
