using Godot;
using System;
using System.Collections;
using Godot.Collections;

// Managing Settings and Recents of Stories | Singleton(AutoLoaded)
public class StorierManager : Node
{
    // Singleton
    private static StorierManager _StorierManager;

    // User Directories
    public static string workspace = OS.GetSystemDir(OS.SystemDir.Documents) + "/Storier/";
    public static string userDataDirectory = OS.GetUserDataDir() + "/"; // "user://"
    
    // Custom Storier Data
    public static Dictionary<string, string> StorierData { get; set; } = new Dictionary<string, string>()
    {
        {"Version", "v0.3"}
    }; 
    

    public StorierManager()
    {
        _StorierManager = this;
    }

    public override void _Ready()
    {

    }

//  public override void _Process(float delta)
//  {
//      
//  }

    // Generic/Global Inputs
    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("Help"))
        {
            GD.Print("Help");
            GD.Print(StorierManager.userDataDirectory);
            GD.Print(StorierManager.userDataDirectory + "UserStories.ini");
        }
        else if (inputEvent.IsActionPressed("Fullscreen"))
        {
            GD.Print("Fullscreen");
            OS.WindowFullscreen = !OS.WindowFullscreen;
        }
    }

    // Recording/Writing Recently Story
    public static void WriteRecentStory()
    {
        string configFileName = StorierManager.userDataDirectory + "UserStories.ini";
        ConfigFile configFile = new ConfigFile();
        Directory dir = new Directory();
        dir.MakeDirRecursive(configFileName.GetBaseDir());
        configFile.SetValue("Stories", "RecentStory", StoryManager.CurrentStory.StoryPath);
        Error err = configFile.Save(configFileName);
        if (err != Error.Ok) { GD.Print(err); }
    }
    
    // Peeking/Read Recently Story
    public static string ReadRecentStory()
    {
        string configFileName = StorierManager.userDataDirectory + "UserStories.ini";
        ConfigFile configFile = new ConfigFile();
        Error err = configFile.Load(configFileName);
        if (err != Error.Ok) { GD.Print(err); return ""; }
        return configFile.GetValue("Stories", "RecentStory").ToString(); // RecentStory
    }

    //  - !WARNING: Change Workspace isn't in use! - 

    // Save Config file use as UserStories settings
    public static void SaveUserConfig(string fileName = "UserSettings")
    {
        string configFileName = StorierManager.userDataDirectory + fileName + ".ini";
        ConfigFile configFile = new ConfigFile();
        Directory dir = new Directory();
        dir.MakeDirRecursive(configFileName.GetBaseDir());
        configFile.SetValue("Workspace", "Directory", StorierManager.workspace);
        Error err = configFile.Save(configFileName);
        if (err != Error.Ok) { GD.Print(err); }
    }

    // Load Config file
    public static void LoadUserConfig(string fileName = "UserSettings")
    {
        string configFileName = StorierManager.userDataDirectory + fileName + ".ini";
        ConfigFile configFile = new ConfigFile();
        Error err = configFile.Load(configFileName);
        if (err != Error.Ok) { GD.Print(err); }
        StorierManager.workspace = configFile.GetValue("Workspace", "Directory").ToString(); // Workscape
    }
}


/*
    UserStories.ini

    [Stories]
    RecentStory Directory

    ---

    UserSettings.ini
    
    [Workspace]
    Directory "" ; Empty is Documents

*/