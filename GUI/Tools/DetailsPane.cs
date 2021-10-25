using Godot;
using System;

public class DetailsPane : Panel
{
    [Export]
    NodePath fragmentDetailsPath = "ScrollContainer/VBoxContainer/Fragment";
    [Export]
    NodePath nodeDetailsPath = "ScrollContainer/VBoxContainer/Node";
    // [Export]
    // NodePath detailTextPath = "ScrollContainer/VBoxContainer/Node/DetailsText";
    

    private VBoxContainer fragmentDetails;
    private VBoxContainer nodeDetails;

    private StoryNode DraggedFlowNode;

    public override void _Ready()
    {
        StoryManager.CurrentStoryManager.Connect("OnStoryEdited", this, "_OnStoryEdited");
        StoryManager.CurrentStoryManager.Connect("OnStoryElementEdited", this, "_OnStoryElementEdited");
        StoryManager.CurrentStoryManager.Connect("OnStoryNodeEdited", this, "_OnStoryNodeEdited");

        fragmentDetails = GetNode<VBoxContainer>(fragmentDetailsPath);
        nodeDetails = GetNode<VBoxContainer>(nodeDetailsPath);
    }

    public override void _Process(float delta)
    {
        
    }

    public void CreateFragmentDetails(StoryNode flowNode)
    {
        DeleteDetails(fragmentDetails);
        // if (!IsInstanceValid(flowNode) || !IsInstanceValid(flowNode.NodeFragment)) { return; }
        // StoryFragment fragment = flowNode.NodeFragment;
        // foreach (var nodeData in fragment.FragmentSaveData)
        // {
        //     // GD.Print("NodeDetail: " + nodeData.Value.GetType());
        //     Variant.Type variantType = Variant.Type.String;
        //     if (nodeData.Value.GetType() == typeof(String)) { variantType = Variant.Type.String; }
        //     else if (nodeData.Value.GetType() == typeof(int)) { variantType = Variant.Type.Int; } 
        //     else if (nodeData.Value.GetType() == typeof(Vector2)) { variantType = Variant.Type.Vector2; }
        //     else if (nodeData.Value.GetType() == typeof(Enum)) { /* Create Enum Dropdown */ }
        //     DetailsProperty detailsProperty = new DetailsProperty();
        //     detailsProperty.CreateProperty(nodeData.Key, nodeData.Value, variantType, nodeData.Key); 
        //     detailsProperty.Connect("PropertyUpdated", this, "_OnFragmentPropertyUpdated");
        //     fragmentDetails.AddChild(detailsProperty);
        // }
    }

    public void CreateNodeDetails(StoryNode flowNode)
    {
        DeleteDetails(nodeDetails);
        if (!IsInstanceValid(flowNode)) { return; }
        foreach (var nodeData in flowNode.NodeData)
        {
            // GD.Print("NodeDetail: " + nodeData.Value.GetType());
            Variant.Type variantType = Variant.Type.String;
            if (nodeData.Value.GetType() == typeof(String)) { variantType = Variant.Type.String; }
            else if (nodeData.Value.GetType() == typeof(int)) { variantType = Variant.Type.Int; } 
            else if (nodeData.Value.GetType() == typeof(Vector2)) { variantType = Variant.Type.Vector2; }
            else if (nodeData.Value.GetType() == typeof(Enum)) { /* Create Enum Dropdown */ }
            DetailsProperty detailsProperty = new DetailsProperty();
            detailsProperty.CreateProperty(nodeData.Key, nodeData.Value, variantType, nodeData.Key, false); 
            detailsProperty.Connect("PropertyUpdated", this, "_OnNodePropertyUpdated");
            nodeDetails.AddChild(detailsProperty);
        }
    }

    public void DeleteDetails(Node detailContainer)
    {
        foreach (Node n in detailContainer.GetChildren())
        {
            n.Name += "Deleted";
            n.QueueFree();
        }
    }

    public void _OnFragmentPropertyUpdated(string key, string value, Variant.Type type)
    {
        GD.Print(key + "  _OnPropertyUpdated: " + value);
        if(IsInstanceValid(DraggedFlowNode)) 
        { 
            // DraggedFlowNode.NodeFragment.StoryFragmentData[key] = value;
            DraggedFlowNode._Draw();
        } 
    }

    public void _OnNodePropertyUpdated(string key, string value, Variant.Type type)
    {
        GD.Print(key + "  _OnPropertyUpdated: " + value);

    }

    public void _OnStoryEdited(Story story)
    {
        if (!IsInstanceValid(story)) { return; }
    }

    public void _OnStoryElementEdited(StoryElement storyElement)
    {
        if (!IsInstanceValid(storyElement)) { return; }
    }

    public void _OnStoryNodeEdited(StoryNode flowNode)
    {
        if (!IsInstanceValid(flowNode)) { DeleteDetails(fragmentDetails); DeleteDetails(nodeDetails); return; }
        if(IsInstanceValid(DraggedFlowNode)) { DraggedFlowNode.Disconnect("dragged", this, "_OnFlowNodeDragged"); } 
        DraggedFlowNode = flowNode;
        flowNode.Connect("dragged", this, "_OnFlowNodeDragged");
        CreateFragmentDetails(flowNode);
        CreateNodeDetails(flowNode);
    }

    public void _OnFlowNodeDragged(Vector2 from, Vector2 to)
    {
        if (IsInstanceValid(StoryManager.CurrentStoryNode))
        {
            CreateNodeDetails(StoryManager.CurrentStoryNode);
        }
    }
}

/*
    DetailsProperty <- Control | Editor Property 
    DetailsContainer <- Scroll Container | Editor Inspector
    PropertyContainer <- Grid Container (Property Editor) 
*/