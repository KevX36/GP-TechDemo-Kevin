using UnityEngine;

public class healthAndCheckPoints : MonoBehaviour
{
    public int health = 100;
    private int maxHealth = 100;

    private void Start()
    {
        maxHealth = health;
    }
    [SerializeField]private GameObject checkPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            Debug.Log("checkPointSaved");
            checkPoint = other.gameObject;
        }
        if (other.gameObject.CompareTag("KillZone"))
        {
            Debug.Log("killed player");
            health = 0;
        }
    }
    private void Update()
    {
        if(health <= 0)
        {
            health = maxHealth;
            transform.position = checkPoint.transform.position;
        }
    }
}
