# Technical
[Design](./Design.md)

## Workspace & Settings Folder
- Default project folder: ***"~/Documents/Storier/"***
- User(Recent) Stories: ***"user://UserStories.ini"*** 
- User Settings(Data): ***"user://UserSettings.cfg"*** 

## Hierarchy Formats
- Story(.storier)             -> json/config
    - Flows(.storyel)         -> config
    - Entities(.storyel)      -> config
    - Assets(.storyel)        -> config, png, jpg, txt, csv... 
    - Journeys(.storyel)      -> config
    - Templates(.storyel)     -> tscn, config
    - References(.storyel)    -> config
    - ~Export -> config, json, csv(Entities)
    ~StoryElement(.storyel) 

## Story Element
Path = ... /Flow/Chapter1 .storyel
- ... -> StoryBase
- /Flow/Chapter1 -> Id
- storyel -> Extension

Enum(Formats) of StoryElements
Flow, Entity{Character, Location, Item}, Assets(Resource/Research), Journey, ~Templates, ~References

StoryElement Nodes and Query
storier://Flows/Flow?Id="NodeId"&Name="NodeName"
Query(?): /Flows/Flow.storyel?id="NodeId"
Tag(#), Mention(@)
Similar to Web

## Classes
Storier/Story Manager : Node
Story : Node
StoryElement : Resource
StoryFragment : Godot.Object

Editors Display Resources

### Config Format
```
[StoryElement]
Data={
    Name: "", 
    Description: ""
    ...
}

[StoryElementType]
Data= ...

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
General Film Scenario, Fountain

Int ... [Scene Heading]
Character1, ... [Scene Characters]
... [Action]
    Character           -
    (Parenthetical)         Dialogue Fragment
    Dialogue            -
Shot, Transition, Non-printable, Lyrics

## Story Boarding
Graphical Scenario, Japan(E-Conte) Scenario (No: Sheet Number, S/C: Scene/Cut Number)

No ...                                         | Page _/_        ProjectName: __
S/C, Action,  Picture, Dialogue&Sound, Time

## Extra
Template Defination 
Name : Value ~:Type
Story <- Scene <- Sequence <- Shot
