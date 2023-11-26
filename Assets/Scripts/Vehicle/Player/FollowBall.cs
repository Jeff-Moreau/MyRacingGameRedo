using UnityEngine;

public class FollowBall : MonoBehaviour
{
    // INSPECTOR VARIABLES
    [Header("Objects Needed")]
    [SerializeField] private Transform Ball = null;
    [SerializeField] private VehicleData ArmorOffset = null;

    private void Update()
    {
        transform.position = new Vector3(Ball.transform.position.x, Ball.transform.position.y + ArmorOffset.GetArmorHeight, Ball.transform.position.z);
    }
}