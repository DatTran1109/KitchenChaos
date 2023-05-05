using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnSpawnPlate;
    public event EventHandler OnPlateGrabbed;

    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer = 3f;
    private float spawnPlateTimerMax = 4f;
    private int spawnPlateAmount;
    private int spawnPlateAmountMax = 4;

    private void Update() {
        if (spawnPlateAmount < spawnPlateAmountMax && KitchenGameManager.Instance.IsGamePlayingActive()) {
            spawnPlateTimer += Time.deltaTime;
           
            if (spawnPlateTimer >= spawnPlateTimerMax) {
                spawnPlateTimer = 0f;
                spawnPlateAmount++;
                OnSpawnPlate?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player) {
        if (spawnPlateAmount > 0 && !player.HasKitchenObject()) {
            spawnPlateAmount--;
            KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
            OnPlateGrabbed?.Invoke(this, EventArgs.Empty);
        }
    }
}
