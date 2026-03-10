using UnityEngine;
using UnityEngine.InputSystem;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _force = 5;
    [SerializeField] private float _turnSpeed = 360;
    private Vector3 _input;
    private Vector2 inputRaw;
    private float direction;

    private void Update()
    {
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {

        inputRaw = new Vector2(
               (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0),
               (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0));

        _input = new Vector3(inputRaw.x, 0f, inputRaw.y);
    }

    private void Look()
    {
        direction = Vector3.Dot(_rb.linearVelocity, transform.forward);

        if (_input == Vector3.zero || _rb.linearVelocity.magnitude < 0.1f) return;

        transform.Rotate(Vector3.up * _input.x * (direction > 0 ? 1 : -1) * _turnSpeed * Mathf.Sqrt(_rb.linearVelocity.magnitude) /5 * Time.deltaTime);
    }

    private void Move()
    {
        _rb.AddForce(transform.forward * _input.z * _force);
    }
}