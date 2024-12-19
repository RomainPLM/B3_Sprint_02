using UnityEngine;

public class VfxAnimManager : MonoBehaviour
{
    [SerializeField] private GameObject _Anticipation, _Release;
    public void SpawnAnticipation()
    {
        Instantiate(_Anticipation, transform.position, Quaternion.identity);
    }

    public void SpawnRelease()
    {
        Instantiate(_Release, transform.position, Quaternion.identity);
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
