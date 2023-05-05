using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private const string SOUND_EFFECT_VOLUME = "SoundEffectVolume";
    private float volume;

    private void Awake() {
        Instance = this;
        volume = PlayerPrefs.GetFloat(SOUND_EFFECT_VOLUME, 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += Instance_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFail += Instance_OnRecipeFail;
        CuttingCounter.OnAnyCutAction += CuttingCounter_OnAnyCutAction;
        Player.Instance.OnPickupSomething += Player_OnPickupSomething;
        BaseCounter.OnAnyObjectDrop += BaseCounter_OnObjectDrop;
        TrashCounter.OnAnyObjectTrash += TrashCounter_OnAnyObjectTrash;
    }

    private void TrashCounter_OnAnyObjectTrash(object sender, System.EventArgs e) {
        TrashCounter trashCounter = (TrashCounter) sender;
        PlaySound(audioClipRefSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnObjectDrop(object sender, System.EventArgs e) {
        BaseCounter baseCounter = (BaseCounter) sender;
        PlaySound(audioClipRefSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickupSomething(object sender, System.EventArgs e) {
        PlaySound(audioClipRefSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCutAction(object sender, System.EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefSO.chop, cuttingCounter.transform.position);
    }

    private void Instance_OnRecipeFail(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void Instance_OnRecipeSuccess(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootstepSound(Vector3 position) {
        PlaySound(audioClipRefSO.footstep, position);
    }

    public void PlayCountdownSound() {
        PlaySound(audioClipRefSO.warning, Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position) {
        PlaySound(audioClipRefSO.warning, position);
    }

    public void ChangeVolume() {
        volume += 0.1f;

        if (volume > 1f) {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(SOUND_EFFECT_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}
