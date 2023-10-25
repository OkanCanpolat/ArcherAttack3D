using UnityEngine;

public class SimpleArrow : Arrow
{
    private void Update()
    {
        if (!collision)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        OnTrigger(other);
    }
    public override void OnTrigger(Collider other)
    {
        collision = true;
        transform.parent = other.transform;

        Enemy enemy = other.transform.root.GetComponent<Enemy>();

        if (enemy)
        {
            Collider arrowCollider = GetComponent<Collider>();
            arrowCollider.enabled = false;

            BodyPart hittedPart = other.GetComponent<BodyPartContainer>().BodyPart;
            BodyPartHitInformation info = BodyPartInformationProvider.Instance.GetHitPart(hittedPart);
            CombatUIManager.Instance.ActivateHitInfo(info);
            enemy.TakeDamage();
            StartCoroutine(CollisionDelay());
        }

        else
        {
            WaypointManager.Instance.AlertEnemies();
            ArrowNotCollided();
        }
    }
}
