using UnityEngine;
using UnityEngine.UI;

public class ScreenOrientationHandler : MonoBehaviour
{
    [SerializeField]
    private ScrollRect scrollRect; // drag your scroll rect here in inspector

    [SerializeField]
    private Vector2 scrollSizeForPortrait; // define the size for portrait orientation

    [SerializeField]
    private Vector2 scrollSizeForLandscape; // define the size for landscape orientation

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft; // locks the screen to Landscape (too much effort to do all the cases even with AI)
    }


}
