using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
public class BlockDisplacement : MonoBehaviour
{


    public float _blocDisplacementSize;
    public float _blocDisplacementTimeBetween = 0.5f;
    private bool _blocDisplacementEnabled;
    private float _timer;
    private Renderer _renderer;
    private bool _placable;
    public PlayerScript _playerScript;
    public bool _blocIsPosed;
    public Vector2 _blocDisplacementDirection;
    private double _timestamp;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = Color.green;
        _blocDisplacementEnabled = true;

    }

    private void Update()
    {
        print("placable" + _playerScript._playerNumber);


        //if (Time.timeAsDouble == _playerScript.timestamp)
        //    return;

        if (_placable == true && _playerScript._placeBloc == true)
        {
            if (_playerScript._placeBloc == true)
            {
                
                _blocIsPosed = true;
                _renderer.material.color = Color.gray;
                Destroy(this);
            }
        }
        if (_blocDisplacementEnabled == true)
        {
            if (_playerScript._playerNumber == 0)
            {
                _blocDisplacementDirection = _playerScript._blocDisplacementDirection1;
            }
            else if (_playerScript._playerNumber == 1)
            {
                _blocDisplacementDirection = _playerScript._blocDisplacementDirection2;
            }

            if (_blocDisplacementDirection.x < -0.25f)
            {
                this.gameObject.transform.position = new Vector3((this.gameObject.transform.position.x - _blocDisplacementSize), this.transform.position.y, this.transform.position.z);
                print("is moving actually");
                _blocDisplacementEnabled = false;
                _timer = 0;
            }
            else if (_blocDisplacementDirection.x > 0.25f)
            {
                this.gameObject.transform.position = new Vector3((this.gameObject.transform.position.x + _blocDisplacementSize), this.transform.position.y, this.transform.position.z);
                print("is moving actually");
                _blocDisplacementEnabled = false;
                _timer = 0;
            }
            else if (_blocDisplacementDirection.y < -0.25f)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.transform.position.y, (this.transform.position.z - _blocDisplacementSize));
                print("is moving actually");
                _blocDisplacementEnabled = false;
                _timer = 0;
            }
            else if (_blocDisplacementDirection.y > 0.25f)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.transform.position.y, (this.transform.position.z + _blocDisplacementSize));
                print("is moving actually");
                _blocDisplacementEnabled = false;
                _timer = 0;
            }
        }
        else if (_blocDisplacementEnabled == false)
        {
            _timer = _timer + 1.0f * Time.deltaTime;
        }
        if (_timer >= _blocDisplacementTimeBetween)
        {
            _blocDisplacementEnabled = true;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 6) { _renderer.material.color = Color.red;/* print(other.gameObject);*/ _placable = false; _playerScript._placeBloc = false; }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 6) { _renderer.material.color = Color.green; _placable = true; }
    }
}
