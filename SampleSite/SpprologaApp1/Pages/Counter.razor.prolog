count(0).

count_up :- 
	count(X), 
	N is X + 1, 
	retract(count(_)), 
	assert(count(N)).

% This sample source codes are distributed under The Unlicense. (https://unlicense.org/)