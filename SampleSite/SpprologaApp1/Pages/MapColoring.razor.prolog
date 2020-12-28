﻿color(red).
color(green).
color(blue).
color(yellow).

solved(no).

color_of_map(Alabama, Mississippi, Georgia, Tennessee, Florida) :-
    Alabama = white, 
    Mississippi = white, 
    Georgia = white, 
    Tennessee = white, 
    Florida = white.

resolve_map_coloring :-
    color(Alabama), color(Mississippi), color(Georgia), color(Tennessee), color(Florida),
    Tennessee \= Mississippi,
    Tennessee \= Alabama,
    Tennessee \= Georgia,
    Mississippi \= Alabama,
    Alabama \= Georgia,
    Alabama \= Florida,
    Georgia \= Florida,

    retract(color_of_map(_, _, _, _, _)),
    assert(color_of_map(Alabama, Mississippi, Georgia, Tennessee, Florida)),

    retract(solved(_)),
    assert(solved(yes)), !.

reset_map_coloring :-

    retract(color_of_map(_, _, _, _, _)),
    assert(color_of_map(white, white, white, white, white)),

    retract(solved(_)),
    assert(solved(no)), !.
    
