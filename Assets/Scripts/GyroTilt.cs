using UnityEngine;
using UnityEngine.InputSystem;

public class GyroTilt : MonoBehaviour
{
    private const float GravityForce = 9.81f;

    [SerializeField] private PlayerInputReader _inputReader;
    [SerializeField] private float _sensitivity = 2.5f;
    [SerializeField] private float _deadZone = 0.05f;
    [SerializeField] private bool _invertX;
    [SerializeField] private bool _invertY;

    private bool _isActive;
    private Vector2 _calibrationOffset;

    public void SetActive(bool value)
    {
        GravitySensor sensor = GravitySensor.current;

        if (sensor == null)
        {
            _isActive = false;
            _inputReader.TiltDirection = Vector2.zero;
            return;
        }

        _isActive = value;

        if (value)
        {
            InputSystem.EnableDevice(sensor);

            Vector3 gravity = sensor.gravity.ReadValue();
            _calibrationOffset = new Vector2(gravity.x, gravity.y);
        }
        else
        {
            InputSystem.DisableDevice(sensor);
            _inputReader.TiltDirection = Vector2.zero;
        }
    }

    private void Update()
    {
        if (!_isActive)
        {
            return;
        }

        GravitySensor sensor = GravitySensor.current;

        if (sensor == null)
        {
            return;
        }

        Vector3 gravity = sensor.gravity.ReadValue();
        Vector2 tilt = (new Vector2(gravity.x, gravity.y) - _calibrationOffset) / GravityForce * _sensitivity;

        if (_invertX)
        {
            tilt.x = -tilt.x;
        }

        if (_invertY)
        {
            tilt.y = -tilt.y;
        }

        if (tilt.magnitude < _deadZone)
        {
            tilt = Vector2.zero;
        }

        _inputReader.TiltDirection = Vector2.ClampMagnitude(tilt, 1f);
    }
}