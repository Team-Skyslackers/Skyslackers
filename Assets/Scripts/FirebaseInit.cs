using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;

public class FirebaseInit : MonoBehaviour
{
    protected string MyStorageBucket = "gs://test-7f7c0.appspot.com/";
    protected static string UriFileScheme = Uri.UriSchemeFile + "://";

    protected Firebase.LogLevel logLevel = Firebase.LogLevel.Info;

    protected bool isFirebaseInitialized = false;

    private DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    public FirebaseStorage storage;
    static public StorageReference storageReference;
    protected string persistentDataPath;


    // Start is called before the first frame update
    void Start()
    {
        persistentDataPath = Application.persistentDataPath;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError(
                  "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    protected virtual void InitializeFirebase()
    {
        var appBucket = FirebaseApp.DefaultInstance.Options.StorageBucket;
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://test-7f7c0.appspot.com");
        if (!String.IsNullOrEmpty(appBucket))
        {
            MyStorageBucket = String.Format("gs://{0}/", appBucket);
        }
        storage.LogLevel = logLevel;
        isFirebaseInitialized = true;
    }



    //static public string GetURL(string dir)
    //{
    //    string URLretrived = "";
    //    storageReference.Child(dir).GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
    //    {
    //        if (!task.IsFaulted && !task.IsCanceled)
    //        {
    //            URLretrived = task.Result.ToString();
    //        };

    //    });
    //    return URLretrived;
    //}
}
