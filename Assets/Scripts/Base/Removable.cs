using UnityEngine;
using Zenject;

public class Removable : MonoBehaviour {
    [Inject] private TubeMapService _tubeMapService;

    [Inject] private TubeManager _tubeManager;
    [Inject] private HomeManager _homeManager;
    [Inject] private StoneManager _stoneManager;
    [Inject] private WaterManager _waterManager;

    private PointController[] _points;

    private TubeController tubeController;
    private HomeController homeController;
    private StoneController stoneController;
    private WaterController waterController;

    private void Start() {
        _points = GetComponentsInChildren<PointController>();

        tubeController = GetComponent<TubeController>();
        homeController = GetComponent<HomeController>();
        stoneController = GetComponent<StoneController>();
        waterController = GetComponent<WaterController>();
    }

    //ObjectManager<MonoBehaviour, BaseDto, object> objectManager
    private void Update() {
        CheckRemove();
    }

    private void CheckRemove() {
        if (Input.GetMouseButtonDown(1) && ClickOnCurrent()) {
            _tubeMapService.Free(_points);

            if (tubeController != null) {
                _tubeManager.Remove(this);
            }

            if (homeController != null) {
                _homeManager.Remove(this);
            }

            if (stoneController != null) {
                _stoneManager.Remove(this);
            }

            if (waterController != null) {
                _waterManager.Remove(this);
            }
        }
    }

    private bool ClickOnCurrent() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Removable controller = hit.transform.GetComponent<Removable>();
            return controller == this;
        }

        return false;
    }
}