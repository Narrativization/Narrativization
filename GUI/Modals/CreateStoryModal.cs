using Godot;
using System;

public class CreateStoryModal : ConfirmationDialog
{
    // Exports
    [Export]
    private NodePath storyNodePath = new NodePath("./Content/VBoxContainer/Story");
    [Export]
    private NodePath descriptionNodePath = new NodePath("./Content/VBoxContainer/Description");
    [Export]
    private NodePath authorNodePath = new NodePath("./Content/VBoxContainer/Author");
    [Export]
    private NodePath copyrightNodePath = new NodePath("./Content/VBoxContainer/Copyright");

    public LineEdit storyName;
    public LineEdit descriptionName;
    public LineEdit authorName;
    public LineEdit copyrightName;

    public override void _Ready()
    {
        storyName = GetNode<LineEdit>(storyNodePath);
        descriptionName = GetNode<LineEdit>(descriptionNodePath);
        authorName = GetNode<LineEdit>(authorNodePath);
        copyrightName = GetNode<LineEdit>(copyrightNodePath);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    // public StoryData GetInfos()
    // {
    //     ValidateInfos();
    //     StoryData sData = new StoryData(
    //         storyName.Text, 
    //         descriptionName.Text, 
    //         OS.GetSystemDir(OS.SystemDir.Documents) + "/Storier/" + storyName.Text, 
    //         authorName.Text, 
    //         copyrightName.Text
    //     );
    //     ClearDialogue();
    //     return sData;
    // }
    
    public string GetDir()
    {
        return OS.GetSystemDir(OS.SystemDir.Documents) + "/Storier/" + storyName.Text + "/";
    }

    public void ValidateInfos()
    {
        if (storyName.Text == "")
        {
            storyName.Text = "Untitled";
        }
    }

    public void ClearDialogue()
    {
        storyName.Text = "";
        descriptionName.Text = "";
        authorName.Text = "";
        copyrightName.Text = "";
    }

}
