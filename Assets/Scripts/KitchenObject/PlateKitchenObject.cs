using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddEventArgs> OnIngredientAdd;
    public class OnIngredientAddEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList; 
    
    private List<KitchenObjectSO> kitchenObjectSOList;
    private const string BREAD = "Bread";

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false;
        }

        if (kitchenObjectSO.objectName == BREAD && kitchenObjectSOList.Contains(kitchenObjectSO)) {
            return false;
        }
        else {
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdd?.Invoke(this, new OnIngredientAddEventArgs {
                kitchenObjectSO = kitchenObjectSO
            });

            return true;
        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList() {
        return kitchenObjectSOList;
    }
}
