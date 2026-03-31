using UnityEngine;

public class basicBox : MonoBehaviour,IBox
{
    [SerializeField] private SurviceHub hub;
    [SerializeField] private PlayerController player;
    private void Start()
    {




        player = hub.player.GetComponent<PlayerController>();
    }
    public void Open()
    {
        Debug.Log("cut basic box");

        this.gameObject.SetActive(false);
    }
}
