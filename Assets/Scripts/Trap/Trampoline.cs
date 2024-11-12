using System.Collections;
using UnityEngine;

public class Trampoline : InvertableBehaviour3D
{
    [Header("Trampoline Settings")]
    public float verticalForce = 10f;
    public float sideForce = 5f;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip activatedAudio;

    private readonly int AnimationHash = Animator.StringToHash("activated");

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LaunchPlayer());
        }
    }

    private IEnumerator LaunchPlayer()
    {
        animator.Play(AnimationHash);
        AudioManager.Instance.PlaySound(activatedAudio, Random.Range(0.9f, 1.1f));

        Vector3 launchVelocity = Vector3.up * verticalForce;

        if (isInverted)
        {
            launchVelocity += Vector3.right * sideForce;
        }
        Player.Instance.PlayerMovement.Rigidbody.velocity = launchVelocity;
        yield return new WaitForSeconds(0.1f);
        Player.Instance.PlayerMovement.JumpOnSpring();
    }

    protected override void OnInverted()
    {

    }
}

