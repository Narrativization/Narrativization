using Godot;
using System;

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
            case 0: // Create Fragment
                GD.Print("Create Fragment");
                GetParent<FlowEditor>().CreateFlowNode();
                break;
            case 1: // Create Character
                GD.Print("Create Character");
                ModalManager.Modals.ShowCreatorModal("Character");
                break;
            case 2: // Create Location 
                GD.Print("Create Location");
                ModalManager.Modals.ShowCreatorModal("Location");
                break;
            case 3: // Create Flow 
                GD.Print("Create Flow");
                ModalManager.Modals.ShowCreatorModal("Flow");
                break;
            case 4: // Create Entity 
                GD.Print("Create Entity");
                ModalManager.Modals.ShowCreatorModal("Entity");
                break;
            default:
                GD.Print("Flow Context Menu: " + Id);
                break;
        }
    }
}

/*



*/