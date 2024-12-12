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


    public List<BlockDisplacement> _bloc = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerJoinScript = GetComponent<PlayerJoin>();
    }

    // Update is called once per frame  
    void Update()
    {
        foreach (var x in _bloc)
        {
          if(x == null)
            {
               _bloc.Remove(x);
            }
        }
        if (_bloc.Count == 0)
        {
            print("list is empty");
        }
        // cree list avec bloc instancier, supprimer les blocs une fois poser et reactiver l input systeme , remettre le boolean de fin de manche en faux et revierifier le deplacement des joueurs.
        if (_playerHasJoin == true)
        {         
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

