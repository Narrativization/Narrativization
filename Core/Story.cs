using Godot;
using System;
using Godot.Collections;
using System.Reflection;

namespace Storier
{
    // Story Informations
    public struct StoryData
    {
        public string name;
        public string directory;
        public string author;
        public string copyright;
        
        // Default Documents
        public StoryData(string _name = "Untitled")
        {
            name = _name;
            directory = OS.GetSystemDir(OS.SystemDir.Documents) + "/Storier/" + name + "/";
            author = "";
            copyright = "";
        }

        public StoryData(string _name = "Untitled", string _directory = "")
        {
            name = _name;
            directory = _directory + "/";
            author = "";
            copyright = "";
        }

        // Default Documents
        public StoryData(string _name = "Untitled", string _author = "", string _copyright = "")
        {
            name = _name;
            directory = OS.GetSystemDir(OS.SystemDir.Documents) + "/Storier/" + name + "/"; // Default Documents
            author = _author;
            copyright = _copyright;
        }

        public StoryData(string _name = "Untitled", string _directory = "", string _author = "", string _copyright = "")
        {
            name = _name;
            directory = _directory + "/";
            author = _author;
            copyright = _copyright;
        }

        // "Member: Value" based ToString 
        public override string ToString()
        {
            // return base.ToString();
            FieldInfo[] fi = typeof(StoryData).GetFields(BindingFlags.Public | BindingFlags.Instance);
            string tString = "\n[" + base.ToString() + "]\n";
            foreach (FieldInfo info in fi)
            {
                tString += info.Name + ":\t" + (string)info.GetValue(this).ToString() + "\n";
            }
            tString += "\n";
            return tString;
        }
    }

    // Story(Project) 
    public class Story : Node
    {
        // Story Data
        public StoryData storyData = new StoryData();


        public Story(StoryData _storyData)
        {
            storyData = _storyData;
            SaveStory();
        }
        public Story(string _storyName = "Untitled")
        {
            storyData = new StoryData(_storyName);
            SaveStory();
        }
        public Story(string _storyName = "Untitled", string _directory = "")
        {
            storyData = new StoryData(_storyName, _directory);
            SaveStory();
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            
        }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

        public void SaveStory()
        {
            Dictionary saveData = new Dictionary()
            {
                {
                    "Story", new Dictionary(){
                        {"Name", storyData.name},
                        {"Author", storyData.author},
                        {"Copyright", storyData.copyright}
                    }
                },
                {
                    "Storier", new Dictionary(){
                        // Version: "v0.2"
                    }
                }
            };

            FileManager.SaveFile(storyData.directory + storyData.name + ".storier", JSON.Print(saveData, "\t"));
            CreateHierarchy();
        }

        public void LoadStory(string fileName)
        {
            string str = FileManager.LoadFile(fileName);
            JSONParseResult jsonObject = JSON.Parse(str);

            if(jsonObject.Error != 0)
            {
                GD.Print("Error:", jsonObject.ErrorLine);
            } 
            else
            {
                GD.Print(JSON.Print(jsonObject.Result)); // Print
                Dictionary storyJson = jsonObject.Result as Dictionary; // Godot.Collections.Array  <-  [] 
                storyData = new StoryData(fileName.BaseName(), fileName.GetBaseDir());

                if (storyJson.Contains("Story"))
                {
                    Dictionary storyInfos = storyJson["Story"] as Dictionary; 
                    if (storyInfos.Contains("Name") && storyInfos.Contains("Author") && storyInfos.Contains("Copyright"))
                    {
                        storyData.name = storyInfos["Name"] as string;
                        storyData.author = storyInfos["Author"] as string;
                        storyData.copyright = storyInfos["Copyright"] as string;
                    }
                }
                GD.Print(storyData); // Print
            }
        }

        // Create Folder/File Structure of project
        public void CreateHierarchy()
        {
            Godot.Collections.Array<string> defaultDirs = new Godot.Collections.Array<string>()
            {
                storyData.directory + "Flows/",
                storyData.directory + "Entities/",
                storyData.directory + "Entities/Characters/",
                storyData.directory + "Entities/Locations/",
                // storyData.directory + "Entities/Items/",
                storyData.directory + "Assets/",
                // storyData.directory + "Journeys/"
                // storyData.directory + "Templates/"
                storyData.directory + "References/"
            };
            Directory dir = new Directory();
            for (int i = 0; i < defaultDirs.Count; i++)
            {
                dir.MakeDirRecursive(defaultDirs[i]);
            }
        }

        public Dictionary LookHierarchy()
        {
            return FileManager.DirectoryContents(storyData.directory);
        }

    }
}

/*
    Create a project file structure | .storier
    {
        Story: {
            Name: "Storier",
            Author: "Atakan Ateş",
            Copyright: "Copyright © 2020 Atakan Ateş, All Rights Reserved!"
        },
        Storier: {
            // 
            // Version: "v0.2"
        }
    }
*/

/*
Story Hierarchy - File/Folder Structure
- Story(.storier) -> json
    - Flows(.flow) -> config
    - Entities(.entity) -> json
        - Characters
        - Locations
        - ~Items
    - Assets -> png, jpg, txt, csv... 
    - Journeys() -> config
    - Templates(.template) -> config, json, ~xml
    - References(.reference) -> config
*/
