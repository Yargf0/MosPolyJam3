using System.Collections;
using UnityEngine;

public class SpeedBuster : InvertableBehaviour
{
    public float multiplayer = 0.3f;
    public float duration = 5f;
    private bool isOnCooldown = false;

    [SerializeField] private AudioClip activatedAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (isOnCooldown)
            return;
        if (other.CompareTag("Player"))
        {
            if (isInverted)
                Player.Instance.PlayerMovement.ChangeSpeedTemporarily(-multiplayer, duration);
            else
                Player.Instance.PlayerMovement.ChangeSpeedTemporarily(multiplayer, duration);

            AudioManager.Instance.PlaySound(activatedAudio, Random.Range(0.9f, 1.1f));
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
