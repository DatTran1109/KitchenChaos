using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterAnimator : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;

    private Animator animator;
    private const string OPEN_CLOSE = "OpenClose";

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerOpenContainer += ContainerCounter_OnPlayerOpenContainer;
    }

    private void ContainerCounter_OnPlayerOpenContainer(object sender, System.EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
