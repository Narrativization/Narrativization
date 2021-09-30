using Godot;
using System;
using System.Collections;
using Godot.Collections;

// Managing Story, StoryElement, TitleId | Singleton(AutoLoaded)
public class StoryManager : Node    
{
    // Story Manager
    private static StoryManager _StoryManager { get; set; } // Story Manager Singleton
    public static StoryManager CurrentStoryManager 
    {
        get { return _StoryManager; } 
    } 

    // Story
    private static Story _Story { get; set; } // Currently Edited Story 
    public static Story CurrentStory 
    {
        get { return _Story; } 
        set 
        {
            _Story = value; 
            _StoryManager.EmitSignal("OnStoryEdited", _Story); 
            CurrentSeid = "";
        }
    } 

    // Story Element
    private static StoryElement _StoryElement { get; set; } // Currently Edited Story Element
    public static StoryElement CurrentStoryElement 
    {
        get { return _StoryElement; } 
        set 
        {
            _StoryElement = value; 
            _StoryManager.EmitSignal("OnStoryElementEdited", _StoryElement); 
            CurrentSeid = IsInstanceValid(_StoryElement) ? _StoryElement.StoryElementId : "";
        }
    } 

    // Story Node
    private static StoryNode _StoryNode { get; set; } // Currently Edited Story Node
    public static StoryNode CurrentStoryNode 
    {
        get { return _StoryNode; } 
        set 
        {
            _StoryNode = value; 
            _StoryManager.EmitSignal("OnStoryNodeEdited", _StoryNode); 
        }
    } 

    // Story Fragment
    private static StoryFragment _StoryFragment { get; set; } // Currently Edited Story Fragment
    public static StoryFragment CurrentStoryFragment 
    {
        get { return _StoryFragment; } 
        set 
        {
            CurrentStoryFragment = value; 
            _StoryManager.EmitSignal("OnStoryFragmentEdited", _StoryFragment); 
        }
    } 

    // Seid/StoryElement Displayer | Navigation Bar
    private static string _Seid { get; set; } = ""; // Current Id of Selected/Editing Outline 
    public static string CurrentSeid 
    { 
        get { return _Seid; } 
        set 
        { 
            _Seid = CurrentStory.StoryName + value; 
            _StoryManager.EmitSignal("OnStoryElementIdEdited", _Seid); 
        } 
    }

    [Signal]
    public delegate void OnStoryCreated(Story story);
    [Signal]
    public delegate void OnStoryEdited(Story story);
    [Signal]
    public delegate void OnStoryElementCreated(StoryElement storyElement);
    [Signal]
    public delegate void OnStoryElementEdited(StoryElement storyElement);
    [Signal]
    public delegate void OnStoryNodeCreated(StoryNode storyNode);
    [Signal]
    public delegate void OnStoryNodeEdited(StoryNode storyNode);
    [Signal]
    public delegate void OnStoryFragmentCreated(StoryFragment storyFragment);
    [Signal]
    public delegate void OnStoryFragmentEdited(StoryFragment storyFragment);
    [Signal]
    public delegate void OnStoryElementIdEdited(string seid);

    private StoryManager()
    {
        _StoryManager = this;
    }
    
    public override void _Ready()
    {
        
    }

//  public override void _Process(float delta)
//  {
//      
//  }

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent.IsActionPressed("SaveAll"))
        {
            GD.Print("Save");
            if (IsInstanceValid(CurrentStory))
            {
                SaveStory();
            }
            if (IsInstanceValid(CurrentStoryElement))
            {
                CurrentStoryElement.Save();
            }
        }
    }

    // Create Story
    public static Story CreateStory(string name, string description, string directory, string author = "", string copyright = "")
    {
        Story story = new Story(name, description, directory, author, copyright);
        _StoryManager.EmitSignal("OnStoryCreated", story); 
        CurrentStory = story;
        CurrentStoryManager.AddChild(CurrentStory);
        SaveStory();
        return CurrentStory;
    }

    // Save Story (Only Story[.storier] File)
    public static Error SaveStory()
    {
        Dictionary storierData = new Dictionary() 
        {
            { "Story", CurrentStory.StoryData },
            { "Storier", StorierManager.StorierData }
        };
        Error isSuccess = FileManager.SaveFile(CurrentStory.StoryPath, JSON.Print(storierData, "\t"));
        if (isSuccess == Error.Ok) { StorierManager.WriteRecentStory(); } 
        return isSuccess;
    }

    // Load Story
    private static Error LoadStory(string fileName)
    {
        string content = "";
        Error err = FileManager.LoadFile(fileName, out content);
        if (err != Error.Ok) { GD.PrintErr(err); }
        JSONParseResult jsonObject = JSON.Parse(content);
        if(jsonObject.Error != 0) { GD.PrintErr( "Error:", jsonObject.ErrorLine); } 
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
                // CreateStory(storyInfos["Name"], storyInfos["Description"], fileName.GetBaseDir() + "/");
                CurrentStory.StoryData = storyInfos;
            }
            if (storyJson.Contains("Storier"))
            {
                Dictionary storierDic = storyJson["Storier"] as Dictionary; 
                Dictionary<string, string> storierInfos = new Dictionary<string, string>(storierDic);
                string sVersion = storierInfos["Version"] as string; 
                if (sVersion != StorierManager.StorierData["Version"]) { GD.Print(sVersion + " != " + StorierManager.StorierData["Version"] + "[Current] Storier Version!"); }
                else { GD.Print("Same Storier Version!"); }
                StorierManager.StorierData = storierInfos;
            }
        }
        return Error.Ok;
    }

    public static Story OpenStory(string directory)
    {
        CurrentStory = new Story();
        Error err = LoadStory(directory);
        return CurrentStory;
    }

    // Close Story
    public static Error CloseStory()
    {
        CurrentStory.Free();
        return Error.Ok;
    }

    public static void CloseAllStories()
    {
        Godot.Collections.Array _stories = _StoryManager.GetChildren();
        for (int i = 0; i < _stories.Count; i++)
        {
            if (_stories[i] is Story s)
            {
                s.Free();
            }
        }
    }
}

/*
    Create a project file structure | .storier
    {
        Story: {
            Name: "Storier",
            Description: "Story Managing Software for Game Creators.",
            Author: "Atakan Ateş",
            Copyright: "Copyright © Atakan Ateş, All Rights Reserved!"
            ...
        },
        Storier: {
            ...
            Version: "v<Major>.<Minor>"
        }
    }
*/
