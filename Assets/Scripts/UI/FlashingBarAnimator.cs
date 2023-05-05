using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingBarAnimator : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;
    private const string IS_FLASHING = "IsFlashing";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(IS_FLASHING, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnWarningPoint = 0.5f;
        bool isBurnWarning = stoveCounter.IsFried() && e.progress >= burnWarningPoint;

        animator.SetBool(IS_FLASHING, isBurnWarning);
    }
}
