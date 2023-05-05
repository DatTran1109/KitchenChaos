using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    [SerializeField] PlateCounter plateCounter;
    [SerializeField] Transform plateVisualPrefab;

    private Transform counterTopPoint;
    private List<GameObject> plateGameObjectList;

    private void Awake() {
        plateGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        counterTopPoint = plateCounter.GetHoldPoint();
        plateCounter.OnSpawnPlate += PlateCounter_OnSpawnPlate;
        plateCounter.OnPlateGrabbed += PlateCounter_OnPlateGrabbed;
    }

    private void PlateCounter_OnSpawnPlate(object sender, System.EventArgs e) {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateGameObjectList.Count, 0);

        plateGameObjectList.Add(plateVisualTransform.gameObject);
    }

    private void PlateCounter_OnPlateGrabbed(object sender, System.EventArgs e) {
        GameObject plateGameObject = plateGameObjectList[plateGameObjectList.Count - 1];
        Destroy(plateGameObject);

        plateGameObjectList.Remove(plateGameObject);
    }
}
