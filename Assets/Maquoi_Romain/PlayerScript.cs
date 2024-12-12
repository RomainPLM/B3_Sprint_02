using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private Vector2 _playerPosition;
    private float _blocDisplacementDirectionX;
    public Vector3 _movement;
    public float _movementSpeed;
    public int _playerNumber;
    public bool _isDead;
    public PlayerInput _playerInput;

    public Vector2 _blocDisplacementDirection;


    public BlockDisplacement _blockDisplacement;
    public GameGestion _gameGestion;
    public bool _didInstance = false;
    public GameObject _placableBloc;

    public bool _placeBloc;
    private void Start()
    {
        _placeBloc=false;
        _gameGestion = Object.FindAnyObjectByType<GameGestion>();
        _playerInput = GetComponent<PlayerInput>();
        // instancier bloc recuperer son scirpt et lui attribué nombre du joueur!
    }
    private void Update()
    {
        
        print(_playerInput.actions.FindAction("look").ReadValue<Vector2>() + "value in read");
        //move
        this.transform.position = transform.position + (_movement * _movementSpeed * Time.deltaTime);
        //life
        //if (_isDead == true)
        //{

        //}
        if (_gameGestion._mancheEnd == true && _didInstance == false)
        {
            _didInstance = true;
            GameObject bloc = Instantiate(_placableBloc, new Vector3(transform.position.x, 00, transform.position.z), transform.rotation);
            _gameGestion._bloc.Add(bloc.GetComponent<BlockDisplacement>());
            bloc.GetComponent<BlockDisplacement>()._playerScript = this;


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
        //_blockDisplacement.BlockDisplacementDirection = selectionInputValue;
        _blocDisplacementDirection = selectionInputValue;
        print(_blocDisplacementDirection + "value in player");
       
    }
    private void OnInteract()
    {
        print("i clic on Y"+_playerNumber);
        _placeBloc = true;


    }
}
