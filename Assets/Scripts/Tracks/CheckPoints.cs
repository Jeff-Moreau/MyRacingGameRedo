using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!RaceManager.Load.GetRaceOver)
        {
            if (RaceManager.Load.GetRacers < LoadingManager.Load.GetTrackPolePositions)
            {
                RaceManager.Load.AddRacers(other.gameObject);

                if (other.gameObject.CompareTag("Player"))
                {
                    Debug.Log("What the hell");
                    RaceManager.Load.SetCurrentLap(RaceManager.Load.GetCurrentLap + 1);
                    RaceManager.Load.SetPlayerPosition(RaceManager.Load.GetRacers);
                }
            }
            else if (RaceManager.Load.GetRacers >= LoadingManager.Load.GetTrackPolePositions)
            {
                RaceManager.Load.ResetRacers();
                RaceManager.Load.AddRacers(other.gameObject);
            }
        }
    }
}
