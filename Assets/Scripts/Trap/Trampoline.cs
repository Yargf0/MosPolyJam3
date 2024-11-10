using UnityEngine;

public class Trampoline : InvertableBehaviour
{
    [Header("Trampoline Settings")]
    public float verticalForce = 10f;  
    public float sideForce = 5f; 
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LaunchPlayer();
        }
    }

    private void LaunchPlayer()
    {
        Debug.Log("thth");
        Vector3 launchVelocity = Vector3.up * verticalForce;

        if (isInverted)
        {
            launchVelocity += Vector3.right * sideForce;
        }
        Player.PlayerMovement.Rigidbody.velocity += launchVelocity;
    }

    protected override void OnInverted()
    {

    }
}

