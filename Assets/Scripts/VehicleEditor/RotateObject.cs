using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // INSPECTOR VARIABLES
    [SerializeField] private Camera myMenuCamera = null;

    // PRIVATE VARIABLES
    private Vector3 myPreviousPosition;
    private Vector3 myPositionDelta;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        myPreviousPosition = Vector3.zero;
        myPositionDelta = Vector3.zero;
    }

    private void Update()
    {
        ClickAndDragRotateObject();

        myPreviousPosition = Input.mousePosition;
    }

    private void ClickAndDragRotateObject()
    {
        if (Input.GetMouseButton(1))
        {
            myPositionDelta = Input.mousePosition - myPreviousPosition;

            if (Vector3.Dot(transform.up, Vector3.up) >= 0)
            {
                transform.Rotate(transform.up, -Vector3.Dot(myPositionDelta, myMenuCamera.transform.right), Space.World);
            }
            else
            {
                transform.Rotate(transform.up, Vector3.Dot(myPositionDelta, myMenuCamera.transform.right), Space.World);
            }

            transform.Rotate(myMenuCamera.transform.right, Vector3.Dot(myPositionDelta, myMenuCamera.transform.up), Space.World);
        }
    }
}