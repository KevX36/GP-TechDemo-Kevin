using UnityEngine;

public class JumpBox : MonoBehaviour, IBox
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private SurviceHub hub;
    [SerializeField] private PlayerController player;
    private void Awake()
    {
        player = hub.player.GetComponent<PlayerController>();
    }
    public void Open()
    {
        Debug.Log("Box activated");
        player.SetBuff("jump");

        this.gameObject.SetActive(false);
    }
}
