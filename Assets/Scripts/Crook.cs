using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crook : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _waitTime = 2f;

    private int _currentPointIndex = 0;
    private bool _isWaiting = false;

    private void Start()
    {
        if (_points.Length > 0)
            transform.position = _points[0].position;
    }

    private void Update()
    {
        if (_points.Length == 0 || _isWaiting)
            return;

        Transform target = _points[_currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if ((transform.position - _points[_currentPointIndex].position).sqrMagnitude <= 0.25f)
            StartCoroutine(WaitAtWaypoint());
    }

    private IEnumerator WaitAtWaypoint()
    {
        _isWaiting = true;
        yield return new WaitForSeconds(_waitTime);

        _currentPointIndex = (_currentPointIndex + 1) % _points.Length;
        _isWaiting = false;
    }
}
