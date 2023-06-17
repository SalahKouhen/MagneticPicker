using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
    public FlexibleColorPicker colorPicker;
    public GameObject magnet;
    public Button colorIconButton;

    void Start()
    {
        colorIconButton = GetComponentInChildren<Button>();
        colorIconButton.onClick.AddListener(OpenColorPicker);
        colorPicker.gameObject.SetActive(false);
    }

    void OpenColorPicker()
    {
        // Show the color picker
        colorPicker.gameObject.SetActive(true);

        // Set the initial color to the current color of the magnet
        colorPicker.color = magnet.GetComponent<Renderer>().material.color;

        // Start a coroutine to monitor the color picker
        StartCoroutine(MonitorColorPicker());
    }

    IEnumerator MonitorColorPicker()
    {
        Color oldColor = colorPicker.color;

        while (colorPicker.gameObject.activeSelf)
        {
            if (colorPicker.color != oldColor)
            {
                UpdateColor(colorPicker.color);
                oldColor = colorPicker.color;
            }

            yield return null; // Wait until the next frame
        }
    }


    void UpdateColor(Color newColor)
    {
        // Update the color of the magnet and list item background
        magnet.GetComponent<Renderer>().material.color = newColor;
        colorIconButton.image.color = newColor;

        // Hide the color picker
        colorPicker.gameObject.SetActive(false);
    }
}
