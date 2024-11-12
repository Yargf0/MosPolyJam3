using UnityEngine;

public class StarCollectable : BaseCollectable
{
    [Header("Audio")]
    [SerializeField] private AudioClip collectAudio;
    [SerializeField] private AudioClip invertedCollectAudio;

    public override void Collect()
    {
        if (isInverted)
        {
            AudioManager.Instance.PlaySoundAtPosition(invertedCollectAudio, transform.position, Random.Range(0.9f, 1.1f));
            Player.RemoveStar();
        }
        else
        {
            AudioManager.Instance.PlaySoundAtPosition(collectAudio, transform.position, Random.Range(0.9f, 1.1f));
            Player.AddStar();
        }
        Destroy(gameObject);
    }

    protected override void OnInverted()
    {

    }
}