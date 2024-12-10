using UnityEngine;

public class BouncingProjectileXZ : MonoBehaviour
{
    public float speed = 10f; 
    public int maxBounces = 3;

    private int currentBounces = 0; 
    private Vector3 direction; 

    void Start()
    {
        direction = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        
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
}