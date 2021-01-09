using Godot;
using System;
using Storier;

public class ModalWindow : Control
{
    // Exports
    [Export]
    private NodePath overlayNodePath = new NodePath("./BackgroundOverlay");
    [Export]
    private NodePath createDialogNodePath = new NodePath("./CreateDialog");
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
    public CreateDialog createDialog;
    public FileDialog fileDialog;
    public WindowDialog navigator;
    
    public override void _Ready()
    {
        _modals = this;
        overlay = GetNode<Panel>(overlayNodePath);
        createDialog = GetNode<CreateDialog>(createDialogNodePath);
        fileDialog = GetNode<FileDialog>(fileDialogNodePath);
        navigator = GetNode<WindowDialog>(navigatorNodePath);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

    
    public void ShowFileModal()
    {
        overlay.Hide();
        fileDialog.ShowModal();
    }
    
    public void ShowCreateModal()
    {
        overlay.Hide();
        createDialog.ShowModal();
    }

    public void ShowLoadModal()
    {
        overlay.Hide();
        fileDialog.Mode = FileDialog.ModeEnum.OpenFile;
        fileDialog.Access = FileDialog.AccessEnum.Filesystem;
        fileDialog.CurrentDir = OS.GetSystemDir(OS.SystemDir.Documents) + "/Storier/";
        fileDialog.Filters = new string[] {"*.storier"};
        fileDialog.ShowModal(); // ~Popup Centered
        fileDialog.Invalidate();
    }

    public void ShowNavigatorModal()
    {
        overlay.Hide();
        navigator.ShowModal();
        navigator.GetNode<NavigationTree>("./Tree").Refresh();
    }


    
    // Some actions displaying an overlay so after that hide the overlay
    public void _OnHideOverlay()
    {
        overlay.Hide();
    }

    public void _OnFileDialogDirSelected(string dir)
    {

    }

    public void _OnFileDialogFileSelected(string path)
    {
        Story story = new Story();
        GetTree().Root.AddChild(story);
        Story.story.LoadStory(path);
    }

    public void _OnCreateDialogConfirmed()
    {
        Story story = new Story(createDialog.GetInfos());
        GetTree().Root.AddChild(story);
    }

    
}
