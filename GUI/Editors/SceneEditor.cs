using Godot;
using System;

public class SceneEditor : ScrollContainer
{
    [Export]
    private NodePath sceneNodePath = new NodePath("./Scene");
    [Export]
    private PackedScene sceneBlockPack = GD.Load<PackedScene>("res://GUI/Blocks/SceneBlock.tscn");

    private VBoxContainer storyScenes;

    public override void _Ready()
    {
        storyScenes = GetNode<VBoxContainer>(sceneNodePath);
    }

//  public override void _Process(float delta)
//  {
//      
//  }

    public void Reset()
    {
        for (int i = 0; i < storyScenes.GetChildren().Count; i++)
        {
            storyScenes.GetChild(i).Name += "Deleted";
            storyScenes.GetChild(i).QueueFree();
        }
    }

    public void StartFromNode(GraphNode selectedNode)
    {
        Reset();
        if (selectedNode is FlowNode flowNode)
        {
            AddSceneBlock(flowNode);
        }
    }

    public void AddSceneBlock(FlowNode flowNode)
    {
        SceneBlock sceneBlock = sceneBlockPack.Instance() as SceneBlock;
        storyScenes.AddChild(sceneBlock);
        GD.Print("Start");
        GD.Print("flowNode.NodeFragment" + flowNode.NodeFragment);
        GD.Print("End");
        sceneBlock.Fragment = flowNode.NodeFragment;
    }

}
