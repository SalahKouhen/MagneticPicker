using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class StringController : MonoBehaviour
{
    public GameObject pivot; // assign in Inspector
    public GameObject bob; // assign in Inspector

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // we need two points for the string: pivot and bob
    }

    void Update()
    {
        // Update positions to match pivot and bob
        lineRenderer.SetPosition(0, pivot.transform.position);
        lineRenderer.SetPosition(1, bob.transform.position);
    }
}
