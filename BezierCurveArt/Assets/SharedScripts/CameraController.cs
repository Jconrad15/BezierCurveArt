using UnityEngine;

public class CameraController : MonoBehaviour
{
    private readonly float rotateSpeed = 1.5f;
    private readonly float zoomSpeed = 80f;

    [SerializeField]
    private float radius = 10;
    private float t = 90;
    [SerializeField]
    private float startRotation = 0f;

    void Start()
    {
        Rotate(startRotation);
    }

    void Update()
    {
        TryUserRotate();
        TryUserZoom();
    }

    private void TryUserZoom()
    {
        float zoomMovement = Input.mouseScrollDelta.y;
        if (zoomMovement != 0)
        {
            Zoom(zoomMovement);
        }
    }

    private void Zoom(float zoomMovement)
    {
        radius -= zoomMovement *
            zoomSpeed *
            (radius / 10) *
            Time.deltaTime;

        UpdatePosition();
    }

    private void TryUserRotate()
    {
        float hMovement = Input.GetAxisRaw("Horizontal");
        if (hMovement != 0)
        {
            Rotate(hMovement);
        }
    }

    private void Rotate(float hMovement)
    {
        t += hMovement * rotateSpeed * Time.deltaTime;
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 newPos = new Vector3(
            radius * Mathf.Cos(t),
            0,
            radius * Mathf.Sin(t));

        transform.position = newPos;
        transform.LookAt(Vector3.zero);
    }
}