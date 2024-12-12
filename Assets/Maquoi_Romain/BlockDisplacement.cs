using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
public class BlockDisplacement : MonoBehaviour
{
    private Vector2 _blocDisplacementDirection;
    private float _blocDisplacementDirectionX;
    public float _blocDisplacementSize;
    public float _blocDisplacementTimeBetween = 0.5f;
    private bool _blocDisplacementEnabled;
    private float _timer;
    private Renderer _renderer;
    private bool _placable;
    public PlayerScript _playerScript;
    public bool _blocIsPosed;

    public Vector2 _blocPositiontryRecup;

    public Vector2 BlockDisplacementDirection
    {
        get
        {
            return _blocDisplacementDirection;
        }
        set
        {
            print("value set to " +  value);
            _blocDisplacementDirection = value;
        }
    }


    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = Color.green;
        _blocDisplacementEnabled = true;
        _blocDisplacementDirection = new Vector2(1, 1);

    }
    private void Update()
    {
        _blocPositiontryRecup = _playerScript._blocDisplacementDirection;
        print("block movement value" + _playerScript._blocDisplacementDirection);
        print("one axis" + _blocDisplacementDirection);
        

        //if (_placable == true)
        //{
        //    _blocIsPosed = true;
        //    _renderer.material.color = Color.gray;
        //    Destroy(this);
        print(_playerScript._blocDisplacementDirection + "In block");
        if (_playerScript._placeBloc == true)
        {
            _blocIsPosed = true;
            _renderer.material.color = Color.gray;
            Destroy(this);
        }
    }
    private void FixedUpdate()
    {
      
        // print(_playerScript+"player script");
    //    print(_playerScript._playerNumber);
        //print(_timer+"timer");
        if (_blocDisplacementEnabled == true)
        {

            // print("le block peut bouger");
        
            if (_playerScript._blocDisplacementDirection.x < -0.25f)
            {
                this.gameObject.transform.position = new Vector3((this.gameObject.transform.position.x - _blocDisplacementSize), this.transform.position.y, this.transform.position.z);
                print("is moving actually");
                _blocDisplacementEnabled = false;
                _timer = 0;
            }
            else if (_playerScript._blocDisplacementDirection.x > 0.25f)
            {
                this.gameObject.transform.position = new Vector3((this.gameObject.transform.position.x + _blocDisplacementSize), this.transform.position.y, this.transform.position.z);
                print("is moving actually");
                _blocDisplacementEnabled = false;
                _timer = 0;
            }
            else if (_playerScript._blocDisplacementDirection.y < -0.25f)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.transform.position.y, (this.transform.position.z - _blocDisplacementSize));
                print("is moving actually");
                _blocDisplacementEnabled = false;
                _timer = 0;
            }
            else if (_playerScript._blocDisplacementDirection.y > 0.25f)
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
        if (other.gameObject.layer != 6) { _renderer.material.color = Color.red;/* print(other.gameObject);*/ _placable = false; }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 6) { _renderer.material.color = Color.green; _placable = true; }
    }
}
