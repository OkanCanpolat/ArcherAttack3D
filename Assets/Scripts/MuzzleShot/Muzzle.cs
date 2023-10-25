using UnityEngine;

public class Muzzle : MonoBehaviour
{
    public MuzzleShotObjectPool pool;
    private float lifeTime = 0.2f;
    private float elapsedTime;
   
    public void Init(MuzzleShotObjectPool pool)
    {
        this.pool = pool;
        Player player = GameManager.Instance.Player;
        transform.LookAt(player.transform);
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime >= lifeTime)
        {
            
            pool.ReturnToPool(gameObject);
            elapsedTime = 0;
        }
    }
}
