using Godot;
using System;
using Godot.Collections;

public class FlowEditor : FlowGraphEditor
{
    // Exports
    [Export]
    private PackedScene flowContextMenuScene = GD.Load<PackedScene>("res://GUI/Contexts/FlowContextMenu.tscn");
    [Export]
    private PackedScene storyNode = GD.Load<PackedScene>("res://GUI/Nodes/StoryNode.tscn");


    private FlowContextMenu contextMenu;

    public override void _Ready()
    {                
        contextMenu = flowContextMenuScene.Instance<FlowContextMenu>();
        AddChild(contextMenu);

        string recentStory = StorierManager.ReadRecentStory();
        Directory dir = new Directory(); 
        if (dir.FileExists(recentStory))
        {
            AddChild(storyNode.Instance());
        }

        Connect("node_selected", this, "_OnNodeSelected");
        Connect("node_unselected", this, "_OnNodeUnselected");
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
        
        if (inputEvent.IsActionPressed("Open"))
        {
            GD.Print("Open");
            Godot.Collections.Array<GraphNode> selected = GetSelectedNodes();
            if (selected.Count == 1 && selected[0] is FlowGraphNode flowGraphNode)
            {
                flowGraphNode.OpenNode();
                GD.Print(flowGraphNode.Name);
            }
        }

        if (inputEvent.IsActionPressed("Help"))
        {
            GD.Print("Editor Help");
            Godot.Collections.Array allNodes = GetChildren();
            Godot.Collections.Array graphNodes = new Godot.Collections.Array();
            for (int i = 0; i < allNodes.Count; i++)
            {
                if (allNodes[i] is GraphNode graphNode)
                {
                    graphNodes.Add(graphNode);
                }
            }
            GD.Print(graphNodes);
            GD.Print(GetConnectionList());
        }
        
        base._GuiInput(inputEvent);
    }

    public void _OnNodeSelected(Node node)
    {
        StoryManager.CurrentFlowNode = node as FlowGraphNode;
    }

    public void _OnNodeUnselected(Node node)
    {
        if (StoryManager.CurrentFlowNode == node as FlowGraphNode)
        {
            StoryManager.CurrentFlowNode = null;
        }
    }
}
