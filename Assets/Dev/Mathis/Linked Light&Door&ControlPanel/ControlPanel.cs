using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public Door[] _linkedDoorsOpen;
    public Door[] _linkedDoorsClose;
    public void activation()
    {
        for (int i = 0; i < (_linkedDoorsOpen.Length - 1); i++)
        {
            _linkedDoorsOpen[i].open();
        }
        for (int i = 0; i < (_linkedDoorsClose.Length - 1); i++)
        {
            _linkedDoorsClose[i].close();
        }
    }
}