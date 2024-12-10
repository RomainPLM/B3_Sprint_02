using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoin : MonoBehaviour
{
    public Transform[] _menuSpawnPoints;
    private int _numPlayers;
    [SerializeField] InputAction _joinAction;
    private void Start()
    {
        _joinAction.Enable();
        _numPlayers = 0;
    }
    //automatically called when player joins the game session
    void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Spawn Position: " + _menuSpawnPoints[_numPlayers].position);
        playerInput.gameObject.transform.position = _menuSpawnPoints[_numPlayers].position;
        if (_numPlayers == 0)
        {
            playerInput.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            playerInput.gameObject.GetComponent<PlayerScript>()._playerNumber = _numPlayers;
            //donner script a gameobject + dans ce script faire event qui detecte la mort pour donner point
            // mettre les gamesobject dans une liste
        }
        else if (_numPlayers == 1)
        {
            playerInput.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            playerInput.gameObject.GetComponent<PlayerScript>()._playerNumber = _numPlayers;
        }
        

        _numPlayers++;


    }
}
