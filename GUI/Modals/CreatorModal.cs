using Godot;
using System;
using Storier;

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

    private string StoryElementExtension = ""; 

    public override void _Ready()
    {
        idEdit = GetNode<LineEdit>(idNodePath);
        idBaseEdit = GetNode<LineEdit>(idBaseNodePath);
        idNameEdit = GetNode<LineEdit>(idNameNodePath);
        nameEdit = GetNode<LineEdit>(nameNodePath);
        descriptionEdit = GetNode<TextEdit>(descriptionNodePath);
        tagEdit = GetNode<LineEdit>(tagNodePath);

        idBaseEdit.Connect("text_changed", this, "_OnIdChanged");
        idBaseEdit.Connect("text_entered", this, "_OnIdChanged");

        idNameEdit.Connect("text_changed", this, "_OnIdChanged");
        idNameEdit.Connect("text_entered", this, "_OnIdChanged");

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
                StoryElementExtension = "entity";
                break;
            case "Location":
                idBaseEdit.Text = "/Entities/Locations/";
                StoryElementExtension = "entity";
                break;
            case "Flow":
                idBaseEdit.Text = "/Flows/";
                StoryElementExtension = "flow";
                break;
            case "Entity":
                idBaseEdit.Text = "/Entities/";
                StoryElementExtension = "entity";
                break;
            default:
                break;
        }
        ResetParams();
        _OnIdChanged("");
    }

    public void ResetParams()
    {
        idNameEdit.Text = "";
        nameEdit.Text = "";
        descriptionEdit.Text = "";
        tagEdit.Text = "";
    }

    public void _OnConfirmed()
    {
        StoryEntity storyEntity = new StoryEntity(StoryManager.CurrentStory.StoryBase + idEdit.Text + "." + StoryElementExtension); 
        storyEntity.Save();
    }

    public void _OnIdChanged(string newText)
    {
        idEdit.Text = idBaseEdit.Text + idNameEdit.Text;
    }

}
