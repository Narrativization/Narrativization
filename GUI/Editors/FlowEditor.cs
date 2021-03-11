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


    private FlowContextMenu contextMenu;

    public override void _Ready()
    {        
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

        if (inputEvent.IsActionPressed("Backward"))
        {
            GD.Print("Previous Flow");
        }
        else if (inputEvent.IsActionPressed("Forward"))
        {
            GD.Print("Next Flow");
        }

        base._GuiInput(inputEvent);
    }

    // Removes All child Graph Nodes
    public void RemoveFlowNodes()
    {
        Godot.Collections.Array children = GetChildren();
        for (int i = 0; i < children.Count; i++)
        {
            if (children[i] is GraphNode c)
            {
                c.QueueFree();
            }
        }
    }


}
