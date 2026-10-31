using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputReader _inputReader;
    [SerializeField] private float _maxSpeed = 6f;
    [SerializeField] private float _acceleration = 30f;

    private IGameInput _input;
    private Rigidbody2D _rigidbody;
    private float _speedMultiplier = 1f;
    private Coroutine _boostRoutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _input = _inputReader;
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = _input.Move * _maxSpeed * _speedMultiplier;

        _rigidbody.linearVelocity = Vector2.MoveTowards(
            _rigidbody.linearVelocity,
            targetVelocity,
            _acceleration * Time.fixedDeltaTime);
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        if (_boostRoutine != null)
        {
            StopCoroutine(_boostRoutine);
        }

        _boostRoutine = StartCoroutine(BoostRoutine(multiplier, duration));
    }

    private IEnumerator BoostRoutine(float multiplier, float duration)
    {
        _speedMultiplier = multiplier;

        yield return new WaitForSeconds(duration);

        _speedMultiplier = 1f;
        _boostRoutine = null;
    }
}