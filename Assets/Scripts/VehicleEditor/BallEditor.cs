using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallEditor : MonoBehaviour
{
    private enum MaterialType
    {
        DragonScale,
        Fire,
        Lightning,
        Lava,
        Chrome,
        Water
    }

    private enum ArmorType
    {
        Default,
        MoHawk,
        BigBooty
    }

    // INSPECTOR VARIABLES
    [Header("Materials and Objects")]
    [SerializeField] private Renderer myOnScreenBall = null;
    [SerializeField] private Material[] myBallMaterials = null;
    [SerializeField] private GameObject[] myArmorTypes = null;
    [Header("Sliders")]
    [SerializeField] private Slider myRedSlider = null;
    [SerializeField] private Slider myGreenSlider = null;
    [SerializeField] private Slider myBlueSlider = null;
    [SerializeField] private Slider myAlphaSlider = null;
    [Header("DropDowns")]
    [SerializeField] private TMP_Dropdown myDropdown = null;
    [SerializeField] private TMP_Dropdown myArmorDropDown = null;

    // LOCAL VARIABLES
    private int myArmorNumber;
    private float myRendererRed;
    private float myRendererGreen;
    private float myRendererBlue;
    private float myRendererAlpha;
    private string myVehicleName;
    private Material myRendererMaterial;

    // GETTERS
    public string GetName => myVehicleName;
    public int GetArmorNumber => myArmorNumber;
    public float GetMaterialRed => myRendererRed;
    public float GetMaterialBlue => myRendererBlue;
    public float GetMaterialGreen => myRendererGreen;
    public float GetMaterialAlpha => myRendererAlpha;
    public Material GetMaterial => myRendererMaterial;

    // SETTERS
    public int SetArmorNumber(int armor) =>  myArmorNumber = armor;
    public float SetMaterialRed(float amount) => myRendererRed = amount;
    public Material SetMaterial(Material mat) => myRendererMaterial = mat;
    public float SetMaterialBlue(float amount) => myRendererBlue = amount;
    public float SetMaterialGreen(float amount) => myRendererGreen = amount;
    public float SetMaterialAlpha(float amount) => myRendererAlpha = amount;

    private void Start()
    {
        InitializeVariables();
    }

    private void Update()
    {
        myArmorDropDown.value = myArmorNumber;
        myRedSlider.value = myRendererRed;
        myGreenSlider.value = myRendererGreen;
        myBlueSlider.value = myRendererBlue;
        myAlphaSlider.value = myRendererAlpha;
    }

    private void InitializeVariables()
    {
        myArmorNumber = 0;
        myVehicleName = "DragonScale";

        myArmorDropDown.value = myArmorNumber;

        myRendererRed = myOnScreenBall.material.GetColor("_Color").r;
        myRendererGreen = myOnScreenBall.material.GetColor("_Color").g;
        myRendererBlue = myOnScreenBall.material.GetColor("_Color").b;
        myRendererAlpha = myOnScreenBall.material.GetColor("_Color").a;

        myRedSlider.value = myRendererRed;
        myGreenSlider.value = myRendererGreen;
        myBlueSlider.value = myRendererBlue;
        myAlphaSlider.value = myRendererAlpha;
    }

    public void ChangeColor()
    {
        SliderAdjust();

        myOnScreenBall.material.SetColor("_Color", new Color(myRedSlider.value, myGreenSlider.value, myBlueSlider.value, myAlphaSlider.value));
    }

    public void RandomColor()
    {
        SliderAdjust();

        var randomMaterial = Random.Range(0, myBallMaterials.Length);

        myRendererMaterial = myBallMaterials[randomMaterial];
        myOnScreenBall.material = myBallMaterials[randomMaterial];
        myArmorDropDown.value = Random.Range(0, myArmorTypes.Length);
        myDropdown.value = randomMaterial;

        var randomRedColor = Random.Range(0f, 1f);
        var randomGreenColor = Random.Range(0f, 1f);
        var randomBlueColor = Random.Range(0f, 1f);
        var randomAlphaColor = Random.Range(0.8f, 1f);

        myRedSlider.value = randomRedColor;
        myGreenSlider.value = randomGreenColor;
        myBlueSlider.value = randomBlueColor;
        myAlphaSlider.value = randomAlphaColor;

        myOnScreenBall.material.SetColor("_Color", new Color(randomRedColor, randomGreenColor, randomBlueColor, randomAlphaColor));
    }

    private void SliderAdjust()
    {
        myRendererRed = myRedSlider.value;
        myRendererGreen = myGreenSlider.value;
        myRendererBlue = myBlueSlider.value;
        myRendererAlpha = myAlphaSlider.value;
    }

    public void ChangeBallMaterial()
    {
        switch (myDropdown.value)
        {
            case (int)MaterialType.DragonScale:
                myRendererMaterial = myBallMaterials[(int)MaterialType.DragonScale];
                myOnScreenBall.material = myBallMaterials[(int)MaterialType.DragonScale];
                myVehicleName = "DragonScale";
                break;

            case (int)MaterialType.Fire:
                myRendererMaterial = myBallMaterials[(int)MaterialType.Fire];
                myOnScreenBall.material = myBallMaterials[(int)MaterialType.Fire];
                myVehicleName = "FireBall";
                break;

            case (int)MaterialType.Lightning:
                myRendererMaterial = myBallMaterials[(int)MaterialType.Lightning];
                myOnScreenBall.material = myBallMaterials[(int)MaterialType.Lightning];
                myVehicleName = "LightningRod";
                break;

            case (int)MaterialType.Lava:
                myRendererMaterial = myBallMaterials[(int)MaterialType.Lava];
                myOnScreenBall.material = myBallMaterials[(int)MaterialType.Lava];
                myVehicleName = "LavaPool";
                break;

            case (int)MaterialType.Chrome:
                myRendererMaterial = myBallMaterials[(int)MaterialType.Chrome];
                myOnScreenBall.material = myBallMaterials[(int)MaterialType.Chrome];
                myVehicleName = "ChromeReflection";
                break;

            case (int)MaterialType.Water:
                myRendererMaterial = myBallMaterials[(int)MaterialType.Water];
                myOnScreenBall.material = myBallMaterials[(int)MaterialType.Water];
                myVehicleName = "WaterHole";
                break;
        }
    }

    public void ChangeArmorType()
    {
        switch (myArmorDropDown.value)
        {
            case (int)ArmorType.Default:
                myArmorTypes[(int)ArmorType.Default].SetActive(true);
                myArmorTypes[(int)ArmorType.BigBooty].SetActive(false);
                myArmorTypes[(int)ArmorType.MoHawk].SetActive(false);
                myArmorNumber = (int)ArmorType.Default;
                break;

            case (int)ArmorType.MoHawk:
                myArmorTypes[(int)ArmorType.Default].SetActive(false);
                myArmorTypes[(int)ArmorType.BigBooty].SetActive(false);
                myArmorTypes[(int)ArmorType.MoHawk].SetActive(true);
                myArmorNumber = (int)ArmorType.MoHawk;
                break;

            case (int)ArmorType.BigBooty:
                myArmorTypes[(int)ArmorType.Default].SetActive(false);
                myArmorTypes[(int)ArmorType.BigBooty].SetActive(true);
                myArmorTypes[(int)ArmorType.MoHawk].SetActive(false);
                myArmorNumber = (int)ArmorType.BigBooty;
                break;
        }
    }
}
