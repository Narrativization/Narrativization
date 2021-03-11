using Godot;
using System;

namespace Storier
{
    // Entity Defination
    public class StoryEntity : StoryElement
    {
        protected StoryEntity() {} // Called after base class!
        public StoryEntity(string _name, string _path) : base(_name, _path) 
        {
            storyElementType = StoryElementType.Entity;
        }

        public override void Save(string _data)
        {
            if (!FileManager.SaveFile(ResourcePath + ResourceName + ".entity", _data))
            {
                GD.Print(ResourceName + " save Failed!");
                return; 
            }
            base.Save(_data);
        }

        public override void Load()
        {
            if (FileManager.LoadFile(ResourcePath + ResourceName + ".entity") == "")
            {
                GD.Print(ResourcePath + ResourceName + ".entity" + " load failed!");
                return; 
            }
            base.Load();
        }
    }
}
