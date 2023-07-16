# Headwater

## Syntax
Similar to [Fountain](https://fountain.io/syntax/). 

```fountain
/*Headwaters (Head of the fountain)*/
/*HeadWaters, Fountain format with braching*/

/*Additional Syntax to Fountain*/
/*"<Function>" Function*/
/*"CHOOSE TO:" Branching Scenario*/
/*"OPTION TO:" Go to Scene Name or Scene Number, and displays next line*/

@Char2
(Look at us <Lookat>)
Hey you? Stop and look at to me!


CHOOSE TO:
OPTION TO: #1.1.1#
> Remember
OPTION TO: #1.1.2#
> What?


. Outside - DAY - FLASHBACK #1.1.1#
@Char1
I remember know...


. Outside - DAY - CAN'T FLASHBACK #1.1.2#
@Char1
I don't Remember

CHOOSE TO:
OPTION TO: #1.1.1#
> Now Remember
OPTION TO: #1.1.3#
> Tell me

. Outside - DAY - Tell ME #1.1.3#
@Char1
Hey Tell me

@Char2
Allright, wait 2 minutes.

```

