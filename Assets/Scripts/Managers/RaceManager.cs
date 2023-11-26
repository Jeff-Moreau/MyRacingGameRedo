using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    // SINGLETON STARTS
    private static RaceManager myInstance;
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
    public static RaceManager Load => myInstance;
    // SINGLETON ENDS

    // INSPECTOR VARIABLES
    [Header("Audio")]
    [NonReorderable]
    [SerializeField] private AudioSource myAudioSource = null;
    [NonReorderable]
    [SerializeField] private AudioClip[] myCountDownAudioClips = null;
    [NonReorderable]
    [SerializeField] private AudioClip[] myCheerClips = null;

    [Header("Other Data Needed")]
    [SerializeField] private TextMeshProUGUI theCountDownText = null;
    [SerializeField] private TextMeshProUGUI theUpdateText = null;
    [SerializeField] private TextMeshProUGUI theLapText = null;
    [SerializeField] private TextMeshProUGUI thePositionText = null;
    [SerializeField] private GameObject theFinishUI = null;

    // LOCAL VARIABLES
    private int myTrackLaps;
    private int myCurrentLap;
    private int myAudioCount;
    private int thePlayerPosition;
    private bool myGameStart;
    private bool myRaceOver;
    private bool myResetText;
    private float myStartRaceCountdownSeconds;
    private GameObject thePlayer;

    // LOCAL CONTAINERS
    private List<GameObject> myRacers;

    // GETTERS
    public bool GetRaceOver => myRaceOver;
    public bool GetGameStarted => myGameStart;
    public int GetRacers => myRacers.Count;
    public int GetCurrentLap => myCurrentLap;
    public int GetPlayerPosition => thePlayerPosition;

    // SETTERS
    public void AddRacers(GameObject me) => myRacers.Add(me);
    public void ResetRacers() => myRacers.Clear();
    public int SetPlayerPosition(int position) => thePlayerPosition = position;
    public int SetCurrentLap(int lap) => myCurrentLap = lap;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        InitializeVariables();
    }

    private void Update()
    {
        myStartRaceCountdownSeconds -= Time.deltaTime;
        RaceCountdown();
        UpdateHUDText();
        UpdateLastLap();
        RaceOver();
    }

    private void InitializeVariables()
    {
        myRaceOver = false;
        myGameStart = false;
        myResetText = false;
        myCurrentLap = 0;
        myAudioCount = 0;
        myStartRaceCountdownSeconds = 4;
        thePlayer = LoadingManager.Load.GetPlayer;
        myRacers = LoadingManager.Load.GetTotalVehicles;
        myTrackLaps = LoadingManager.Load.GetCurrentTrackLaps;
    }

    private void RaceOver()
    {
        if (myCurrentLap > myTrackLaps)
        {
            thePositionText.text = "Position: " + thePlayerPosition + " of " + (LoadingManager.Load.GetTrackPolePositions);
            theLapText.text = "Lap: " + myTrackLaps + " of " + myTrackLaps;
            theUpdateText.text = "  YOU FINISHED  " + thePlayerPosition;

            if (!myAudioSource.isPlaying)
            {
                if (thePlayerPosition == 1)
                {
                    myAudioSource.PlayOneShot(myCheerClips[0]);
                }
                else
                {
                    myAudioSource.PlayOneShot(myCheerClips[1]);
                }
            }
            myRaceOver = true;
            theFinishUI.SetActive(true);
        }
    }

    private void UpdateLastLap()
    {
        if (myCurrentLap == myTrackLaps && !myResetText)
        {
            if (!myAudioSource.isPlaying && myAudioCount == 0)
            {
                myAudioSource.PlayOneShot(myCountDownAudioClips[4]);
                myAudioCount++;
            }

            theUpdateText.text = "LAST LAP!";
            Invoke(nameof(ResetLapText), 2);
        }
    }

    private void UpdateHUDText()
    {
        if (myGameStart && myCurrentLap <= myTrackLaps)
        {
            theLapText.text = "Lap: " + myCurrentLap + " of " + myTrackLaps;
            thePositionText.text = "Position: " + thePlayerPosition + " of " + (LoadingManager.Load.GetTrackPolePositions);
        }
    }

    private void RaceCountdown()
    {
        if (!myGameStart && myStartRaceCountdownSeconds > 1)
        {
            if (Mathf.Floor(myStartRaceCountdownSeconds) == 3)
            {
                if (!myAudioSource.isPlaying && myAudioCount == 0)
                {
                    myAudioSource.PlayOneShot(myCountDownAudioClips[0]);
                    myAudioCount++;
                }
            }
            else if (Mathf.Floor(myStartRaceCountdownSeconds) == 2)
            {
                if (!myAudioSource.isPlaying && myAudioCount == 1)
                {
                    myAudioSource.PlayOneShot(myCountDownAudioClips[1]);
                    myAudioCount++;
                }
            }
            else if (Mathf.Floor(myStartRaceCountdownSeconds) == 1)
            {
                if (!myAudioSource.isPlaying && myAudioCount == 2)
                {
                    myAudioSource.PlayOneShot(myCountDownAudioClips[2]);
                    myAudioCount++;
                }
            }
            theCountDownText.text = Mathf.Floor(myStartRaceCountdownSeconds).ToString();
        }
        else if (myStartRaceCountdownSeconds < 1 && myStartRaceCountdownSeconds >= -1)
        {
            if (!myAudioSource.isPlaying && myAudioCount == 3)
            {
                myAudioSource.PlayOneShot(myCountDownAudioClips[3]);
                myAudioCount = 0;
            }
            theCountDownText.text = "GO";
            myGameStart = true;
        }
        else
        {
            theCountDownText.text = "";
        }
    }

    private void ResetLapText()
    {
        myResetText = true;
        theUpdateText.text = "";
    }

    public void ResetLevel()
    {
        InitializeVariables();
        myRacers.Clear();
        thePositionText.text = "";
        theLapText.text = "";
        theUpdateText.text = "";
    }
}
