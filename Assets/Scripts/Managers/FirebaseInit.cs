using Firebase;
using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseInit : MonoBehaviour
{
    public static FirebaseInit Instance;

    public static bool isFirebaseInitComplete = false;

    private FirebaseApp app;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = Firebase.FirebaseApp.DefaultInstance;
                isFirebaseInitComplete = true;
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    public void LogFirebaseEvent(string message)
    {
        if (!isFirebaseInitComplete) return;
        FirebaseAnalytics.LogEvent(message);
    }

    public void LogFirebaseEventWithParam(string name, string parameterName, string paramterValue)
    {
        if (!isFirebaseInitComplete) return;
        FirebaseAnalytics.LogEvent(name, parameterName, paramterValue);
    }
}
