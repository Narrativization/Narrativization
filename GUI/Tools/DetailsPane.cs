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

    private FlowGraphNode DraggedFlowNode;

    public override void _Ready()
    {
        StoryManager.CurrentStoryManager.Connect("StoryChanged", this, "_OnStoryChanged");
        StoryManager.CurrentStoryManager.Connect("StoryElementChanged", this, "_OnStoryElementChanged");
        StoryManager.CurrentStoryManager.Connect("FlowNodeChanged", this, "_OnFlowNodeChanged");

        fragmentDetails = GetNode<VBoxContainer>(fragmentDetailsPath);
        nodeDetails = GetNode<VBoxContainer>(nodeDetailsPath);
    }

    public override void _Process(float delta)
    {
        
    }

    public void CreateFragmentDetails(FlowGraphNode flowNode)
    {
        DeleteFragmentDetails();
        if (!IsInstanceValid(flowNode) || !IsInstanceValid(flowNode.NodeFragment)) { return; }
        StoryFragment fragment = flowNode.NodeFragment;
        foreach (var nodeData in fragment.FragmentData)
        {
            // GD.Print("NodeDetail: " + nodeData.Value.GetType());
            if (nodeData.Value.GetType() == typeof(String))
            {
                LineEdit lineEdit = DetailString((string)nodeData.Value, nodeData.Key, fragmentDetails);
                lineEdit.Editable = false;
            }
            else if (nodeData.Value.GetType() == typeof(int))
            {
                SpinBox spinBox = DetailInt((int)nodeData.Value, nodeData.Key, fragmentDetails);
                spinBox.Editable = false;
            }
            else if (nodeData.Value.GetType() == typeof(Vector2))
            {
                Vector2 vector2 = (Vector2)nodeData.Value;
                HBoxContainer hBox = DetailVector2(vector2, nodeData.Key, fragmentDetails);
                foreach (SpinBox spinBox in hBox.GetChildren())
                {
                    spinBox.Editable = false;
                }
            }
            else if (nodeData.Value.GetType() == typeof(Enum))
            {
                // Create Enum Dropdown
            }
        }
    }

    public void DeleteFragmentDetails()
    {
        foreach (Node n in fragmentDetails.GetChildren())
        {
            n.Name += "Deleted";
            n.QueueFree();
        }
    }

    public void CreateNodeDetails(FlowGraphNode flowNode)
    {
        DeleteNodeDetails();
        foreach (var nodeData in flowNode.NodeData)
        {
            // GD.Print("NodeDetail: " + nodeData.Value.GetType());
            if (nodeData.Value.GetType() == typeof(String))
            {
                LineEdit lineEdit = DetailString((string)nodeData.Value, nodeData.Key, nodeDetails);
                lineEdit.Editable = false;
            }
            else if (nodeData.Value.GetType() == typeof(int))
            {
                SpinBox spinBox = DetailInt((int)nodeData.Value, nodeData.Key, nodeDetails);
                spinBox.Editable = false;
            }
            else if (nodeData.Value.GetType() == typeof(Vector2))
            {
                Vector2 vector2 = (Vector2)nodeData.Value;
                HBoxContainer hBox = DetailVector2(vector2, nodeData.Key, nodeDetails);
                foreach (SpinBox spinBox in hBox.GetChildren())
                {
                    spinBox.Editable = false;
                }
            }
            else if (nodeData.Value.GetType() == typeof(Enum))
            {
                // Create Enum Dropdown
            }
        }
    }

    public void DeleteNodeDetails()
    {
        foreach (Node n in nodeDetails.GetChildren())
        {
            n.Name += "Deleted";
            n.QueueFree();
        }
    }

    private LineEdit DetailString(string text, string tooltip, Node parent)
    {
        LineEdit lineEdit = new LineEdit();
        lineEdit.Text = text;
        lineEdit.HintTooltip = tooltip;
        parent.AddChild(lineEdit);
        return lineEdit;
    }

    private SpinBox DetailInt(int value, string tooltip, Node parent, double min = -Mathf.Inf, double max = Mathf.Inf)
    {
        SpinBox spin = new SpinBox();
        spin.MinValue = min;
        spin.MaxValue = max;
        spin.Value = value;
        spin.HintTooltip = tooltip;
        parent.AddChild(spin);
        return spin;
    }

    private SpinBox DetailFloat(double value, string tooltip, Node parent, double step = 0.001f, double min = -Mathf.Inf, double max = Mathf.Inf)
    {
        SpinBox spin = new SpinBox();
        spin.MinValue = min;
        spin.MaxValue = max;
        spin.Step = step;
        spin.Value = value;
        spin.HintTooltip = tooltip;
        parent.AddChild(spin);
        return spin;
    }

    private HBoxContainer DetailVector2(Vector2 vector2, string tooltip, Node parent, double step = 0.001f, double min = -Mathf.Inf, double max = Mathf.Inf)
    {
        HBoxContainer hBox = new HBoxContainer();
        DetailFloat(vector2.x, tooltip + ".x", hBox, step, min, max);
        DetailFloat(vector2.y, tooltip + ".y", hBox, step, min, max);
        // hBox.Alignment = BoxContainer.AlignMode.Center;
        parent.AddChild(hBox);
        return hBox;
    }

    public void _OnStoryChanged(Story story)
    {
        if (!IsInstanceValid(story)) { return; }
    }

    public void _OnStoryElementChanged(StoryElement storyElement)
    {
        if (!IsInstanceValid(storyElement)) { return; }
    }

    public void _OnFlowNodeChanged(FlowGraphNode flowNode)
    {
        if (!IsInstanceValid(flowNode)) { DeleteFragmentDetails(); DeleteNodeDetails(); return; }
        if(IsInstanceValid(DraggedFlowNode)) { DraggedFlowNode.Disconnect("dragged", this, "_OnFlowNodeDragged"); } 
        DraggedFlowNode = flowNode;
        flowNode.Connect("dragged", this, "_OnFlowNodeDragged");
        if (flowNode is FlowNode) { CreateFragmentDetails(flowNode); }
        CreateNodeDetails(flowNode);
    }

    public void _OnFlowNodeDragged(Vector2 from, Vector2 to)
    {
        if (IsInstanceValid(StoryManager.CurrentFlowNode))
        {
            CreateNodeDetails(StoryManager.CurrentFlowNode);
        }
    }
}
