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
    private PackedScene storyFlowNodeScene = GD.Load<PackedScene>("res://GUI/Nodes/StoryFlowNode.tscn");


    private FlowContextMenu contextMenu;

    public override void _Ready()
    {        
        base._Ready(); 
        
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
        if (inputEvent.IsActionReleased("Context"))
        {
            InputEventMouse mouse = inputEvent as InputEventMouse;
            contextMenu.SetPosition(mouse.GlobalPosition);
            contextMenu.ShowModal();
        }
        if (inputEvent.IsActionPressed("Delete"))
        {
            GD.Print("Delete Selected Nodes");
            DeleteSelected();
        }    

        if (inputEvent.IsActionPressed("Backward"))
        {
            GD.Print("Previous Flow");
        }
        else if (inputEvent.IsActionPressed("Forward"))
        {
            GD.Print("Next Flow");
        }
        
        
        if (inputEvent.IsActionPressed("Enter"))
        {
            GD.Print("Enter");
            Godot.Collections.Array selected = GetSelected();
            if (selected.Count == 1)
            {
                GraphNode graphNode = selected[0] as GraphNode;
                // graphNode.Open();
                GD.Print(graphNode.Name);
            }
        }

        base._GuiInput(inputEvent);
    }

    int ind = 1;
    public bool CreateFlowNode()
    {
        GD.Print("Create Story Flow Node!");
        StoryFlowNode storyFlowNode = storyFlowNodeScene.Instance() as StoryFlowNode;
        storyFlowNode.InitNode("/Flows/Chapter" + ind++);
        // StoryFlowNode storyFlowNode = new StoryFlowNode("/Flows/Chapter1");
        AddChild(storyFlowNode);
        GD.Print("AddChild Story Flow Node!");
        return true;
    }
}
