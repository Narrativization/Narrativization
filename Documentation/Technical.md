# Technical
[Design](./Design.md), [User Interface](#User-Interfaces)

## Workspace & Settings Folder
- Default project folder: ***"~/Documents/Storier/"***
- User (Recent) Stories: ***"user://UserStories.ini"*** 
- User Settings (Data): ***"user://UserSettings.cfg"*** 
- Project Settings (Data): ***"<ProjectDirectory>/Config/ProjectSettings.ini"*** 

## Project Folder
- StoryName
    - Assets                    : Document, Image, Audio, Video, Miscellaneous
    - Config                    : Project Config(.ini)
    - Entities                  : Character, Location, Item
    - Flows                     : Act/Chapter, Quest/Objective, Conversation/Dialogue
    - Researches                : Example Images, Real world references, Trivia(any format)
    - StoryName.storier         : Project Launcher(.json)

Possible Folders
**Journeys:** Run/Play like Visual Novel
**Variables:** Decisions, Requirements, Instruction(Set Value), Conditions (Check/if), Chooses, Global Variables, Situations
**Templates:** Presets of custom informations. For example Human Character
Universe, Timeline

~Some Signitures
- Id(/)
- Mentions(@)
- Tags(#)
- Links(Storier://, ->)

## File Formats
Storier uses some file formats

- Story                         -> storier (json)
- Configuration                 -> config (ini, cfg)
- StoryElement(Flows, Entities) -> storyel (tres)
- Assets                        -> png, jpg, txt, csv... 

## Story Element
Path = ... /Flow/Chapter1 .storyel
- ... -> StoryBase
- /Flow/Chapter1 -> Id/Seid
- storyel -> Extension (or tres)

StoryElement Nodes and Query
Similar to Web
- storier://Flows/Flow?Id="NodeId"&Name="NodeName"
- Query(?): /Flows/Flow.storyel?id="NodeId"
- Tag(#) 
- Mention(@)

Informations
- Id // Unique Id
- Name // Name of Fragment
- Description // Synopsis, Tooltip 
- Content // Information of Story Element
~ Template?

## Classes
Storier/Story Manager: Node
Story: Node -> Resource
StoryElement: Resource
StoryFragment: Godot.Object
Editors: Displays Resources and uses custom parent class

## Flows
Subflows can be contain Begin-End Nodes

### Flow Node
Displays of Fragment
- NodeId    // Unique Id
- Title     // Short Name of Node
- Icon      // Image
- Fragment  // Referenced Fragment
- Elements  // Referenced Objects or Story Elements

### Flow Fragment 
Part of Flow
- FragmentId // Unique Id
- Name // Name of Fragment
- Description // Synopsis, Tooltip 
- Content // Text or Custom Information, References another flow

I.e.: Story, Quest, Dialogue

### Extra
Template Defination 
Name : Value ~:Type
Story <- Scene <- Sequence <- Shot

## User Interfaces

### Editors
Editors work based on same data.
- Flow Editor (Graph/Node Editor) -> Flow
- Block Editor (Visual Novel, Scene/Stage Editor) -> Flow
- Script Editor (Document, Text Editor) -> Story Element
- Property Editor (Entity, File Editor) -> Entity
- ~Sheet Editor (List/Tile, Data Editor) -> Multiple Story Element, Folders

### Views
Tools
- Editors: [Editing](#Editors)
- Navigator(Outline): File Structure/FolderTree
- Details(Inspector): Edit Selected
- Journey: Play, Presentation like Visual Novel
- Menu Bar: Menu button and Title | on Top
- Status Bar: Give information about story, Includes Ideas/Notes(Fastly take Notes) button | on Bottom

### HotKeys
Be UX focused!
- ALT + Arrows: Jump to nearby nodes. 
- CTRL + ALT + Arrows: Create/Popup vertical node list Context Menu and Jump to hovered/selected item/node.
- CTRL + TAB: Move next input field.
- CTRL + SHIFT + TAB: Move previous input field.
- ALT + Mouse Back/Forward: Opening previous/next flow.
- CTRL + SHIFT + LMB: Create empty flow.
- CTRL + E: Edit last selected flow.
- CTRL + D: duplicate selected flow.
- Alt + D: duplicate instance of selected flow.

### Import/Export
Multi and Single File Save(or Export)
Game Engine integrated import/exports
