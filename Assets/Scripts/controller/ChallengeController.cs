using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using manager;
using entity;
using Firebase;
using Firebase.Auth;

public class ChallengeController : MonoBehaviour
{
    public int panCount  = 0; 
    public GameObject scrollbar;
    public GameObject panPrefab;
    public GameObject worldname;
    public GameObject userimg;
    private UserManager userMananger;
    private List<User> users;
    private Firebase.Auth.FirebaseUser currentUser;
    

    // Start is called before the first frame update
    void Start()
    {
        userMananger = new UserManager();
        currentUser = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
        users = new List<User>();
        setdata();
    } 

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void setdata()
    {
        var data = await userMananger.GetAllUsersList();
        
        foreach (var user in data)
        {
            if(!(user.uid == currentUser.UserId))
            users.Add(user);
        }
        Debug.Log(users);
        panCount = users.Count;
        setScene();
    }

    public void setScene()
    {
        for (int i = 0; i < panCount; i++)
        {
            worldname.transform.GetChild(0).GetComponent<Text>().text = users[i].name;
            Sprite DaSprite = Resources.Load(users[i].character, typeof(Sprite)) as Sprite;
            userimg.GetComponent<Image>().sprite = DaSprite;
            //worldname.transform.GetChild(1).GetComponent<Text>().text = "level " + i;
            //worldname.transform.GetChild(2).GetComponent<Text>().text = "points " + i;
            Instantiate(panPrefab, transform, false);

        }
        Destroy(panPrefab);
    }
}
