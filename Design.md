# Design
[General Design](#General), [Technical Design](#Technical)

## General
[Design](#Design), [Story Design](#Story-Design), [User Interface](#User-Interfaces), 

### Story Design

- Story
    - Plot[Flows]
    - Character[Entity/Characters]
    - Setting[Entity/Locations]
    - Theme[StoryInfo], Conflict[StoryInfo]...

Storier uses Story as Project.
Flow consists of flows and contains Entity & Assets. 
Entities consists of derived informations and contains Assets.
Assets are pure informations such as images.  
Every Flow and Entity templatable with changing their details.
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
SnowFlake

## User Interfaces
[General Design](#General)

### Editors
Editors
- Flow Editor (Graph-Node Editor)
- Property Editor (Entity Editor)
- Text Editor (Document Editor)
- ~Sheet Editor (List-Data Editor)

### Views
Modals
- Detail(Inspector): Edit   ~Property
- Navigator(Tree): File Structure
- List & Tile 
- Journey: Presentation like Visual Novel

### HotKeys
Be UX focused!
- CTRL + Arrows: Moving in the flow.
- (ALT + Arrows), (Mouse Back, Forward): Moving between flows.(Editors)
- ~(CTRL + Tab), (CTRL + Shift + Tab): Moving in Nodes Slot.

### Import/Export
Multi and Single File Save(or Export)
Game Engine integrated import/exports

### Narrative Structure
Good for creating causation; also ***Hero's Journey***, ***Three Act Structure*** etc. 
[Narrative Structures](https://en.wikipedia.org/wiki/Narrative_structure)

## Technical
[Design](#Design)

Workspace & Settings Folder
- Default project folder: ***"~/Documents/Storier"***
- User(Recent) Stories: ***"user://UserStories.ini"*** 
- User Settings(Data): ***"user://UserSettings.cfg"*** 

### Hierarchy Formats
- Story(.storier) -> json
    - Flows(.flow) -> config
    - Entities(.entity) -> json, ~config(Internationalization)
    - Assets -> png, jpg, txt, csv... 
    - Journeys() -> config
    - Templates(.template) -> config, json, ~xml
    - References(.reference) -> config
    - .Export -> json/csv(Entities)

Story
    - Story Elements (Flow, Entity, Assets, ...)
        - Files
        - SubFolders
            - Files


### Entity Format
Entity Example(.entity) -> .json
{
    Id: "", 
    Name: "", 
    Description: "",
    Template: "",
    Data: {
        ...
    }
}

StoryElement General Informations
Id, Name, Description, Template 
StoryElement Template Informations
Data
