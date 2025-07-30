using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _volumeChangeSpeed = .5f;

    private HouseEvents _houseEvents;
    private float _targetVolume = 0f;

    private void Awake()
    {
        _houseEvents = GetComponent<HouseEvents>();
        _alarmSound = GetComponentInChildren<AudioSource>();

        _houseEvents.CrookEntered += ActivateAlarm;
        _houseEvents.CrookExited += DeactivateAlarm;
    }

    private void Update()
    {
        _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, _targetVolume, _volumeChangeSpeed * Time.deltaTime);
    }

    private void ActivateAlarm()
    {
        _targetVolume = 1f;

        if (_alarmSound.isPlaying == false)
            _alarmSound.Play();
    }

    private void DeactivateAlarm()
    {
        _targetVolume = 0f;
    }

    private void OnDestroy()
    {
        _houseEvents.CrookEntered -= ActivateAlarm;
        _houseEvents.CrookExited -= DeactivateAlarm;
    }
}
