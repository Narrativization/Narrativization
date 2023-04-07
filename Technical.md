# Technical
[Design](./Design.md)

## Workspace & Settings Folder
- Default project folder: ***"~/Documents/Storier/"***
- User(Recent) Stories: ***"user://UserStories.ini"*** 
- User Settings(Data): ***"user://UserSettings.cfg"*** 

## Project Folder
- StoryName
    - Config                    : Project Config(.ini)
    - Content                   : Story(.tres)
        - Flows                 : Act/Chapter, Quest/Objective, Conversation/Dialogue
        - Entities              : Character, Location, Item
        - Assets                : Document, Image, Audio, Video, Miscellaneous
    - References                : Example Images, Real world references, Trivia(any format)
    - StoryName.storier         : Project Launcher(.json)

Possible Folders
Journeys: Global Variables(Decisions)
Templates: Presets of custom informations. For example Human Character
Universe, Timeline

~Some Signitures
- Id(/)
- Mentions(@)
- Tags(#)
- Links(Storier://, ->)

## File Formats
StoryElement(.storyel) file formats

- Story(.storier)               -> json
- Configuration(.ini)           -> config
- Flows, Entities(.storyel)     -> tres
- Assets(.storyel)              -> png, jpg, txt, csv... 

## Story Element
Path = ... /Flow/Chapter1 .storyel
- ... -> StoryBase
- /Flow/Chapter1 -> Id/Seid
- storyel -> Extension

Enum(Formats) of StoryElements
Flow, Entity{Character, Location, Item}, Assets(Resource/Research), ~Journey, ~Templates, ~References

StoryElement Nodes and Query
Similar to Web
- storier://Flows/Flow?Id="NodeId"&Name="NodeName"
- Query(?): /Flows/Flow.storyel?id="NodeId"
- Tag(#) 
- Mention(@)

## Classes
Storier/Story Manager : Node
Story : Node
StoryElement : Resource
StoryFragment : Godot.Object
Editors: Displays Resources and uses custom parent class

### Config Format
```
[StoryElement]
Name: "", 
Description: ""
StoryElementType: <Type>
...

[StoryElementType]
...
..
.

```

## Flows
### Flow Node & Flow Fragment
#### Node
Nodes with connection creates flow's display
- NodeId // Unique Id
- Type // or Template: GraphNode Scene(Dispay) Reference
- Offset // Location Of Node
- Title // Top Text ~[<15]
- Fragment // Referenced Fragment

#### Fragment 
Part of Story information
- FragmentId // Unique Id
- Name // Name of Fragment
- Content // Text Information
- Description // Tooltip 
- ~Type // Fragment Type(Flow, Action, Scene, Script)

### Line Segment
Includes Begin-End Nodes

### Options
Same use in Flow, Scene and Script
Flow uses slot to slot, scene uses choose, Script uses listed text 
    ->Name1         |        Name1        |        - Name1
Node->Name2         |        Name2        |        - Name2
    ->Name3         |        Name3        |        - Name3

## Scenario

Fountain
```
# Act

= Synopses 

EXT. Set (Location) - Time

Action

CHARACTER1
(Parenthetical)
Dialogue1

@CHARACTER2
Dialogue2

...

```

## Extra
Template Defination 
Name : Value ~:Type
Story <- Scene <- Sequence <- Shot
