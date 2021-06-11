using Godot;
using System;

public class FlowContextMenu : PopupMenu
{
    [Export]
    public NodePath CreatorContextPath = "./CreatorContext";

    private PopupMenu creatorContext;

    public override void _Ready()
    {
        Connect("id_pressed", this, "_OnFlowContextIdPressed");

        creatorContext = GetNode<PopupMenu>(CreatorContextPath);
        if (IsInstanceValid(creatorContext)) 
        { 
            AddSubmenuItem("Create Story Element ", "CreatorContext"); 
            creatorContext.Connect("id_pressed", this, "_OnCreatorContextIdPressed");
        }
    }

    public void _OnFlowContextIdPressed(int Id)
    {
        switch (Id)
        {
            case 0: // Create Fragment
                GD.Print("Create Fragment");
                GetParent<FlowEditor>().CreateFlowNode();
                break;
            default:
                GD.Print("Flow Context Menu: " + Id);
                break;
        }
    }

    public void _OnCreatorContextIdPressed(int Id)
    {
        switch (Id)
        {
            case 0: // Create Flow 
                GD.Print("Create Flow");
                ModalManager.Modals.ShowCreatorModal("Flow");
                break;
            case 1: // Create Character
                GD.Print("Create Character");
                ModalManager.Modals.ShowCreatorModal("Character");
                break;
            case 2: // Create Location 
                GD.Print("Create Location");
                ModalManager.Modals.ShowCreatorModal("Location");
                break;
            case 3: // Create Entity 
                GD.Print("Create Entity");
                ModalManager.Modals.ShowCreatorModal("Entity");
                break;
            default:
                GD.Print("Creator Context Menu: " + Id);
                break;
        }
    }
}

/*



*/