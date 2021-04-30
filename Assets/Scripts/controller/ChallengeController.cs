using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using manager;
using entity;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Newtonsoft.Json;
using UnityEngine.EventSystems;

public class ChallengeController : MonoBehaviour
{
    public int panCount = 0;
    public GameObject scrollbar;
    public GameObject panPrefab;
    public GameObject worldname;
    public GameObject userimg;
    public GameObject uid;
    private UserManager userMananger;
    private List<User> users;
    private Firebase.Auth.FirebaseUser currentUser;
    private User user;
    public EventSystem eventSystem;
    public GameObject currentobj;
    private string opponent;
    private Dictionary<string, string> challenges;
    private PlayManager playManager;
    // Start is called before the first frame update
    void Start()
    {
        userMananger = new UserManager();
        playManager = new PlayManager();
        currentUser = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
        users = new List<User>();
        challenges = new Dictionary<string, string>();
        setdata();
    }

    // Update is called once per frame
    void Update()
    {
        if (eventSystem.currentSelectedGameObject != null)
        {
            currentobj = eventSystem.currentSelectedGameObject;
            if (currentobj != null)
            {
                try {
                    opponent = currentobj.transform.GetChild(2).GetComponent<Text>().text;
                    print(opponent);
                }
                catch
                {
                    
                }
               

            }
        }
    }

    public async void setdata()
    {
        var data = await userMananger.GetAllUsersList();

        foreach (var user in data)
        {
            if (!(user.uid == currentUser.UserId))
                users.Add(user);
        }
        Debug.Log(users);
        panCount = users.Count;
        setScene();

        user = await userMananger.GetCurrentUserFromDB();

        challenges = await playManager.getChallenges();
        foreach (KeyValuePair<string, string> entry in challenges)
        {
            Debug.Log("challenge id - " + entry.Key + "level - " + entry.Value);

        }

    }

    public void setScene()
    {
        for (int i = 0; i < panCount; i++)
        {
            worldname.transform.GetChild(0).GetComponent<Text>().text = users[i].name;
            worldname.transform.GetChild(2).GetComponent<Text>().text = users[i].uid;
            
            Sprite DaSprite = Resources.Load(users[i].character, typeof(Sprite)) as Sprite;
            userimg.GetComponent<Image>().sprite = DaSprite;
            //worldname.transform.GetChild(1).GetComponent<Text>().text = "level " + i;
            //worldname.transform.GetChild(2).GetComponent<Text>().text = "points " + i;
            Instantiate(panPrefab, transform, false);

        }
        Destroy(panPrefab);
    }

    public void createChallenge()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        Challenge challenge = new Challenge();
       // opponent = "8yoi7DcFUwN5sWDqO8LQDmlFtBh2";
        challenge.level = user.levelselected;
        challenge.users = new string[] { currentUser.UserId, opponent };
        //UserLevelData userLevelData = new UserLevelData();
        string json = JsonConvert.SerializeObject(challenge);
        string userLevelData = JsonConvert.SerializeObject(new UserLevelData(user.current_world,user.current_level));
        Debug.Log("challenge is " + json + "\n"+ "userLevelData is " + userLevelData);
        string key = reference.Child("Challenge").Push().Key;
        reference.Child("Challenge_scores").Child(currentUser.UserId).Child(key).SetRawJsonValueAsync(userLevelData);
        reference.Child("Challenge").Child(key).SetRawJsonValueAsync(json);
        reference.Child("Challenge_scores").Child(opponent).Child(key).SetRawJsonValueAsync(userLevelData);
    }


    

}