using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // SINGLETON STARTS
    private static MenuManager myInstance;
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
    public static MenuManager Load => myInstance;
    // SINGLETON ENDS

    // INSPECTOR VARIABLES
    [Header("Cameras")]
    [SerializeField] private Camera theMainCamera = null;
    [Header("Screens")]
    [SerializeField] private GameObject thePlayingHUD = null;
    [SerializeField] private GameObject theFinalWindow = null;
    [SerializeField] private GameObject theCenterUpdates = null;
    [SerializeField] private GameObject theIntro = null;
    [Header("Menus")]
    [SerializeField] private GameObject theMainMenu = null;
    [SerializeField] private GameObject theMultiplayerMenu = null;
    [SerializeField] private GameObject theNewGameMenu = null;
    [Header("Managers")]
    [SerializeField] private GameObject theGameManager = null;
    [SerializeField] private GameObject theLoadingManager = null;
    [SerializeField] private GameObject theRaceManager = null;
    [Header("Videos")]
    [SerializeField] private GameObject theVideo = null;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        Invoke(nameof(ShowIntro), 1.7f);
    }

    public void ShowIntro()
    {
        theVideo.SetActive(true);
        Invoke(nameof(GoToMainMenu), 3.6f);
    }

    public void GoToMainMenu()
    {
        theIntro.SetActive(false);
        theMainMenu.SetActive(true);
    }

    public void NewGame()
    {
        theMainMenu.SetActive(false);
        theNewGameMenu.SetActive(true);
    }

    public void ExitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void JoinGame()
    {

    }

    public void HostGame()
    {

    }

    public void BackToMain()
    {
        theMainMenu.SetActive(true);
        theMultiplayerMenu.SetActive(false);
        theNewGameMenu.SetActive(false);
    }

    public void NextRace()
    {
        theNewGameMenu.SetActive(false);
        theGameManager.SetActive(true);
        theLoadingManager.SetActive(true);
        theRaceManager.SetActive(true);
        theMainCamera.gameObject.SetActive(false);
        thePlayingHUD.SetActive(true);
        theCenterUpdates.SetActive(true);
    }

    public void MultiplayerMenu()
    {
        theMainMenu.SetActive(false);
        theMultiplayerMenu.SetActive(true);
    }

    public void MainFromRace()
    {
        theGameManager.SetActive(false);
        theLoadingManager.SetActive(false);
        theRaceManager.SetActive(false);
        theFinalWindow.SetActive(false);
        theCenterUpdates.SetActive(false);
        thePlayingHUD.SetActive(false);
        theMainCamera.gameObject.SetActive(true);
        theMainMenu.SetActive(true);
    }
}
