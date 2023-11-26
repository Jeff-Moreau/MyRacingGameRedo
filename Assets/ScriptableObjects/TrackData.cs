using UnityEngine;

[CreateAssetMenu(fileName = "TrackData", menuName = "ScriptableObject/TrackData")]
public class TrackData : ScriptableObject
{
    // INSPECTOR VARIABLES
    [Header("Track Information")]
    [SerializeField] private string myName = "";
    [SerializeField] private int myLaps = 0;

    // LOCAL VARIABLES
    public int GetLaps => myLaps;
    public string GetName => myName;

    public void Start()
    {
        myName = "Johnny";
    }
}
