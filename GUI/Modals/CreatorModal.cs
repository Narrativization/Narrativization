using Godot;
using System;

public class CreatorModal : ConfirmationDialog
{
    // Exports
    [Export]
    private NodePath idNodePath = new NodePath("./MarginContainer/VBoxContainer/IdBar/Id");
    [Export]
    private NodePath idBaseNodePath = new NodePath("./MarginContainer/VBoxContainer/PathBar/IdBase");
    [Export]
    private NodePath idNameNodePath = new NodePath("./MarginContainer/VBoxContainer/PathBar/IdName");
    [Export]
    private NodePath nameNodePath = new NodePath("./MarginContainer/VBoxContainer/NameBar/Name");
    [Export]
    private NodePath descriptionNodePath = new NodePath("./MarginContainer/VBoxContainer/DescriptionBar/Description");
    [Export]
    private NodePath tagNodePath = new NodePath("./MarginContainer/VBoxContainer/TagBar/Tag");


    private LineEdit idEdit;
    private LineEdit idBaseEdit;
    private LineEdit idNameEdit;
    private LineEdit nameEdit;
    private TextEdit descriptionEdit;
    private LineEdit tagEdit;

    // StoryElement
    private string _suffix = ""; 
    private string _extension = ""; 

    // Visual
    private Color _fontColor;

    public override void _Ready()
    {
        idEdit = GetNode<LineEdit>(idNodePath);
        idBaseEdit = GetNode<LineEdit>(idBaseNodePath);
        idNameEdit = GetNode<LineEdit>(idNameNodePath);
        nameEdit = GetNode<LineEdit>(nameNodePath);
        descriptionEdit = GetNode<TextEdit>(descriptionNodePath);
        tagEdit = GetNode<LineEdit>(tagNodePath);

        idBaseEdit.Connect("text_changed", this, "_OnIdBaseChanged");
        idBaseEdit.Connect("text_entered", this, "_OnIdBaseChanged");

        idNameEdit.Connect("text_changed", this, "_OnIdNameChanged");
        idNameEdit.Connect("text_entered", this, "_OnIdNameChanged");
        idNameEdit.Connect("focus_exited", this, "_OnIdNameFocusExited");
        
        _fontColor = idNameEdit.GetColor("font_color", "LineEdit");

        Connect("confirmed", this, "_OnConfirmed");
    }

//  public override void _Process(float delta)
//  {
//      
//  }

    public void InitModal(string creation)
    {
        WindowTitle = "Create Story " + creation;
        switch (creation)
        {
            case "Character":
                idBaseEdit.Text = "/Entities/Characters/";
                _extension = "entity";
                break;
            case "Location":
                idBaseEdit.Text = "/Entities/Locations/";
                _extension = "entity";
                break;
            case "Flow":
                idBaseEdit.Text = "/Flows/";
                _extension = "flow";
                break;
            case "Entity":
                idBaseEdit.Text = "/Entities/";
                _extension = "entity";
                break;
            default:
                break;
        }
        ResetParams();
        IdChanged();
    }

    public void ResetParams()
    {
        idNameEdit.Text = "";
        nameEdit.Text = "";
        descriptionEdit.Text = "";
        tagEdit.Text = "";
        idNameEdit.AddColorOverride("font_color", _fontColor);
        _suffix = "";
    }

    public void IdChanged()
    {
        idEdit.Text = idBaseEdit.Text + idNameEdit.Text + _suffix;
    }

    public void _OnConfirmed()
    {
        Editors.flowEditor.Reset();
        string path = StoryManager.CurrentStory.StoryBase + idEdit.Text + "." + _extension;
        StoryElement storyElement = Story.CreateStoryElement(path);
        storyElement.Save();
        StoryManager.CurrentStoryElement = storyElement;
    }

    public void _OnIdBaseChanged(string newText)
    {
        IdChanged();
    }

    public void _OnIdNameChanged(string newText)
    {
        IdChanged();
        
        string possibleIdPath = StoryManager.CurrentStory.StoryBase + idEdit.Text + "." + _extension;
        Directory directory = new Directory();
        if (directory.FileExists(possibleIdPath))
        {
            idNameEdit.AddColorOverride("font_color", new Color(1, 0, 0)); // idNameEdit.Set("custom_colors/font_color", new Color(1, 0, 0));
            bool exist = directory.FileExists(possibleIdPath);
            int suffixNumber = 2;
            while (exist)
            {
                _suffix = suffixNumber.ToString();
                IdChanged();
                possibleIdPath = StoryManager.CurrentStory.StoryBase + idEdit.Text + "." + _extension;
                exist = directory.FileExists(possibleIdPath);
                suffixNumber++;
            }
        }
        else
        {
            _suffix = "";
            idNameEdit.AddColorOverride("font_color", _fontColor);
        }
        IdChanged();
    }

    public void _OnIdNameFocusExited()
    {
        idNameEdit.Text += _suffix; 
        _suffix = "";
        idNameEdit.AddColorOverride("font_color", _fontColor);
        IdChanged();
    }
}
