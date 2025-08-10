using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("Popup Animation Settings")]
    public float fadeTime = 1f;
    public CanvasGroup popupCanvasGroup;
    public RectTransform popupRectTransform;

    public bool IsPopupVisible { get; private set; }

    private void Awake()
    {
        // Cari otomatis kalau belum diassign di Inspector
        if (popupCanvasGroup == null)
            popupCanvasGroup = GetComponentInChildren<CanvasGroup>(true);

        if (popupRectTransform == null && popupCanvasGroup != null)
            popupRectTransform = popupCanvasGroup.GetComponent<RectTransform>();

        if (popupCanvasGroup == null || popupRectTransform == null)
            Debug.LogWarning("[UIManager] Popup references not found! Please assign manually in Inspector.");
    }

    public void ShowPopup()
    {
        if (popupCanvasGroup == null || popupRectTransform == null)
        {
            Debug.LogError("[UIManager] Cannot show popup — missing references!");
            return;
        }

        popupCanvasGroup.gameObject.SetActive(true);
        popupCanvasGroup.alpha = 0f;
        popupRectTransform.anchoredPosition = new Vector2(0f, -1400f);

        popupRectTransform
            .DOAnchorPos(Vector2.zero, fadeTime)
            .SetEase(Ease.OutElastic);

        popupCanvasGroup
            .DOFade(1, fadeTime)
            .OnComplete(() => IsPopupVisible = true);
    }

    public void HidePopup()
    {
        if (popupCanvasGroup == null || popupRectTransform == null)
        {
            Debug.LogError("[UIManager] Cannot hide popup — missing references!");
            return;
        }

        popupRectTransform
            .DOAnchorPos(new Vector2(0f, -1400f), fadeTime)
            .SetEase(Ease.InOutQuint);

        popupCanvasGroup
            .DOFade(0, fadeTime)
            .OnComplete(() =>
            {
                popupCanvasGroup.gameObject.SetActive(false);
                IsPopupVisible = false;
            });
    }
}
