using UnityEngine;

public enum QuestStatus
{
    InProgress,
    Complete,
    Claimed
}

[System.Serializable]
public class QuestData
{
    public Sprite Gambar;
    public string Nama;
    public string Deskripsi;
    public QuestStatus Status;
    public bool IsClaimed;
}