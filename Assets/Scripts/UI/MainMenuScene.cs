using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScene : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button quitGameButton;

    [Header("Animation")]
    [SerializeField] private RectTransform buttonsContainer;
    [SerializeField] private float fromX, toX;
    [SerializeField] private TweenOptions tweenOptions;

    private void Start()
    {
        quitGameButton.onClick.AddListener(Application.Quit);
        StartAnimation();
    }

    private void StartAnimation()
    {
        Vector3 pos = buttonsContainer.anchoredPosition;
        pos.x = fromX;
        buttonsContainer.anchoredPosition = pos;

        buttonsContainer.
            DOAnchorPosX(toX, tweenOptions.Duration).
            SetEase(tweenOptions.Ease).
            Play();
    }
}