using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCutAction;

    new public static void ClearStaticData() {
        OnAnyCutAction = null;
    }

    public event EventHandler OnCutAction;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] CuttingRecipeSO[] cuttingRecipeSOArray;
    
    private int cuttingProgress;
    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            if (player.HasKitchenObject()) {
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(player.GetKitchenObject().GetKitchenObjectSO());

                if (cuttingRecipeSO) {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                        progress = (float) cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
            }
        }
        else {
            if (!player.HasKitchenObject()) {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }

    public override void InteractAlternate() {
        if (HasKitchenObject()) {
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());

            if (cuttingRecipeSO != null) {
                cuttingProgress++;

                OnCutAction?.Invoke(this, EventArgs.Empty);
                OnAnyCutAction?.Invoke(this, EventArgs.Empty);

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                    progress = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                });

                if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax) {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(cuttingRecipeSO.output, this);
                }
            }
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSO(KitchenObjectSO inputKitchenObjectSO) {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray) {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}
