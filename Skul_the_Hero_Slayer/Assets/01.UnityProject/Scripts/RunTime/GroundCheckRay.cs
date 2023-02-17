using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckRay : MonoBehaviour
{
    public RaycastHit2D hit;
    public bool _isRight = true; //방향전환 변수
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //삼항연산자로 방향전환 체크
        Debug.DrawRay(transform.position, _isRight == true ? new Vector2(1,-1).normalized * 2 : new Vector2(-1,-1).normalized * 2, Color.red);
        hit = Physics2D.Raycast(transform.position, _isRight == true ? new Vector2(1,-1).normalized : new Vector2(-1,-1).normalized , 2, LayerMask.GetMask("Ground"));
    }
}
