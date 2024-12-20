using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldRaise : MonoBehaviour
{
    public GameObject shield;
    public bool shieldActive;
    public PlayerInput _playerInput;
    public Animator animator;
    public AudioClip[] audios;

    private IEnumerator DisableShieldAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        shieldActive = false;
        animator.SetBool("IsActive", false);
        shield.SetActive(false);
        print("Shield deactivated");
    }

    public void OnBlock()
    {
        SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

        print("Block");
        shield.SetActive(true);
        shieldActive = true;
        animator.SetBool("IsActive", true);
        
        StartCoroutine(DisableShieldAfterDelay(1.5f));
        
    }
}