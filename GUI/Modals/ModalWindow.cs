using Godot;
using System;
using Storier;

public class ModalWindow : Control
{
    // Exports
    [Export]
    private NodePath overlayNodePath = new NodePath("./BackgroundOverlay");
    [Export]
    private NodePath fileDialogNodePath = new NodePath("./FileDialog");
    [Export]
    private NodePath navigatorNodePath = new NodePath("./Navigator");

    private static ModalWindow _modals = null;  
    public static ModalWindow modals  
    {  
        get  
        {  
            if (_modals == null)  
            {  
                GD.PushWarning("Modals wasn't found on scenes!");
            }  
            return _modals;  
        }  
    }  

    public Panel overlay;
    public FileDialog fileDialog;
    public WindowDialog navigator;
    
    public override void _Ready()
    {
        _modals = this;
        overlay = GetNode<Panel>(overlayNodePath);
        fileDialog = GetNode<FileDialog>(fileDialogNodePath);
        navigator = GetNode<WindowDialog>(navigatorNodePath);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }


    //  Type Modal Name and display as named modal window.
    public void ShowModalWindow(string modalName)
    {
        overlay.Hide();
        switch (modalName)
        {
            case "File":
                overlay.Show();
                break;
            case "Navigator":
                navigator.ShowModal();
                navigator.GetNode<NavigationTree>("./Tree").Refresh();
                break;
            default:
                GD.PushWarning("Unexpected ShowModalWindow name: " + modalName);
                break;
        }
    }
    
    // Some actions displaying an overlay so after that hide the overlay
    public void _OnHideOverlay()
    {
        overlay.Hide();
    }

}
