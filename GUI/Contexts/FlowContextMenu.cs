using Godot;
using System;

public class FlowContextMenu : PopupMenu
{
    
    public override void _Ready()
    {
        
    }

    public void _OnFlowContextIdPressed(int Id)
    {
        switch (Id)
        {
            case 0:
                ModalManager.modals.ShowCreateModal();
                break;
            case 1:
                ModalManager.modals.ShowDetailsModal();
                break;
            default:
                GD.Print("Flow Context Menu: " + Id);
                break;
        }
    }
}
