using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoin : MonoBehaviour
{
    public Transform[] _spawnPoint;
    private int _numPlayers;
    [SerializeField] InputAction _joinAction;
    private PlayerInputManager _playerInputManager;
    // private bool _connected = false;
    public string _controllers;
    public Gamepad[] _gamepads;
    public GameObject[] _player;
   
    public List<PlayerInput> _pInputs = new List<PlayerInput>();
    public PlayerInput _p2;
    public PlayerInput _p1;
    private void Start()
    {
        _playerInputManager = GetComponent<PlayerInputManager>();
        _joinAction.Enable();
        _numPlayers = 0;
        _gamepads = Gamepad.all.ToArray();

        print(_gamepads.Length);
        print(_gamepads[0] + "connected first");
        print(_gamepads[1] + "second connected");


       // InputDevice device = gamepads[playerIndex];

    }
    private void Update()
    {
        _gamepads = Gamepad.all.ToArray();
        print(_gamepads.Length);
        print(_gamepads[0] + "connected first");
        print(_gamepads[1] + "second connected");
        if (_numPlayers == 0)
        {
            _p1 = PlayerInput.Instantiate(_player[0], _numPlayers, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[0]);
            _p1.gameObject.transform.position = _spawnPoint[_numPlayers].position;
            _p1.gameObject.transform.rotation = _spawnPoint[_numPlayers].rotation;
            //_p1.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            _p1.gameObject.GetComponent<PlayerScript>()._playerNumber = _numPlayers;
            _numPlayers++;

        }
        else if (_numPlayers == 1)
        {
            _p2 = PlayerInput.Instantiate(_player[1], _numPlayers, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[1]);
            _p2.gameObject.transform.position = _spawnPoint[_numPlayers].position;
            _p2.gameObject.transform.rotation = _spawnPoint[_numPlayers].rotation;
            //_p2.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            _p2.gameObject.GetComponent<PlayerScript>()._playerNumber = _numPlayers;
            _numPlayers++;
        }


    }

    void Awake()
    {
        _gamepads = Gamepad.all.ToArray();
        _controllers = Input.GetJoystickNames()[0];

        //for (int i = 0; i < _player.Length; i++)
        //{
        //    _pInputs.Add(PlayerInput.Instantiate(_player[i], _numPlayers, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[i]));
        //    _pInputs[i].gameObject.transform.position = _spawnPoint[i].position;
        //    _pInputs[i].gameObject.transform.rotation = _spawnPoint[i].rotation;
        //    _pInputs[i].gameObject.GetComponent<PlayerScript>()._playerNumber = i;
        //    print($"Connected{i}");
        //}
    }

}
