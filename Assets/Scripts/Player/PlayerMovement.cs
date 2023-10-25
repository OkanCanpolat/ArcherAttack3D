using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public Transform SpineTransform;
    public Transform AimCameraFollowTarget;
    private NavMeshAgent navMeshAgent;
    [Header ("Rotation")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float minYRotation;
    [SerializeField] private float maxYRotation;
    [SerializeField] private float minXRotation;
    [SerializeField] private float maxXRotation;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;

    }

    public float GetRotationSpeed()
    {
        return rotationSpeed;
    }
    public float GetMaxYRotation()
    {
        return maxYRotation;
    }
    public float GetMinYRotation()
    {
        return minYRotation;
    }
    public float GetMaxXRotation()
    {
        return maxXRotation;
    }
    public float GetMinXRotation()
    {
        return minXRotation;
    }
    public void MoveTarget(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
    }
    public void LookTarget(Transform lookAt = null)
    {
        StartCoroutine(LookAtTarget(lookAt));
    }
    private IEnumerator LookAtTarget(Transform lookAt)
    {
        if(lookAt == null)
        {
            lookAt = WaypointManager.Instance.GetLookAtTransform();
        }

        Vector3 relativePosition = new Vector3(lookAt.position.x, transform.position.y, lookAt.position.z);
        Quaternion lookRotation = Quaternion.LookRotation(relativePosition - transform.position);
        float time = 0;

        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime / rotationSpeed;
            yield return null;
        }

    }
}
