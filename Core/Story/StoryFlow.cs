using Godot;
using System;

namespace Storier
{
    // Flow Definations
    public class StoryFlow : StoryElement
    {
        protected StoryFlow() {} // Called after base class!
        public StoryFlow(string _name, string _path) : base(_name, _path) 
        {
            storyElementType = StoryElementType.Flow;
        }

        public override void Save(string _data)
        {
            ConfigFile configFile = new ConfigFile();
            configFile.SetValue("StoryElement", "Name", ResourceName);
            Error err = configFile.Save(ResourcePath + ResourceName + ".flow");
            if (err != Error.Ok) { GD.Print(err); }
            base.Save(_data);
        }
    }
}
