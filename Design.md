# Design
[Technical Design](./Technical.md), [Story Design](#Story-Design), [User Interface](#User-Interfaces)

## Story Design
The story consists of various [elements](https://en.wikipedia.org/wiki/Literary_element). Storier interprets some of these elements in its own way.
- Story
    - Plot[Flows]
    - Character[Entity/Characters]
    - Setting[Entity/Locations]
    - Theme[StoryInfo], Conflict[StoryInfo]...

Storier uses *Story* as Project. And contain StoryElements which means Flow, Entity, Asset... 

Flow is the basic sequence of events and consists of flows/story fragments. Flows can be marked as keypoint similar to [Plot](https://en.wikipedia.org/wiki/Plot_(narrative)). Story Flows and Fragments can be referencing to Entities & Assets. 

Entities and Assets are different from each other. Entities are elements that are directly involved in the story for example Characters, Settings, Items. Assets indirectly affect the story and give more informations for example Character Image, Research Notes.

---

### Basic Folder Structure

- Story
    - Flows
    - Entities
        - Characters
        - Locations
    - Assets
    - ~Journeys
    - ~Templates
    - ~References

### Advanced Folder Structure

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
        - Links(Storier://, ->)

Also can consist of flows such as Universe, Timeline. 

## User Interfaces
[Design](#Design)

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
- CTRL + Arrows: Moving in the flow Connections.
- CTRL + SHIFT + Arrows: Jumping in the flow.
- ALT + Arrows, Mouse Back/Forward: Opening Previous and Next Edited Flows.

### Import/Export
Multi and Single File Save(or Export)
Game Engine integrated import/exports

### Inspirations
Good for creating causation; also ***Hero's Journey***, ***Three Act Structure*** etc. 
[Narrative Structures](https://en.wikipedia.org/wiki/Narrative_structure)
[Inspiration Helper](https://en.wikipedia.org/wiki/The_Thirty-Six_Dramatic_Situations)
SnowFlake, Story Structure Circle

## Extra
Scene Clapper, Take, Stage
~Aspect, Emotions