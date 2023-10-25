using System.Collections.Generic;
using UnityEngine;

public class MuzzleShotObjectPool : MonoBehaviour
{
    [SerializeField] private int startingCount;
    [SerializeField] private GameObject muzzlePrefab;
    public Queue<GameObject> pool = new Queue<GameObject>();

    private void Start()
    {
        SetStartingObjects();
    }
    public GameObject Get()
    {
        if(pool.Count == 0)
        {
            GameObject obj = Instantiate(muzzlePrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }

        return pool.Dequeue();
    }
    public void ReturnToPool(GameObject muzzle)
    {
        muzzle.SetActive(false);
        pool.Enqueue(muzzle);
    }
    private void SetStartingObjects()
    {
        for (int i = 0; i < startingCount; i++)
        {
            GameObject obj = Instantiate(muzzlePrefab);
            Muzzle muzzle = obj.GetComponent<Muzzle>();
            muzzle.Init(this);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
