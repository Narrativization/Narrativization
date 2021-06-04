using Godot;
using System;

public class SceneBlock : Panel
{
    [Export]
    private NodePath speakerNodePath = new NodePath("./Heading/Speaker");
    [Export]
    private NodePath idNameNodePath = new NodePath("./Heading/IdName");
    [Export]
    private NodePath contentNodePath = new NodePath("./Content");

    public LineEdit speakerEdit;
    public Label idNameEdit;
    public TextEdit contentEdit;

    public StoryFragment _Fragment { get; set; }
    public StoryFragment Fragment 
    { 
        get 
        {
            return _Fragment;
        } 
        set 
        {
            _Fragment = value;
            if (IsInstanceValid(_Fragment))
            {
                // speakerEdit.Text = _Fragment.FragmentDescription;
                idNameEdit.Text = _Fragment.FragmentName;
                // contentEdit.Text = _Fragment.FragmentContent;
            }
        } 
    }

    public override void _Ready()
    {
        speakerEdit = GetNode<LineEdit>(speakerNodePath);
        idNameEdit = GetNode<Label>(idNameNodePath);
        contentEdit = GetNode<TextEdit>(contentNodePath);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

}
