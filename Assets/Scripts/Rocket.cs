using UnityEngine;
using UnityEngine.Playables;

public class Rocket : MonoBehaviour
{
    public float speed = 10f;
    public int maxBounces = 0;
    public GameObject RocketExplosion;
    private PlayableGraph graph;
    

    public int currentBounces = 0;
    private Vector3 direction;

    void Start()
    {
        direction = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
    }

    void FixedUpdate()
    {
        transform.position += direction * speed * Time.fixedDeltaTime;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        CheckCollision();
    }

    void CheckCollision()
    {
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, speed * Time.deltaTime))
        {
            if (hit.collider.CompareTag("Shield"))
            {
                
                direction = -direction.normalized;

                transform.position = hit.point;

                currentBounces = 0;
                return;
            }

            Vector3 normal = new Vector3(hit.normal.x, 0, hit.normal.z).normalized;
                direction = Vector3.Reflect(direction, normal).normalized;

                transform.position = hit.point;

                currentBounces++;

                if (currentBounces >= maxBounces)
                {
                    Destroy(gameObject);
                }
            
        }
    }

    public void OnDestroy()
    {
        if (graph.IsValid())
            graph.Destroy();
        Instantiate(RocketExplosion, transform.position, transform.rotation);
    }
}