using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private float warningSoundTimer;
    private bool isBurnWarning;
    private float warningSoundTimerMax = 0.2f;

    private void Start() {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        gameObject.SetActive(false);
    }

    private void Update() {
        if (isBurnWarning) {
            warningSoundTimer -= Time.deltaTime;

            if (warningSoundTimer <= 0f) {
                warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnWarningPoint = 0.5f;
        isBurnWarning = stoveCounter.IsFried() && e.progress >= burnWarningPoint;

        if (isBurnWarning) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
