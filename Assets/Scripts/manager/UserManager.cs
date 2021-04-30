using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using entity;
using Firebase.Auth;
using System;
using Firebase.Database;
using controller;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace manager
{
    public class UserManager
    {
        Firebase.Auth.FirebaseUser currentUser = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
        User user;

        public void createUser()
        {
            User user = new User();
            Debug.Log("inside create user");
            //user.name = "joel";
            user.name = currentUser.DisplayName;
            //user.emailid = "test@gmail.com";
            user.emailid = currentUser.Email;
            user.enable_email = true;
            user.role = "student";
            user.current_level = "Average";
            user.current_world = "Basics";
            user.levelselected = "";
            user.worldselected = "";
            user.total_points = 0;
            user.character = "";
            //user.uid = "vlcP7MmerUYrbds2RuiC7oLY5bn1";
            user.uid = currentUser.UserId;

            DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
            string json = JsonConvert.SerializeObject(user);
            Debug.Log(json);
            reference.Child("User").Child(user.uid).SetRawJsonValueAsync(json);
            

        }

       public async Task<List<User>> GetAllUsersList()
        {
            DatabaseReference user_reference = FirebaseDatabase.DefaultInstance.GetReference("User");
            var users = new List<User>();
            Task<DataSnapshot> task = user_reference.GetValueAsync();
            DataSnapshot snapshot = await task;
            
                   
            foreach(var _users in snapshot.Children)
            {
                String str = _users.GetRawJsonValue();
                users.Add(JsonConvert.DeserializeObject<User>(str));
            }
            return users;
                
           
        }

       

        public async Task<User> GetCurrentUserFromDB()
        {
            DatabaseReference user_reference = FirebaseDatabase.DefaultInstance.GetReference("User");
            Task<DataSnapshot> task = user_reference.GetValueAsync();

            DataSnapshot snapshot = await task;
            //string str = snapshot.Child(currentUser.UserId).GetRawJsonValue();
            string str = snapshot.Child(currentUser.UserId).GetRawJsonValue();
            Debug.Log(str);
            user = JsonConvert.DeserializeObject<User>(str);
            //User user = juser.user;
            Debug.Log("Current user is " + user.name);

            SetUser(user);
            
            return user;
        }


        public void SetUser(User user)
        {
            this.user = user;
        }
       

    }
}

