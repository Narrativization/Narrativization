using Godot;
using System;
using Storier;

public class MenuStoryBar : HBoxContainer
{
    // Exports
    [Export]
    private NodePath storyNodePath = new NodePath("./Story");
    [Export]
    private NodePath toolsNodePath = new NodePath("./Tools");

    // Publics
    public MenuButton storyMenu;
    public Button toolsButton;

    public override void _Ready()
    {
        storyMenu = GetNode<MenuButton>(storyNodePath);
        if (IsInstanceValid(storyMenu))
        {
            storyMenu.GetPopup().Connect("id_pressed", this, "_OnStoryMenuItemPressed");
        }

        toolsButton = GetNode<Button>(toolsNodePath);
        toolsButton.Connect("pressed", this, "_OnToolsPressed");
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
                OS.ShellOpen(StoryManager.workspace);
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

    public void _OnToolsPressed()
    {
        GetTree().CallGroup("Docks", "ToggleTools");
        GetTree().CallGroup("Navigator", "Refresh");
    }

}
