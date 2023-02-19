using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWoodenAttackB : MonoBehaviour
{
    public List<GameObject> objPool;
    public List<Vector3> vectorList;

    // Start is called before the first frame update
    void Start()
    {
        objPool = new List<GameObject>();
        vectorList = new List<Vector3>();
        vectorList = SetUpVectorList();
        objPool = SetUpObjPool();
    } //Start

    //obj의 이동방향을 담을 vectorList 채우는 함수
    private List<Vector3> SetUpVectorList()
    {
        vectorList.Add(Vector3.up);
        vectorList.Add(new Vector3(1f, 1f, 0f).normalized);
        vectorList.Add(Vector3.right);
        vectorList.Add(new Vector3(1f, -1f, 0f).normalized);
        vectorList.Add(Vector3.down);
        vectorList.Add(new Vector3(-1f, -1f, 0f).normalized);
        vectorList.Add(Vector3.left);
        vectorList.Add(new Vector3(-1f, 1f, 0f).normalized);
        return vectorList;
    } //SetUpVectorList

    //objPool 채우는 함수
    private List<GameObject> SetUpObjPool()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject bullet = Instantiate(Resources.Load("Prefabs/BigWoodenBullet") as GameObject);
            bullet.transform.parent = gameObject.transform;
            bullet.name = $"Bullet{i}";
            bullet.SetActive(false);
            objPool.Add(bullet);
        }
        return objPool;
    } //SetUpObjPool

    //탄환 발사하는 함수
    public void ShootBullet()
    {
        for (int i = 0; i < objPool.Count; i++)
        {
            objPool[i].transform.position = transform.parent.position;
            if (!objPool[i].gameObject.activeInHierarchy)
            {
                objPool[i].gameObject.SetActive(true);
            }
        }
    } //ShootBullet
}
