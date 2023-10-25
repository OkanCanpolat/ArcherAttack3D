using System;
using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public event Action onCollide;
    public event Action onNotCollide;
    [SerializeField] protected float speed;
    [SerializeField] protected float lifeTime;
    [SerializeField] protected float collisionDelay;

    protected Vector3 direction;
    protected bool collision;
    public void Init(Vector3 direction)
    {
        this.direction = direction;
        transform.LookAt(transform.position + direction);
        StartCoroutine(StartTimer());
    }

    protected IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(lifeTime);

        if (!collision)
        {
            onNotCollide?.Invoke();
            Destroy(gameObject);
        }
    }

    public virtual void OnTrigger(Collider other)
    {
        
    }

    protected IEnumerator CollisionDelay()
    {
        yield return new WaitForSeconds(collisionDelay);
        onCollide?.Invoke();
        WaypointManager.Instance.AlertEnemies();
    }
    protected void ArrowNotCollided()
    {
        onNotCollide?.Invoke();
    }
}
