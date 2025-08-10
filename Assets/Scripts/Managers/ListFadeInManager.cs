using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class ListFadeInManager : MonoBehaviour
{
    [Header("Animasi Settings")]
    public float fadeTime = 0.5f;
    public float delayBetweenItems = 0.1f;

    public void PlayFadeIn(Transform container)
    {
        List<CanvasGroup> canvasGroups = new List<CanvasGroup>();

        // Siapkan CanvasGroup untuk setiap item
        foreach (Transform child in container)
        {
            if (child == null) continue;

            CanvasGroup cg = child.GetComponent<CanvasGroup>();
            if (cg == null) cg = child.gameObject.AddComponent<CanvasGroup>();

            cg.alpha = 0f;
            canvasGroups.Add(cg);
        }

        // Jalankan animasi fade-in
        for (int i = 0; i < canvasGroups.Count; i++)
        {
            canvasGroups[i]
                .DOFade(1f, fadeTime)
                .SetDelay(i * delayBetweenItems)
                .SetEase(Ease.OutCubic);
        }
    }
}
