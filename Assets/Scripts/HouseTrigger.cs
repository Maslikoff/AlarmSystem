using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Crook>(out _))
            _alarm?.ActivateAlarm();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Crook>(out _))
            _alarm?.DeactivateAlarm();
    }
}