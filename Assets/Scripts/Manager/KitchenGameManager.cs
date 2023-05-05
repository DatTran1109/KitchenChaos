using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State {
        Waiting,
        Countdown,
        GamePlaying,
        GameOver,
    } 

    private State state;
    private float countdownTimer = 3f;
    private float gameplayingTimer;
    private float gameplayingTimerMax = 20f;
    private bool isGamePaused = false;

    private void Awake() {
        Instance = this;
        state = State.Waiting;
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if (state == State.Waiting) {
            state = State.Countdown;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e) {
        gameplayingTimer = 0f;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void Update() {
        switch (state) {
            case State.Waiting:
                break;
            case State.Countdown:
                countdownTimer -= Time.deltaTime;
                if (countdownTimer <= 0f) {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gameplayingTimer += Time.deltaTime;
                if (gameplayingTimer >= gameplayingTimerMax) {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlayingActive() {
        return state == State.GamePlaying;
    }

    public bool IsGameOverActive() {
        return state == State.GameOver;
    }

    public bool IsCountdownActive() {
        return state == State.Countdown;
    }

    public float GetCountdownTimer() {
        return countdownTimer;
    }

    public float GetGamePlayingTimerNormalized() {
        return gameplayingTimer / gameplayingTimerMax;
    }

    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;

        if (isGamePaused) {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
