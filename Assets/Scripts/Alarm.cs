using System.Collections;
using UnityEngine;

[RequireComponent (typeof(HouseEvents))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _volumeChangeSpeed = .5f;

    private HouseEvents _houseEvents;
    private Coroutine _volumeCoroutine;

    private void Awake()
    {
        _houseEvents = GetComponent<HouseEvents>();

        _alarmSound.volume = 0f;

        _houseEvents.CrookEntered += OnCrookEntered;
        _houseEvents.CrookExited += OnCrookExited;
    }

    private void OnCrookEntered()
    {
        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);

        _volumeCoroutine = StartCoroutine(FadeVolume(1));

        if (_alarmSound.isPlaying == false)
            _alarmSound.Play();
    }

    private void OnCrookExited()
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
        _houseEvents.CrookEntered -= OnCrookEntered;
        _houseEvents.CrookExited -= OnCrookExited;

        if (_volumeCoroutine != null)
            StopCoroutine(_volumeCoroutine);
    }
}