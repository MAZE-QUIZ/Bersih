using UnityEngine;

public class GoldenDoor : DoorBase
{
    [Header("Golden Door Requirement")]
    [SerializeField] private bool requireGoldenKey = true;
    [SerializeField] private bool requireQuizToOpen = true;

    [Header("Quiz Settings")]
    [SerializeField] private QuizQuestion doorQuizQuestion;
    [SerializeField] private int wrongAnswerDamage = 1;
    [SerializeField] private bool canCancelQuiz = true;

    public override void Interact(PlayerInteraction player)
    {
        if (isOpened)
        {
            Debug.Log("Golden Door sudah terbuka.");
            return;
        }

        PlayerInventory inventory = player.GetComponent<PlayerInventory>();
        if (inventory == null)
        {
            Debug.LogError("PlayerInventory tidak ditemukan pada Player.");
            return;
        }

        if (requireGoldenKey && !inventory.HasItem(ItemType.GoldenKey, 1))
        {
            Debug.Log("Golden Key diperlukan untuk membuka pintu ini.");
            return;
        }

        if (!requireQuizToOpen)
        {
            OpenGoldenDoor(inventory);
            return;
        }

        if (QuizManager.Instance == null)
        {
            Debug.LogError("QuizManager tidak ditemukan di scene.");
            return;
        }

        if (doorQuizQuestion == null)
        {
            Debug.LogError("Door Quiz Question belum diisi di Inspector GoldenDoor.");
            return;
        }

        QuizManager.Instance.StartQuiz(
            doorQuizQuestion,
            onCorrect: () =>
            {
                Debug.Log("Jawaban benar. Golden Door akan dibuka.");
                OpenGoldenDoor(inventory);
            },
            onWrong: () =>
            {
                Debug.Log("Jawaban salah. Golden Door tetap tertutup.");

                PlayerHealth health = player.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(wrongAnswerDamage);
                }
                else
                {
                    Debug.LogWarning("PlayerHealth tidak ditemukan pada Player.");
                }
            },
            canCancel: canCancelQuiz
        );
    }

    public override string GetInteractionText()
    {
        if (isOpened)
            return "";

        return "Buka Golden Door";
    }

    private void OpenGoldenDoor(PlayerInventory inventory)
    {
        if (requireGoldenKey)
        {
            bool keyUsed = inventory.UseItem(ItemType.GoldenKey, 1);

            if (!keyUsed)
            {
                Debug.LogWarning("Gagal menggunakan Golden Key.");
                return;
            }
        }

        OpenDoor();

        Debug.Log("LEVEL COMPLETE (Demo)");
    }
}