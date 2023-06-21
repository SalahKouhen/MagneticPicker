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

    public Button removeButton;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(AddNewItem);
        removeButton.onClick.AddListener(RemoveItem);
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

        // Assign a random color to the new magnet
        Renderer magnetRenderer = newMagnet.GetComponent<Renderer>();
        magnetRenderer.material.color = new Color(Random.value, Random.value, Random.value);

        // Add the new magnet to the list of magnets in the PendulumBob script
        bobScript.AddMagnet(newMagnet);

        // Implement additional functionality (like naming and coloring) here
        UpdateMagnetPositions(magnetContainer.childCount);
    }
    void UpdateMagnetPositions(int numMagnets)
    {
        float circleRadius = Mathf.Max(0f, 2f * (numMagnets / 5f)); // calculate circleRadius

        for (int i = 0; i < numMagnets; i++)
        {
            Transform magnet = magnetContainer.GetChild(i);

            float angle = (i / (float)numMagnets) * 360f * Mathf.Deg2Rad + 0.1f; // angle in radians

            Vector3 magnetPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * circleRadius;
            magnet.position = new Vector3(5, 0, 5) + magnetPos;
        }
    }
    void RemoveItem()
    {
        if (magnetContainer.childCount > 0)
        {
            // Remove the last magnet
            int lastMagnetIndex = magnetContainer.childCount - 1;
            GameObject lastMagnet = magnetContainer.GetChild(lastMagnetIndex).gameObject;
            bobScript.RemoveMagnet(lastMagnet);
            Destroy(lastMagnet);

            // Remove the associated list item
            int lastItemIndex = listContent.childCount - 1;
            Destroy(listContent.GetChild(lastItemIndex).gameObject);

            UpdateMagnetPositions(magnetContainer.childCount - 1);
        }
    }
}
