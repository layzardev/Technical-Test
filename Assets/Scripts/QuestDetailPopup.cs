using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestDetailPopup : MonoBehaviour
{
    [Header("UI Components")]
    public TMP_Text namaText;
    public TMP_Text deskripsiText;
    public Button closeButton;
    public Button claimButton;
    public TMP_Text claimButtonText; // teks di tombol Claim

    [Header("Button Colors")]
    public Color inProgressColor = Color.gray;
    public Color claimColor = new Color(0.2f, 0.8f, 0.2f); // hijau
    public Color claimedColor = new Color(0.5f, 0.5f, 0.5f); // abu gelap

    private QuestData currentQuest;
    private QuestManager questManager;

    public void Show(QuestData quest, QuestManager manager)
    {
        currentQuest = quest;
        questManager = manager;

        namaText.text = quest.Nama;
        deskripsiText.text = quest.Deskripsi;

        UpdateClaimButtonUI(quest);

        // Event Claim
        claimButton.onClick.RemoveAllListeners();
        claimButton.onClick.AddListener(() =>
        {
            if (quest.Status == QuestStatus.Complete && !quest.IsClaimed)
            {
                questManager.ClaimQuest(currentQuest);
                quest.Status = QuestStatus.Claimed;
                quest.IsClaimed = true;
                UpdateClaimButtonUI(quest);
            }
        });

        // Event Close
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(Hide);

        gameObject.SetActive(true);
    }

    private void UpdateClaimButtonUI(QuestData quest)
    {
        if (quest.Status == QuestStatus.Claimed || quest.IsClaimed)
        {
            claimButtonText.text = "Claimed";
            claimButton.interactable = false;
            claimButton.image.color = claimedColor;
        }
        else if (quest.Status == QuestStatus.Complete && !quest.IsClaimed)
        {
            claimButtonText.text = "Claim";
            claimButton.interactable = true;
            claimButton.image.color = claimColor;
        }
        else // On Progress
        {
            claimButtonText.text = "In Progress";
            claimButton.interactable = false;
            claimButton.image.color = inProgressColor;
        }
    }

    public void Hide()
    {
        // gameObject.SetActive(false);
        questManager.HideDetail();
    }
}
