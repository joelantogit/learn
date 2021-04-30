using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using manager;
using UnityEngine.UI;

public class profileController : MonoBehaviour
{
    private UserManager userManager;
    public GameObject charimage;
    public Text  username;
    public Text points;
    public Text level;
    // Start is called before the first frame update
    void Start()
    {
        userManager = new UserManager();
        setdata();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void setdata()
    {
        var user = await  userManager.GetCurrentUserFromDB();
        Debug.Log("current user is " + user.name);
        username.text = user.name;
        points.text = user.total_points.ToString();
        level.text = user.current_level;
        Sprite DaSprite = Resources.Load(user.character, typeof(Sprite)) as Sprite;
        charimage.GetComponent<Image> ().sprite = DaSprite;


    }
}
