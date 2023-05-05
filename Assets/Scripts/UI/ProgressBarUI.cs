using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject hasProgressGameObject;
    private IHasProgress hasProgress;

    private void Start() {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        
        if (hasProgress == null ) {
            Debug.LogError("Game Object dont implement IHasProgress!");
        }

        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        progressBar.fillAmount = 0f;
        gameObject.SetActive(false);
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        progressBar.fillAmount = e.progress;

        if (e.progress == 0f || e.progress == 1f) {
            gameObject.SetActive(false);
        }
        else {
            gameObject.SetActive(true);
        }
    }
}
