using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpPool : MonoBehaviour
{
    private List<GameObject> corpPool;
    private BossMonster parentObj;
    // Start is called before the first frame update
    void Start()
    {
        corpPool = new List<GameObject>();
        parentObj = gameObject.transform.parent.GetComponent<BossMonster>();
        SetupCorpPool();
    } //Start

    private void SetupCorpPool()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject corp = Instantiate<GameObject>(Resources.Load("Prefabs/Monster/EnergyCorp") as GameObject);
            corp.transform.parent = gameObject.transform;
            corp.SetActive(false);
            corpPool.Add(corp);
        }
    } //SetupCorpPool

    //탄환 발사하는 함수
    public void ShootBullet()
    {
        if (parentObj.isChangePhase == false)
        {
            //1페이즈 Corp공격 실행
            foreach (var corp in corpPool)
            {
                if (!corp.activeInHierarchy)
                {
                    corp.transform.position = new Vector3(0f, 5f, 0f);
                    corp.SetActive(true);
                    return;
                }
            }
        }
        else if (parentObj.isChangePhase == true)
        {
            StartCoroutine(ShootCorp2Phase());
        }
    } //ShootBullet

    private IEnumerator ShootCorp2Phase()
    {
        //2페이즈 Corp공격 실행
        float offsetX = -10f;
        foreach (var corp in corpPool)
        {
            if (!corp.activeInHierarchy)
            {
                corp.transform.position = new Vector3(offsetX, 5f, 0f);
                corp.SetActive(true);
                offsetX += 5f;
                yield return new WaitForSeconds(0.5f);
            }
        }
    } //ShootCorp2Phase
}
