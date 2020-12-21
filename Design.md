# Design
[General Design](#General)

## General
[Design](#Design), [Folder Structure and Flows](#Folder-Structure-and-Flows), [User Interface](#User-Interfaces), 

### Folder Structure and Flows
[General Design](#General)

Storier uses "Flow in Flow" Logic: Each Flow consists of another flow.

Flow consists of flows and contains Entity & Assets. Entity consist of assets. 
Every node(Story) templatable with changing their details.

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
- Flow Editor (Node Editor (json))
- Data Editor (Sheet Editor (csv))
- Document Editor (Text Editor (txt))
- ~Property Editor (Entity Editor (json+ img))

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


