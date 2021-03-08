# Design
[General Design](#General), [Technical Design](#Technical)

## General
[Design](#Design), [Folder Structure and Flows](#Folder-Structure-and-Flows), [User Interface](#User-Interfaces), 

### Folder Structure and Flows
[General Design](#General)

Storier uses "Flow in Flow" Logic: Each Flow consists of another flow.

Story(Project) 
Flow consists of flows and contains Entity & Assets. 
While Entities are derived informations from assets and user defines, Assets are pure informations.
Every Flow and Entity templatable with changing their details.

Some points(nodes or subflow) in the flow can be marked as keypoints([Plot](https://en.wikipedia.org/wiki/Plot_(narrative))). [Narrative Structures](#Narrative-Structure)

### Basic

Basic Folder Structure
- Story
    - Flows
    - Entities
    - Assets
    - Journeys
    - Templates
    - References

### Advanced

Customized or enhanced file structure
- Story
    - Flows
        - Act
            - Chapter
                - Quest(~Cinamatic/Gameplay)
                    - Dialogue
    - Entities
        - Character
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
        - Mentions(@)
        - Tags(#)
        - Links(href, :, ->)

### Depth of Flows

Flows can be deepened as desired.
- Use case 1
    - Flow
        - Act 
            - Chapter
                - Quests(~Cinamatic/Gameplay)
                    - Dialogues
- Use case 2
    - Flow
        - Universe
            - Timeline
                - Act 
                    - Chapter
                        - Quests(~Cinamatic/Gameplay)
                            - Dialogues
- Use case 3
    - Flow
        - Story Flow
            - Dialogue

## User Interfaces
[General Design](#General)

### Editors
Main Panel
- Flow Editor (Node-Graph Editor)
- Data Editor (Sheet Editor)
- Document Editor (Text Editor)
- ~Property Editor (Entity Editor)

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

### Story Elements
- Story
    - Plot[Flow]
    - Character[Entity]
    - Settings[Entity]
    - ~Theme[StoryInfo], Conflict[?], ~~Point of View, Tone, Style

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
