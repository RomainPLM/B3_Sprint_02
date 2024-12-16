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
    public GameObject _playerPrefab;
    public PlayerInput _p1;
    public PlayerInput _p2;
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
       
        


      

        if (_numPlayers == 0)
        {
            _p1 = PlayerInput.Instantiate(_playerPrefab, _numPlayers, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[0]);
            _p1.gameObject.transform.position = _spawnPoint[_numPlayers].position;
            _p1.gameObject.transform.rotation = _spawnPoint[_numPlayers].rotation;
            _p1.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            _p1.gameObject.GetComponent<PlayerScript>()._playerNumber = _numPlayers;
            _numPlayers++;

        }
        else if (_numPlayers == 1)
        {
            _p2 = PlayerInput.Instantiate(_playerPrefab, _numPlayers, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[1]);
            _p2.gameObject.transform.position = _spawnPoint[_numPlayers].position;
            _p2.gameObject.transform.rotation = _spawnPoint[_numPlayers].rotation;
            _p2.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            _p2.gameObject.GetComponent<PlayerScript>()._playerNumber = _numPlayers;
            _numPlayers++;
        }
    }

    void Awake()
    {
        _controllers = Input.GetJoystickNames()[0];
     
    }

    //automatically called when player joins the game session
    //void OnPlayerJoined(PlayerInput playerInput)
    //{

    //    Debug.Log("Spawn Position: " + _spawnPoint[_numPlayers].position);

    //    playerInput.gameObject.transform.position = _spawnPoint[_numPlayers].position;
    //    playerInput.gameObject.transform.rotation = _spawnPoint[_numPlayers].rotation;

    //    if (_numPlayers == 0)
    //    {
    //        var p1 = PlayerInput.Instantiate(_playerPrefab, _numPlayers, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[0]);
    //        playerInput.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
    //        playerInput.gameObject.GetComponent<PlayerScript>()._playerNumber = _numPlayers;

    //    }
    //    else if (_numPlayers == 1)
    //    {
    //        var p2 = PlayerInput.Instantiate(_playerPrefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[0]);
    //        playerInput.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    //        playerInput.gameObject.GetComponent<PlayerScript>()._playerNumber = _numPlayers;
    //    }


    //    _numPlayers++;

    //    if (_numPlayers >= 2)
    //    {
    //        _playerInputManager.enabled = false;
    //    }

    //}
}
