using UnityEngine;

public class HelpUIManager : MonoBehaviour
{
    public GameObject helpPanel;

    public void ToggleHelp()
    {
        helpPanel.SetActive(!helpPanel.activeSelf);
    }
}