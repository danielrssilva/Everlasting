using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    public float panSpeed = 2f;
    public float zoomSpeed = 2f;
    public float maxZoom = 8f;
    public float minZoom = 15f;
    public float cameraBounds = 90f;

    private Camera cam;
    private Vector3 lastPosition;
    private bool pauseCamera;
    private Vector3 targetPosition;

    void Awake() {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if (pauseCamera) {
            return;
        }
        HandleMouse();
    }

    private void HandleMouse() {
        if (Input.GetMouseButtonDown(2)) {
            lastPosition = Input.mousePosition;
        } else if (Input.GetMouseButton(2)) {
            PanCamera(Input.mousePosition);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, zoomSpeed);
    }

    public void PanCamera(Vector3 mousePosition) {
        Vector3 offset = cam.ScreenToViewportPoint(lastPosition - mousePosition);
        Vector3 move = new Vector3(offset.x * panSpeed * cam.orthographicSize,
            offset.y * panSpeed * cam.orthographicSize, 0);

        transform.Translate(move, Space.World);

        transform.position = Vector3.ClampMagnitude(transform.position, cameraBounds);

        lastPosition = mousePosition;
    }

    private void ZoomCamera(float offset, float speed) {
        if (offset == 0) {
            return;
        }
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - (offset * speed), maxZoom, minZoom);
    }
    Vector3 camSmoothDampV;

    public void PauseCamera(bool pause) {
        pauseCamera = pause;
    }

    IEnumerator ScrollToPosition() {
        float progress = 0;
        while (!targetPosition.Equals(transform.position)) {
            progress += Time.deltaTime * panSpeed;
            transform.position = Vector3.Lerp(transform.position, targetPosition, progress);
            yield return null;
        }
    }
}