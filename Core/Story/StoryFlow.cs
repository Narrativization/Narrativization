using Godot;
using System;
using Godot.Collections;

namespace Storier
{
    // Flow Definations
    public class StoryFlow : StoryElement
    {
        // Custom Story Element Data
        public Dictionary<string, string> StoryFlowData { get; set; } = new Dictionary<string, string>();

        protected StoryFlow() {} 
        // StoryElementPath = StoryBase[.../<StoryName>] + StoryElementId[/Flow/Chapter1] + StoryElementExtension
        public StoryFlow(string path) : base(path)
        {
            StoryElementType = StoryElementTypes.Flow;
            StoryElementExtension = ".flow";
        }

        public override bool Save()
        {
            ConfigFile configFile = new ConfigFile();
            configFile.SetValue("StoryElement", "Data", StoryElementData);
            configFile.SetValue("Flow", "Data", StoryFlowData);
            Error err = configFile.Save(ResourcePath);
            if (err != Error.Ok) { GD.Print(err); return false; }
            return true;
        }

        // Loading Story Element
        public static new StoryFlow Load(string path) 
        { 
            StoryFlow loaded = new StoryFlow(path);
            ConfigFile configFile = new ConfigFile();
            Error err = configFile.Load(path);
            if (err != Error.Ok) { GD.Print(err); return null; }
            Dictionary elementDictionary = configFile.GetValue("StoryElement", "Data") as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
            loaded.StoryElementData = new Dictionary<string, string>(elementDictionary);
            Dictionary flowDictionary = configFile.GetValue("Flow", "Data") as Dictionary;  // Godot Problem: "as Dictionary<string, string>" return null
            loaded.StoryFlowData = new Dictionary<string, string>(flowDictionary);
            return loaded; 
        }
    }
}