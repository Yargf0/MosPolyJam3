using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuster : InvertableBehaviour
{
    public float multiplayer = 0.3f;
    public float duration = 5f;
    private bool isOnCooldown = false;
    private void OnTriggerEnter(Collider other)
    {
        if (isOnCooldown)
            return;
        if (other.CompareTag("Player") )
        {
            if(isInverted)
                Player.PlayerMovement.ChangeSpeedTemporarily(-multiplayer, duration);
            else
                Player.PlayerMovement.ChangeSpeedTemporarily(multiplayer, duration);

            StartCoroutine(CooldownRoutine());
        }
    }
    protected override void OnInverted()
    {
        
    }

    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(duration); 
        isOnCooldown = false;
        GetComponent<Collider>().enabled = true;
    }
}
