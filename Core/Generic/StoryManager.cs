using Godot;
using System;
using Godot.Collections;

namespace Storier
{
    // Managing Settings and recents of Stories | Singleton(AutoLoaded)
    public class StoryManager : Node    
    {
        // Singleton
        private static StoryManager storyManager;

        public static string workspace = OS.GetSystemDir(OS.SystemDir.Documents) + "/Storier/";
        public static string StorierVersion = "v0.2";
        public static Story currentStory;
        public static string flowName = "";

        private StoryManager()
        {
            storyManager = this;
        }

        // Create Story
        public static Story CreateStory(StoryData _storyData)
        {
            currentStory = new Story(_storyData);
            storyManager.AddChild(currentStory);
            SaveStory();
            return currentStory;
        }

        // Save Story
        public static bool SaveStory()
        {
            Dictionary storierData = new Dictionary() {
                {
                    "Story", currentStory.storyData.SaveData()
                },
                {
                    "Storier", new Dictionary(){
                        {"Version", StorierVersion}
                    }
                }
            };
            bool isSuccess = FileManager.SaveFile(
                currentStory.storyData.directory + currentStory.storyData.name + ".storier", 
                JSON.Print(storierData, "\t"));
            PushRecentStory();
            return isSuccess;
        }

        // Load Story
        public static Story LoadStory(string fileName)
        {
            string str = FileManager.LoadFile(fileName);
            JSONParseResult jsonObject = JSON.Parse(str);
            StoryData storyData = new StoryData();

            if(jsonObject.Error != 0) { GD.Print("Error:", jsonObject.ErrorLine); } 
            else
            {
                // GD.Print(JSON.Print(jsonObject.Result));
                Dictionary storyJson = jsonObject.Result as Dictionary; // Godot.Collections.Array  <-  [] 
                storyData = new StoryData(fileName.BaseName(), "", fileName.GetBaseDir());

                if (storyJson.Contains("Story"))
                {
                    Dictionary storyInfos = storyJson["Story"] as Dictionary; 
                    storyData.LoadData(storyInfos);
                }
                if (storyJson.Contains("Storier"))
                {
                    Dictionary storierInfos = storyJson["Storier"] as Dictionary; 
                    string sVersion = storierInfos["Version"] as string; 
                    if (sVersion != StorierVersion) { GD.Print(sVersion + " != " + StorierVersion + "[Current] Storier Version!"); }
                    else { GD.Print("Same Storier Version!"); }
                }
                // GD.Print(storyData);
            }
            return CreateStory(storyData);
        }

        // Record Recently Story
        public static void PushRecentStory()
        {
            string configFileName = "user://UserStories.ini";
            ConfigFile configFile = new ConfigFile();
            Directory dir = new Directory();
            dir.MakeDirRecursive(configFileName.GetBaseDir());
            configFile.SetValue("Stories", "RecentStory", currentStory.storyData.directory + currentStory.storyData.name + ".storier");
            Error err = configFile.Save(configFileName);
            if (err != Error.Ok) { GD.Print(err); }
        }
        
        // Picking Recently Story
        public static string PopRecentStory()
        {
            string configFileName = "user://UserStories.ini";
            ConfigFile configFile = new ConfigFile();
            Error err = configFile.Load(configFileName);
            if (err != Error.Ok) { GD.Print(err); return ""; }
            return configFile.GetValue("Stories", "RecentStory").ToString(); // RecentStory
        }
        
        // // Return All Recently Story as String
        // public static Array<string> PeekRecentStory()
        // {
        //     return new Array<string>();
        // }

        // !WARNING: Change Workspace isn't in use!

        // Save Config file to "user://", use as UserStories settings
        public static void SaveUserConfig(string fileName = "UserStories")
        {
            string configFileName = "user://" + fileName + ".ini";
            ConfigFile configFile = new ConfigFile();
            Directory dir = new Directory();
            dir.MakeDirRecursive(configFileName.GetBaseDir());
            configFile.SetValue("Workspace", "Directory", workspace);
            Error err = configFile.Save(configFileName);
            if (err != Error.Ok) { GD.Print(err); }
        }

        // Load Config file from "user://"
        public static void LoadUserConfig(string fileName = "UserStories")
        {
            string configFileName = "user://" + fileName + ".ini";
            ConfigFile configFile = new ConfigFile();
            Error err = configFile.Load(configFileName);
            if (err != Error.Ok) { GD.Print(err); }
            workspace = configFile.GetValue("Workspace", "Directory").ToString(); // Workscape
        }

        public static Story GetNamedStory(string name, SceneTree sceneTree)
        {
            if (Godot.Object.IsInstanceValid(sceneTree))
            {
                for (int i = 0; i < sceneTree.GetNodesInGroup("Story").Count; i++)
                {
                    Story s = sceneTree.GetNodesInGroup("Story")[i] as Story;
                    if (s.Name == name)
                    {
                        return s;
                    }
                }
            }
            return null;
        }
    }
}

/*
    UserStories.ini

    [Workspace]
    Directory "" ; Empty is Documents

    [Stories]
    RecentStory Directory

*/