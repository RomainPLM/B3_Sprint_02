using UnityEngine;

public class BouncyRocket : MonoBehaviour
{
    public float speed = 10f;
    public int maxBounces = 3;
    public GameObject bounceExplosion, bounceVfx;
    public AudioClip[] audios;


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
            Instantiate(bounceVfx, transform.position, Quaternion.identity);
            SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

            direction = Vector3.Reflect(direction, normal).normalized;

                transform.position = hit.point;
                Instantiate(bounceExplosion, transform.position, transform.rotation);

                currentBounces++;

                if (currentBounces >= maxBounces)
                {
                    Destroy(gameObject);
                }
               
            
        }
    }
}