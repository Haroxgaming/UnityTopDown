using UnityEngine;

public class LightSphere : MonoBehaviour
{
    public GameObject colorChange;
    public Door linkedDoor;
    public bool activate;
    public bool haveDoorLinked;

    public Material matEnable;
    public Material matDisable;
    public LightSphere[] _NeedForActivate;

    public void activation()
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
        }
    }

    private void ActivateThisSphere()
    {
        if (colorChange != null && colorChange.GetComponent<Renderer>() != null)
        {
            colorChange.GetComponent<Renderer>().material = matEnable;
        }
        activate = true;

        if (haveDoorLinked && linkedDoor != null)
        {
            linkedDoor.open();
        }
    }

    public void reset()
    {
        if (colorChange != null && colorChange.GetComponent<Renderer>() != null)
        {
            colorChange.GetComponent<Renderer>().material = matDisable; // Utilise le matériau désactivé
        }
        activate = false;
    }
}