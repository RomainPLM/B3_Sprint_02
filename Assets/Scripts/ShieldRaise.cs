using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldRaise : MonoBehaviour
{
    public GameObject shield;
    public bool shieldActive;
    public PlayerInput _playerInput;
    public Collider shieldCollider;
    public Material shieldMaterial;
    public MeshRenderer shieldRenderer;

    private void Awake()
    {
        if (shield == null)
        {
            Debug.LogError("Shield GameObject is not assigned!");
        }
        else
        {
            shieldCollider = shield.GetComponent<Collider>();
            Renderer renderer = shield.GetComponent<Renderer>();
            if (renderer != null)
            {
                shieldMaterial = renderer.material;
            }
        }
    }

    private IEnumerator DisableShieldAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Fade out the shield's material alpha
        float fadeDuration = 1f; // Time to fully fade out
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            if (shieldMaterial != null)
            {
                shieldMaterial.SetFloat("_Alpha", alpha); // Assuming the alpha property is named _Alpha in Amplify Shader
            }
            yield return null;
        }

        // Disable the shield GameObject and collider
        if (shieldCollider != null)
        {
            shieldCollider.enabled = false;
        }
        shield.SetActive(false);
        shieldActive = false;

        print("Shield deactivated");
    }

    public void OnBlock()
    {
        print("Block");

        // Enable the shield and reset its alpha
        shield.SetActive(true);
        shieldActive = true;
        if (shieldCollider != null)
        {
            shieldCollider.enabled = true;
        }
        if (shieldMaterial != null)
        {
            shieldMaterial.SetFloat("_Alpha", 1f); // Reset alpha to fully visible
        }

        // Start the coroutine to disable the shield after 3 seconds
        StartCoroutine(DisableShieldAfterDelay(3f));
    }
}
