using System.IO;
using UnityEngine;

public class SavingAndLoading : MonoBehaviour
{
    // INSPECTOR VARIABLES
    [SerializeField] private GameObject thePlayer;
    [SerializeField] private BallEditor theEditor;

    // LOCAL VARIABLES
    private string mySaveFilePath;
    private VehicleController myPlayer;

    void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        myPlayer = thePlayer.GetComponentInChildren<VehicleController>();
    }

    public void SaveGame()
    {
        myPlayer.SetName(theEditor.GetName);
        myPlayer.SetBallMaterial(theEditor.GetMaterial);
        myPlayer.SetMaterialRed(theEditor.GetMaterialRed);
        myPlayer.SetMaterialGreen(theEditor.GetMaterialGreen);
        myPlayer.SetMaterialBlue(theEditor.GetMaterialBlue);
        myPlayer.SetMaterialAlpha(theEditor.GetMaterialAlpha);
        myPlayer.SetArmorType(theEditor.GetArmorNumber);

        mySaveFilePath = Application.persistentDataPath + "/VehicleData-" + myPlayer.GetName + ".json";

        if (File.Exists(mySaveFilePath))
        {
            var savePlayerData = JsonUtility.ToJson(myPlayer);
            File.WriteAllText(mySaveFilePath, savePlayerData);
            Debug.Log("Change Vehicle Config for : " + myPlayer.GetName);
        }
        else if (!File.Exists(mySaveFilePath))
        {
            var savePlayerData = JsonUtility.ToJson(myPlayer);
            File.WriteAllText(mySaveFilePath, savePlayerData);
            Debug.Log("New Vehicle Config for : " + myPlayer.GetName);
        }
    }

    public void LoadGame()
    {
        mySaveFilePath = Application.persistentDataPath + "/VehicleData-" + theEditor.GetName + ".json";

        if (File.Exists(mySaveFilePath))
        {
            var loadVehicleData = File.ReadAllText(mySaveFilePath);
            JsonUtility.FromJsonOverwrite(loadVehicleData, myPlayer);

            Debug.Log("Loaded Vehicle : "+ theEditor.GetName);

            UpdateModel();
        }
        else
        {
            Debug.Log("No Vehicle Found!");
        }
    }

    public void UpdateModel()
    {
        theEditor.SetMaterial(myPlayer.GetMaterial);
        theEditor.SetMaterialRed(myPlayer.GetMaterialRed);
        theEditor.SetMaterialGreen(myPlayer.GetMaterialGreen);
        theEditor.SetMaterialBlue(myPlayer.GetMaterialBlue);
        theEditor.SetMaterialAlpha(myPlayer.GetMaterialAlpha);
        theEditor.SetArmorNumber(myPlayer.GetArmorType);
    }
}