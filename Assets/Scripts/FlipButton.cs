using UnityEngine;
using UnityEngine.UI;

public class FlipButton : MonoBehaviour
{
    private bool isFlipped = true;
    private Button button;
    private RectTransform rectTransform;

    public GameObject scrollView;
    void Start()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        button.onClick.AddListener(FlipImage);
        rectTransform.rotation = Quaternion.Euler(0, 0, 90);
        scrollView.SetActive(false);
    }

    void FlipImage()
    {
        isFlipped = !isFlipped;

        if (isFlipped)
        {
            // rotate 90 degrees to the left
            rectTransform.rotation = Quaternion.Euler(0, 0, 90);
            // Close the menu
            scrollView.SetActive(false);
            
        }
        else
        {
            // rotate 90 degrees to the right
            rectTransform.rotation = Quaternion.Euler(0, 0, -90);
            // Open the menu
            scrollView.SetActive(true);
        }
    }
}
