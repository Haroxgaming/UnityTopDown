using UnityEngine;

public class LightSphere : MonoBehaviour
{
    public Door linkedDoor;
    public bool activate;
    public bool haveDoorLinked;
    public GameObject Vfx;
    public LightSphere[] _NeedForActivate;

    public void activation()
    {
        if (!activate)
        {
            if (_NeedForActivate == null || _NeedForActivate.Length == 0)
            {
                ActivateThisSphere();
            }
            else
            { 
                bool allRequiredActivated = true;
                foreach (var sphere in _NeedForActivate)
                {
                    if (!sphere.activate)
                    {
                        allRequiredActivated = false;
                        break;
                    }
                }

                if (allRequiredActivated && !activate)
                {
                    ActivateThisSphere();
                }
                else
                {
                    foreach (var sphere in _NeedForActivate)
                    {
                        sphere.reset();
                    }
                }
            }
        }
        
    }

    private void ActivateThisSphere()
    {
        Vfx.SetActive(true);
        activate = true;
        if (haveDoorLinked && linkedDoor != null)
        {
            linkedDoor.open();
        }
    }

    public void reset()
    {
        Vfx.SetActive(false);
        activate = false;
    }
}