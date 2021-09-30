using Godot;
using System;

public class NavigatorContextMenu : PopupMenu
{
    public TreeItem treeItem = null;

    public override void _Ready()
    {
        Connect("id_pressed", this, "_OnNavigatorContextIdPressed");
    }

    public void _OnNavigatorContextIdPressed(int Id)
    {
        if (!IsInstanceValid(treeItem)) { return; }
        string path = StoryManager.CurrentStory.StoryBase + (treeItem.GetMeta("RelativePath") as string);
        switch (Id)
        {
            case 0: // Open
                GD.Print("Open");
                Editors.flowEditor.Reset();
                // StoryManager.CurrentStoryElement = Story.CreateStoryElement(path);
                break;
            case 1: // Rename
                GD.Print("Rename");
                GD.Print("Not implamented!");
                break;
            case 2: // Delete
                GD.Print("Delete");
                if (!FileManager.DeleteFile(path))
                {
                    FileManager.DeleteRecursively(path);
                }
                break;
            case 3: // Show in Folder
                GD.Print("Show in Folder");
                OS.ShellOpen(path.GetBaseDir());
                break;
            default:
                GD.Print("Navigator Context Menu: " + Id);
                break;
        }
    }
}
