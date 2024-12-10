using UnityEngine;
using UnityEngine.InputSystem;

public class GameGestion : MonoBehaviour
{
    private int _numPlayers;
    private PlayerScript _playerScript1;
    private PlayerScript _playerScript2;
    private bool _playerHasJoin;
    //
    public int _mancheForWin = 5;
    public int _player1WinAmmount;
    public int _player2WinAmmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame  
    void Update()
    {
        if (_playerHasJoin == true)
        {
            if (_playerScript1._isDead == true)
            {
                _player2WinAmmount++;
            }
            else if (_playerScript2._isDead == true)
            {
                _player1WinAmmount++;     
            }
        }
        if (_player1WinAmmount == _mancheForWin)
        {
            print("Player 1 win");
        }
        else if (_player2WinAmmount == _mancheForWin)
        {
            print("Player 2 win");
        }
    }
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

