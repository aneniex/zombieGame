using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif
using System.IO;

public class PostBuildStep
{
    //set the IDFA request description
    const string k_TrackingDescription = "Your data will be used to provide you a better and personalized ad experience. We personally don't use your data.";

    [PostProcessBuild(0)]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToXcode)
    {
        if(buildTarget == BuildTarget.iOS)
        {
            AddPListValues(pathToXcode);
        }
    }

    //Implement a function to read and write values to the plist file
    static void AddPListValues(string pathToXcode)
    {
        string pListPath = pathToXcode + "/Info.plist";

        PlistDocument plistObject = new PlistDocument();

        plistObject.ReadFromString(File.ReadAllText(pListPath));

        PlistElementDict pListRoot = new PlistElementDict();

        pListRoot.SetString("NSUserTrackingUsageDescription", k_TrackingDescription);

        File.WriteAllText(pListPath, plistObject.WriteToString());
    }
}
