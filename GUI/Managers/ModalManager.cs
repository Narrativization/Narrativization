using Godot;
using System;

public class ModalManager : Control
{
    // Exports
    [Export]
    private NodePath overlayNodePath = new NodePath("./BackgroundOverlay");
    [Export]
    private NodePath createDialogNodePath = new NodePath("./CreateStoryDialog");
    [Export]
    private NodePath fileDialogNodePath = new NodePath("./FileDialog");
    [Export]
    private NodePath CreatorModalNodePath = new NodePath("./CreatorModal");
    [Export]
    private NodePath settingsModalNodePath = new NodePath("./SettingsModal");

    private static ModalManager _Modals = null;  
    public static ModalManager Modals  
    {  
        get  
        {  
            if (_Modals == null)  
            {  
                GD.PushWarning("Modals wasn't found on scenes!");
            }  
            return _Modals;  
        }  
    }  

    public Panel overlay;
    public CreateStoryModal createStoryDialogue;
    public FileDialog fileDialog;
    public CreatorModal creatorModal;
    public AcceptDialog settingsModal;
    
    public override void _Ready()
    {
        _Modals = this;
        overlay = GetNode<Panel>(overlayNodePath);
        createStoryDialogue = GetNode<CreateStoryModal>(createDialogNodePath);
        fileDialog = GetNode<FileDialog>(fileDialogNodePath);
        creatorModal = GetNode<CreatorModal>(CreatorModalNodePath);
        settingsModal = GetNode<AcceptDialog>(settingsModalNodePath);
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
        createStoryDialogue.ShowModal();
    }
    
    public void ShowCreatorModal(string creation)
    {
        overlay.Hide();
        creatorModal.InitModal(creation);
        creatorModal.ShowModal();
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

    public void ShowSettingsModal()
    {
        overlay.Hide();
        settingsModal.ShowModal();
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
        createStoryDialogue.ValidateInfos();
        StoryManager.CreateStory(createStoryDialogue.storyName.Text, createStoryDialogue.descriptionName.Text, createStoryDialogue.GetDir());
        createStoryDialogue.ClearDialogue();
    }

    
}
