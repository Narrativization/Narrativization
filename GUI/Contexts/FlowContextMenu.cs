using Godot;
using System;
using Storier;

public class FlowContextMenu : PopupMenu
{
    
    public override void _Ready()
    {
        Connect("id_pressed", this, "_OnFlowContextIdPressed");
    }

    public void _OnFlowContextIdPressed(int Id)
    {
        switch (Id)
        {
            case 0: // Create Story
                ModalManager.modals.ShowCreateModal();
                break;
            case 1: // Create Entity
                // ModalManager.modals.ShowDetailsModal();
                break;
            case 2: // Create Flow 
                GetParent<FlowEditor>().CreateFlowNode();
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