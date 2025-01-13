using UnityEngine;

public class LightSphere : MonoBehaviour
{
    public GameObject colorChange;
    public Door linkedDoor;

    public Material matDisable, matEnable;

    public void activation()
    {
        colorChange.GetComponent<Renderer>().material = matEnable;
        linkedDoor.open();
    }
    
    public void Disable()
    {
        colorChange.GetComponent<Renderer>().material = matDisable;
    }
}
