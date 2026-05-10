using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private Text questionText;
    [SerializeField] private InputField answerInput;
    [SerializeField] private Text resultText;
    [SerializeField] private Button submitButton;
    [SerializeField] private Button cancelButton;

    private QuizQuestion currentQuestion;
    private Action onCorrectAnswer;
    private Action onWrongAnswer;

    private bool isQuizActive = false;

    public bool IsQuizActive => isQuizActive;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate QuizManager found. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (quizPanel != null)
            quizPanel.SetActive(false);
    }

    private void Start()
    {
        if (submitButton != null)
            submitButton.onClick.AddListener(SubmitAnswer);

        if (cancelButton != null)
            cancelButton.onClick.AddListener(CancelQuiz);
    }

    private void Update()

{
    if (!isQuizActive)
        return;

    if (answerInput != null)
    {
        Debug.Log("Current answer: " + answerInput.text);
    }

    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
    {
        SubmitAnswer();
    }
}


    public void StartQuiz(
        QuizQuestion question,
        Action onCorrect,
        Action onWrong,
        bool canCancel = true
    )
    {
        if (isQuizActive)
        {
            Debug.LogWarning("Quiz is already active.");
            return;
        }

        if (question == null)
        {
            Debug.LogError("Quiz question is null.");
            return;
        }

        currentQuestion = question;
        onCorrectAnswer = onCorrect;
        onWrongAnswer = onWrong;

        isQuizActive = true;

        if (quizPanel != null)
            quizPanel.SetActive(true);

        if (questionText != null)
            questionText.text = currentQuestion.question;

        
        if (answerInput != null)
        {
            answerInput.text = "";

            EventSystem.current.SetSelectedGameObject(answerInput.gameObject);
            answerInput.Select();
            answerInput.ActivateInputField();
        }


        if (resultText != null)
            resultText.text = "";

        if (cancelButton != null)
            cancelButton.gameObject.SetActive(canCancel);

        Debug.Log("Quiz started.");
    }

    public void SubmitAnswer()
    {
        if (!isQuizActive)
            return;

        string playerAnswer = answerInput != null ? answerInput.text : "";

        bool isCorrect = currentQuestion.IsCorrectAnswer(playerAnswer);

        if (isCorrect)
        {
            Debug.Log("Quiz answer correct.");

            if (resultText != null)
                resultText.text = "Correct!";

            Action correctCallback = onCorrectAnswer;
            EndQuiz();

            correctCallback?.Invoke();
        }
        else
        {
            Debug.Log("Quiz answer wrong.");

            if (resultText != null)
                resultText.text = "Wrong!";

            Action wrongCallback = onWrongAnswer;
            EndQuiz();

            wrongCallback?.Invoke();
        }
    }

    public void CancelQuiz()
    {
        if (!isQuizActive)
            return;

        Debug.Log("Quiz cancelled.");
        EndQuiz();
    }

    private void EndQuiz()
    {
        isQuizActive = false;

        currentQuestion = null;
        onCorrectAnswer = null;
        onWrongAnswer = null;

        if (quizPanel != null)
            quizPanel.SetActive(false);

        if (answerInput != null)
            answerInput.text = "";
    }
}