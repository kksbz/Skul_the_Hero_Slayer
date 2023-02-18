using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagetSearchRay : MonoBehaviour
{
    MonsterController monsterController;
    [HideInInspector]
    public Collider2D hit;
    // Start is called before the first frame update
    void Start()
    {
        monsterController = gameObject.GetComponentMust<MonsterController>();
    }

    // Update is called once per frame
    void Update()
    {
        TagetCheckRay();
    }

    public void TagetCheckRay()
    {
        hit = Physics2D.OverlapBox(monsterController.monster.transform.position,
        new Vector2(monsterController.monster.sightRangeX, monsterController.monster.sightRangeY), 0, LayerMask.GetMask(GData.PLAYER_LAYER_MASK));
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (monsterController != null)
        {
            Gizmos.DrawWireCube(monsterController.monster.transform.position, 
            new Vector2(monsterController.monster.sightRangeX, monsterController.monster.sightRangeY));
        }
    }
}
