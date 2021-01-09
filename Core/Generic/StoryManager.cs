using Godot;
using System;
using Godot.Collections;

namespace Storier
{
    // Managing Settings and recents of Stories
    public static class StoryManager
    {
        public static string workspace = OS.GetSystemDir(OS.SystemDir.Documents) + "/Storier/";

        // Record Recently Story
        public static void PushRecentStory()
        {
            string configFileName = "user://UserStories.ini";
            ConfigFile configFile = new ConfigFile();
            Directory dir = new Directory();
            dir.MakeDirRecursive(configFileName.GetBaseDir());
            configFile.SetValue("Stories", "RecentStory", Story.story.storyData.directory);
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