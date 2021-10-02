using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

public class NodeEditor : FlowGraphEditor
{
    // Exports
    [Export(PropertyHint.File, "*.tscn,*.scn")]
    private string  nodeEditorContextMenuPath = "res://GUI/Contexts/FlowContextMenu.tscn";
    [Export(PropertyHint.File, "*.tscn,*.scn")]
    private string  storyNodePath = "res://GUI/Nodes/StoryNode.tscn";

    private FlowContextMenu contextMenu;
    private PackedScene storyNode;

    public override void _Ready()
    {                
        PackedScene flowContextMenuScene = GD.Load<PackedScene>(nodeEditorContextMenuPath);
        contextMenu = flowContextMenuScene.Instance<FlowContextMenu>();
        AddChild(contextMenu);

        storyNode = GD.Load<PackedScene>(storyNodePath);
        AddChild(storyNode.Instance());

        Connect("node_selected", this, "_OnNodeSelected");
        Connect("node_unselected", this, "_OnNodeUnselected");
        Connect("_begin_node_move", this, "_OnBeginNodeMove");
        // Connect("_end_node_move", this, "_OnEndNodeMove");
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

    public override void Reset() { base.Reset(); }

    public bool CreateFlowNode(string nodeType = "res://GUI/Nodes/FlowNode.tscn")
    {
        GD.Print("Create Story Flow Node");
        PackedScene storyFlowNodeScene = GD.Load<PackedScene>(nodeType);
        StoryNode storyNode = storyFlowNodeScene.Instance() as StoryNode;
        AddChild(storyNode);
        storyNode.Offset = ScrollOffset + GetLocalMousePosition() - (storyNode.RectSize/2);
        return true;
    }

    public bool CreateFlowNode(Dictionary<string, object> nodeData)
    {
        GD.Print("Create Story Flow Node");
        PackedScene storyFlowNodeScene = GD.Load<PackedScene>(nodeData["Type"] as string);
        StoryNode flowGraphNode = storyFlowNodeScene.Instance() as StoryNode;
        // flowGraphNode.CreateFlowNode(nodeData);
        AddChild(flowGraphNode);
        return true;
    }

    public void _OnNodeSelected(Node node)
    {
        if (node is StoryNode)
        {
            StoryManager.CurrentStoryNode = node as StoryNode;
        }
    }

    public void _OnNodeUnselected(Node node)
    {
        if (node is StoryNode)
        {
            StoryManager.CurrentStoryNode = null;
        }
    }

    public void _OnBeginNodeMove()
    {
        TitleDisplayer displayer = GetTree().GetNodesInGroup("SeidDisplayer")[0] as TitleDisplayer;
        if (IsInstanceValid(displayer)) { displayer.isModified = true; displayer.UpdateTitle(); }
    }

    public void _OnEndNodeMove() {  }
}
