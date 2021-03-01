using Godot;
using System;

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
        flowGraphEditor.AddChild(storyNode.Instance());
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
