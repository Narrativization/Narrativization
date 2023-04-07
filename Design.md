# Design
[Technical Design](./Technical.md)

[Story Design](#Story-Design), [Project Folder](#Project-Folder), [User Interface](#User-Interfaces)

## Story Design
The story consists of various [narrative element](https://en.wikipedia.org/wiki/Literary_element). Storier interprets some of these elements in its own way.
- Story
    - Plot[Flows]
    - Character[Entity/Characters]
    - Setting[Entity/Locations]
    - Theme[StoryInfo], Conflict[StoryInfo]...

### Story
Storier uses *Story* as Project. And contain StoryElements which means Flow, Entity, Asset... 

### Flow
Flow is the basic sequence of events and consists of flows/story fragments. Flows can be marked as keypoint similar to [Plot](https://en.wikipedia.org/wiki/Plot_(narrative)). Story Flows and Fragments can be referencing to Entities & Assets. 

### Entity & Asset
Entities and Assets are different from each other. Entities are elements that are directly involved in the story for example Characters, Settings, Items. Assets indirectly affect the story and give more informations for example Character Image, Research Notes.

---

## Project
### Project Folder
- StoryName
    - Config
    - Content
        - Flows
        - Entities
            - Characters
            - Locations
        - Assets
    - References (External Informations, Trivia)

## User Interfaces

### Views
Tools
- Editors: [Editing](#Editors)
- Navigator(Outline): File Structure/FolderTree
- Details(Inspector): Edit Selected
- Journey: Play, Presentation like Visual Novel
- Menu Bar: Menu button and Title | on Top
- Status Bar: Give information about story, Includes Ideas/Notes(Fastly take Notes) button | on Bottom

### Editors
Editors work based on same data.
- Node Editor (Graph-Node Editor) -> Flow
- Block Editor (Visual Novel-Scene/Stage Editor) -> Flow
- Script Editor (Document-Text Editor) -> Story Element
- Property Editor (Entity-File Editor) -> Entity
- ~Sheet Editor (List/Tile-Data Editor) -> Multiple Story Element, Folders

### HotKeys
Be UX focused!
- CTRL + Arrows: Moving in the flow Connections.
- CTRL + SHIFT + Arrows: Jumping in the flow.
- ALT + Arrows, Mouse Back/Forward: Opening Previous and Next Edited Story Element.

### Import/Export
Multi and Single File Save(or Export)
Game Engine integrated import/exports

## Narrations
Short Story, Scenario, E-Conte, etc. scripting/screenplay formats.
> Note: Currently Storier isn't supporting of known screenplays, mainly focuses on branching. 

### Scenario
General Film Scenario, Fountain
```
Int ... [Scene Heading]
Character1, ... [Scene Characters]
... [Action]
    Character           -
    (Parenthetical)         Dialogue Fragment
    Dialogue            -
Shot, Transition, Non-printable, Lyrics
```

### Story Boarding
Graphical Scenario, Japan(E-Conte) Scenario (No: Sheet Number, S/C: Scene/Cut Number)
```
No ...                                         | Page _/_        ProjectName: __
S/C, Action,  Picture, Dialogue&Sound, Time
```

### Inspirations
Good for creating causation; also ***Hero's Journey***, ***Three Act Structure*** etc. 
[Narrative Structures](https://en.wikipedia.org/wiki/Narrative_structure)
[Inspiration Helper](https://en.wikipedia.org/wiki/The_Thirty-Six_Dramatic_Situations)
SnowFlake, Story Structure Circle

## Extra
Scene Clapper, Take, Stage
~Aspect, Emotions [Techniques](https://en.wikipedia.org/wiki/List_of_narrative_techniques)