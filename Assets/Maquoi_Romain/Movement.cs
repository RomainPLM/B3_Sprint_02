using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTester : MonoBehaviour
{
    public Vector2 _playerPosition;
    public Vector3 _movement;
    public float _movementSpeed;

    private void Update()
    {
        this.transform.position=transform.position+(_movement * _movementSpeed * Time.deltaTime);
    }
    private void OnMove(InputValue value)
    {
        Vector2 moveInputValue = value.Get<Vector2>();
        Debug.Log("LeftStick " + moveInputValue);
        _playerPosition.x = transform.position.x + moveInputValue.x;
        _playerPosition.y = transform.position.z + moveInputValue.y;
        _movement = new Vector3(moveInputValue.x, 0.0f, moveInputValue.y);
      this.transform.LookAt(transform.position + (-_movement) + transform.forward);
    }
    private void OnSelection(InputValue value)
    {
        Vector2 selectionInputValue = value.Get<Vector2>();
        Debug.Log("RightStick " + selectionInputValue);     
    }
    private void OnDodge()
    {
        Debug.Log("Dodge");
    }
    private void OnInteract()
    {
        Debug.Log("Interact");
    }
    private void OnMeleeAttack()
    {
        Debug.Log("MeleeAttack");
    }
    private void OnDistanceAttack()
    {
        Debug.Log("DistanceAttack");
    }
    private void OnInvocation()
    {
        Debug.Log("Invocation");
    }
    private void OnHeal()
    {
        Debug.Log("Heal");
    }
    private void OnStart()
    {
        Debug.Log("Sart");
    }
}
