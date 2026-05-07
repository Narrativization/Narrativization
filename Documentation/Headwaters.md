# Headwater
Fountain + Choose = Non linear(Branching), Interactive scenario format

## Syntax
Similar to [Fountain](https://fountain.io/syntax/). 

```fountain
/*Headwaters (Head of the Fountain)*/
/*HeadWaters, Fountain format with braching*/

/*Additional Syntax to Fountain*/
/*"<Function>" Function*/
/*"CHOOSE TO:" Branching Scenario*/
/*"> ... -> #0.0.0#" Go to Scene Name or Scene Number, and displays next line*/
/*"CONDITION TO:" Conditional Branching Scenario*/
/*">? ... -> #0.0.0#" Go to Scene Name or Scene Number, and displays next line by condition*/

EXT. Outside - DAY - MEETING #1.1#
@Char2
(Look at us)
Hey you? Stop!


CHOOSE TO:
> Stop -> #1.1.1#
> Ignore -> #1.2.1#


EXT. Outside - DAY - STOP #1.1.1#
@Char1
Are you calling to me?

@Char2
Yeah, you!
Remember me?

CHOOSE TO:
> Ask Again -> #1.1.1#
> Yes -> #1.1.1.2#

EXT. Outside - DAY - YES #1.1.1.2#
@Char1
Yes, I know you.

@Char2
Hmm... what will you call me?

CONDITION TO:
>? @Char1 is Young   -> #1.1.1.2.1#
>? @Char1 is Elder  -> #1.1.1.2.2#


EXT. Outside - DAY - YOUNG #1.1.1.2.1#
@Char1
You are the well-known gentleman, sir.

CUT TO:


EXT. Outside - DAY - ELDER #1.1.1.2.2#
@Char1
(Pointing to the house)
Young man, I was actually headed for that house.

CUT TO:


EXT. Outside - DAY - IGNORE #1.1.2#
@Char1
Good day...

CUT TO:

```

