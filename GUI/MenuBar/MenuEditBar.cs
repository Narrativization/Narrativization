using Godot;
using System;
using Storier;

public class MenuEditBar : HBoxContainer
{
    // Exports
    [Export]
    private NodePath editorNodePath = new NodePath("./Editor");
    [Export]
    private NodePath manageNodePath = new NodePath("./Manage");

    // Publics
    public Button editorButton;
    public MenuButton manageMenu;

    public override void _Ready()
    {
        editorButton = GetNode<Button>(editorNodePath);
        editorButton.Connect("pressed", this, "_OnEditorPressed");

        manageMenu = GetNode<MenuButton>(manageNodePath);
        if (IsInstanceValid(manageMenu))
        {
            manageMenu.GetPopup().Connect("id_pressed", this, "_OnManageMenuItemPressed");
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    public void _OnEditorPressed()
    {
        Editors editors = GetTree().GetNodesInGroup("Editors")[0] as Editors;
        editorButton.Text = editors.ChangeEditor();
    }

    public void _OnManageMenuItemPressed(int _id)
    {
        switch (_id)
        {
            case 0: // Journeys
                GD.Print("Journeys");
                break;
            case 1: // Decisions
                GD.Print("Decisions");
                break;
            case 2: // Annotations
                GD.Print("Annotations");
                break;
            default:
            break;
        }
    }
}
