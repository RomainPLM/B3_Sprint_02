using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
public class BlockDisplacement : MonoBehaviour
{
    private Vector2 _playerScript_blocDisplacementDirection;
    public float _blocDisplacementSize;
    public float _blocDisplacementTimeBetween = 0.5f;
    private bool _blocDisplacementEnabled;
    private float _timer;
    private Renderer _renderer;
    private bool _placable;
    private PlayerScript _playerScript;
    public bool _blocIsPosed;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = Color.green;
    }

    private void FixedUpdate()
    {
        //print(_timer+"timer");
        if (_blocDisplacementEnabled == true)
        {
            if (_playerScript._blocDisplacementDirection.x < -0.25f)
            {
                this.gameObject.transform.position = new Vector3((this.gameObject.transform.position.x - _blocDisplacementSize), this.transform.position.y, this.transform.position.z);
                //print("is moving actually");
                _blocDisplacementEnabled = false;
                _timer = 0;
            }
            else if (_playerScript._blocDisplacementDirection.x > 0.25f)
            {
                this.gameObject.transform.position = new Vector3((this.gameObject.transform.position.x + _blocDisplacementSize), this.transform.position.y, this.transform.position.z);
                //print("is moving actually");
                _blocDisplacementEnabled = false;
                _timer = 0;
            }
            else if (_playerScript._blocDisplacementDirection.y < -0.25f)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.transform.position.y, (this.transform.position.z - _blocDisplacementSize));
                //print("is moving actually");
                _blocDisplacementEnabled = false;
                _timer = 0;
            }
            else if (_playerScript._blocDisplacementDirection.y > 0.25f)
            {
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.transform.position.y, (this.transform.position.z + _blocDisplacementSize));
                //print("is moving actually");
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
    private void OnInteract()
    {
        if (_placable == true)
        {
            _blocIsPosed = true;
            _renderer.material.color = Color.gray;
            Destroy(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 6) { _renderer.material.color = Color.red; print(other.gameObject); _placable = false; }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 6) { _renderer.material.color = Color.green; _placable = true; }
    }
}

