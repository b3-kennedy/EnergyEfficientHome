using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        panel.SetActive(true); // Activate the panel at the beginning
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }
}
