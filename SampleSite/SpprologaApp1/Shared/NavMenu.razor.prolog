navmenu_css_class(collapse).

toggle_navmenu :-
	navmenu_css_class(collapse),

	retract(navmenu_css_class(_)),
	assert(navmenu_css_class(expand)), !.

toggle_navmenu :-
	navmenu_css_class(expand),

	retract(navmenu_css_class(_)),
	assert(navmenu_css_class(collapse)), !.

