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
        private static StoryManager _StoryManager;
        public static StoryManager Self { get { return _StoryManager; } }
        public static Story CurrentStory { get; set; }

        public static string workspace = OS.GetSystemDir(OS.SystemDir.Documents) + "/Storier/";
        public static string userDataDirectory = OS.GetUserDataDir(); // "user://"
        // Custom Story Data
        public static Dictionary<string, string> StorierData { get; set; } = new Dictionary<string, string>()
        {
            {"Version", "v0.2"}
        }; 

        [Signal]
        public delegate void StoryOpened(Story story);
        [Signal]
        public delegate void StoryElementOpened(StoryElement storyElement);
        [Signal]
        public delegate void StoryFragmentSelected(StoryFlowNode storyFlowNode);

        private StoryManager()
        {
            _StoryManager = this;
        }

        // Create Story
        public static Story CreateStory(string name, string description, string directory)
        {
            CloseAllStories(); // Single Story Workflow
            CurrentStory = new Story(name, description, directory);
            _StoryManager.AddChild(CurrentStory);
            if (!(new Directory()).DirExists(directory)) { CurrentStory.CreateHierarchy(); } 
            SaveStory();
            _StoryManager.EmitSignal("StoryOpened", CurrentStory);
            CurrentStory.Connect("StoryElementOpened", _StoryManager, "_OnStoryElementOpened");
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
                    "Storier", StorierData
                }
            };
            bool isSuccess = FileManager.SaveFile(
                CurrentStory.StoryDirectory + CurrentStory.StoryName + ".storier", 
                JSON.Print(storierData, "\t"));
            if (isSuccess) { PushRecentStory(); } 
            if (IsInstanceValid(CurrentStory) && IsInstanceValid(CurrentStory.CurrentStoryElement)) { CurrentStory.CurrentStoryElement.Save(); }
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
                    Dictionary storierDic = storyJson["Storier"] as Dictionary; 
                    Dictionary<string, string> storierInfos = new Dictionary<string, string>(storierDic);
                    string sVersion = storierInfos["Version"] as string; 
                    if (sVersion != StorierData["Version"]) { GD.Print(sVersion + " != " + StorierData["Version"] + "[Current] Storier Version!"); }
                    else { GD.Print("Same Storier Version!"); }
                    StorierData = storierInfos;
                }
            }
            return true;
        }

        public static void CloseAllStories()
        {
            Godot.Collections.Array _stories = _StoryManager.GetChildren();
            for (int i = 0; i < _stories.Count; i++)
            {
                if (_stories[i] is Story s)
                {
                    s.Disconnect("StoryElementOpened", _StoryManager, "_OnStoryElementOpened");
                    s.Free();
                }
            }
        }

        // Record Recently Story
        public static void PushRecentStory()
        {
            string configFileName = userDataDirectory + "UserStories.ini";
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
            string configFileName = userDataDirectory + "UserStories.ini";
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

        // Save Config file use as UserStories settings
        public static void SaveUserConfig(string fileName = "UserStories")
        {
            string configFileName = userDataDirectory + fileName + ".ini";
            ConfigFile configFile = new ConfigFile();
            Directory dir = new Directory();
            dir.MakeDirRecursive(configFileName.GetBaseDir());
            configFile.SetValue("Workspace", "Directory", workspace);
            Error err = configFile.Save(configFileName);
            if (err != Error.Ok) { GD.Print(err); }
        }

        // Load Config file
        public static void LoadUserConfig(string fileName = "UserStories")
        {
            string configFileName = userDataDirectory + fileName + ".ini";
            ConfigFile configFile = new ConfigFile();
            Error err = configFile.Load(configFileName);
            if (err != Error.Ok) { GD.Print(err); }
            workspace = configFile.GetValue("Workspace", "Directory").ToString(); // Workscape
        }

        // Story Element Handler/Listener
        private void _OnStoryElementOpened(StoryElement storyElement)
        {
            _StoryManager.EmitSignal("StoryElementOpened", storyElement);
        }

        // Story Element Handler/Listener
        private void _OnNodeSelected(Node node)
        {
            if (node is StoryFlowNode storyFlowNode)
            {
                _StoryManager.EmitSignal("StoryFragmentSelected", storyFlowNode);
            }
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
