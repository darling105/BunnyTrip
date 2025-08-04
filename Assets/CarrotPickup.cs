using UnityEngine;
using DG.Tweening;
using System.Collections;
public class CarrotPickup : MonoBehaviour
{
    public float jumpHeight = 0.5f;
    public float scaleUp = 1.2f;
    public float duration = 0.3f;

    private bool isCollected = false;
    private Sequence pickupSequence;

    private void OnTriggerEnter(Collider other)
    {
        if (isCollected) return;

        if (other.CompareTag("Player"))
        {
            isCollected = true;
            SoundManager.Instance.PlayPickupCarrot();
            GameManager.Instance.AddCarrot();
            PlayPickupEffect();
        }
    }

    private void PlayPickupEffect()
    {
        pickupSequence = DOTween.Sequence();

        pickupSequence.Append(transform.DOScale(scaleUp, duration / 2f)
            .SetEase(Ease.OutBack).SetLink(gameObject));

        pickupSequence.Join(transform.DOLocalMoveY(transform.localPosition.y + jumpHeight, duration / 2f)
            .SetEase(Ease.OutQuad).SetLink(gameObject));

        pickupSequence.Append(transform.DOScale(0f, duration / 2f)
            .SetEase(Ease.InBack).SetLink(gameObject));

        pickupSequence.Join(transform.DOLocalMoveY(transform.localPosition.y + 1f, duration / 2f)
            .SetLink(gameObject));

        pickupSequence.SetAutoKill(true); // đảm bảo tự huỷ sequence

        // Gọi huỷ bằng delay bên ngoài callback
        StartCoroutine(DestroyAfter(duration)); // Delay đúng thời gian tween
    }

    private IEnumerator DestroyAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (this != null && gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
