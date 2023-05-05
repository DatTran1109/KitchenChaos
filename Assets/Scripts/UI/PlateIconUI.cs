using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] Transform iconTemplate;

    private void Start() {
        plateKitchenObject.OnIngredientAdd += PlateKitchenObject_OnIngredientAdd;
    }

    private void PlateKitchenObject_OnIngredientAdd(object sender, PlateKitchenObject.OnIngredientAddEventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        foreach (Transform child in transform) {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<IconTemplate>().SetPlateIcon(kitchenObjectSO);
        }
    }
}
