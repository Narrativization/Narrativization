using Godot;
using System;

public class PropertyEditor : GridContainer
{
    

    
    public override void _Ready()
    {
        
    }

//  public override void _Process(float delta)
//  {
//      
//  }

    public void CreateProperties()
    {
        if (!IsInstanceValid(StoryManager.CurrentStoryElement)) { return; }

        DeleteNodeDetails();
        foreach (var seData in StoryManager.CurrentStoryElement.StoryElementData)
        {
            // GD.Print("NodeDetail: " + nodeData.Value.GetType());
            if (seData.Value.GetType() == typeof(String))
            {
                LineEdit lineEdit = DetailString((string)seData.Value, seData.Key, this);
                lineEdit.Editable = false;
            }
            // else if (seData.Value.GetType() == typeof(int))
            // {
            //     SpinBox spinBox = DetailInt((int)seData.Value, seData.Key, this);
            //     spinBox.Editable = false;
            // }
            // else if (seData.Value.GetType() == typeof(Vector2))
            // {
            //     Vector2 vector2 = (Vector2)seData.Value;
            //     HBoxContainer hBox = DetailVector2(vector2, seData.Key, this);
            //     foreach (SpinBox spinBox in hBox.GetChildren())
            //     {
            //         spinBox.Editable = false;
            //     }
            // }
            // else if (seData.Value.GetType() == typeof(Enum))
            // {
            //     // Create Enum Dropdown
            // }
        }
    }

    public void DeleteNodeDetails()
    {
        foreach (Node n in this.GetChildren())
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


}
