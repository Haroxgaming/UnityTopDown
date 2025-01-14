using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public Door[] linkedDoorsOpen;
    public Door[] linkedDoorsClose;
    public void activation()
    {
        foreach (Door linkedDoorOpen in linkedDoorsOpen)
        {
            linkedDoorOpen.open();
        }

        foreach (Door linkedDoorClose in linkedDoorsClose)
        {
            linkedDoorClose.close();
        }
    }
}
