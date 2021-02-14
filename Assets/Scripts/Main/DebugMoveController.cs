using System;
using UnityEngine;
using Zenject;

public class DebugMoveController : MonoBehaviour {
    Vector3 tapPosition = Vector3.zero;
    Vector3 finishPosition = Vector3.zero;
    Vector3 startPosition = Vector3.zero;
    private bool inMove = false;
    // [Inject] private TubeMapService _tubeMapService;

    
    // [Inject] private TubeMapService _tubeMapService;
    // private PointController[] _points;

    // private void Start() {
        // _points = GetComponentsInChildren<PointController>();
    // }

    // временно, для отладки
    private void Update() {
        if (Input.GetMouseButtonDown(0) && ClickOnCurrent()) {
            inMove = true;
            tapPosition = Input.mousePosition;
            startPosition = transform.position;
            // _tubeMapService.Free(GetComponentsInChildren<PointController>());
        }

        if (Input.GetMouseButton(0) && inMove) {
            finishPosition = Input.mousePosition;
            LeftMouseDrag();
        }

        if (Input.GetMouseButtonUp(0)) {
            inMove = false;
            // _tubeMapService.Busy(GetComponentsInChildren<PointController>());
        }
    }

    // временно, для отладки
    void LeftMouseDrag() {
        // вектор направлениея движения в мире в плоскости экрана
        Vector3 direction = Camera.main.ScreenToWorldPoint(finishPosition) -
                            Camera.main.ScreenToWorldPoint(tapPosition);
        if (direction == Vector3.zero) {
            return;
        }

        Vector3 position = startPosition + direction;
        if (Vector2.Distance(position, transform.position) > 1f) {
            transform.position = new Vector3Int((int) position.x, (int) position.y, 0);
        }
    }

    // временно, для отладки
    private bool ClickOnCurrent() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Debug.Log(hit.transform.name);
            DebugMoveController controller = hit.transform.GetComponent<DebugMoveController>();
            return controller == this;
        }

        return false;
    }
}