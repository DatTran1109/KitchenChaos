using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent 
{
    public static event EventHandler OnAnyObjectDrop;

    public static void ClearStaticData() {
        OnAnyObjectDrop = null;
    }

    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;

    public virtual void Interact(Player player) {
        Debug.Log("!");
    }

    public virtual void InteractAlternate() {
        Debug.Log("!");
    }

    public Transform GetHoldPoint() {
        return counterTopPoint;
    }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null ) {
            OnAnyObjectDrop?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearKitchenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
