using UnityEngine;
using UnityEngine.UI;

public class AddButton : MonoBehaviour
{
    public GameObject listItemPrefab;
    public GameObject magnetPrefab;
    public Transform listContent;
    public Transform magnetContainer;

    public FlexibleColorPicker colorPicker;

    public PendulumBob bobScript;
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(AddNewItem);
        colorPicker.gameObject.SetActive(false);
    }

    void AddNewItem()
    {
        // Instantiate a new list item
        GameObject newListItem = Instantiate(listItemPrefab, listContent);
        ListItem listItemScript = newListItem.GetComponent<ListItem>();
        listItemScript.colorPicker = colorPicker;

        // Instantiate a new magnet
        GameObject newMagnet = Instantiate(magnetPrefab, magnetContainer);

        listItemScript.magnet = newMagnet;

        // Add the new magnet to the list of magnets in the PendulumBob script
        bobScript.AddMagnet(newMagnet);

        // Implement additional functionality (like naming and coloring) here
        UpdateMagnetPositions();
    }
    void UpdateMagnetPositions()
    {
        int numMagnets = magnetContainer.childCount;

        for (int i = 0; i < numMagnets; i++)
        {
            Transform magnet = magnetContainer.GetChild(i);

            float angle = (i / (float)numMagnets) * 360f * Mathf.Deg2Rad; // angle in radians
            float circleRadius = 3f; // change this to adjust the circle size

            Vector3 magnetPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * circleRadius;
            magnet.position = new Vector3(5,0,5) + magnetPos;
        }
    }


}
