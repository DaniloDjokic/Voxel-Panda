using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public Button optionsButton;
    public GameObject optionsOverlay;

    private void AttachEvents()
    {
        optionsButton.onClick.AddListener(OpenOptions);
    }

    private void OpenOptions()
    {
        Time.timeScale = 0.0f;
        optionsOverlay.SetActive(true);
    }
}
