using UnityEngine;
using UnityEngine.UI;

public class AddButton : MonoBehaviour
{
    public GameObject listItemPrefab;
    public GameObject magnetPrefab;
    public Transform listContent;
    public Transform magnetContainer;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(AddNewItem);
    }

    void AddNewItem()
    {
        // Instantiate a new list item
        GameObject newListItem = Instantiate(listItemPrefab, listContent);

        // Instantiate a new magnet
        GameObject newMagnet = Instantiate(magnetPrefab, magnetContainer);

        // Implement additional functionality (like naming and coloring) here
    }
}
