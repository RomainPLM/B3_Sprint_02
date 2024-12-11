using System.Collections;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerJoinScript=GetComponent<PlayerJoin>();
    }

    // Update is called once per frame  
    void Update()
    {
        if (_playerHasJoin == true)
        {
            //Death detection
            if (_playerScript1._isDead == true)
            {
                _player2WinAmmount++;
                _playerScript1._playerInput.enabled = false;
                _playerScript2._playerInput.enabled = false;
                //respawnPose
                _playerScript1.gameObject.transform.position= _playerJoinScript._spawnPoint[_playerScript1._playerNumber].position;
                _playerScript2.gameObject.transform.position = _playerJoinScript._spawnPoint[_playerScript2._playerNumber].position;
                //respawnRota
                _playerScript1.gameObject.transform.rotation = _playerJoinScript._spawnPoint[_playerScript1._playerNumber].rotation;
                _playerScript2.gameObject.transform.rotation = _playerJoinScript._spawnPoint[_playerScript2._playerNumber].rotation;
                //Revive
                _playerScript1._isDead=false;
                //StartCoroutine(PlayBack());
               
            }
            else if (_playerScript2._isDead == true)
            {
                _player1WinAmmount++;
                _playerScript1._playerInput.enabled = false;
                _playerScript2._playerInput.enabled = false;
                _playerScript1.gameObject.transform.position = _playerJoinScript._spawnPoint[_playerScript1._playerNumber].position;
                _playerScript2.gameObject.transform.position = _playerJoinScript._spawnPoint[_playerScript2._playerNumber].position;
                _playerScript1.gameObject.transform.rotation = _playerJoinScript._spawnPoint[_playerScript1._playerNumber].rotation;
                _playerScript2.gameObject.transform.rotation = _playerJoinScript._spawnPoint[_playerScript2._playerNumber].rotation;
                _playerScript2._isDead = false;
                //StartCoroutine(PlayBack());

            }
        }
        if (_player1WinAmmount == _mancheForWin)
        {
            print("Player 1 win");
            _playerScript1._playerInput.enabled = false;
            _playerScript2._playerInput.enabled = false;
        }
        else if (_player2WinAmmount == _mancheForWin)
        {
            print("Player 2 win");
            _playerScript1._playerInput.enabled = false;
            _playerScript2._playerInput.enabled = false;
        }

    }
    //IEnumerator PlayBack()
    //{
    //    yield return new WaitForSeconds(2.5f);
    //    _playerScript1._playerInput.enabled = true;
    //    _playerScript2._playerInput.enabled = true;

    //}
    void OnPlayerJoined(PlayerInput playerInput)
    {
        if (_numPlayers == 0)
        {

            _playerScript1=playerInput.gameObject.GetComponent<PlayerScript>();
        }
        else if (_numPlayers == 1)
        {
            _playerScript2 = playerInput.gameObject.GetComponent<PlayerScript>();
           
        }
        _numPlayers++;
        if(_numPlayers == 2)
        {
            _playerHasJoin = true;
        }

}
}

