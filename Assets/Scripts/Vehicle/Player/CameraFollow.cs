using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Ball;
    [SerializeField] private GameObject VehicleArmor;

    private void Update()
    {
        transform.position = new Vector3(VehicleArmor.transform.position.x, VehicleArmor.transform.position.y - 0.01f, VehicleArmor.transform.position.z - 1.8f);
        transform.rotation = VehicleArmor.transform.rotation;
    }
}
