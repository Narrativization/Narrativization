using Godot;
using System;

public class MenuEditBar : HBoxContainer
{
    // Exports
    [Export]
    public NodePath StoryNode = new NodePath("./Story");
    [Export]
    public NodePath EditNode = new NodePath("./Edit");

    // Publics
    public MenuButton storyMenu;
    public MenuButton editMenu;
    
    public override void _Ready()
    {
        storyMenu = GetNode<MenuButton>(StoryNode);
        editMenu = GetNode<MenuButton>(EditNode);
        if (IsInstanceValid(storyMenu))
        {
            storyMenu.GetPopup().Connect("id_pressed", this, "_OnStoryMenuItemPressed");
        }
        if (IsInstanceValid(editMenu))
        {
            editMenu.GetPopup().Connect("id_pressed", this, "_OnEditMenuItemPressed");
        }
    }

    public void _OnStoryMenuItemPressed(int _id)
    {
        switch (_id)
        {
            case 0: // New
                GD.Print("New");
                break;
            case 1: // Save
                GD.Print("Save");
                break;
            case 2: // Load
                GD.Print("Load");
                break;
            case 3: // Settings
                GD.Print("Settings");
                break;
            case 4: // Help
                GD.Print("Help");
                break;
            case 5: // Quit
                GetTree().Quit();
                break;
            default:
            break;
        }
    }


    public void _OnEditMenuItemPressed(int _id)
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
