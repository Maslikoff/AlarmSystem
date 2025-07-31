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
        float startVolume = _alarmSound.volume;
        float elapsedTime = 0f;

        while (elapsedTime < _volumeChangeSpeed)
        {
            _alarmSound.volume = Mathf.MoveTowards(startVolume, targetVolume, elapsedTime/_volumeChangeSpeed); 
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _alarmSound.volume = targetVolume;

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