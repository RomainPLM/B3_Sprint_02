using UnityEngine;

public class TestVfx : MonoBehaviour
{

    [SerializeField] private GameObject m_Particle;
    public void InstanceParticleTest()
    {
        Instantiate(m_Particle, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
    }
}
