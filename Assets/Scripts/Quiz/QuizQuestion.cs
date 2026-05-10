using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuizQuestion
{
    [TextArea]
    public string question;

    public List<string> validAnswers = new List<string>();

    public bool IsCorrectAnswer(string playerAnswer)
    {
        if (string.IsNullOrWhiteSpace(playerAnswer))
            return false;

        string normalizedPlayerAnswer = NormalizeAnswer(playerAnswer);

        foreach (string answer in validAnswers)
        {
            string normalizedValidAnswer = NormalizeAnswer(answer);

            if (normalizedPlayerAnswer == normalizedValidAnswer)
                return true;
        }

        return false;
    }

    private string NormalizeAnswer(string answer)
    {
        return answer.Trim().ToUpperInvariant();
    }
}
