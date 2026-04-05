using UnityEngine;

public class basicBox : MonoBehaviour,IBox
{
    
    
    public void Open()
    {
        Debug.Log("cut basic box");

        Destroy(this.gameObject);
    }
}
