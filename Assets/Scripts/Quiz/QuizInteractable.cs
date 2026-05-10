using UnityEngine;

public class QuizInteractable : MonoBehaviour, IInteractable
{
    [Header("Quiz")]
    [SerializeField] private QuizQuestion quizQuestion;

    [Header("Settings")]
    [SerializeField] private bool damagePlayerOnWrongAnswer = true;
    [SerializeField] private int wrongAnswerDamage = 1;
    [SerializeField] private bool canCancelQuiz = true;

    public void Interact(PlayerInteraction player)
    {
        if (QuizManager.Instance == null)
        {
            Debug.LogError("QuizManager not found in scene.");
            return;
        }

        QuizManager.Instance.StartQuiz(
            quizQuestion,
            onCorrect: () =>
            {
                Debug.Log($"{gameObject.name}: quiz answered correctly.");
            },
            onWrong: () =>
            {
                Debug.Log($"{gameObject.name}: quiz answered incorrectly.");

                if (!damagePlayerOnWrongAnswer)
                    return;

                PlayerHealth health = player.GetComponent<PlayerHealth>();

                if (health != null)
                {
                    health.TakeDamage(wrongAnswerDamage);
                }
                else
                {
                    Debug.LogWarning("PlayerHealth not found on Player.");
                }
            },
            canCancel: canCancelQuiz
        );
    }

    public string GetInteractionText()
    {
        return "Answer Quiz";
    }
}