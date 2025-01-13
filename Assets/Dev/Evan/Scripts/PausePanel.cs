using UnityEngine;

public class PausePanel : MonoBehaviour
{
    public GameObject pausePanel;
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnResume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
