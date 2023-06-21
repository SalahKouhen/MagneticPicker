using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PendulumBob : MonoBehaviour
{
    public GameObject pivot;
    public float stringLength = 6f;
    public float force = 100f;
    public float damping = 0.8f;
    public float perpendicularDamping = 0.995f;
    public float gravity = 9.8f;
    public float magnetStrength = 100f; // new field to control the strength of the magnetic attraction
    public float minMagnetDistance = 0.1f;
    public float displacementMagnitude = 0.01f;
    public float stopThreshold = 0.1f;
    public UIManager uimanager;
    public TMPro.TMP_InputField inputField;

    public ListItem listItem;
    public AddButton addButtonScript;

    public Button pickButton;
    public TMPro.TextMeshProUGUI pickButtonText;

    private bool isMoving = true;

    private Vector3 velocity = Vector3.zero;
    private List<GameObject> magnets = new List<GameObject>(); // new list to store the magnets
    private Vector3 initialPosition;

    private bool isPicked = false;

    private void Start()
    {
        foreach (GameObject magnet in GameObject.FindGameObjectsWithTag("Magnet")) // assuming the magnets are tagged with "Magnet"
        {
            magnets.Add(magnet);
        }

        initialPosition = transform.position;
    }

    private void Update()
    {
        if (isPicked)
        {
            Vector3 directionFromPivotToBob = (transform.position - pivot.transform.position).normalized;
            Vector3 desiredPosition = pivot.transform.position + directionFromPivotToBob * stringLength;

            // Decompose the velocity vector into components
            Vector3 velocityAlongString = Vector3.Project(velocity, directionFromPivotToBob);
            Vector3 velocityPerpendicularToString = velocity - velocityAlongString;

            // Apply damping only to the component of velocity along the string
            velocityAlongString *= damping;
            velocityPerpendicularToString *= perpendicularDamping;

            // Reconstruct the velocity vector
            velocity = velocityAlongString + velocityPerpendicularToString;

            // Apply force towards desired position
            velocity += (desiredPosition - transform.position) * force * Time.deltaTime * 2;

            // Apply gravity
            velocity += Vector3.down * gravity * Time.deltaTime * 2;

            // Apply magnetic attraction

            foreach (GameObject magnet in magnets)
            {
                Vector3 directionFromBobToMagnet = (magnet.transform.position - transform.position).normalized;
                float distanceToMagnet = Vector3.Distance(magnet.transform.position, transform.position);
                // Clamp the distance so that it never goes below minMagnetDistance
                distanceToMagnet = Mathf.Max(distanceToMagnet, minMagnetDistance);
                velocity += directionFromBobToMagnet * magnetStrength / (distanceToMagnet * distanceToMagnet) * Time.deltaTime * 2;

            }

            // Update the position
            transform.position += velocity * Time.deltaTime * 2;

            // Check if the bob's velocity is below the threshold
            if (velocity.magnitude < stopThreshold)
            {
                GameObject closestMagnet = null;
                float closestDistance = float.MaxValue;

                // Loop through all magnets
                for (int i = 0; i < magnets.Count; i++)
                {
                    GameObject magnet = magnets[i];
                    float distance = Vector3.Distance(magnet.transform.position, transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestMagnet = magnet;
                    }
                }

                // Check if the bob is close to a magnet
                if (closestDistance < stringLength * 0.05f)  // Change this factor if needed
                {
                    if (isMoving)
                    {
                        isMoving = false; // The bob has stopped moving
                        CheckClosestMagnet();
                    }
                }
                else
                {
                    isMoving = true;
                }
            }
            else
            {
                isMoving = true;
            }
        }
        
    }

    // This method is now called to displace the bob and allow it to move
    public void Pick()
    {
        if (isPicked)
        {
            // Reset the bob to its initial position and stop it moving
            transform.position = initialPosition;
            velocity = Vector3.zero;

            // Reset the UI
            UIManager.Instance.ResetUI();

            // Change the button's text back to "Pick"
            pickButtonText.text = "Pick";
        }
        else
        {
            // Displace the bob slightly from its initial position
            transform.position = initialPosition + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * displacementMagnitude;

            // Change the button's text to "Reset"
            pickButtonText.text = "Reset";
        }

        isPicked = !isPicked;
    }

    private void CheckClosestMagnet()
    {
        int closestMagnetIndex = -1;
        float closestDistance = float.MaxValue;

        // Loop through all magnets
        for (int i = 0; i < magnets.Count; i++)
        {
            GameObject magnet = magnets[i];
            float distance = Vector3.Distance(magnet.transform.position, transform.position);

            if (distance < closestDistance)
            {
                closestMagnetIndex = i;
                closestDistance = distance;
            }
        }

        if (closestMagnetIndex != -1)
        {
            Transform listItem = addButtonScript.listContent.GetChild(closestMagnetIndex);
            TMPro.TMP_InputField inputField = listItem.GetComponentInChildren<TMPro.TMP_InputField>();
            string magnetName = inputField.text;
            UIManager.Instance.DisplayWinningOption(magnetName, magnets[closestMagnetIndex].GetComponent<Renderer>().material.color);
        }
    }

    public void AddMagnet(GameObject newMagnet)
    {
        magnets.Add(newMagnet);
    }

    public void RemoveMagnet(GameObject magnet)
    {
        magnets.Remove(magnet);
    }
}
