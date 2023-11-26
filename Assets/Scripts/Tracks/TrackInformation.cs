using UnityEngine;

public class TrackInformation : MonoBehaviour
{
    // INSPECTOR VARIABLES
    [Header("Scriptable Object Data")]
    [SerializeField] private TrackData myData = null;

    [Header("Track Information")]
    [NonReorderable]
    [SerializeField] private GameObject[] myWaypointsContainer = null;
    [NonReorderable]
    [SerializeField] private GameObject[] myPolePositions = null;

    // GETTERS
    public int GetLaps => myData.GetLaps;
    public string GetName => myData.GetName;
    public GameObject[] GetPolePositions => myPolePositions;
    public GameObject[] GetWaypointContainer => myWaypointsContainer;
}
