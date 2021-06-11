# Design
[General Design](#General), [Technical Design](#Technical)

## General
[Story Design](#Story-Design), [User Interface](#User-Interfaces)

### Story Design

- Story
    - Plot[Flows]
    - Character[Entity/Characters]
    - Setting[Entity/Locations]
    - Theme[StoryInfo], Conflict[StoryInfo]...

---

Storier uses Story as Project.
Flow consists of flows/story fragments.
Flows/story fragments contains Entity & Assets. 
Entities consists of derived informations and contains Assets.
Assets are pure informations such as images.  
~~Every Flow and Entity templatable with changing their details.~~
Some points(nodes or subflow) in the flow can be marked as keypoints([Plot](https://en.wikipedia.org/wiki/Plot_(narrative))).

#### Basic Folder Structure

- Story
    - Flows
    - Entities
        - Characters
        - Locations
    - Assets
    - Journeys
    - Templates
    - References

#### Advanced Folder Structure

- Story
    - Flows
        - Act
            - Chapter
        - Quest(~Cinamatic/Gameplay)
            - Dialogue
    - Entities
        - Characters
            - Files
            - Folders
                - Files
        - Items
        - Locations
        - ~Information 
        - ~Abstraction
    - Assets
        - Documents(~Notes)
        - Images
        - Audios
        - Videos
        - Miscellaneous
        - ~Thrash
    - Journeys
        - Global Variables(Decisions)
    - Templates
        - Act ...
        - Entities(Pre Templated)
            - Character …
    - References
        - Id(/)
        - Mentions(@)
        - Tags(#)
        - Links(href, :, ->)

Also can consist of flows such as Universe, Timeline. 

## User Interfaces
[General Design](#General)

### Views
Tools
- Editors: [Editing Files](#Editors)
- Navigator(Outline): File Structure/FolderTree
- Details(Inspector): Edit Selected
- Journey: Play, Presentation like Visual Novel
- Idea, Notes: Fastly take Notes

### Editors
Editors work based on same data.
- Flow Editor (Graph-Node Editor) -> Flow
- Scene Editor (Visual Novel-Slide Editor) -> Flow
- Script Editor (Document-Text Editor) -> Story Element
- Property Editor (Entity-File Editor) -> Entity
- ~Sheet Editor (List/Tile-Data Editor) -> Multiple Story Element, Folders

### HotKeys
Be UX focused!
- CTRL + Arrows: Moving in the flow.
- CTRL + SHIFT + Arrows: Jumping in the flow.
- ALT + Arrows, Mouse Back, Forward: Opening Previous and Next Edited Flows.
- ~(CTRL + Tab), (CTRL + Shift + Tab): Moving in Nodes Slot.

### Import/Export
Multi and Single File Save(or Export)
Game Engine integrated import/exports

### Inspirations
Good for creating causation; also ***Hero's Journey***, ***Three Act Structure*** etc. 
[Narrative Structures](https://en.wikipedia.org/wiki/Narrative_structure)
[Inspiration Helper](https://en.wikipedia.org/wiki/The_Thirty-Six_Dramatic_Situations)
SnowFlake, Story Structure Circle

## Technical
[Design](#Design)

Workspace & Settings Folder
- Default project folder: ***"~/Documents/Storier/"***
- User(Recent) Stories: ***"user://UserStories.ini"*** 
- User Settings(Data): ***"user://UserSettings.cfg"*** 

### Hierarchy Formats
- Story(.storier)               -> json
    - Flows(.flow)              -> config
    - Entities(.entity)         -> config
    - Assets(.asset)            -> config, png, jpg, txt, csv... 
    - Journeys(.journey)        -> config
    - Templates(.template)      -> tscn, config
    - References(.reference)    -> config
    - .Export -> config, json, csv(Entities)
    ~StoryElement(.storyel) 

### Story Element
Path = ... /Flow/Chapter1 .flow
- ... -> StoryBase
- /Flow/Chapter1 -> Id
- flow -> Extension

#### Config Format
[StoryElement]

{
    Name: "", 
    Description: ""
    ...
}

[StoryElementType]

Data: ...

### Extra
Template Defination
Name : Value ~:Type
Story <- Scene <- Sequence <- Shot

Scene Clapper, Take, Stage
~Aspect, Emotions