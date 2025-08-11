using UnityEngine;
using DG.Tweening;

public class ListFadeInManager : MonoBehaviour
{
    [Header("Animasi")]
    public float fadeTime = 0.5f;
    public float delayBetweenItems = 0.1f;

    // Menjalankan animasi fade-in untuk semua child di container
    public void PlayFadeIn(Transform container)
    {
        for (int i = 0; i < container.childCount; i++)
        {
            Transform child = container.GetChild(i);

            CanvasGroup cg = child.GetComponent<CanvasGroup>();
            if (cg == null) cg = child.gameObject.AddComponent<CanvasGroup>();

            cg.alpha = 0f;
            cg.DOFade(1f, fadeTime)
              .SetDelay(i * delayBetweenItems)
              .SetEase(Ease.OutCubic);
        }
    }

    // Langsung tampilkan semua child tanpa animasi
    public void ShowInstant(Transform container)
    {
        for (int i = 0; i < container.childCount; i++)
        {
            Transform child = container.GetChild(i);

            CanvasGroup cg = child.GetComponent<CanvasGroup>();
            if (cg == null) cg = child.gameObject.AddComponent<CanvasGroup>();

            cg.alpha = 1f;
        }
    }
}
