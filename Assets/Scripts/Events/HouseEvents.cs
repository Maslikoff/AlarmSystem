using System;
using UnityEngine;

public class HouseEvents : MonoBehaviour
{
    public event Action CrookEntered;
    public event Action CrookExited;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Crook>(out _))
        {
            Debug.Log("����� ����� � ���!");
            CrookEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Crook>(out _))
        {
            Debug.Log("����� ����� �� ����!");
            CrookExited?.Invoke();
        }
    }
}
