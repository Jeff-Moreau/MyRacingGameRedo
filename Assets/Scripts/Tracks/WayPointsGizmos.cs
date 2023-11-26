using UnityEngine;

public class WayPointsGizmos : MonoBehaviour
{
    private const string WAYPOINT = "Waypoint ";

    [SerializeField] private float myWaypointSphereSize = 1.0f;
    [SerializeField] private Color myWaypointColor = Color.red;

    private Transform[] theWayPoints = null;

    private void OnDrawGizmos()
    {
        theWayPoints = GetComponentsInChildren<Transform>();
        var lastWaypoint = theWayPoints[theWayPoints.Length - 1].position;

        for (int i = 1; i < theWayPoints.Length; i++)
        {
            Gizmos.color = myWaypointColor;
            Gizmos.DrawSphere(theWayPoints[i].position, myWaypointSphereSize);
            Gizmos.DrawLine(lastWaypoint, theWayPoints[i].position);
            lastWaypoint = theWayPoints[i].position;

            theWayPoints[i].name = WAYPOINT + i.ToString();
        }
    }
}
