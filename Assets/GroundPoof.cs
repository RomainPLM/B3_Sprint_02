using UnityEngine;

public class GroundPoof : MonoBehaviour
{
    [SerializeField] private GameObject _groundPoof;
    public void GroundPoofParticle()
    {
        Instantiate(_groundPoof, transform.position, Quaternion.identity);
    }
}
