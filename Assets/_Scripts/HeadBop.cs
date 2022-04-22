using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBop : MonoBehaviour
{
    private bool _enable = true;

    [Header("Customization")]
    [SerializeField, Range(0, 0.1f)] private float _amplitude = 0.01f;
    [SerializeField, Range(0, 30)] private float _walkFrequency = 20.0f;
    [SerializeField, Range(0, 30)] private float _runFrequency = 30.0f;

    [Header("References")]
    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;

    [Header("Player")]
    Rigidbody _rb;
    [SerializeField] private float playerHeight = 2.0f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _startPos = _camera.localPosition;
    }

    void Update()
    {
        if (!_enable) return;

        ResetPosition();

        if (_rb.velocity.magnitude > 6.1f) CheckMotion(_runFrequency);
        else CheckMotion(_walkFrequency);

        _camera.LookAt(FocusTarget());
    }

    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }

    private void CheckMotion(float _frequency)
    {
        float speed = new Vector3(_rb.velocity.x, 0, _rb.velocity.z).magnitude;
        
        if (speed < _toggleSpeed) return;
        if (!Physics.Raycast(transform.position + Vector3.up * 0.01f, Vector3.down, playerHeight * 0.5f)) return;
        
        PlayMotion(FootStepMotion(_frequency));
    }

    private Vector3 FootStepMotion(float _frequency)
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _amplitude;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _amplitude * 2;
        return pos;
    }

    private void ResetPosition()
    {
        if (_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
}
