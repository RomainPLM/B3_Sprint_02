using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
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

    public Vector2 _blocDisplacementDirection1;
    public Vector2 _blocDisplacementDirection2;


    public BlockDisplacement _blockDisplacement;
    public GameGestion _gameGestion;
    public bool _didInstance = false;
    public GameObject _placableBloc;

    public bool _placeBloc;


    //public double timestamp;

    private void Start()
    {
        _placeBloc=false;
        _gameGestion = Object.FindAnyObjectByType<GameGestion>();
        _playerInput = GetComponent<PlayerInput>();
        // instancier bloc recuperer son scirpt et lui attribué nombre du joueur!
    }
    private void Update()
    {
       

        if (Input.GetAxisRaw("VerticalJoy1") > 0.15 || Input.GetAxisRaw("VerticalJoy1") < -0.15)
        {
            _blocDisplacementDirection1.x = -Input.GetAxisRaw("VerticalJoy1");
    
        }
        else
        {
            _blocDisplacementDirection1.x = 0;
        }
        if (Input.GetAxisRaw("HorizontalJoy1") > 0.15 ||  Input.GetAxisRaw("HorizontalJoy1") < -0.15)
        {
            _blocDisplacementDirection1.y = -Input.GetAxisRaw("HorizontalJoy1");
        }
        else
        {
            _blocDisplacementDirection1.y = 0;
        }
        if (Input.GetAxisRaw("VerticalJoy2")  > 0.15 || Input.GetAxisRaw("VerticalJoy2") < -0.15)
        {
            _blocDisplacementDirection2.x = -Input.GetAxisRaw("VerticalJoy2");

        }
        else
        {
            _blocDisplacementDirection2.x = 0;
        }
        if (Input.GetAxisRaw("HorizontalJoy2") > 0.15 || Input.GetAxisRaw("HorizontalJoy2") < -0.15)
        {
            _blocDisplacementDirection2.y = -Input.GetAxisRaw("HorizontalJoy2");
            
        }
        else
        {
            _blocDisplacementDirection2.y = 0;
        }

  
        this.transform.position = transform.position + (_movement * _movementSpeed * Time.deltaTime);

        if (_gameGestion._mancheEnd == true && _didInstance == false)
        {
           
            _didInstance = true;
            var randomInList =  Random.Range(0, _gameGestion._blocTypes.Count );
            GameObject bloc = Instantiate(_gameGestion._blocTypes[/*randomInList*/0], new Vector3(transform.position.x, 00, transform.position.z),Quaternion.identity);
           // GameObject bloc = Instantiate(_placableBloc, new Vector3(transform.position.x, 00, transform.position.z), transform.rotation);
            _blockDisplacement = bloc.GetComponent<BlockDisplacement>();
            _gameGestion._blocList.Add(_blockDisplacement);
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
    private void OnInteract()
    {
        print("i clic on Y"+_playerNumber);
       _placeBloc = true;


    }

}
