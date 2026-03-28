
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject closePoint;
    [SerializeField] private GameObject openPoint;
    public float doorSpeed;
    private bool isClosed = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("opened");
            isClosed = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("closed");
            isClosed = true;
        }
    }
    private void Update()
    {
        if(!isClosed)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position,openPoint.transform.position,doorSpeed *Time.deltaTime);
        }
        else
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, closePoint.transform.position, doorSpeed * Time.deltaTime);

        }
    }
}
