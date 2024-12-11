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
    public Vector2 _blocDisplacementDirection;


    public BlockDisplacement _blockDisplacement;
    public GameGestion _gameGestion;
    public bool _didInstance=false;
    public GameObject _placableBloc;
    private void Start()
    {
        _didInstance=false;
        _gameGestion =Object.FindAnyObjectByType<GameGestion>();
        _playerInput = GetComponent<PlayerInput>();
        // instancier bloc recuperer son scirpt et lui attribué nombre du joueur!
    }
    private void Update()
    {
        //move
        this.transform.position = transform.position + (_movement * _movementSpeed * Time.deltaTime);
        //life
        //if (_isDead == true)
        //{

        //}
        if (_gameGestion._mancheEnd==true && _didInstance==false)
        {
           Instantiate(_placableBloc);
            _didInstance=true;
            return;
        }
    }
    private void OnMove(InputValue value)
    {
        //if (_blockDisplacement._blocIsPosed == true)
        //{
            Vector2 moveInputValue = value.Get<Vector2>();
            _playerPosition.x = transform.position.x + moveInputValue.x;
            _playerPosition.y = transform.position.z + moveInputValue.y;
            _movement = new Vector3(moveInputValue.x, 0.0f, moveInputValue.y);//dupliquer dans le joystick de droite
            this.transform.LookAt(transform.position + (-_movement) + transform.forward);// transferer dans le joystick de droite 
        //}
        
    }
    private void OnLook(InputValue value)
    {
        Vector2 selectionInputValue = value.Get<Vector2>();
        //print(selectionInputValue);
        _blocDisplacementDirection = selectionInputValue;
    }
}
