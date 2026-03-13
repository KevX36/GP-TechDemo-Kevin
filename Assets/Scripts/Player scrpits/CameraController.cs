using UnityEngine;

public class CameraController : MonoBehaviour
{
    

    private Camera cam;
    private float pitch = 0;
    private float yaw = 0;
    public float pitchMax = 60;
    public float pitchMin = -60;
    public float turnspeed = 2;
    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }
    void Update()
    {
        pitch += Input.GetAxis("Mouse Y") * turnspeed;
        if (pitch < pitchMin)
        {
            pitch = pitchMin;
        }
        if (pitch > pitchMax)
        {
            pitch = pitchMax;
        }

        yaw += Input.GetAxis("Mouse X") * turnspeed;

        cam.transform.rotation = Quaternion.Euler(-pitch, yaw, 0);
        transform.rotation = Quaternion.Euler(0, yaw, 0);

    }
}
