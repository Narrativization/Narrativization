using Godot;
using System;
using System.Collections;
using Godot.Collections;
using Array = Godot.Collections.Array;

public class Navigator : Tree
{
    // Tree's Root
    private TreeItem root;
    private Dictionary<string, TreeItem> tree = new Dictionary<string, TreeItem>(); // Seid:TreeItem

    private Dictionary<string, string> iconResources = new Dictionary<string, string>()
    {
        {"Story", "res://ThirdParty/FeatherIcons/book-open.svg"},
        {"Folder", "res://ThirdParty/FeatherIcons/folder.svg"},
        {"File", "res://ThirdParty/FeatherIcons/file.svg"},
        {"XSquare", "res://ThirdParty/FeatherIcons/x-square.svg"},
        {"Flows", "res://ThirdParty/FeatherIcons/wind.svg"},
        {"Entities", "res://ThirdParty/FeatherIcons/codepen.svg"},
        {"Assets", "res://ThirdParty/FeatherIcons/shopping-bag.svg"},
        {"References", "res://ThirdParty/FeatherIcons/link-2.svg"},
        {"Characters", "res://ThirdParty/FeatherIcons/user.svg"},
        {"Locations", "res://ThirdParty/FeatherIcons/map-pin.svg"},
    };

    private Dictionary<string, Texture> icons = new Dictionary<string, Texture>();

    public override void _Ready()
    {
        // Navigator Group
        AddToGroup("Navigator");
        
        // Resize icons 16x16
        foreach (var iRes in iconResources)
        {
            ImageTexture imgTex = FileManager.ResizeImage(ResourceLoader.Load<Texture>(iRes.Value), 16, 16); 
            icons.Add(iRes.Key, (Texture)imgTex);
        }
        Refresh();
    }

//  public override void _Process(float delta)
//  {
//      
//  }

    public override void _GuiInput(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.Doubleclick && IsInstanceValid(GetItemAtPosition(mouseEvent.Position)))
            {
                TreeItem treeItem = GetItemAtPosition(mouseEvent.Position);
                string itemName = treeItem.GetText(0);
                string relativePath = treeItem.GetMeta("RelativePath") as string;
                GD.Print("Double Click: " + itemName + " -> \"" + relativePath + "\"");

                Editors.flowEditor.Reset();
                StoryManager.CurrentStoryElement = Story.CreateStoryElement(StoryManager.CurrentStory.StoryBase + relativePath);
            }
            else if (inputEvent.IsActionPressed("Context") && IsInstanceValid(GetItemAtPosition(mouseEvent.Position)))
            {
                GD.Print("NavContext");
                TreeItem treeItem = GetItemAtPosition(mouseEvent.Position);
                PackedScene navContext = GD.Load<PackedScene>("res://GUI/Contexts/NavigatorContextMenu.tscn");
                NavigatorContextMenu navMenu = navContext.Instance() as NavigatorContextMenu;
                navMenu.treeItem = treeItem;
                AddChild(navMenu);
                navMenu.SetPosition(mouseEvent.GlobalPosition);
                navMenu.ShowModal();
            }
        }        
    }

    public void Refresh()
    {
        Clear();
        tree.Clear();
        
        if(IsInstanceValid(StoryManager.CurrentStory))
        {
            CreateTree(StoryManager.CurrentStory.GetDirectoryTree(), StoryManager.CurrentStory.StoryName);
        }
        else
        {
            root = CreateItem();
            root.SetText(0,"Open Project!");
        }
    }

    // Create Tree From TreeArray ["/.../", "/.../.../"]
    public void CreateTree(Array fileTree, string rootName)
    {
        root = CreateDefaultTreeItem(rootName, "/");

        foreach (string fileTreeItem in fileTree)
        {
            if (fileTreeItem.Extension() == "storier") { continue; } // Ignore Project File
            if (fileTreeItem.GetFile() == "") // Folder
            {
                string parentDir = fileTreeItem.Left(fileTreeItem.GetBaseDir().LastIndexOf('/')) + '/';
                string itemName = fileTreeItem.Right(fileTreeItem.GetBaseDir().LastIndexOf('/')).Trim('/');
                
                TreeItem parentItem = root;
                tree.TryGetValue(parentDir, out parentItem);
                CreateDefaultTreeItem(itemName, fileTreeItem, parentItem);
            }
            else // File
            {
                string parentDir = fileTreeItem.GetBaseDir() + '/';
                string itemName = fileTreeItem.GetFile().Replace("." + fileTreeItem.Extension(), "");

                TreeItem parentItem = root;
                tree.TryGetValue(parentDir, out parentItem);
                CreateDefaultTreeItem(itemName, fileTreeItem, parentItem);
            }
        }
    }

    private TreeItem CreateDefaultTreeItem(string itemName, string relativePath, TreeItem parent = null)
    {
        TreeItem treeItem = CreateTreeItem(0, itemName, SelectItemIcon(relativePath, parent), parent);
        treeItem.SetTooltip(0, relativePath);
        treeItem.SetMeta("RelativePath", relativePath);
        tree.Add(relativePath, treeItem);
        return treeItem;
    }

    private TreeItem CreateTreeItem(int column, string text, Texture icon, TreeItem parent = null)
    {
        TreeItem treeItem = CreateItem(parent);
        treeItem.SetText(column, text);
        if (IsInstanceValid(icon)) treeItem.SetIcon(column, icon);
        return treeItem;
    }
    
    private Texture SelectItemIcon(string relativePath, TreeItem parent)
    {   
        Texture selected = icons["Folder"];

        switch (relativePath)
        {
            case "/":
                selected = icons["Story"];
                break;
            case "/Flows/":
                selected = icons["Flows"];
                break;
            case "/Entities/":
                selected = icons["Entities"];
                break;
            case "/Assets/":
                selected = icons["Assets"];
                break;
            case "/References/":
                selected = icons["References"];
                break;
            case "/Entities/Characters/":
                selected = icons["Characters"];
                break;
            case "/Entities/Locations/":
                selected = icons["Locations"];
                break;
            default:
                if (relativePath.GetFile() != "") { selected = icons["File"]; }
                else { selected = icons["Folder"]; }
                break;
        }
        return selected;
    }
    
}
