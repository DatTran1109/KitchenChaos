using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO() { 
        return kitchenObjectSO;
    }

    public IKitchenObjectParent GetKitchenObjectParent() { 
        return kitchenObjectParent;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {
        if (kitchenObjectParent.HasKitchenObject()) {
            Debug.LogError("Parent already have kitchen object!");
        }

        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;
        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetHoldPoint();
        transform.localPosition = Vector3.zero;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if (this is PlateKitchenObject) {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else {
            plateKitchenObject = null;
            return false;
        }
    }

    public void DestroySelf() {
        if (this.kitchenObjectParent != null) {
            this.kitchenObjectParent.ClearKitchenObject();
        }

        Destroy(gameObject);
    }

    public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent) {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(kitchenObjectParent);
    }
}
