using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Google;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Database;
using manager;
using entity;

namespace controller

{
    public class GoogleSignInController : MonoBehaviour
{
    public Text infoText;
    public string webClientId = "<your client id here>";

    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;
    private FirebaseUser user;
    private UserManager userManager;
    private DatabaseReference user_reference;
        

        private void Awake()
        {
            configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
            //CheckFirebaseDependencies();
            FirebaseApp.CheckAndFixDependenciesAsync();
            user_reference = FirebaseDatabase.DefaultInstance.GetReference("User");
            userManager = new UserManager();

        }

    private void CheckFirebaseDependencies()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                {
                    auth = FirebaseAuth.DefaultInstance;
                    auth.StateChanged += AuthStateChanged;
                    AuthStateChanged(this, null);
                }

                else
                    AddToInformation("Could not resolve all Firebase dependencies: " + task.Result.ToString());
            }
            else
            {
                AddToInformation("Dependency check was not completed. Error : " + task.Exception.Message);
            }
        });
    }

    //auth state change event handler -  if login screen is called without proper signout then, it should persist
    //the login session for the current user


    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                AddToInformation("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                AddToInformation("Signed in " + user.UserId);
                    //CheckRole();
            }
        }
    }

    //function to call from unity for loggin in

    public void SignInWithGoogle()
    {
        AddToInformation("Login pressed");
            checkIfUserExists();
           // OnSignIn();
            

    }
    public void SignOutFromGoogle() { OnSignOut(); }

    private void OnSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    private void OnSignOut()
    {
        AddToInformation("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect()
    {
        AddToInformation("Calling Disconnect");
        GoogleSignIn.DefaultInstance.Disconnect();
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                    AddToInformation("Got Error: " + error.Status + " " + error.Message);
                }
                else
                {
                    AddToInformation("Got Unexpected Exception?!?" + task.Exception);
                }
            }
        }
        else if (task.IsCanceled)
        {
            AddToInformation("Canceled");
        }
        else
        {
            AddToInformation("Welcome: " + task.Result.DisplayName + "!");
            AddToInformation("Email = " + task.Result.Email);
            //AddToInformation("Google ID Token = " + task.Result.IdToken);
            SignInWithGoogleOnFirebase(task.Result.IdToken);
        }
    }

    private void SignInWithGoogleOnFirebase(string idToken)
    {
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            AggregateException ex = task.Exception;
            if (ex != null)
            {
                if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                    AddToInformation("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
            }
            else
            {
                AddToInformation("Sign In Successful.");
                //check if user is new then add him to database else continue
                //checkTestdata();
               
                //checkIfUserExists();


                //sceneChangeAfterLogin();
            }
        });
    }

        private void checkIfUserExists()
        {

            user_reference.GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Debug.Log("error from database");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    //Debug.Log("User exists \n user - " + snapshot.Child("8yoi7DcFUwN5sWDqO8LQDmlFtBh2").Child("name").Value.ToString());
                    //Debug.Log("Checking if user 1 exists");

                    //if (snapshot.Child(user.UserId).Exists)
                    if (snapshot.Child("vlcP7MmerUYrbds2RuiC7oLY5bn1").Exists)
                    {
                        Debug.Log("User exists");

                        var users = userManager.GetAllUsersList();
                        List<User> usr = users.Result.ToList();
                        foreach(var user in usr)
                        {
                            print("user name is" + user.name);
                        }
                       
                        CheckRole();

                    }
                    else
                    {
                        Debug.Log("User  does not exist need creating user");
                        userManager.createUser();
                        CheckRole();
                    }

                }
            });
        }

       
        public void CheckRole()
        {
            var user = userManager.GetCurrentUserFromDB();
            if(user.Result.role == "teacher")
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    sceneChangeAfterLogin("teacher_mainpage");
                });
            }
            else if (user.Result.role == "student")
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    sceneChangeAfterLogin("Main_Page");
                });
            }
            else
            {
                Debug.Log("user is admin");
            }
        }



        public void OnSignInSilently()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        AddToInformation("Calling SignIn Silently");

        GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
    }

    public void OnGamesSignIn()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = true;
        GoogleSignIn.Configuration.RequestIdToken = false;

        AddToInformation("Calling Games SignIn");

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
    }

    public void AddToInformation(string str) {
            Debug.Log(str);
            infoText.text += "\n" + str; }

        public void sceneChangeAfterLogin(string str)
        {
            SceneManager.LoadScene("Scenes/"+ str);
           
        }



    }

}

