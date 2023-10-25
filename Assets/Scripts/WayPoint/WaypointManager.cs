using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance;
    [SerializeField] private List<WayPoint> wayPoints;
    private int index = 0;
    private WayPoint currentPoint;

    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion
    }

    public Transform GetNextPosition()
    {
        return wayPoints[index].transform;
    }

    public void OnReach()
    {
        currentPoint = wayPoints[index];
        ActivateEnemies(currentPoint);
        index++;
    }

    public Transform GetLookAtTransform()
    {
        Transform lookAt = wayPoints[index].WaypointEnemies[0].transform;
        return lookAt;
    }
    private void ActivateEnemies(WayPoint wayPoint)
    {
        foreach(Enemy enemy in wayPoint.WaypointEnemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }
    public bool ThereIsEnemy()
    {
        return currentPoint.WaypointEnemies.Count > 0;
    }

    public void OnEnemyDied(Enemy enemy)
    {
        currentPoint.WaypointEnemies.Remove(enemy);
    }

    public bool ThereIsWaypoint()
    {
        return index < wayPoints.Count;
    }

    public void AlertEnemies()
    {
        foreach(Enemy enemy in currentPoint.WaypointEnemies)
        {
            enemy.AlertEnemy();
        }
    }

}
