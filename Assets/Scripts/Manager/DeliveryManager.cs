using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawn;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFail;

    [SerializeField] private OrderRecipeListSO orderRecipeListSO;
    
    public static DeliveryManager Instance { get; private set; }

    private List<OrderRecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 5;
    private int successRecipeAmount;
    private int score;
    private int lastRecord;
    private const string LAST_RECORD = "LastRecord";

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<OrderRecipeSO>();
        lastRecord = PlayerPrefs.GetInt(LAST_RECORD, 0);
    }

    private void Update()
    {
        if (KitchenGameManager.Instance.IsGamePlayingActive()) {
            spawnRecipeTimer -= Time.deltaTime;

            if (spawnRecipeTimer <= 0f) {
                spawnRecipeTimer = spawnRecipeTimerMax;

                if (waitingRecipeSOList.Count < waitingRecipeMax) {
                    OrderRecipeSO waitingRecipeSO = orderRecipeListSO.orderRecipeSOList[UnityEngine.Random.Range(0, orderRecipeListSO.orderRecipeSOList.Count)];
                    waitingRecipeSOList.Add(waitingRecipeSO);

                    OnRecipeSpawn?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }

    public bool DeliveryOrderRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            OrderRecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            List<KitchenObjectSO> plateKitchenObjectSOList = plateKitchenObject.GetKitchenObjectSOList();

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObjectSOList.Count) {
                bool plateRecipeMatched = true;
                bool ingredientFound = false;

                foreach (KitchenObjectSO waitingKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObjectSOList) {
                        if (plateKitchenObjectSO == waitingKitchenObjectSO) {
                            plateKitchenObjectSOList.Remove(plateKitchenObjectSO);
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound) {
                        plateRecipeMatched = false;
                        break;
                    }
                }

                if (plateRecipeMatched) {
                    score += waitingRecipeSO.score;

                    if (score > lastRecord) {
                        SaveBestScore(score);
                    }

                    successRecipeAmount ++;
                    waitingRecipeSOList.RemoveAt(i);

                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return true;
                }
            }
        }

        OnRecipeFail?.Invoke(this, EventArgs.Empty);
        return false;
    }

    public List<OrderRecipeSO> GetOrderRecipeSOList() {
        return waitingRecipeSOList;
    }

    public int GetSuccessRecipeAmount() {
        return successRecipeAmount;
    }

    private void SaveBestScore(int score) {
        PlayerPrefs.SetInt(LAST_RECORD, score);
        PlayerPrefs.Save();
    }

    public int GetScore() {
        return score;
    }

    public int GetLastRecord() {
        return lastRecord;
    }
}
