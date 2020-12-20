using Godot;
using System;

public class FileMenu : MenuButton
{
    


    public override void _Ready()
    {
        GetPopup().Connect("id_pressed", this, "_OnItemPressed");
    }

    private void _OnItemPressed(int _id)
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
}
