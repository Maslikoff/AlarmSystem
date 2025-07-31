using System.Collections;
using UnityEngine;

[RequireComponent (typeof(HouseTrigger))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _volumeChangeSpeed = .5f;

    private Coroutine _volumeCoroutine;

    private void Awake()
    {
        _alarmSound.volume = 0f;
    }

    public void ActivateAlarm()
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);

        _volumeCoroutine = StartCoroutine(FadeVolume(1));

        if (_alarmSound.isPlaying == false)
            _alarmSound.Play();
    }

    public void DeactivateAlarm()
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);

        _volumeCoroutine = StartCoroutine(FadeVolume(0));
    }

    private IEnumerator FadeVolume(float targetVolume)
    {
        while (Mathf.Approximately(_alarmSound.volume, targetVolume) == false)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, targetVolume, _volumeChangeSpeed * Time.deltaTime);

            yield return null;
        }

        if (Mathf.Approximately(targetVolume, 0f))
            _alarmSound.Stop();
    }

    private void OnDestroy()
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);
    }
}