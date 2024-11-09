using UnityEngine;

public class Trampoline : InvertableBehaviour
{
    [Header("Trampoline Settings")]
    public float verticalForce = 10f;  
    public float sideForce = 5f;       

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Rigidbody playerRigidbody))
        {
            LaunchPlayer(playerRigidbody);
        }
    }

    private void LaunchPlayer(Rigidbody playerRigidbody)
    {     
        Vector3 launchVelocity = Vector3.up * verticalForce;

        if (isInverted)
        {
            launchVelocity += Vector3.right * sideForce;
        }

        playerRigidbody.velocity = launchVelocity;
    }

    protected override void OnInverted()
    {

    }
}

