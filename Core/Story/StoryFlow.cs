using Godot;
using System;

namespace Storier
{
    // Flow Definations
    public class StoryFlow : StoryElement
    {
        // All Opened Flows
        private static Godot.Collections.Array<StoryFlow> storyFlows = new Godot.Collections.Array<StoryFlow>();

        protected StoryFlow() {} 
        public StoryFlow(string _id, string _name, string _description) 
        {
            storyElementType = StoryElementType.Flow;
            base.InitStoryElement(_id, _name, _description, ".flow");
            storyFlows.Add(this);
        }

        public override void Save(string _data)
        {
            ConfigFile configFile = new ConfigFile();
            configFile.SetValue("StoryElement", "Name", ResourceName);
            Error err = configFile.Save(ResourcePath + ResourceName + ".flow");
            if (err != Error.Ok) { GD.Print(err); }
            base.Save(_data);
        }

        

        // public static StoryFlow Load(string _id)
        // {
        //     string fileName = StoryManager.currentStory.storyData.directory + _id + ".entity";
        //     ConfigFile configFile = new ConfigFile();
        //     Error err = configFile.Load(fileName);
        //     if (err != Error.Ok) { GD.Print(err); return null; }
        //     string loadedId = configFile.GetValue("StoryElement", "Id").ToString();
        //     if (loadedId != _id) { GD.Print("Loaded File Id and Id isn't same!"); }
        //     string loadedName = configFile.GetValue("StoryElement", "Name").ToString();
        //     string loadedDescription = configFile.GetValue("StoryElement", "Id").ToString();
        //     return new StoryEntity(_id, loadedName, loadedDescription);
        // }
    }
}
