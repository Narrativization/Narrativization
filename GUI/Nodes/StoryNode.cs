using Godot;
using System;
using Godot.Collections;

// Story Node
public class StoryNode : FlowGraphNode
{
    [Export] // Export(Dictionary, string, NodePath)
    private Array<NodePath> editables = new Array<NodePath>(); // NodePath: JSON.Direction  

    private string recentStory = "";
    private Dictionary storyData;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        recentStory = StorierManager.ReadRecentStory();
        Directory dir = new Directory(); 
        if (dir.FileExists(recentStory))
        {
            GD.Print("Recent Story: " + recentStory);
            storyData = JSON.Parse(FileManager.LoadFile(recentStory)).Result as Dictionary;
            DisplayData();
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public override void _GuiInput(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton eventMouseButton)
        {
            if (eventMouseButton.Doubleclick)
            {
                if (recentStory != "")
                {
                    GD.Print("Load Story");
                    if (StoryManager.LoadStory(recentStory))
                    {
                        GetParent<FlowEditor>().DeleteAll();
                        // StoryManager.CurrentStory.CurrentStoryElement = StoryFlow.Load();
                    } 
                }
            }
        }
    }

    public void DisplayData()
    {
        for (int i = 0; i < editables.Count; i++)
        {
            if (GetNode(editables[i]) is Label lbl)
            {
                Dictionary _story = (Dictionary)storyData["Story"];
                lbl.Text = _story[lbl.Name as string] as string;
            }
        }
    }
}
