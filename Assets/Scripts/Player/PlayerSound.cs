using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Player player;

    private void Awake() {
        player = GetComponent<Player>();
    }

    private void Update() {
        if (player.GetIsWalking()) {
            SoundManager.Instance.PlayFootstepSound(player.transform.position);
        }
    }
}
