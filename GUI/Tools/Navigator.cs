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
                // string path = PathFromTree(treeItem);
                GD.Print("Double Click: " + treeItem.GetText(0) + " -> \"" + treeItem.GetTooltip(0) + "\"");
                Editors.flowEditor.Reset();
                // StoryManager.CurrentStoryElement = Story.CreateStoryElement(StoryManager.CurrentStory.StoryBase + path);
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
            // CreateTree(StoryManager.CurrentStory.LookHierarchy(), StoryManager.CurrentStory.StoryName);
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
            // if (fileTreeItem.Extension() == "storier") { continue; } // Ignore Project File
            if (fileTreeItem.GetFile() == "") // Folder
            {
                string parentDir = fileTreeItem.Left(fileTreeItem.GetBaseDir().LastIndexOf('/')) + '/';
                string itemName = fileTreeItem.Right(fileTreeItem.GetBaseDir().LastIndexOf('/')).Trim('/');

                if (tree.TryGetValue(parentDir, out TreeItem parentItem)) // SubFolder, Every Story Element child of Root('/')
                {
                    CreateDefaultTreeItem(itemName, fileTreeItem, parentItem);
                }
                else
                {
                    GD.Print(itemName + " Folder can't created!");
                }
            }
            else // File
            {
                string parentDir = fileTreeItem.GetBaseDir() + '/';
                string itemName = fileTreeItem.GetFile().Replace("." + fileTreeItem.Extension(), "");

                if (tree.TryGetValue(parentDir, out TreeItem parentItem)) // SubFile, Every Story Element child of Root('/')
                {
                    CreateDefaultTreeItem(itemName, fileTreeItem, parentItem);
                }
                else
                {
                    GD.Print(itemName + " File can't created!");
                }
            }
        }
    }

    private TreeItem CreateDefaultTreeItem(string itemName, string relativePath, TreeItem parent = null)
    {
        TreeItem treeItem = CreateTreeItem(0, itemName, SelectItemIcon(relativePath, parent), parent);
        treeItem.SetTooltip(0, relativePath);
        treeItem.Set("StoryElementId", relativePath);
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

    // Dictionary
    public void CreateTree(Dictionary dictionary, string rootName)
    {
        Clear();
        root = CreateItemWithIcon(0, rootName, icons["Folder"], null);
        CreateTreeDisplay(dictionary, root); 
    }

    private void CreateTreeDisplay(Dictionary dictionary, TreeItem parent = null)
    {
        IDictionaryEnumerator _dicEn = dictionary.GetEnumerator();
        for (int i = 0; i < dictionary.Count; i++)
        {
            _dicEn.MoveNext();
            if (_dicEn.Key is string && _dicEn.Value is string)
            {
                if (IsInstanceValid(parent) && !((parent == root) && (parent.GetText(0) == (string)_dicEn.Key))) // Ignore Project(.storier) File
                {
                    TreeItem treeItem = CreateItemWithIcon(0, (string)_dicEn.Key, icons["File"], parent);
                    treeItem.SetMeta("Extension", _dicEn.Value);
                }
            }
            else if (_dicEn.Key is string && _dicEn.Value is Dictionary)
            {
                TreeItem treeItem = CreateItemWithIcon(0, (string)_dicEn.Key, SelectIcon((string)_dicEn.Key, parent), parent);
                CreateTreeDisplay((Dictionary)_dicEn.Value, treeItem);

                // if (!IsInstanceValid(treeItem.GetChildren()))
                // {
                //     TreeItem treeChild = CreateItemWithIcon(0, "> Empty", icons["XSquare"], treeItem);
                // }
                treeItem.Collapsed = false;
            }
            else
            {
                GD.PushWarning("Unknown dictionary member: " + _dicEn.Value);
            }
        }
    }

    private TreeItem CreateItemWithIcon(int column, string text, Texture icon, TreeItem parent = null)
    {
        TreeItem treeItem = CreateItem(parent);
        treeItem.SetText(column, text);
        if (IsInstanceValid(icon)) treeItem.SetIcon(column, icon);
        return treeItem;
    }
    
    private Texture SelectIcon(string idBase, TreeItem parent)
    {   
        Texture selected = icons["Folder"];
        if (!IsPredefined(idBase, parent))
        {
            return selected; 
        }
        
        switch (idBase)
        {
            case "Flows":
                selected = icons["Flows"];
                break;
            case "Entities":
                selected = icons["Entities"];
                break;
            case "Assets":
                selected = icons["Assets"];
                break;
            case "References":
                selected = icons["References"];
                break;
            case "Characters":
                selected = icons["Characters"];
                break;
            case "Locations":
                selected = icons["Locations"];
                break;
            default:
                selected = icons["Folder"];
                break;
        }
        return selected;
    }

    private bool IsPredefined(string _str, TreeItem _parent)
    {
        return IsInstanceValid(_parent) 
        && ((_parent == root) 
        ||  (_parent.GetText(0) == "Entities" && (
            _str =="Characters" || _str =="Locations"// || _str ==""
        )));
    }

    private string PathFromTree(TreeItem treeItem)
    {
        if (treeItem == root) { return "/"; }
        TreeItem parent = treeItem.GetParent();
        string path = "/" + treeItem.GetText(0) + (treeItem.GetChildren() != null ? "/" : "");
        while (parent != root)
        {
            path =  "/" + parent.GetText(0) + path;
            parent = parent.GetParent();
        }
        if (treeItem.GetChildren() == null) { path += "." + treeItem.GetMeta("Extension"); }
        return path;
    }
}
