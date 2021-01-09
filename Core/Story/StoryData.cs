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
        public string description;
        public string directory;
        public string author;
        public string copyright;
        
        // Default Documents
        public StoryData(string _name = "Untitled")
        {
            name = _name;
            description = "";
            directory = StoryManager.workspace + name + "/";
            author = "";
            copyright = "";
        }

        public StoryData(string _name = "Untitled", string _description = "", string _directory = "", string _author = "", string _copyright = "")
        {
            name = _name;
            description = _description;
            directory = (_directory == "") ?  StoryManager.workspace + name + "/" : _directory + "/";
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

        public Dictionary SaveData()
        {
            return new Dictionary()
            {
                {"Name", name},
                {"Description", description},
                {"Author", author},
                {"Copyright", copyright}
            };
        }

        public void LoadData(Dictionary storyInfos)
        {
            if (storyInfos.Contains("Name") 
            && storyInfos.Contains("Description") 
            && storyInfos.Contains("Author") 
            && storyInfos.Contains("Copyright"))
            {
                name = storyInfos["Name"] as string;
                description = storyInfos["Description"] as string;
                author = storyInfos["Author"] as string;
                copyright = storyInfos["Copyright"] as string;
            }
        }
    }
}