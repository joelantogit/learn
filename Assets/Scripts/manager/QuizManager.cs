using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using System;
using Random=UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    public class QuestionAndAnswers
    {
        public string Question { get; set; }
        public string[] Answers { get; set; }
        public int CorrectAnswer { get; set; }
    }
    
    public GameObject[] options;
    public int currentQuestion;
    public int score = 0;
    int totalQuestions = 0;
    public string world = "World1";
    public string level = "Addition & Subtraction";
    public int totalQuestionNum = 0;
    
    public string question = "H";
    public string[] answers = new string[4];
    public int correctanswer = 1;

    public GameObject QuizPanel;
    public GameObject GoPanel;

    public Text QuestionTxt;
    public Text ScoreTxt;

    
    public List<QuestionAndAnswers> QnA = new List<QuestionAndAnswers>();
    

    void Start()
    {
        createQA();
        totalQuestions = QnA.Count;
        print(totalQuestions);
        GoPanel.SetActive(false);
        generateQuestion();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void createQA()
    {   
        int questionNum = 1;
        int optionNum = 1;
        
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
                question  = snapshot.Child(level).Child(string.Format("question{0}", questionNum)).Child("question").Value.ToString(); 
                correctanswer = Convert.ToInt32(snapshot.Child(level).Child(string.Format("question{0}", questionNum)).Child("correctans").Value.ToString());
                print(correctanswer);
                for(int i=0;i<4;i++) 
                {
                    answers[i]  = snapshot.Child(level).Child(string.Format("question{0}", questionNum)).Child(string.Format("option{0}", optionNum)).Value.ToString(); 
                    optionNum ++;
                }

            }      
        }
        );
            
        QnA.Add(new QuestionAndAnswers
        {
            Question = question,
            Answers = answers,
            CorrectAnswer = correctanswer,
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
