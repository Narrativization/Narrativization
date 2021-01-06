using Godot;
using System;
using Godot.Collections;

namespace Storier
{
    // Creating, Storing and Managing Stories
    public static class StoryManager
    {
        public static Story story = new Story("Untitled");

        // Create New Story 
        public static bool CreateStory(StoryData _storyData)
        {
            story = new Story(_storyData);
            PushRecentStory(story.storyData.directory);
            // Story Configurations
            return true;
        }
        
        // Save Story
        public static bool SaveStory()
        {
            story.SaveStory();
            PushRecentStory(story.storyData.directory);
            return true;
        }

        // Load Story
        public static bool LoadStory(string fileName)
        {
            story.LoadStory(fileName);
            // Add Recently
            return true;
        }
        
        // Record Recently Story
        public static void PushRecentStory(string fileName)
        {
            // Record Recently
        }
        
        // Picking Recently(Last) Story
        public static bool PopRecentStory(string fileName)
        {
            // LoadStory();
            return true;
        }
        
        // Gettings All Recently Story as String
        public static Array<string> PeekRecentStory()
        {
            // CreatePopUpMenu
            // Select Recently
            // LoadStory();
            return new Array<string>();
        }

        private static void SaveConfigFile(string fileName = "UserConfiguration") // Save Config file to "user://", use as Storier(editor) settings
        {
            string configFileName =  fileName + ".ini";
            ConfigFile configFile = new ConfigFile();
            Directory dir = new Directory();
            dir.MakeDirRecursive(fileName.GetBaseDir());
            // configFile.SetValue("Stories", "RecentStories", /*Load.Add*/"Load then add");
            Error err = configFile.Save(configFileName);
            if (err != Error.Ok)
            {
                GD.Print(err);
            }
        }

        public static void LoadConfigFile(string fileName = "UserConfiguration")
        {
            string configFileName = fileName + ".ini";
            ConfigFile configFile = new ConfigFile();
            configFile.Load(configFileName);
            // story.storyName = configFile.GetValue("Stories", "RecentStories").ToString();
        }

    }
}

/*
    UserStories.ini ~.cfg

    [Workspace]
    Directory [] ;Empty is Documents ~UserPersist

    [Stories]
    RecentStories [Story1, Story2, Story3]

    [StoryDirectories]
    Story1 Directory
    Story2 Directory1
    Story3 Directory2

*/