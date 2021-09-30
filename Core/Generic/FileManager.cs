using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

// File system managing like Save/Load, Search, Directory Structure etc.
public static class FileManager
{
    public static Error SaveFile(string fileName, string content = "", File.ModeFlags fmode = File.ModeFlags.WriteRead)
    {
        Error err = Error.Ok;
        if (fileName == null) { return Error.FileBadPath; }
        Directory dir = new Directory(); 
        if (!dir.DirExists(fileName.GetBaseDir())) { err = dir.MakeDirRecursive(fileName.GetBaseDir()); } // Create Path Directory
        File saveFile = new File(); 
        err = saveFile.Open(fileName, fmode);
        if (err != Error.Ok) { return err; }
        saveFile.StoreString(content);
        saveFile.Close();
        return err;
    }     

    public static Error LoadFile(string fileName, out string content, File.ModeFlags fmode = File.ModeFlags.Read)
    {
        Error err = Error.Ok;
        content = ""; 
        if (fileName == null) { return Error.FileBadPath; }
        File loadFile = new File(); 
        err = loadFile.Open(fileName, fmode);
        if (err != Error.Ok) { return err; }
        content = loadFile.GetAsText();
        loadFile.Close();
        return err;
    }

    public static Error DeleteFile(string fileName, File.ModeFlags fmode = File.ModeFlags.WriteRead)
    {
        Error err = Error.Ok;
        if (fileName == null) { return Error.FileBadPath; }
        Directory deleteDir = new Directory(); 
        err = deleteDir.Remove(fileName);
        return err;
    }
    
    public static Error DeleteRecursively(string fileName)
    {
        Directory directory = new Directory();
        Error err = directory.Open(fileName);
        if (err != Error.Ok) { return err; }
        directory.ListDirBegin(true); // List directory content
        string fName = directory.GetNext();
        while (fName != "")
        {
            if (directory.CurrentIsDir())
            {
                err = DeleteRecursively(fileName + "/" + fName);
            }
            else
            {
                err = directory.Remove(fName);
            }
            fName = directory.GetNext();
        }
        directory.Remove(fileName);
        return err;
    }

    public static Error CopyFile(string fileName, string copyName, File.ModeFlags fmode = File.ModeFlags.ReadWrite)
    {
        Error err = Error.Ok;
        string content = ""; 
        if (fileName == null) { return Error.FileBadPath; }
        err = LoadFile(fileName, out content);
        if (err != Error.Ok) { return err; }
        err = SaveFile(copyName, content);
        return err;
    }

    public static Error MoveFile(string from, string to, File.ModeFlags fmode = File.ModeFlags.ReadWrite)
    {
        Error err = Error.Ok;
        string content = "";
        if (from == null) { return Error.FileBadPath; }
        err = LoadFile(from, out content);
        if (err != Error.Ok) { return err; }
        err = SaveFile(to, content);
        if (err != Error.Ok) { return err; }
        if(DeleteFile(from) != Error.Ok)
        {
            DeleteRecursively(from);
        }
        return err;
    }

    //  - Up Updated! - 

    public static void ReadCsvFile(string fileName)
    {
        if (fileName != null)
        {
            File csvFile = new File(); 
            Error err = csvFile.Open(fileName, File.ModeFlags.Read);
            if (err == Error.Ok)
            {
                while (!csvFile.EofReached())
                {
                    string[] csvLine = csvFile.GetCsvLine(); // ";"
                    for (int i = 0; i < csvLine.Length; i++)
                    {
                        GD.Print("csvLine" + i + ": " + csvLine[i]);
                    }
                }
            }
            else
            {
                GD.Print(err);
            }
            csvFile.Close();
        }
    }

    // Example -> 0: "/Flow/Flow1.storyel"
    public static Array DirectoryTreeAsArray(string fileName)
    {
        Array treeArray = new Array(){}; 
        treeArray = DirectoryTreeWalkerAsArray(fileName, fileName, treeArray);
        return treeArray;
    }

    private static Array DirectoryTreeWalkerAsArray(string fileName, string baseFileName, Array tree)
    {
        Directory dir = new Directory();
        if (dir.Open(fileName) == Error.Ok)
        {
            dir.ListDirBegin(true, true);
            string fName = dir.GetNext();
            while (fName != "")
            {
                if (dir.CurrentIsDir())
                {
                    tree.Add(fileName.Replace(baseFileName, "") + "/" + fName + "/");
                    tree += DirectoryTreeWalkerAsArray(fileName + "/" + fName, baseFileName, new Array());
                }
                else
                {
                    tree.Add(fileName.Replace(baseFileName, "") + "/" + fName);
                }
                fName = dir.GetNext();
            }
            dir.ListDirEnd();
        }
        return tree;
    }

    // { Flows: { Flow:flow }}
    public static Dictionary DirectoryTreeAsDictionary(string fileName)
    {
        Dictionary dictionary = new Dictionary(){};
        dictionary = DirectoryTreeWalkerAsDictionary(fileName, dictionary);
        return dictionary;
    }

    private static Dictionary DirectoryTreeWalkerAsDictionary(string fileName, Dictionary dictionary)
    {
        Directory dir = new Directory();
        if (dir.Open(fileName) == Error.Ok)
        {
            dir.ListDirBegin(true, true);
            string fName = dir.GetNext();
            while (fName != "")
            {
                if (dir.CurrentIsDir())
                {
                    dictionary.Add(fName, DirectoryTreeWalkerAsDictionary(fileName + "/" + fName, new Dictionary()));
                }
                else
                {
                    dictionary.Add(fName.BaseName(), fName.Extension());
                }
                fName = dir.GetNext();
            }
            dir.ListDirEnd();
        }
        return dictionary;
    }

    private static Dictionary DirectoryTreeWalkerBaseName(string fileName, Dictionary dictionary)
    {
        Directory dir = new Directory();
        if (dir.Open(fileName) == Error.Ok)
        {
            dir.ListDirBegin(true, true);
            string fName = dir.GetNext();
            while (fName != "")
            {
                if (dir.CurrentIsDir())
                {
                    dictionary.Add(fName.BaseName(), DirectoryTreeWalkerAsDictionary(fileName + "/" + fName, new Dictionary()));
                }
                else
                {
                    dictionary.Add(fName.BaseName(), "");
                }
                fName = dir.GetNext();
            }
            dir.ListDirEnd();
        }
        return dictionary;
    }

    public static Godot.Collections.Array DirectoryContents(string fileName)
    {
        Godot.Collections.Array dictionary = new Godot.Collections.Array();
        Directory dir = new Directory();
        if (dir.Open(fileName) == Error.Ok)
        {
            dir.ListDirBegin(true, true);
            string fName = dir.GetNext();
            while (fName != "")
            {
                if (dir.CurrentIsDir())
                {
                    dictionary.Add(fName.BaseName());
                }
                else
                {
                    dictionary.Add(fName.BaseName());
                }
                fName = dir.GetNext();
            }
            dir.ListDirEnd();
        }
        return dictionary;
    }

    public static Godot.Collections.Array ScanDirectory(string fileName)
    {
        string fName = "";
        Godot.Collections.Array files = new Godot.Collections.Array();
        Directory dir = new Directory();
        Error err = dir.Open(fileName);
        if (err != Error.Ok)
        {
            GD.Print("ScanDirectory ErrorCode:" + err);
        }
        dir.ListDirBegin(true);
        fName = dir.GetNext();
        while (fName != "")
        {
            if (dir.CurrentIsDir())
            {
                string subFileName = fileName + "/" + fName; 
                foreach (var f in ScanDirectory(subFileName)) // Append not found in Godot Array
                {
                    files.Add(f);
                }
            }
            else
            {
                string n = fileName + "/" + fName; 
                files.Add(n);
            }
            fName = dir.GetNext();
        }
        dir.ListDirEnd();
        return files;
    }

    public static ImageTexture ResizeImage(Texture texture, int width = 0, int height = 0)
    {
        Image img = texture.GetData();
        ImageTexture imgTex = new ImageTexture();
        if (width > 0 && height > 0)
        {
            img.Resize(width, height);
        }
        imgTex.CreateFromImage(img);
        return imgTex;
    }
}