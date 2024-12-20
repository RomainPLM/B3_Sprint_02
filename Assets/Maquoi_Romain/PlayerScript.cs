using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class PlayerScript : MonoBehaviour
{
    private Vector2 _playerPosition;
    public Vector3 _movement;
    public float _movementSpeed;
    public int _playerNumber;
    public bool _isDead;
    public PlayerInput _playerInput;

    public Vector2 _blocDisplacementDirection1;
    public Vector2 _blocDisplacementDirection2;


    public BlockDisplacement _blockDisplacement;
    public GameGestion _gameGestion;
    public bool _didInstance = false;
    public GameObject _placableBloc;

    public bool _placeBloc;
    private float timer = 0;
    [Range(.5f, 10f)] public float dashcooldown;
    private bool dashOncool;
    public float _dodgeDistance = 5;
    [SerializeField] private GameObject _dashStart, _deathExplosion;
    private Animator _animator;


    private int _bulletLayer = 10;

    public bool _rotateBloc;

    public bool _iemIsOn = false;

    public AudioClip[] placeBlocaudios, dashAudios, deathAudios;
    public float soundVolume;
    public int timebfrSiap;
    private float baseMovSpeed;

    private MeshRenderer[] _renderer;
    private SkinnedMeshRenderer[] _Srenderer;
    //public double timestamp;

    private void Start()
    {
        dashOncool = true;
        _placeBloc = false;
        _gameGestion = Object.FindAnyObjectByType<GameGestion>();
        _playerInput = GetComponent<PlayerInput>();
        // instancier bloc recuperer son scirpt et lui attribu  nombre du joueur!
        _animator = GetComponentInChildren<Animator>();
        _renderer = GetComponentsInChildren<MeshRenderer>();
        _Srenderer = GetComponentsInChildren<SkinnedMeshRenderer>();
        baseMovSpeed = _movementSpeed;
    }
    private void Update()
    {
        BlocDisplacement();
        this.transform.position = transform.position + (_movement * _movementSpeed * Time.deltaTime);
        if (dashOncool)
            timer += Time.deltaTime;
        if (timer >= dashcooldown)
        {
            dashOncool = false;
            timer = 0;

        }
        if (_gameGestion._mancheEnd == true && _didInstance == false)
        {

            _didInstance = true;
            var randomInList = Random.Range(0, _gameGestion._blocTypes.Length);
            GameObject bloc = Instantiate(_gameGestion._blocTypes[randomInList], new Vector3(transform.position.x, _gameGestion._blocTypes[randomInList].transform.localScale.y / 2, transform.position.z), Quaternion.identity);
            // GameObject bloc = Instantiate(_placableBloc, new Vector3(transform.position.x, 00, transform.position.z), transform.rotation);
            _blockDisplacement = bloc.GetComponent<BlockDisplacement>();
            _gameGestion._blocList.Add(_blockDisplacement);
            bloc.GetComponent<BlockDisplacement>()._playerScript = this;


        }
    }

    private void BlocDisplacement()
    {
        if (Input.GetAxisRaw("VerticalJoy1") > 0.15 || Input.GetAxisRaw("VerticalJoy1") < -0.15)
        {
            _blocDisplacementDirection1.x = -Input.GetAxisRaw("VerticalJoy1");

        }
        else
        {
            _blocDisplacementDirection1.x = 0;
        }
        if (Input.GetAxisRaw("HorizontalJoy1") > 0.15 || Input.GetAxisRaw("HorizontalJoy1") < -0.15)
        {
            _blocDisplacementDirection1.y = -Input.GetAxisRaw("HorizontalJoy1");
        }
        else
        {
            _blocDisplacementDirection1.y = 0;
        }
        if (Input.GetAxisRaw("VerticalJoy2") > 0.15 || Input.GetAxisRaw("VerticalJoy2") < -0.15)
        {
            _blocDisplacementDirection2.x = -Input.GetAxisRaw("VerticalJoy2");

        }
        else
        {
            _blocDisplacementDirection2.x = 0;
        }
        if (Input.GetAxisRaw("HorizontalJoy2") > 0.15 || Input.GetAxisRaw("HorizontalJoy2") < -0.15)
        {
            _blocDisplacementDirection2.y = -Input.GetAxisRaw("HorizontalJoy2");

        }
        else
        {
            _blocDisplacementDirection2.y = 0;
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 moveInputValue = value.Get<Vector2>();
        //if (_blockDisplacement._blocIsPosed == true)
        //{
        if (_iemIsOn == false)
        {

            _playerPosition.x = transform.position.x + moveInputValue.x;
            _playerPosition.y = transform.position.z + moveInputValue.y;
            _movement = new Vector3(moveInputValue.x, 0.0f, moveInputValue.y);
            this.transform.LookAt(transform.position + (-_movement) + transform.forward);
        }
        else
        {
            _playerPosition.x = transform.position.x - moveInputValue.x;
            _playerPosition.y = transform.position.z - moveInputValue.y;
            _movement = new Vector3(moveInputValue.x, 0.0f, moveInputValue.y);
        }


    }
    private void OnInteract()
    {
        print("i clic on Y" + _playerNumber);
        SfxManager._instance.PlayAudioClip(placeBlocaudios, transform, false, soundVolume);
        _placeBloc = true;

    }
    private void OnDash()
    {
        if (!dashOncool)
        {
            float time = 0;

            float curveTime = 1;
            SfxManager._instance.PlayAudioClip(dashAudios, transform, false, soundVolume);
            Instantiate(_dashStart, transform.position, transform.rotation);
            Dash(time, baseMovSpeed, curveTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _bulletLayer && gameObject.layer != 8)
        {
            _isDead = true;
            SfxManager._instance.PlayAudioClip(deathAudios, transform, false, soundVolume);
            Instantiate(_deathExplosion, transform.position, Quaternion.identity);
            Death();
            Destroy(other.gameObject);
            //print("Ouch i got hit by" + other.name);
        }
    }
    private void OnRotate()
    {
        _rotateBloc = true;
        print("rotate" + _rotateBloc + "fck l input systeme");


    }
    private async void Dash(float time, float baseMovSpeed, float curveTime)
    {
        bool asMultiplied = false;
        _animator.SetTrigger("Dash");
        if (asMultiplied == false)
            _movementSpeed *= _dodgeDistance;
        asMultiplied = true;
        await Task.Delay(500);
        _movementSpeed = baseMovSpeed;

        dashOncool = true;
    }

    private async void Death()
    {
        await Task.Delay(250);
        foreach (MeshRenderer mRend in _renderer)
        {
            mRend.enabled = false;
        }
        foreach (SkinnedMeshRenderer smRend in _Srenderer)
        {
            smRend.enabled = false;
        }
        await Task.Delay(1000);
        foreach (MeshRenderer mRend in _renderer)
        {
            mRend.enabled = true;
        }
        foreach (SkinnedMeshRenderer smRend in _Srenderer)
        {
            smRend.enabled = true;
        }
    }
}
