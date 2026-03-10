using System.Collections;
using UnityEngine;

public class Warp : MonoBehaviour
{
    [SerializeField] private SurviceHub hub;
    public GameObject warpPoint;
    private bool canwarp = true;
    public GameObject warpVisual;
    public float warpCooldown = 5;
    private void OnTriggerEnter(Collider other)
    {
        if (canwarp)
        {
            
            other.gameObject.transform.position = warpPoint.transform.position;
            StartCoroutine(WarpTimer());
        }
    }

    


    IEnumerator WarpTimer()
    {
        canwarp = false;
        warpPoint.SetActive(false);
        warpVisual.SetActive(false);
        yield return new WaitForSecondsRealtime(warpCooldown);
        warpVisual.SetActive(true);
        warpPoint.SetActive(true);
        canwarp = true;


    }
    
}
