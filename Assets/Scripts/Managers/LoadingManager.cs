using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    // SINGLETON STARTS
    private static LoadingManager myInstance;
    private void Singleton()
    {
        if (myInstance != null && myInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            myInstance = this;
        }
    }

    public static LoadingManager Load => myInstance;
    // SINGLETON ENDS

    // INSPECTOR VARIABLES
    [Header("Other Data Needed")]
    [SerializeField] private AIVehiclePool theAIVehiclePool = null;
    [NonReorderable]
    [SerializeField] private GameObject[] theTrackList = null;
    [SerializeField] private GameObject[] thePlayerVehicleList = null;
    
    // LOCAL VARIABLES
    private int theTotalAIVehicles;
    private int theTotalPlayers;
    private int theCurrentTrackLaps;
    private TrackInformation theCurrentTrackInformation;

    // LOCAL CONTAINERS
    private List<GameObject> theTotalVehicles;
    private GameObject[] theTrackPolePositions;
    private PolePositionMarker[] theTrackPolePositionMarkers;

    // GETTERS
    public int GetTrackPolePositions => theTrackPolePositions.Length;
    public TrackInformation GetCurrentTrackInformation => theCurrentTrackInformation;
    public int GetCurrentTrackLaps => theCurrentTrackLaps;
    public GameObject GetPlayer => thePlayerVehicleList[0];
    public List<GameObject> GetTotalVehicles => theTotalVehicles;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        Instantiate(theTrackList[0]);
        InitializeVariables();

        theAIVehiclePool.SetTotalPrefabsNeeded(theTrackPolePositions.Length - thePlayerVehicleList.Length);
    }

    private void Update()
    {
        LoadPlayersInTrack();
        LoadAIVehiclesInTrack();
    }

    private void InitializeVariables()
    {
        theCurrentTrackInformation = theTrackList[0].GetComponent<TrackInformation>();
        theTrackPolePositions = theCurrentTrackInformation.GetPolePositions;
        theCurrentTrackLaps = theCurrentTrackInformation.GetLaps;
        theTrackPolePositionMarkers = new PolePositionMarker[theTrackPolePositions.Length];
        theTotalVehicles = new List<GameObject>();
        theTotalAIVehicles = 0;
        theTotalPlayers = 0;

        for (int i = 0; i < theTrackPolePositions.Length; i++)
        {
            theTrackPolePositionMarkers[i] = theTrackPolePositions[i].GetComponent<PolePositionMarker>();
            theTrackPolePositionMarkers[i].SetSpotTaken(false);
        }
    }

    private void LoadPlayersInTrack()
    {
        for (int i = 0; i < theTrackPolePositions.Length; i++)
        {
            for (int j = 0; j < thePlayerVehicleList.Length; j++)
            {
                var randomPolePosition = Random.Range(0, thePlayerVehicleList.Length);

                if (!theTrackPolePositionMarkers[randomPolePosition].GetSpotTaken && theTotalPlayers < thePlayerVehicleList.Length)
                {
                    Instantiate(thePlayerVehicleList[j]);
                    thePlayerVehicleList[j].transform.SetPositionAndRotation(theTrackPolePositions[randomPolePosition].transform.position, theTrackPolePositions[randomPolePosition].transform.rotation);
                    theTrackPolePositionMarkers[randomPolePosition].SetSpotTaken(true);
                    thePlayerVehicleList[j].SetActive(true);
                    theTotalVehicles.Add(thePlayerVehicleList[j]);
                    theTotalPlayers++;
                    theTotalAIVehicles++;
                }
            }
        }
    }

    private void LoadAIVehiclesInTrack()
    {
        for (int i = 0; i < theTrackPolePositions.Length; i++)
        {
            if (!theTrackPolePositionMarkers[i].GetSpotTaken && theTotalAIVehicles < theTrackPolePositions.Length)
            {
                var newAIVehicle = theAIVehiclePool.GetAvailableAIVehicle();

                if (newAIVehicle != null)
                {
                    newAIVehicle.transform.SetPositionAndRotation(theTrackPolePositions[i].transform.position, theTrackPolePositions[i].transform.rotation);
                    theTrackPolePositionMarkers[i].SetSpotTaken(true);
                    theTotalVehicles.Add(newAIVehicle);
                    theTotalAIVehicles++;
                }

                for (int j = 0; j < theAIVehiclePool.GetPrefabList.Count; j++)
                {
                    newAIVehicle.SetActive(true);
                }
            }
        }
    }
}