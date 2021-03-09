using Godot;
using System;
using Storier;

public class ModalManager : Control
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
    [Export]
    private NodePath detailsDialogNodePath = new NodePath("./DetailsDialogue");

    private static ModalManager _modals = null;  
    public static ModalManager modals  
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
    public CreateStoryModal createDialog;
    public FileDialog fileDialog;
    public WindowDialog navigator;
    public DetailsModal detailsDialog;
    
    public override void _Ready()
    {
        _modals = this;
        overlay = GetNode<Panel>(overlayNodePath);
        createDialog = GetNode<CreateStoryModal>(createDialogNodePath);
        fileDialog = GetNode<FileDialog>(fileDialogNodePath);
        navigator = GetNode<WindowDialog>(navigatorNodePath);
        detailsDialog = GetNode<DetailsModal>(detailsDialogNodePath);
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
    
    public void ShowDetailsModal()
    {
        overlay.Hide();
        detailsDialog.ShowModal();
        detailsDialog.InitModal();
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
        GD.Print("Load Story");
        StoryManager.LoadStory(path);
    }

    public void _OnCreateDialogConfirmed()
    {
        GD.Print("Create Story");
        StoryManager.CreateStory(createDialog.GetInfos());
    }

    
}
