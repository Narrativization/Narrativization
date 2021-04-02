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

        public override bool Save(string _data)
        {
            ConfigFile configFile = new ConfigFile();
            configFile.SetValue("StoryElement", "Name", ResourceName);
            Error err = configFile.Save(ResourcePath);
            if (err != Error.Ok) { GD.Print(err); }
            base.Save(_data);
            return false;
        }


        public static StoryFlow Load(string _id)
        {
            string fileName = StoryManager.CurrentStory.StoryBase + _id + ".entity";
            ConfigFile configFile = new ConfigFile();
            Error err = configFile.Load(fileName);
            if (err != Error.Ok) { GD.Print(err); return null; }
            string loadedId = configFile.GetValue("StoryElement", "Id").ToString();
            if (loadedId != _id) { GD.Print("Loaded File Id and Id isn't same!"); }
            string loadedName = configFile.GetValue("StoryElement", "Name").ToString();
            string loadedDescription = configFile.GetValue("StoryElement", "Id").ToString();
            return new StoryFlow(_id, loadedName, loadedDescription);
        }

        public static StoryFlow GetStoryFlow(string _id)
        {
            for (int i = 0; i < storyFlows.Count; i++)
            {
                if (IsInstanceValid(storyFlows[i]) && storyFlows[i].Id == _id)
                {
                    return storyFlows[i];
                }
            }
            return Load(_id);
        }

        

    }
}
