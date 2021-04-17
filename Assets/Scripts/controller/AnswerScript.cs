using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    public Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        startColor = GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void Answers()
    {
        if (isCorrect)
        {
            Debug.Log("Correct Answer");
            quizManager.correct();
            
        }
        else
        {
            Debug.Log("Wrong Answer");
            quizManager.wrong();
        }
    }
}
