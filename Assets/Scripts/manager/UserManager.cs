using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using entity;
using Firebase.Auth;
using System;
using Firebase.Database;
using controller;
using Newtonsoft.Json;

namespace manager
{
    public class UserManager
    {
        Firebase.Auth.FirebaseUser currentUser = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;

        public void createUser()
        {
            User user = new User();
            Debug.Log("inside create user");
            user.name = "joel";
            //user.name = currentUser.DisplayName;
            user.emailid = "test@gmail.com";
            //user.emailid = currentUser.Email;
            user.enable_email = true;
            user.role = "student";
            user.current_level = "Average";
            user.current_world = 1;
            user.levelselected = "";
            user.worldselected = 0;
            user.total_points = 0;
            user.character = "";
            user.uid = "vlcP7MmerUYrbds2RuiC7oLY5bn1";
            //user.uid = currentUser.UserId;

            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            string json = JsonConvert.SerializeObject(user);
            Debug.Log(json);
            reference.Child("User").Child(user.uid).SetRawJsonValueAsync(json);

        }
    }
}

