using Godot;
using System;
using Godot.Collections;

public class FlowEditor : GraphNodeEditor
{
    // Exports
    [Export]
    private PackedScene flowContextMenuScene = GD.Load<PackedScene>("res://GUI/Contexts/FlowContextMenu.tscn");
    [Export]
    private PackedScene storyNode = GD.Load<PackedScene>("res://GUI/Nodes/StoryNode.tscn");
    [Export]
    private PackedScene storyFlowNodeScene = GD.Load<PackedScene>("res://GUI/Nodes/StoryFlowFragment.tscn");


    private FlowContextMenu contextMenu;

    public override void _Ready()
    {                
        // AddToGroup("FlowEditor");
        contextMenu = flowContextMenuScene.Instance<FlowContextMenu>();
        AddChild(contextMenu);

        string recentStory = StoryManager.PopRecentStory();
        Directory dir = new Directory(); 
        if (dir.FileExists(recentStory))
        {
            AddChild(storyNode.Instance());
        }

        Connect("node_selected", StoryManager.Self, "_OnNodeSelected");
        base._Ready(); 
    }


    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    public override void _GuiInput(InputEvent inputEvent)
    {
        // Flow Editor Inputs
        if (IsInstanceValid(StoryManager.CurrentStory) && // if story exist, show context menu 
            inputEvent.IsActionPressed("Context"))
        {
            InputEventMouse mouse = inputEvent as InputEventMouse;
            contextMenu.SetPosition(mouse.GlobalPosition);
            contextMenu.ShowModal();
        }
        else if (IsInstanceValid(StoryManager.CurrentStory) && // if story exist, show context menu 
            inputEvent.IsActionPressed("New"))
        {
            contextMenu.SetPosition(GetGlobalMousePosition());
            contextMenu.ShowModal();
            contextMenu.GrabFocus();
        }
        else if (inputEvent.IsActionPressed("Context") || inputEvent.IsActionPressed("New")) // No Story
        {
            ModalManager.Modals.ShowCreateModal();
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
