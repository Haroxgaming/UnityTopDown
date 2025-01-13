using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public Door linkedDoor;
    public void activation()
    {
        linkedDoor.open();
    }
}
