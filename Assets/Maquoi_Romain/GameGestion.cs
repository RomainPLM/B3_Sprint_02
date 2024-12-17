using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameGestion : MonoBehaviour
{
    private int _numPlayers;
    private PlayerScript _playerScript1;
    private PlayerScript _playerScript2;
    private PlayerJoin _playerJoinScript;
    private bool _playerHasJoin;
    //
    public int _mancheForWin = 5;
    public int _player1WinAmmount;
    public int _player2WinAmmount;

    public bool _mancheEnd;


    public List<BlockDisplacement> _blocList = new();

    public List<GameObject> _blocTypes = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerJoinScript = GetComponent<PlayerJoin>();
    }
    // if je re meurt je ne me stop pas (refaire fin de manche)(bool a reset?)
    // Update is called once per frame  
    void Update()
    {


        if (_playerHasJoin == true)
        {
            foreach (var x in _blocList)
            {
                if (x == null)
                {
                    _blocList.Remove(x);
                }
            }
            if (_blocList.Count == 0)
            {
                _mancheEnd = false;
                _playerScript1._didInstance = false;
                _playerScript2._didInstance = false;
                _playerScript1._placeBloc = false;
                _playerScript2._placeBloc = false;
            }
            //Death detection
            if (_playerScript1._isDead == true)
            {
                _mancheEnd = true;
                _player2WinAmmount++;
                //Revive
                _playerScript1._isDead = false;
            }
            else if (_playerScript2._isDead == true)
            {
                _mancheEnd = true;
                _player1WinAmmount++;
                _playerScript2._isDead = false;
            }
            if (_player1WinAmmount == _mancheForWin)
            {
                print("Player 1 win");
                _playerScript1._playerInput.actions.Disable();
                _playerScript2._playerInput.actions.Disable();

            }
            else if (_player2WinAmmount == _mancheForWin)
            {
                print("Player 2 win");
                _playerScript1._playerInput.actions.Disable();
                _playerScript2._playerInput.actions.Disable();
            }

            if (_mancheEnd == false)
            {
                _playerScript1._playerInput.actions.Enable();
                _playerScript2._playerInput.actions.Enable();
            }
            else
            {

                //Action on player
                _playerScript1._playerInput.actions.Disable();
                _playerScript2._playerInput.actions.Disable();
                //respawnPose
                _playerScript1.gameObject.transform.position = _playerJoinScript._spawnPoint[_playerScript1._playerNumber].position;
                _playerScript2.gameObject.transform.position = _playerJoinScript._spawnPoint[_playerScript2._playerNumber].position;
                //respawnRota
                _playerScript1.gameObject.transform.rotation = _playerJoinScript._spawnPoint[_playerScript1._playerNumber].rotation;
                _playerScript2.gameObject.transform.rotation = _playerJoinScript._spawnPoint[_playerScript2._playerNumber].rotation;

                //Action For bloc
                // _playerScript1._didInstance=false;
                //   _playerScript2._didInstance = false;
                _playerScript1._playerInput.actions.FindAction("Look").Enable();
                _playerScript2._playerInput.actions.FindAction("Look").Enable();

                _playerScript1._playerInput.actions.FindAction("Interact").Enable();
                _playerScript2._playerInput.actions.FindAction("Interact").Enable();

                _playerScript1._playerInput.actions.FindAction("Rotate").Enable();
                _playerScript2._playerInput.actions.FindAction("Rotate").Enable();

            }
        }

    }
    void OnPlayerJoined(PlayerInput playerInput)
    {
        if (_numPlayers == 0)
        {

            _playerScript1 = playerInput.gameObject.GetComponent<PlayerScript>();
        }
        else if (_numPlayers == 1)
        {
            _playerScript2 = playerInput.gameObject.GetComponent<PlayerScript>();

        }
        _numPlayers++;
        if (_numPlayers == 2)
        {
            _playerHasJoin = true;
        }

    }
}

