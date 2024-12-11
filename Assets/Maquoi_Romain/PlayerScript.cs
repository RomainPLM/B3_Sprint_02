using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Vector2 _playerPosition;
    public Vector3 _movement;
    public float _movementSpeed;
    public int _playerNumber;
    public bool _isDead;
    public PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        //move
        this.transform.position = transform.position + (_movement * _movementSpeed * Time.deltaTime);
        //life
        if (_isDead == true)
        {
         
        }
    }
    private void OnMove(InputValue value)
    {
        Vector2 moveInputValue = value.Get<Vector2>();
        _playerPosition.x = transform.position.x + moveInputValue.x;
        _playerPosition.y = transform.position.z + moveInputValue.y;
        _movement = new Vector3(moveInputValue.x, 0.0f, moveInputValue.y);//dupliquer dans le joystick de droite
        this.transform.LookAt(transform.position + (-_movement) + transform.forward);// transferer dans le joystick de droite 
    }
}
