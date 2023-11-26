using UnityEngine;

public class PlayerController : VehicleController
{
    // INSPECTOR VARIABLES
    [Header("Scriptable Object Data")]
    [SerializeField] protected VehicleData myData = null;

    // LOCAL VARIABLES
    private float myCurrentSpeed;

    private void Start()
    {
        InitializeVariables();
        GetWaypoints();
    }

    private void Update()
    {
        if (RaceManager.Load.GetGameStarted && !RaceManager.Load.GetRaceOver)
        {
            PressMoveForwardKey();
            ReleaseMoveForwardKey();
            PressRotateRightKey();
            PressRotateLeftKey();
        }
        else
        {
            AutoDriveTakeOver();
        }
    }

    private void InitializeVariables()
    {

        SetArmorType();
        myCurrentSpeed = myData.GetRollSpeed;
        myData.SetIsMoving(false);
        myCurrentRacePosition = 0;
        myCurrentTrackWaypoint = 0;
        myRenderer.material = myBallMaterial;
        myProximityToCurrentWaypoint = myData.GetWaypointProximity * myData.GetWaypointProximity;
        myRenderer.material.SetColor("_Color", new Color(myMaterialRed, myMaterialGreen, myMaterialBlue, myMaterialAlpha));
        SetupParticles();
    }

    private void SetupParticles()
    {
        for (int i = 0; i < myThrusterParticles.Length; i++)
        {
            myThrusterParticles[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < myExhaustParticles.Length; i++)
        {
            myExhaustParticles[i].gameObject.SetActive(false);
        }
    }

    private void SetArmorType()
    {
        if (myArmorType == 0)
        {
            myArmor[0].SetActive(true);
            myArmor[1].SetActive(false);
            myArmor[2].SetActive(false);
            myExhaustParticles[0].gameObject.SetActive(true);
        }
        else if (myArmorType == 1)
        {
            myArmor[0].SetActive(false);
            myArmor[1].SetActive(true);
            myArmor[2].SetActive(false);
            myExhaustParticles[1].gameObject.SetActive(true);
            myExhaustParticles[2].gameObject.SetActive(true);
        }
        else if (myArmorType == 2)
        {
            myArmor[0].SetActive(false);
            myArmor[1].SetActive(false);
            myArmor[2].SetActive(true);
            myExhaustParticles[3].gameObject.SetActive(true);
        }
/*            switch (myArmorType)
        {
            case 0:
                myArmor[0].SetActive(true);
                myArmor[1].SetActive(false);
                myArmor[2].SetActive(false);
                myExhaustParticles[0].gameObject.SetActive(true);
                break;

            case 1:
                myArmor[0].SetActive(false);
                myArmor[1].SetActive(true);
                myArmor[2].SetActive(false);
                myExhaustParticles[1].gameObject.SetActive(true);
                myExhaustParticles[2].gameObject.SetActive(true);
                break;

            case 2:
                myArmor[0].SetActive(false);
                myArmor[1].SetActive(false);
                myArmor[2].SetActive(true);
                myExhaustParticles[3].gameObject.SetActive(true);
                break;
        }*/
    }

    private void PressRotateLeftKey()
    {
        if (Input.GetKey(KeyCode.A))
        {
            myArmor[myArmorType].transform.Rotate(new Vector3(0, -myData.GetRotationSpeed, 0) * Time.deltaTime);
        }
    }

    private void PressRotateRightKey()
    {
        if (Input.GetKey(KeyCode.D))
        {
            myArmor[myArmorType].transform.Rotate(new Vector3(0, myData.GetRotationSpeed, 0) * Time.deltaTime);
        }
    }

    private void ReleaseMoveForwardKey()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            myData.SetIsMoving(false);

            if (myArmorType == 0)
            {
                myExhaustParticles[0].gameObject.SetActive(true);
                myThrusterParticles[0].gameObject.SetActive(false);
            }
            else if (myArmorType == 1)
            {
                myExhaustParticles[1].gameObject.SetActive(true);
                myExhaustParticles[2].gameObject.SetActive(true);
                myThrusterParticles[1].gameObject.SetActive(false);
                myThrusterParticles[2].gameObject.SetActive(false);
            }
            else if (myArmorType == 2)
            {
                myExhaustParticles[3].gameObject.SetActive(true);
                myThrusterParticles[3].gameObject.SetActive(false);
            }
            /*switch (myArmorType)
            {
                case 0:
                    myExhaustParticles[0].gameObject.SetActive(true);
                    myThrusterParticles[0].gameObject.SetActive(false);
                    break;

                case 1:
                    myExhaustParticles[1].gameObject.SetActive(true);
                    myExhaustParticles[2].gameObject.SetActive(true);
                    myThrusterParticles[1].gameObject.SetActive(false);
                    myThrusterParticles[2].gameObject.SetActive(false);
                    break;

                case 2:
                    myExhaustParticles[3].gameObject.SetActive(true);
                    myThrusterParticles[3].gameObject.SetActive(false);
                    break;
            }*/

            mySphere.AddForce(Physics.gravity * (mySphere.mass * myData.GetMassMultiplier));
        }
    }

    private void PressMoveForwardKey()
    {
        if (Input.GetKey(KeyCode.W))
        {
            myData.SetIsMoving(true);

            if (myArmorType == 0)
            {
                myExhaustParticles[0].gameObject.SetActive(false);
                myThrusterParticles[0].gameObject.SetActive(true);
            }
            else if (myArmorType == 1)
            {
                myExhaustParticles[1].gameObject.SetActive(false);
                myExhaustParticles[2].gameObject.SetActive(false);
                myThrusterParticles[1].gameObject.SetActive(true);
                myThrusterParticles[2].gameObject.SetActive(true);
            }
            else if (myArmorType == 2)
            {
                myExhaustParticles[3].gameObject.SetActive(false);
                myThrusterParticles[3].gameObject.SetActive(true);
            }
            /*switch (myArmorType)
            {
                case 0:
                    myExhaustParticles[0].gameObject.SetActive(false);
                    myThrusterParticles[0].gameObject.SetActive(true);
                    break;

                case 1:
                    myExhaustParticles[1].gameObject.SetActive(false);
                    myExhaustParticles[2].gameObject.SetActive(false);
                    myThrusterParticles[1].gameObject.SetActive(true);
                    myThrusterParticles[2].gameObject.SetActive(true);
                    break;

                case 2:
                    myExhaustParticles[3].gameObject.SetActive(false);
                    myThrusterParticles[3].gameObject.SetActive(true);
                    break;
            }*/

            mySphere.AddForce(myArmor[myArmorType].transform.forward * myCurrentSpeed, ForceMode.Force);
            mySphere.AddForce(Physics.gravity * mySphere.mass);
        }
    }

    private void AutoDriveTakeOver()
    {
        var waypointPosition = theTrackWaypointsToFollow[myCurrentTrackWaypoint].position;
        var relativeWaypointPos = transform.InverseTransformPoint(new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z));

        myData.SetIsMoving(false);

        if (RaceManager.Load.GetGameStarted)
        {
            if (myArmorType == 0)
            {
                myExhaustParticles[0].gameObject.SetActive(true);
                myThrusterParticles[0].gameObject.SetActive(false);
            }
            else if (myArmorType == 1)
            {
                myExhaustParticles[1].gameObject.SetActive(true);
                myExhaustParticles[2].gameObject.SetActive(true);
                myThrusterParticles[1].gameObject.SetActive(false);
                myThrusterParticles[2].gameObject.SetActive(false);
            }
            else if (myArmorType == 2)
            {
                myExhaustParticles[3].gameObject.SetActive(true);
                myThrusterParticles[3].gameObject.SetActive(false);
            }
            /*switch (myArmorType)
            {
                case 0:
                    myExhaustParticles[0].gameObject.SetActive(true);
                    myThrusterParticles[0].gameObject.SetActive(false);
                    break;

                case 1:
                    myExhaustParticles[1].gameObject.SetActive(true);
                    myExhaustParticles[2].gameObject.SetActive(true);
                    myThrusterParticles[1].gameObject.SetActive(false);
                    myThrusterParticles[2].gameObject.SetActive(false);
                    break;

                case 2:
                    myExhaustParticles[3].gameObject.SetActive(true);
                    myThrusterParticles[3].gameObject.SetActive(false);
                    break;
            }*/

            mySphere.AddForce(myArmor[myArmorType].transform.forward * (myData.GetRollSpeed / 2), ForceMode.Force);
            mySphere.AddForce(Physics.gravity * mySphere.mass);
            myArmor[myArmorType].transform.LookAt(theTrackWaypointsToFollow[myCurrentTrackWaypoint]);
        }
        CheckWaypointPosition(relativeWaypointPos);
    }

    private void ResetSpeed()
    {
        myCurrentSpeed = myData.GetRollSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FinishLine" && !RaceManager.Load.GetRaceOver)
        {
            if (RaceManager.Load.GetRacers < LoadingManager.Load.GetTrackPolePositions)
            {
                RaceManager.Load.AddRacers(gameObject);
            }
            else if (RaceManager.Load.GetRacers >= LoadingManager.Load.GetTrackPolePositions)
            {
                RaceManager.Load.ResetRacers();
                RaceManager.Load.AddRacers(gameObject);
            }
            RaceManager.Load.SetPlayerPosition(RaceManager.Load.GetRacers);
            RaceManager.Load.SetCurrentLap(RaceManager.Load.GetCurrentLap + 1);
        }

        if (other.name == "BoostPoint" && !RaceManager.Load.GetRaceOver)
        {
            myCurrentSpeed = myData.GetRollSpeed * 2f;
            Invoke(nameof(ResetSpeed), 1);
        }

        if (other.name == "SlowPoint" && !RaceManager.Load.GetRaceOver)
        {
            myCurrentSpeed = myData.GetRollSpeed * 0.3f;
            Invoke(nameof(ResetSpeed), 1);
        }
    }
}