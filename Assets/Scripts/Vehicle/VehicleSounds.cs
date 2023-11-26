using UnityEngine;

public class VehicleSounds : MonoBehaviour
{
    [SerializeField] private VehicleData Vehicle = null;
    [SerializeField] private AudioSource SoundIdle = null;
    [SerializeField] private AudioSource SoundThruster = null;

    private void Update()
    {
        if (Vehicle.GetIsMoving && !SoundThruster.isPlaying)
        {
            SoundIdle.Stop();
            SoundThruster.PlayOneShot(Vehicle.GetThrusterSound);
        }
        else if (!Vehicle.GetIsMoving && !SoundIdle.isPlaying)
        {
            SoundThruster.Stop();
            SoundIdle.PlayOneShot(Vehicle.GetIdleSound);
        }
    }
}
