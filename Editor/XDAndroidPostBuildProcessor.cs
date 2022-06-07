#if UNITY_EDITOR && UNITY_ANDROID
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
#if UNITY_ANDROID
using UnityEditor.Android;
#endif
using UnityEngine;
using XD.Cn.Common.Editor;

public class XDAndroidPostBuildProcessor : IPostGenerateGradleAndroidProject{
    public int callbackOrder{
        get{ return 999; }
    }

    public static void GenerateAndroidX(string path){
        string gradlePropertiesFile = path + "/gradle.properties";

        Debug.Log($"GradleProperties File:{gradlePropertiesFile}");

        // if (File.Exists(gradlePropertiesFile))
        // {
        //     File.Delete(gradlePropertiesFile);
        // }
        // StreamWriter writer = File.CreateText(gradlePropertiesFile);
        // writer.WriteLine("org.gradle.jvmargs=-Xmx4096M");
        // writer.WriteLine("android.useAndroidX=true");
        // writer.WriteLine("android.enableJetifier=true");
        // writer.WriteLine("unityStreamingAssets=.unity3d");
        // writer.Flush();
        // writer.Close();

        if (File.Exists(gradlePropertiesFile)){
            XDScriptHandlerProcessor writeHelper = new XDScriptHandlerProcessor(gradlePropertiesFile);
            writeHelper.WriteBelow(@"org.gradle.jvmargs=-Xmx4096M", @"
android.useAndroidX=true
android.enableJetifier=true");
        }
    }

    public static bool GeneratedAndroidGradle(string projectPath){
        if (!Directory.Exists(projectPath + "/launcher")){
            Debug.Log($"XDG can't find {projectPath}/launcher");

            string targetGradlePath = projectPath + "/build.gradle";

            if (File.Exists(targetGradlePath)){
                XD.Cn.Common.Editor.XDScriptHandlerProcessor writeHelper =
                    new XD.Cn.Common.Editor.XDScriptHandlerProcessor(targetGradlePath);

                writeHelper.WriteBelow(@"implementation fileTree(dir: 'libs', include: ['*.jar'])", @"

                implementation 'com.google.code.gson:gson:2.8.6'
                implementation 'androidx.recyclerview:recyclerview:1.2.1'
                implementation 'androidx.appcompat:appcompat:1.3.1'
                 ");
                return true;
            } else{
                Debug.LogError("TDSGlobal can't find Android Gradle File!");
                return false;
            }
        }

        return false;
    }

    void IPostGenerateGradleAndroidProject.OnPostGenerateGradleAndroidProject(string path){
        GenerateAndroidX(path);

        string projectPath = path;

        if (path.Contains("unityLibrary")){
            projectPath = path.Substring(0, path.Length - 12);
            GenerateAndroidX(projectPath);
        } else{
            GenerateAndroidX(path);
        }

        Debug.Log($"Project path:{path},substring path:{projectPath}");

        if (GeneratedAndroidGradle(projectPath)){
            return;
        }

        string launcherGradle = projectPath + "/launcher/build.gradle";

        string baseProjectGradle = projectPath + "/build.gradle";

        string unityLibraryGradle = projectPath + "/unityLibrary/build.gradle";

        if (File.Exists(launcherGradle)){
            Debug.Log("write launch gradle");

            XD.Cn.Common.Editor.XDScriptHandlerProcessor writerHelper =
                new XD.Cn.Common.Editor.XDScriptHandlerProcessor(launcherGradle);
            writerHelper.WriteBelow(@"implementation project(':unityLibrary')", @"
                
            ");
        }

        if (File.Exists(baseProjectGradle)){
            Debug.Log("write project gradle");
            XD.Cn.Common.Editor.XDScriptHandlerProcessor writerHelper =
                new XD.Cn.Common.Editor.XDScriptHandlerProcessor(baseProjectGradle);
            writerHelper.WriteBelow(@"task clean(type: Delete) {
    delete rootProject.buildDir
}", @"allprojects {
    buildscript {
        dependencies {
        }
    }
}");
        }

        if (File.Exists(unityLibraryGradle)){
            XD.Cn.Common.Editor.XDScriptHandlerProcessor writerHelper =
                new XD.Cn.Common.Editor.XDScriptHandlerProcessor(unityLibraryGradle);
            writerHelper.WriteBelow(@"implementation fileTree(dir: 'libs', include: ['*.jar'])", @"

                implementation 'com.google.code.gson:gson:2.8.6'
                implementation 'androidx.recyclerview:recyclerview:1.2.1'
                implementation 'androidx.appcompat:appcompat:1.3.1'
            ");
        }
    }
}
#endif