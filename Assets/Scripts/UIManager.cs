using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TMPro.TextMeshProUGUI winningOptionText;
    public GameObject confettiPrefab;
    public Vector3 confettiSpawnPoint;
    public Vector3 confettiSpawnRotation;

    private void Awake()
    {
        // Make this script a singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        confettiSpawnPoint = new Vector3(-3.74f, 2.58f, -2.6f);
        confettiSpawnRotation = new Vector3(-3.47f, 46.34f, -1.12f);
        winningOptionText.gameObject.SetActive(false);
    }

    public void DisplayWinningOption(string winningOption, Color winningColor)
    {
        // Set the text of the UI element to the name of the winning option
        winningOptionText.text = "The winning option is: " + winningOption;

        // Set the color of the text to the color of the winning magnet
        winningOptionText.color = winningColor;

        winningOptionText.gameObject.SetActive(true);
        

        // Instantiate the confetti
        Instantiate(confettiPrefab, confettiSpawnPoint, Quaternion.Euler(confettiSpawnRotation));
    }

    public void ResetUI()
    {
        // Turn off the confetti
        foreach (var confetti in GameObject.FindGameObjectsWithTag("Confetti"))
        {
            Destroy(confetti);
        }

        // Clear the winning option text
        winningOptionText.gameObject.SetActive(false);
    }
}
