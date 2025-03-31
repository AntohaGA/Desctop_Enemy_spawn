using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 3.5f;
    [SerializeField] private float _speed = 10f;

    private bool _isGo = false;

    private Vector3 _directionToGo;

    private Rigidbody _rigidBody;

    private IEnumerator _lifeTimerCoroutine;

    private WaitForSeconds _timer;

    public event Action<Enemy> Killed;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _lifeTimerCoroutine = LifeTime();
        _timer = new WaitForSeconds(_lifeTime);
        StartCoroutine(_lifeTimerCoroutine);
    }

    private void OnDisable()
    {
        StopCoroutine(_lifeTimerCoroutine);
        _isGo = false;
    }

    private void FixedUpdate()
    {
        if (_isGo)
        {
          _rigidBody.MovePosition(_rigidBody.position + _directionToGo * _speed * Time.deltaTime);
        }
    }

    public void GoToDirection(Vector3 direction)
    {
        _isGo = true;
        _directionToGo = direction.normalized;
    }

    private IEnumerator LifeTime()
    {
        yield return _timer;

        Killed?.Invoke(this);
    }
}