using Godot;
using System;
using Godot.Collections;
using Storier;

public class FlowEditor : Control
{
    // Exports
    [Export]
    private NodePath flowGraphEditorPath = new NodePath("./Background/FlowGraphEditor");
    [Export]
    PackedScene storyNode = GD.Load<PackedScene>("res://GUI/Nodes/StoryNode.tscn");

    private FlowGraphEditor flowGraphEditor;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        flowGraphEditor = GetNode<FlowGraphEditor>(flowGraphEditorPath);
        
        string recentStory = StoryManager.PopRecentStory();
        Directory dir = new Directory(); 
        if (dir.FileExists(recentStory))
        {
            flowGraphEditor.AddChild(storyNode.Instance());
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    // Removes All child Graph Nodes
    public void RemoveFlowNodes()
    {
        Godot.Collections.Array children = flowGraphEditor.GetChildren();
        for (int i = 0; i < children.Count; i++)
        {
            if (children[i] is GraphNode c)
            {
                c.QueueFree();
            }
        }
    }


}
