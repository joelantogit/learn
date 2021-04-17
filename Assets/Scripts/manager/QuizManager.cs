using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;

public class QuizManager : MonoBehaviour
{

    public List<QuestionAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public int score = 0;
    int totalQuestions = 0;
    public string world = "World1";
    public string level = "Addition & Subtraction";
    public int questionNum = 1;
    public string questionStr = "question1" ;
    public int totalQuestionNum = 0;

    public GameObject QuizPanel;
    public GameObject GoPanel;

    public Text QuestionTxt;
    public Text ScoreTxt;

    void Start()
    {
        totalQuestions = QnA.Count;
        GoPanel.SetActive(false);
        generateQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getQuestions()
    {
        FirebaseDatabase.DefaultInstance      
        .GetReference(world)      
        .GetValueAsync().ContinueWith(task => 
        {        
            if (task.IsFaulted) 
            {
                print("Error");  // Handle the error...                      
            }        
            else if (task.IsCompleted) 
            {          
                DataSnapshot snapshot = task.Result;          // Do something with snapshot...       
                print(snapshot.Child(level).Child(questionStr).Child("question").Value.ToString()); 
            }      
        }
        );
    }
    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GameOver()
    {
        QuizPanel.SetActive(false);
        GoPanel.SetActive(true);
        ScoreTxt.text = score + "/" + totalQuestions*5;

    }

    public void correct()
    {
        score = score + 5;
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }

    void SetAnswers()
    {
        for(int i=0;i<options.Length;i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
            
            if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true; 
            }
        }
    }

    

    void generateQuestion()
    {
        if(QnA.Count>0)
        {
            currentQuestion = Random.Range(0,QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            GameOver();
        }
            
    }
}
