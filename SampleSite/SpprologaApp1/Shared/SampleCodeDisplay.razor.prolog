show_sample_code(none, no).

code_control_text(X) :-
	current_page(P),
	show_sample_code(P, yes),
	X = "Hide the code of this page.", !.
code_control_text("Show the code of this page.").

code_container_css_class(X) :-
	current_page(P),
	show_sample_code(P, yes),
	X = expand, !.
code_container_css_class(collapse).

toggle_showhide_code :-
	current_page(P),
	show_sample_code(P, yes),
	retract(show_sample_code(P, yes)),
	assert(show_sample_code(P, no)), !.
toggle_showhide_code :-
	current_page(P),
	show_sample_code(P, no),
	retract(show_sample_code(P, no)),
	assert(show_sample_code(P, yes)), !.
toggle_showhide_code :-
	current_page(P),
	assert(show_sample_code(P, yes)), !.

% This sample source codes are distributed under The Unlicense. (https://unlicense.org/)