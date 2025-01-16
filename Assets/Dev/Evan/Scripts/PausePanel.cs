using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject HUD;
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnResume()
    {
        HUD.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
