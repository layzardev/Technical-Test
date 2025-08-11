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

    [Header("Komponen Lain")]
    public UIManager uiManager;
    public ListFadeInManager fadeManager;

    private void Start()
    {
        if (uiManager == null) uiManager = FindAnyObjectByType<UIManager>();
        if (fadeManager == null) fadeManager = GetComponent<ListFadeInManager>();

        LoadUI(true);
    }

    public void LoadUI(bool withAnimation = true)
    {
        // Bersihkan list
        foreach (Transform child in questListContainer)
            Destroy(child.gameObject);

        // Spawn quest items
        foreach (var quest in allQuests)
        {
            GameObject item = Instantiate(questItemPrefab, questListContainer);
            UIQuest uiQuest = item.GetComponent<UIQuest>();
            uiQuest.SetData(quest, this);
        }

        // Jalankan animasi / tampilkan langsung
        if (withAnimation)
            fadeManager.PlayFadeIn(questListContainer);
        else
            fadeManager.ShowInstant(questListContainer);
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

        // Refresh tanpa animasi
        LoadUI(false);

        if (uiManager.IsPopupVisible)
        {
            questDetailPopup.Show(quest, this);
        }
    }
}
