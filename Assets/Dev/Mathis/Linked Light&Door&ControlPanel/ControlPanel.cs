using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public Door[] _linkedDoorsOpen;
    public void activation()
    {
        for (int i = 0; i < _linkedDoorsOpen.Length; i++)
        {
            _linkedDoorsOpen[i].open();
        }
    }
}