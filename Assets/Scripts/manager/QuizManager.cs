using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using System;
using System.Threading.Tasks;
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
    int totalQuestions = 4;
    public string world = "World1";
    public string level = "1";
    public int totalQuestionNum = 0;
    static int retryNum = 1;

    public string question = "H";
    public string[] answers = new string[4];
    public int correctanswer = 1;
    public int questionNum = 1;

    public GameObject QuizPanel;
    public GameObject GoPanel;

    public Text QuestionTxt;
    public Text ScoreTxt;

    
    public List<QuestionAndAnswers> QnA = new List<QuestionAndAnswers>();
    

    void Start()
    {
        GoPanel.SetActive(false);
        generateQuestion();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async Task getData()
    {   
        int optionNum = 1;

        await FirebaseDatabase.DefaultInstance      
        .GetReference("Worlds")      
        .GetValueAsync().ContinueWith(task => 
        {        
            if (task.IsFaulted) 
            {
                print("Error");  // Handle the error...                      
            }        
            else if (task.IsCompleted) 
            {          
                DataSnapshot snapshot = task.Result;          // Do something with snapshot...       
                question  = snapshot.Child(world).Child("levels").Child(level).Child(string.Format("question{0}", questionNum)).Child("question").Value.ToString(); 
                correctanswer = Convert.ToInt32(snapshot.Child(level).Child(string.Format("question{0}", questionNum)).Child("correctans").Value.ToString());
                
                for(int i=0;i<4;i++) 
                {
                    answers[i]  = snapshot.Child(level).Child(string.Format("question{0}", questionNum)).Child(string.Format("option{0}", optionNum)).Value.ToString(); 
                    optionNum ++;
                }

            }      
        }
        );
        return;
            
    }
    public void createQA()
    {
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
        if(retryNum<3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            retryNum++;
        }
        else
        {
            print("Maximum attempted");
            
        }
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
        for(int i=0;i<4;i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];
            
            if(QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true; 
            }
        }
    }

    

    async void generateQuestion()
    {
        if(questionNum !=5)
        {
            await getData();
            createQA();
        }
        if(questionNum<5)
        {
            currentQuestion = 0;
            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
            questionNum++;
        }
        else
        {
            GameOver();
            questionNum = 1;
            print(retryNum);
        }
            
    }

}
