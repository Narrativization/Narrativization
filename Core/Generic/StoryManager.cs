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
            _StoryManager.EmitSignal("StoryChanged", _Story); 
            CurrentTitleId = "";
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
            _StoryManager.EmitSignal("StoryElementChanged", _StoryElement); 
            CurrentTitleId = IsInstanceValid(_StoryElement) ? _StoryElement.StoryElementId : "";
        }
    } 

    // Title Displayer
    private static string _TitleId { get; set; } // Current Id of Selected/Editing Outline 
    public static string CurrentTitleId 
    { 
        get { return _TitleId; } 
        set 
        { 
            _TitleId = CurrentStory.StoryName + value; 
            _StoryManager.EmitSignal("TitleIdChanged", _TitleId); 
        } 
    }

    // // Story Fragment
    // private static StoryElement _StoryFragment { get; set; } // Currently Edited Story Element
    // public static StoryElement CurrentStoryFragment 
    // {
    //     get { return _StoryFragment; } 
    //     set 
    //     {
    //         _StoryFragment = value; 
    //         _StoryManager.EmitSignal("FragmentChanged", _StoryFragment); 
    //     }
    // } 

    [Signal]
    public delegate void StoryChanged(Story story);
    [Signal]
    public delegate void StoryElementChanged(StoryElement storyElement);
    [Signal]
    public delegate void TitleIdChanged(string titleId);
    // [Signal]
    // public delegate void FragmentChanged(string titleId);

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
        if (inputEvent.IsActionPressed("Save"))
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

            // if (IsInstanceValid(CurrentStoryElement))
            // {
            //     Editors editors = GetTree().GetNodesInGroup("Editors")[0] as Editors;
            //     CurrentStoryElement.StoryElementData["Flows"] = JSON.Print(editors.flowEditor.SaveData(), "\t");
            //     CurrentStoryElement.Save();
            // }
        }
    }

    // Create Story
    public static Story CreateStory(string name, string description, string directory)
    {
        CloseAllStories(); // Single Story Workflow
        CurrentStory = new Story(name, description, directory);
        CurrentStoryManager.AddChild(CurrentStory);
        SaveStory();
        CurrentStoryManager.EmitSignal("StoryChanged", CurrentStory);
        return CurrentStory;
    }

    // Save Story (Only Story[.storier] File)
    public static bool SaveStory()
    {
        Dictionary storierData = new Dictionary() 
        {
            { "Story", CurrentStory.StoryData },
            { "Storier", StorierManager.StorierData }
        };
        bool isSuccess = FileManager.SaveFile(CurrentStory.StoryPath, JSON.Print(storierData, "\t"));
        if (isSuccess) { StorierManager.WriteRecentStory(); } 
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
                if (sVersion != StorierManager.StorierData["Version"]) { GD.Print(sVersion + " != " + StorierManager.StorierData["Version"] + "[Current] Storier Version!"); }
                else { GD.Print("Same Storier Version!"); }
                StorierManager.StorierData = storierInfos;
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
                s.Free();
            }
        }
    }
}

/*
    Create a project file structure | .storier
    {
        Story: {
            Name: "Storier",  // Title
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
