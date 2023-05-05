using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerOpenContainer;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            OnPlayerOpenContainer?.Invoke(this, EventArgs.Empty);
        }
    }
}
