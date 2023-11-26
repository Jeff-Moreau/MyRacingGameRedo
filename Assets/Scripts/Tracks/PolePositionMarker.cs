using UnityEngine;

public class PolePositionMarker : MonoBehaviour
{
    // LOCAL VARIABLES
    private bool isSpotTaken;

    // GETTERS
    public bool GetSpotTaken => isSpotTaken;

    // SETTERS
    public bool SetSpotTaken(bool spotTaken) => isSpotTaken = spotTaken;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        isSpotTaken = false;
    }
}
