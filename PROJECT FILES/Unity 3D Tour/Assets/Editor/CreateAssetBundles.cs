using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateAssetBundles
{
    [MenuItem("Assets/ Build Asset Bundle")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/BundledAssets";
        // if (!Directory.Exists(Application.streamingAssetsPath))
        // {
        //     Directory.CreateDirectory(assetBundleDirectory);
        // }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }
}