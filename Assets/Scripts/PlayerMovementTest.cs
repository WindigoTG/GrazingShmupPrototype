using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    Animator[] _animators;

    float _vertical = 0f;
    float _horizontal = 0f;
    Vector3 _movement = Vector3.zero;
    ParticleSystem[] _particles;
    [SerializeField] float _speed = 5f;

    float[] _particleSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _animators = GetComponentsInChildren<Animator>();
        _particles = GetComponentsInChildren<ParticleSystem>();
        _particleSpeed = new float[_particles.Length];

        for (int i = 0; i < _particles.Length; i++ )
        {
            var main = _particles[i].main;

            _particleSpeed[i] = main.startSpeedMultiplier;
            main.startSpeedMultiplier = _particleSpeed[i] / 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        _movement = new Vector3(_horizontal, _vertical, 0) * _speed;
    }

    private void FixedUpdate()
    {

        transform.Translate(_movement * Time.fixedDeltaTime);

        foreach (Animator animator in _animators)
        {
            if (_horizontal > 0)
            {
                animator.SetBool("IsMovingRight", true);
                animator.SetBool("IsMovingLeft", false);
            }
            else if (_horizontal < 0)
            {
                animator.SetBool("IsMovingRight", false);
                animator.SetBool("IsMovingLeft", true);
            }
            else
            {

                animator.SetBool("IsMovingRight", false);
                animator.SetBool("IsMovingLeft", false);
            }
        }

        for (int i = 0; i < _particles.Length; i++)
        {
            var main = _particles[i].main;

            if (_vertical > 0)
                main.startSpeedMultiplier = _particleSpeed[i];
            else if (_vertical < 0)
                main.startSpeedMultiplier = _particleSpeed[i] / 4;
            else
                main.startSpeedMultiplier = _particleSpeed[i] / 2;
        }
    }
}
