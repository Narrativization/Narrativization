using Godot;
using System;
using Storier;

public class MenuEditBar : HBoxContainer
{
    // Exports
    [Export]
    private NodePath storyNodePath = new NodePath("./Story");
    [Export]
    private NodePath editNodePath = new NodePath("./Edit");

    // Publics
    public MenuButton storyMenu;
    public MenuButton editMenu;
    
    public override void _Ready()
    {
        storyMenu = GetNode<MenuButton>(storyNodePath);
        editMenu = GetNode<MenuButton>(editNodePath);
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
                GD.Print("New Story");
                ModalManager.modals.ShowCreateModal();
                break;
            case 1: // Save
                GD.Print("Save Story");
                StoryManager.SaveStory();
                break;
            case 2: // Load
                GD.Print("Load Story");
                ModalManager.modals.ShowLoadModal();
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
