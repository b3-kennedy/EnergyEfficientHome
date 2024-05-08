using UnityEngine;

public class GameController : MonoBehaviour
{
    private bool timePaused = true;
    public PanelController panelController;

    void Start()
    {
        ManageTime();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && timePaused)
        {
            timePaused = false;
            ManageTime();

            // Close the panel
            panelController.ClosePanel();
        }
    }

    void ManageTime()
    {
        Time.timeScale = timePaused ? 0 : 1;
    }
}
