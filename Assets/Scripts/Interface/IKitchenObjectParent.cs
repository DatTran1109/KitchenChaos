using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetHoldPoint();

    public KitchenObject GetKitchenObject();

    public void SetKitchenObject(KitchenObject kitchenObject);

    public void ClearKitchenObject();

    public bool HasKitchenObject();
}
