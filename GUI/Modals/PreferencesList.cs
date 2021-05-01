using Godot;
using System;

public class PreferencesList : ItemList
{
    [Export]
    public NodePath workspaceDirectory = "../TabContainer/Directories/VBoxContainer/HBoxContainer/Workspace";
    [Export]
    public NodePath userSettingsDirectory = "../TabContainer/Directories/VBoxContainer/HBoxContainer2/UserSettings";

    private LineEdit workspaceDir;
    private LineEdit userSettingsDir;

    public override void _Ready()
    {
        Connect("item_selected", this, "_OnPreferencesItemSelected");

        workspaceDir = GetNode<LineEdit>(workspaceDirectory);
        userSettingsDir = GetNode<LineEdit>(userSettingsDirectory);
        if (IsInstanceValid(workspaceDir)) { workspaceDir.Text = StoryManager.workspace; }
        if (IsInstanceValid(userSettingsDir)) { workspaceDir.Text = StoryManager.workspace; }
    }

//  public override void _Process(float delta)
//  {
//      
//  }

    public void _OnPreferencesItemSelected(int index)
    {
        switch (Items[index])
        {
            case "Directories": 
                GD.Print();            
                break;
            default:
                break;
        }
    }

    public void _OnOpenWorkspacePressed() { OS.ShellOpen(StoryManager.workspace); }
    public void _OnOpenUserSettingsPressed() { OS.ShellOpen(OS.GetUserDataDir()); }

}
