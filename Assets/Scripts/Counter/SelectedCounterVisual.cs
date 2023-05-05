using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] BaseCounter baseCounter;
    [SerializeField] GameObject[] visualGameObjectArray;

    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == baseCounter) {
            foreach (GameObject gameObject in visualGameObjectArray) {
                gameObject.SetActive(true);
            }
        }
        else {
            foreach (GameObject gameObject in visualGameObjectArray) {
                gameObject.SetActive(false);
            }
        }
    }
}
