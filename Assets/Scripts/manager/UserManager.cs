using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using entity;
using Firebase.Auth;
using System;
using Firebase.Database;
using controller;


namespace manager
{
    public class UserManager : MonoBehaviour
    {
        // Start is called before the first frame update
        User user;
        GoogleSignInController g;
        
        void Start()
        {
            Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            Firebase.Auth.FirebaseUser currentUser = auth.CurrentUser;
            user = new User(currentUser.UserId);
        }

        // Update is called once per frame
        void Update()
        {

        }

        int getCurrentWorld()
        {
            return user.current_world;
        }

        public void createUser(FirebaseUser currentUser )
        {
            g.AddToInformation("inside create user");
            user.name = currentUser.DisplayName;
            user.emailid = currentUser.Email;
            user.enable_email = true;
            user.role = "student";
            user.current_level = "Average";
            user.current_world = 1;
            user.levelselected = "";
            user.worldselected = 0;
            user.total_points = 0;
            user.character = "";
            user.uid = currentUser.UserId;
            save();

        }

        private void save()
        {
            g.AddToInformation("inside save");
            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            string json = JsonUtility.ToJson(user);
            reference.Child("User").Child(user.uid).SetRawJsonValueAsync(json);


        }
    }
}

