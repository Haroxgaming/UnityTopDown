using UnityEngine;

public class LightSphere : MonoBehaviour
{
    public GameObject colorChange;
    public Door linkedDoor;
    private bool activate;

    public Material matEnable;

    public void activation()
    {
        if (!activate)
        {
            colorChange.GetComponent<Renderer>().material = matEnable;
            linkedDoor.open();
            activate = true;
        }
    }
}
