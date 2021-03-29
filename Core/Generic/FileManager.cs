using Godot;
using System;
using Godot.Collections;

namespace Storier
{
    // File system managing like Save/Load, Search, Directory Structure etc.
    public static class FileManager
    {
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
                    GD.PushError("Delete File Error Code: " + err);
                }
            }
            else
            {
                GD.PushError("DeleteFile fileName is null!");
            }
            return false;
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

        public static Dictionary DirectoryTree(string fileName)
        {
            Dictionary dictionary = new Dictionary(){};
            dictionary = DirectoryTreeWalker(fileName, dictionary);
            return dictionary;
        }

        private static Dictionary DirectoryTreeWalker(string fileName, Dictionary dictionary)
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
                        dictionary.Add(fName.BaseName(), DirectoryTreeWalker(fileName + "/" + fName, new Dictionary()));
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
}