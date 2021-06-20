using Godot;
using System;
using Object = Godot.Object;

public class DetailsProperty : MarginContainer
{
    public Control detailControl = null;
    public string detailKey = "";
    public object detailObject = null;
    public Variant.Type detailType = Variant.Type.String;
    // public string propertyPath = "";
    public string detailHint = ""; // Tooltip
    public bool detailEditable = true;
    
    
    // [Signal] // Godot C# isn't support "object" param signal 
    // public delegate void PropertyUpdated(object value);
    [Signal]
    public delegate void PropertyUpdated(string key, string value, Variant.Type type); // Variant type for vector2 etc.

    public override void _Ready()
    {

    }

    // public override void _Process(float delta)
    // {

    // }

    public void CreateProperty(string key, object obj, Variant.Type type, string hint, bool editable = true)
    {
        detailKey = key;
        detailObject = obj;
        detailType = type;
        detailHint = hint;
        detailEditable = editable;
        UpdateProperty();
    }
    
    public void UpdateProperty()
    {
        switch (detailType)
        {
            case Variant.Type.String:
                CreatePropertyString(GD.Str(detailObject), detailHint, detailEditable, this);
                break;
            case Variant.Type.Int:
                CreatePropertyInt((int)(detailObject), detailHint, detailEditable, this);
                break;
            case Variant.Type.Real:
                CreatePropertyFloat((float)(detailObject), detailHint, detailEditable, this);
                break;
            case Variant.Type.Vector2:
                CreatePropertyVector2((Vector2)(detailObject), detailHint, detailEditable, this); // GD.Convert(detailObject, Variant.Type.Vector2)
                break;
            default:
                break;
        }
    }

    public void RemoveProperty()
    {
        detailControl.Name += "Deleted"; 
        detailControl.QueueFree();
    }

    public LineEdit CreatePropertyString(string text, string propertyHint, bool propertyEditable, Node parent)
    {
        LineEdit lineEdit = new LineEdit();
        lineEdit.Text = text;
        lineEdit.HintTooltip = propertyHint;
        lineEdit.Editable = propertyEditable;
        // lineEdit.Connect("text_change_rejected", this, "_OnLineEditChangeRejected");
        // lineEdit.Connect("text_changed", this, "_OnLineEditChanged"); // string newText
        lineEdit.Connect("text_entered", this, "_OnPropertyStringEntered"); // string newText
        detailControl = lineEdit;
        parent.AddChild(detailControl);
        return lineEdit;
    }

    public SpinBox CreatePropertyInt(int value, string propertyHint, bool propertyEditable, Node parent, double min = -Mathf.Inf, double max = Mathf.Inf)
    {
        SpinBox spin = new SpinBox();
        spin.MinValue = min;
        spin.MaxValue = max;
        spin.Value = value;
        spin.HintTooltip = propertyHint;
        spin.Editable = propertyEditable;
        spin.Connect("value_changed", this, "_OnPropertyIntValueChanged");
        detailControl = spin;
        parent.AddChild(spin);
        return spin;
    }

    private SpinBox CreatePropertyFloat(float value, string propertyHint, bool propertyEditable, Node parent, double step = 0.001f, double min = -Mathf.Inf, double max = Mathf.Inf)
    {
        SpinBox spin = new SpinBox();
        spin.MinValue = min;
        spin.MaxValue = max;
        spin.Step = step;
        spin.Value = value;
        spin.HintTooltip = propertyHint;
        spin.Editable = propertyEditable;
        spin.Connect("value_changed", this, "_OnPropertyFloatValueChanged");
        detailControl = spin;
        parent.AddChild(spin);
        return spin;
    }

    private HBoxContainer CreatePropertyVector2(Vector2 vector2, string propertyHint, bool propertyEditable, Node parent, double step = 0.001f, double min = -Mathf.Inf, double max = Mathf.Inf)
    {
        HBoxContainer hBox = new HBoxContainer();
        CreatePropertyFloat(vector2.x, propertyHint + ".x", propertyEditable, hBox, step, min, max);
        CreatePropertyFloat(vector2.y, propertyHint + ".y", propertyEditable, hBox, step, min, max);
        // hBox.Alignment = BoxContainer.AlignMode.Center;
        parent.AddChild(hBox);
        return hBox;
    }

    public void _OnPropertyStringEntered(string newText)
    {
        GD.Print("_OnPropertyStringEntered: " + newText);
        detailObject = newText;
        EmitSignal("PropertyUpdated", detailKey, detailObject, Variant.Type.String);
    }

    public void _OnPropertyIntValueChanged(float value)
    {
        GD.Print("_OnPropertyIntValueChanged: " + value);
        detailObject = value;
        EmitSignal("PropertyUpdated", detailKey, value, Variant.Type.Int);
    }

    public void _OnPropertyFloatValueChanged(float value)
    {
        GD.Print("_OnPropertyFloatValueChanged: " + value);
        detailObject = value;
        EmitSignal("PropertyUpdated", detailKey, value, Variant.Type.Real);
    }
    
}

