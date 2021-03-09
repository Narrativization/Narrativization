using Godot;
using System;
using Godot.Collections;
using Storier;

// Create(or rename) FlowNode -> Generic Flow editor Node
public class StoryNode : GraphNode
{
    // Exports
    // [Export] // Export(Dictionary, string, NodePath)
    // private Dictionary<NodePath, string> editables = new Dictionary<NodePath, string>(); // NodePath: JSON.Direction  

    [Export] // Export(Dictionary, string, NodePath)
    private Array<NodePath> editables = new Array<NodePath>(); // NodePath: JSON.Direction  

    private string recentStory = "";
    private Dictionary storyData;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        try
        {
            recentStory = StoryManager.PopRecentStory();
            GD.Print(FileManager.LoadFile(recentStory));
            storyData = JSON.Parse(FileManager.LoadFile(recentStory)).Result as Dictionary;
            DisplayData();
        }
        catch (System.Exception err)
        {
            GD.Print(err);
            // throw;
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
                    Story loadedStory = StoryManager.LoadStory(recentStory);
                    StoryManager.flowName = loadedStory.Name;
                    GetParent<FlowGraphEditor>().GetParent<PanelContainer>().GetParent<FlowEditor>().RemoveFlowNodes();
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
