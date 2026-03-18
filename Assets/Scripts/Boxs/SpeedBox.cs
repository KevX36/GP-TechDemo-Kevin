using UnityEngine;

public class SpeedBox : MonoBehaviour, IBox
{
    [SerializeField] private SurviceHub hub;
    [SerializeField] private PlayerController player;
    private void Awake()
    {
        player = hub.player.GetComponent<PlayerController>();
    }
    public void Open()
    {
        Debug.Log("Box activated");
        player.SetBuff("speed");

        this.gameObject.SetActive(false);
    }

}
