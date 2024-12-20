using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;
public class BlockDisplacement : MonoBehaviour
{


    public float _blocDisplacementSize;
    public float _blocDisplacementTimeBetween = 0.5f;
    private bool _blocDisplacementEnabled;
    private float _timer;
    private Renderer[] _renderer;
    private bool _placable;
    public PlayerScript _playerScript;
    public bool _blocIsPosed;
    public Vector2 _blocDisplacementDirection;
    private double _timestamp;
    public Vector4 _clampDispValues;
    private Vector3 _clampPosValues;

    private void Start()
    {
        if (_renderer == null)
        {
            _renderer = GetComponents<Renderer>();
            _renderer = GetComponentsInChildren<Renderer>();

        }
        foreach (Renderer renderer in _renderer)
        {
            renderer.material.SetColor("_PlaceColor", Color.green);
            renderer.material.SetFloat("_FresnelEnabled", 1f);
        }
        _blocDisplacementEnabled = true;
        _placable = true;
    }

    private void Update()
    {
        _clampPosValues.x = Mathf.Clamp(transform.position.x, -27.75f, 27.75f);
        _clampPosValues.z = Mathf.Clamp(transform.position.z, -13.4f, 13.4f);
        transform.position = _clampPosValues;

        //if (Time.timeAsDouble == _playerScript.timestamp)
        //    return;

        if (_placable == true && _playerScript._placeBloc == true)
        {
            if (_playerScript._placeBloc == true)
            {
                print("shabingus");
                _blocIsPosed = true;
                print(_renderer);
                foreach (Renderer renderer in _renderer)
                {
                    renderer.material.SetColor("_PlaceColor", Color.gray);
                    renderer.material.SetFloat("_FresnelEnabled", 0f);
                }
                Destroy(this);
            }
        }

        if (_playerScript._rotateBloc == true && _blocDisplacementEnabled == true)
        {
            print("u are actually tring to rotate a gameobject");
            this.gameObject.transform.rotation *= Quaternion.Euler(0, 90, 0);
            _playerScript._rotateBloc = false;
        }

        if (_blocDisplacementEnabled == true)
        {
            //print("oggzzyuvggvtuat");
            if (_playerScript._playerNumber == 0)
            {
                _blocDisplacementDirection = _playerScript._blocDisplacementDirection1;
                // print(_blocDisplacementDirection + "direction 1");
            }
            else if (_playerScript._playerNumber == 1)
            {
                _blocDisplacementDirection = _playerScript._blocDisplacementDirection2;
                //print(_blocDisplacementDirection+"direction 2");
            }

            if (_blocDisplacementDirection.x < -0.25f)
            {
                this.gameObject.transform.position = new Vector3((this.gameObject.transform.position.x - _blocDisplacementSize), this.transform.position.y, this.transform.position.z);
                // print("is moving actually + " + _blocDisplacementDirection);
                //  print(this.gameObject.transform.position);
                _blocDisplacementEnabled = false;
                _timer = 0;
            }
            else if (_blocDisplacementDirection.x > 0.25f)
            {

                this.gameObject.transform.position = new Vector3((this.gameObject.transform.position.x + _blocDisplacementSize), this.transform.position.y, this.transform.position.z);
                //print("is moving actually + " + _blocDisplacementDirection);
                //  print(this.gameObject.transform.position);
                _blocDisplacementEnabled = false;
                _timer = 0;
            }
            else if (_blocDisplacementDirection.y < -0.25f)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.transform.position.y, (this.transform.position.z - _blocDisplacementSize));
                //print("is moving actually + " + _blocDisplacementDirection);
                // print(this.gameObject.transform.position);
                _blocDisplacementEnabled = false;
                _timer = 0;
            }
            else if (_blocDisplacementDirection.y > 0.25f)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.transform.position.y, (this.transform.position.z + _blocDisplacementSize));
                //print("is moving actually + " + _blocDisplacementDirection);
                //  print(this.gameObject.transform.position);

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
        if (other.gameObject.layer != 6)
        {
            foreach (Renderer renderer in _renderer)
                renderer.material.SetColor("_PlaceColor", Color.red);/* print(other.gameObject);*/ _placable = false;
            _playerScript._placeBloc = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 6)
        {
            foreach (Renderer renderer in _renderer)
                renderer.material.SetColor("_PlaceColor", Color.green);
            _placable = true;
        }
    }
}
