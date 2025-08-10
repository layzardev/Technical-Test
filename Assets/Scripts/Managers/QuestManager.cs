using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    [Header("Quest Data")]
    public List<QuestData> allQuests;

    [Header("UI")]
    public Transform questListContainer;
    public GameObject questItemPrefab;
    public QuestDetailPopup questDetailPopup;

    private UIManager uiManager;
    private ListFadeInManager fadeAnimator;

    private void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        fadeAnimator = GetComponent<ListFadeInManager>();

        LoadUI();
    }

    public void LoadUI()
    {
        // Bersihkan list lama
        foreach (Transform child in questListContainer)
            Destroy(child.gameObject);

        // Spawn quest item baru
        foreach (var quest in allQuests)
        {
            GameObject item = Instantiate(questItemPrefab, questListContainer);
            UIQuest uiQuest = item.GetComponent<UIQuest>();
            uiQuest.SetData(quest, this);
        }

        // Panggil animasi fade-in
        if (fadeAnimator != null)
            fadeAnimator.PlayFadeIn(questListContainer);
    }

    public void ShowDetail(QuestData quest)
    {
        questDetailPopup.Show(quest, this);
        uiManager.ShowPopup();
    }

    public void HideDetail()
    {
        uiManager.HidePopup();
    }

    public void ClaimQuest(QuestData quest)
    {
        quest.IsClaimed = true;
        quest.Status = QuestStatus.Claimed;

        Debug.Log($"Quest '{quest.Nama}' berhasil di-claim!");

        LoadUI();

        if (uiManager.IsPopupVisible)
        {
            questDetailPopup.Show(quest, this);
        }
    }
}
