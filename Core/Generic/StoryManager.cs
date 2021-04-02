using Godot;
using System;
using System.Collections;
using Godot.Collections;

namespace Storier
{
    // Managing Settings and recents of Stories | Singleton(AutoLoaded)
    public class StoryManager : Node    
    {
        // Singleton
        private static StoryManager storyManager;
        public static Story CurrentStory { get; set; }

        public static string workspace = OS.GetSystemDir(OS.SystemDir.Documents) + "/Storier/";
        public static string StorierVersion = "v0.2";

        private StoryManager()
        {
            storyManager = this;
        }

        // Create Story
        public static Story CreateStory(string _name, string _description, string _directory)
        {
            CloseAllStories(); // Single Story Workflow
            CurrentStory = new Story(_name, _description, _directory);
            storyManager.AddChild(CurrentStory);
            if (SaveStory()) { CurrentStory.CreateHierarchy(); } 
            return CurrentStory;
        }

        // Save Story
        public static bool SaveStory()
        {
            Dictionary storierData = new Dictionary() {
                {
                    "Story", CurrentStory.StoryData
                },
                {
                    "Storier", new Dictionary(){
                        {"Version", StorierVersion}
                    }
                }
            };
            bool isSuccess = FileManager.SaveFile(
                CurrentStory.StoryDirectory + CurrentStory.StoryName + ".storier", 
                JSON.Print(storierData, "\t"));
            if (isSuccess) { PushRecentStory(); } 
            return isSuccess;
        }

        // Load Story
        public static bool LoadStory(string fileName)
        {
            string str = FileManager.LoadFile(fileName);
            JSONParseResult jsonObject = JSON.Parse(str);

            if(jsonObject.Error != 0) { GD.Print("Error:", jsonObject.ErrorLine); } 
            else
            {
                // GD.Print(JSON.Print(jsonObject.Result));
                Dictionary storyJson = jsonObject.Result as Dictionary; // Godot.Collections.Array  <-  [] 
                GD.Print(JSON.Print(storyJson, "\t"));
                
                if (storyJson.Contains("Story"))
                {
                    // Dictionary<string, string> storyInfos = storyJson["Story"] as Dictionary<string, string>;
                    Dictionary storyDic = storyJson["Story"] as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
                    Dictionary<string, string> storyInfos = new Dictionary<string, string>(storyDic);
                    CreateStory(storyInfos["Name"], storyInfos["Description"], fileName.GetBaseDir() + "/");
                    CurrentStory.StoryData = storyInfos;
                }
                if (storyJson.Contains("Storier"))
                {
                    Dictionary storierInfos = storyJson["Storier"] as Dictionary; 
                    string sVersion = storierInfos["Version"] as string; 
                    if (sVersion != StorierVersion) { GD.Print(sVersion + " != " + StorierVersion + "[Current] Storier Version!"); }
                    else { GD.Print("Same Storier Version!"); }
                }
            }
            return true;
        }

        public static void CloseAllStories()
        {
            Godot.Collections.Array _stories = storyManager.GetChildren();
            for (int i = 0; i < _stories.Count; i++)
            {
                if (_stories[i] is Story s)
                {
                    s.Free();
                }
            }
        }

        // Record Recently Story
        public static void PushRecentStory()
        {
            string configFileName = "user://UserStories.ini";
            ConfigFile configFile = new ConfigFile();
            Directory dir = new Directory();
            dir.MakeDirRecursive(configFileName.GetBaseDir());
            configFile.SetValue("Stories", "RecentStory", CurrentStory.StoryDirectory + CurrentStory.StoryName + ".storier");
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
    }
}

/*
    UserStories.ini

    [Workspace]
    Directory "" ; Empty is Documents

    [Stories]
    RecentStory Directory

*/
