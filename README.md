# Storier
Story Managing Software for Game Creators.
Create awesome stories like a **[Teller of Stories](https://en.wiktionary.org/wiki/storier)**.


## Design

Storier uses "Flow in Flow" Logic: Each Flow consists of another flow.
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
        - Audio
        - Videos
        - Miscellaneous
    - Journeys
        - Global Variables(Decisions)
    - Templates
        - Act ...
        - Entities(Pre Templated)
            - Character …
    - (~)References

Flow consists of flows and contains Entity & Assets. Entity consist of assets. Every node(Story)  templatable with changing their inspector.

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

### Editors
Main Panel
- Flow Editor (Node Editor (json))
- Property Editor (Entity Editor (json+ img))
- Data Editor (Sheet Editor (csv))
- Document Editor (Text Editor (txt))

### Views
Modals
- Detail(Inspector): Right Click
- Navigator(Tree)
- List & Tile 
- Journey: Presentation like Visual Novel

### HotKeys
Be UX focused!
CTRL + Arrows: Moving in the flow.
(ALT + Arrows), (Mouse Back, Forward): Moving between flows.(Editors)
~(CTRL + Tab), (CTRL + Shift + Tab): Moving in Nodes Slot.

### Others
Multi and Single File Save(or Export)