using Godot;
using System;
using Storier;

public class FlowContextMenu : PopupMenu
{
    
    public override void _Ready()
    {
        
    }

    public void _OnFlowContextIdPressed(int Id)
    {
        switch (Id)
        {
            case 0: // Create Story
                ModalManager.modals.ShowCreateModal();
                break;
            case 1: // Create Entity
                ModalManager.modals.ShowDetailsModal();
                break;
            case 2: // Create Flow 
                Story.CreateStoryFlow().Save("Save me!");
                GD.Print("Create Flow");
                break;
            default:
                GD.Print("Flow Context Menu: " + Id);
                break;
        }
    }
}

/*



*/