using Godot;
using System;

public class CreateDialog : ConfirmationDialog
{
    // Exports
    [Export]
    private NodePath storyNodePath = new NodePath("./Content/VBoxContainer/Story");
    [Export]
    private NodePath authorNodePath = new NodePath("./Content/VBoxContainer/Author");
    [Export]
    private NodePath copyrightNodePath = new NodePath("./Content/VBoxContainer/Copyright");

    public LineEdit storyName;
    public LineEdit authorName;
    public LineEdit copyrightName;

    public override void _Ready()
    {
        storyName = GetNode<LineEdit>(storyNodePath);
        authorName = GetNode<LineEdit>(authorNodePath);
        copyrightName = GetNode<LineEdit>(copyrightNodePath);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public Storier.StoryData GetInfos()
    {
        ValidateInfos();
        Storier.StoryData sData = new Storier.StoryData(
            storyName.Text, 
            OS.GetSystemDir(OS.SystemDir.Documents) + "/Storier/" + storyName.Text, 
            authorName.Text, 
            copyrightName.Text
        );
        ClearDialogue();
        return sData;
    }

    private void ValidateInfos()
    {
        if (storyName.Text == "")
        {
            storyName.Text = "Untitled";
        }
    }

    public void ClearDialogue()
    {
        storyName.Text = "";
        authorName.Text = "";
        copyrightName.Text = "";
    }

}
