using Godot;
using System;
using Godot.Collections;
using Array = Godot.Collections.Array;

// File system managing like Save/Load, Search, Directory Structure etc.
public static class FileManager
{
    // TODO: Change Return Values to Error
    

    public static bool SaveFile(string fileName, string content = "", File.ModeFlags fmode = File.ModeFlags.WriteRead)
    {
        if (fileName != null)
        {
            Directory dir = new Directory(); 
            if (!dir.DirExists(fileName.GetBaseDir())) // Check Directory ~Folder can't found!
            {
                Error dirErr = dir.MakeDirRecursive(fileName.GetBaseDir());
                if (dirErr != Error.Ok)
                {
                    GD.PushError("Save File Directory Error Code: " + dirErr);
                }
            }

            File saveFile = new File(); 
            Error err = saveFile.Open(fileName, fmode);
            if (err == Error.Ok)
            {
                saveFile.StoreString(content);
                saveFile.Close();
                return true;
            }
            else
            {
                GD.PushError("Save File Error Code: " + err);
            }
        }
        else
        {
            GD.PushError("SaveFile fileName is null!");
        }
        return false;
    }     

    public static string LoadFile(string fileName, File.ModeFlags fmode = File.ModeFlags.Read)
    {
        string content = ""; 
        if (fileName != null)
        {
            File loadFile = new File(); 
            Error err = loadFile.Open(fileName, fmode);
            if (err == Error.Ok)
            {
                content = loadFile.GetAsText();
                loadFile.Close();
            }
            else
            {
                GD.PushError("Load File Error Code: " + err);
            }
        }
        else
        {
            GD.PushError("LoadFile fileName is null!");
        }
        return content;
    }

    public static bool DeleteFile(string fileName, File.ModeFlags fmode = File.ModeFlags.WriteRead)
    {
        if (fileName != null)
        {
            Directory deleteDir = new Directory(); 
            Error err = deleteDir.Remove(fileName);
            if (err == Error.Ok)
            {
                return true;
            }
            else
            {
                GD.Print("Delete File Error Code: " + err);
            }
        }
        else
        {
            GD.Print("DeleteFile fileName is null!");
        }
        return false;
    }
    
    public static void DeleteRecursively(string fileName)
    {
        Directory directory = new Directory();
        Error err = directory.Open(fileName);
        if (err == Error.Ok)
        {
            // List directory content
            directory.ListDirBegin(true);
            string fName = directory.GetNext();
            while (fName != "")
            {
                if (directory.CurrentIsDir())
                {
                    DeleteRecursively(fileName + "/" + fName);
                }
                else
                {
                    directory.Remove(fName);
                }
                fName = directory.GetNext();
            }
            directory.Remove(fileName);
        }
        else
        {
            GD.Print("DeleteRecursive Error: " + err);
        }
    }

    public static Error CopyFile(string fileName, string copyName, File.ModeFlags fmode = File.ModeFlags.ReadWrite)
    {
        string content = ""; 
        if (fileName != null)
        {
            content = LoadFile(fileName);
            SaveFile(copyName, content);
        }
        else
        {
            GD.PushError("LoadFile fileName is null!");
        }
        return Error.Ok;
    }

    public static Error MoveFile(string from, string to, File.ModeFlags fmode = File.ModeFlags.ReadWrite)
    {
        string content = "";
        if (from != null)
        {
            content = LoadFile(from);
            SaveFile(to, content);
            if(!DeleteFile(from))
            {
                DeleteRecursively(from);
            }
        }
        else
        {
            GD.PushError("MoveFile fileName is null!");
        }
        return Error.Ok;
    }

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

    // Example -> 0: "/Flow/Flow1.flow"
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