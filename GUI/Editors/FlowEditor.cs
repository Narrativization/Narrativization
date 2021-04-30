using Godot;
using System;
using Godot.Collections;
using Storier;

public class FlowEditor : GraphNodeEditor
{
    // Exports
    [Export]
    private NodePath flowContextMenuPath = new NodePath("./FlowContext");
    [Export]
    private PackedScene storyNode = GD.Load<PackedScene>("res://GUI/Nodes/StoryNode.tscn");
    [Export]
    private PackedScene storyFlowNodeScene = GD.Load<PackedScene>("res://GUI/Nodes/StoryFlowFragment.tscn");


    private FlowContextMenu contextMenu;

    public override void _Ready()
    {        
        base._Ready(); 
        
        // AddToGroup("FlowEditor");
        contextMenu = GetNode<FlowContextMenu>("./FlowContext");

        string recentStory = StoryManager.PopRecentStory();
        Directory dir = new Directory(); 
        if (dir.FileExists(recentStory))
        {
            AddChild(storyNode.Instance());
        }
    }


    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override void _GuiInput(InputEvent inputEvent)
    {
        // Flow Editor Inputs
        if (inputEvent.IsActionPressed("Context"))
        {
            InputEventMouse mouse = inputEvent as InputEventMouse;
            contextMenu.SetPosition(mouse.GlobalPosition);
            contextMenu.ShowModal();
        }
        else if (inputEvent.IsActionPressed("New"))
        {
            contextMenu.SetPosition(GetGlobalMousePosition());
            contextMenu.ShowModal();
            contextMenu.GrabFocus();
        }

        if (inputEvent.IsActionPressed("Delete"))
        {
            GD.Print("Delete Selected Nodes");
            DeleteSelected();
        }    

        if (inputEvent.IsActionPressed("Duplicate"))
        {
            GD.Print("Duplicate Selected Nodes");
            DuplicateSelected();
        }   

        if (inputEvent.IsActionPressed("Backward"))
        {
            GD.Print("Previous Flow");
        }
        else if (inputEvent.IsActionPressed("Forward"))
        {
            GD.Print("Next Flow");
        }
        
        if (inputEvent.IsActionPressed("Open"))
        {
            GD.Print("Open");
            Godot.Collections.Array selected = GetSelected();
            if (selected.Count == 1 && selected[0] is FlowGraphNode flowGraphNode)
            {
                flowGraphNode.OpenNode();
                GD.Print(flowGraphNode.Name);
            }
        }
        base._GuiInput(inputEvent);
    }

    int ind = 1;
    public bool CreateFlowNode()
    {
        GD.Print("Create Story Flow Node");
        StoryFlowNode storyFlowNode = storyFlowNodeScene.Instance() as StoryFlowNode;
        // storyFlowNode.InitNode("/Flows/Chapter" + ind++); // StoryFlow.Load(StoryManager.CurrentStory.StoryBase + path)
        StoryFlow storyFlow = storyFlowNode.ReferenceStoryFlow(new StoryFlow(StoryManager.CurrentStory.StoryBase + "/Flows/Chapter" + ind++ + ".flow"));
        AddChild(storyFlowNode);
        storyFlowNode.Offset = ScrollOffset + GetLocalMousePosition() - (storyFlowNode.RectSize/2);
        return true;
    }

    public Dictionary<string, string> SaveData()
    {
        Dictionary<string, string> saveData = new Dictionary<string, string>();
        Godot.Collections.Array graphNodes = GetChildren();
        for (int i = 0; i < graphNodes.Count; i++)
        {
            if (graphNodes[i] is FlowGraphNode flowNode)
            {
                saveData.Add(flowNode.FragmentId, "");  // flowNode.flowData
                // saveData.Add(flowNode.FragmentId, flowNode.Offset);  // flowNode.flowData
            }
        }
        return saveData;
    }

    public void LoadData(Dictionary<string, string> loadData)
    {
        
    }
}
