# Design
[Technical Design](./Technical.md)

[Story Design](#Story-Design), [Project Design](#Project-Design), [Narrations](#Narrations)

## Story Design
The story consists of various [narrative element](https://en.wikipedia.org/wiki/Narrative#Elements). And there are various [narrative techniques](https://en.wikipedia.org/wiki/List_of_narrative_techniques).
Storier interprets some of these elements in its own way. 
- Story
    - Plot[Flows]
    - Character[Entity/Characters]
    - Setting[Entity/Locations]
    - Theme[StoryInfo], Conflict[StoryInfo]...

### Story
Storier uses *Story* as Project. And contain StoryElements which means Flow, Entity, Asset... 

### Flow
Flow is the basic sequence of events and consists of flows/story fragments. Flows can be marked as keypoint similar to [Plot](https://en.wikipedia.org/wiki/Plot_(narrative)). Story Flows and Fragments can be referencing to Entities & Assets. I.e: Plots, Dialogues, Quests etc.

### Entity
Entities are elements that are directly involved in the story for example Characters, Settings, Items. 

### Asset
Assets indirectly affect the story and give more informations for example Character Image, Notes.

## Project Design

### Project Folder
- StoryName
    - Config
    - Flows
    - Entities
        - Characters
        - Locations
    - Assets
    - Researches (External Informations/References, Trivia)

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
S/C, Action,  Picture, Dialogue & Sound, Time
```

### Inspirations
Good for creating causation; also ***Hero's Journey***, ***Three Act Structure*** etc. 
[Narrative Structures](https://en.wikipedia.org/wiki/Narrative_structure), 
[Inspiration Helper](https://en.wikipedia.org/wiki/The_Thirty-Six_Dramatic_Situations), 
SnowFlake, Story Structure Circle

## Extra
Scene Clapper, Take, Stage
~Aspect, Emotions [Techniques](https://en.wikipedia.org/wiki/List_of_narrative_techniques)
