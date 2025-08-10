using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIQuest : MonoBehaviour
{
    [Header("UI Components")]
    public Image gambarUI;
    public TMP_Text namaUI;
    public Button detailButton;

    private QuestData currentQuest;
    private QuestManager questManager;

    public void SetData(QuestData quest, QuestManager manager)
    {
        currentQuest = quest;
        questManager = manager;

        // Isi UI
        gambarUI.sprite = quest.Gambar;
        namaUI.text = quest.Nama;

        // Pasang event tombol Detail
        detailButton.onClick.RemoveAllListeners();
        detailButton.onClick.AddListener(() =>
        {
            questManager.ShowDetail(currentQuest);
        });
    }
}
