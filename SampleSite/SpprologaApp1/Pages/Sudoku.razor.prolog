% sudoku solver

sudoku(Puzzle, Solution) :-
    Solution = Puzzle,
    Puzzle = [
        S11, S12, S13, S14,
        S21, S22, S23, S24,
        S31, S32, S33, S34,
        S41, S42, S43, S44
    ],
    NUMBERS = [1,2,3,4],

    % rows
    permutation(NUMBERS, [S11, S12, S13, S14]),
    permutation(NUMBERS, [S21, S22, S23, S24]),
    permutation(NUMBERS, [S31, S32, S33, S34]),
    permutation(NUMBERS, [S41, S42, S43, S44]),

    % columns
    permutation(NUMBERS, [S11, S21, S31, S41]),
    permutation(NUMBERS, [S12, S22, S32, S42]),
    permutation(NUMBERS, [S13, S23, S33, S43]),
    permutation(NUMBERS, [S14, S24, S34, S44]),

    % square
    permutation(NUMBERS, [S11, S12, S21, S22]),
    permutation(NUMBERS, [S31, S32, S41, S42]),
    permutation(NUMBERS, [S13, S14, S23, S24]),
    permutation(NUMBERS, [S33, S34, S43, S44]).

% Initial state.

sudoku_solved(no).

init_sudoku :-
    retractall(cell(_, _, _)), !,
    member(Row, [1,2,3,4]),
    member(Col, [1,2,3,4]),
    asserta(cell(Row, Col, _)),
    fail.

reset_sudoku :-
    not(init_sudoku),
    retractall(sudoku_solution(_)),
    retractall(sudoku_solved(_)), asserta(sudoku_solved(no)),
    retract(cell(1, 3, _)), asserta(cell(1, 3, 2)),
    retract(cell(1, 4, _)), asserta(cell(1, 4, 3)),
    retract(cell(4, 1, _)), asserta(cell(4, 1, 3)),
    retract(cell(4, 2, _)), asserta(cell(4, 2, 4)), !.

reedit_sudoku :-
    retractall(sudoku_solution(_)),
    retractall(sudoku_solved(_)), asserta(sudoku_solved(no)).

resolve_sudoku :-
    retractall(sudoku_solved(_)), asserta(sudoku_solved(yes)), !,
    inputlist(InputList),
    sudoku(InputList, Solution),
    asserta(sudoku_solution(Solution)).

inputlist(InputList) :- inputlist(0, InputList), !.
inputlist(16, []).
inputlist(N, InputList) :-
    Row is floor(N / 4) + 1,
    Col is (N mod 4) + 1, 
    cell(Row, Col, Val),
    N2 is N + 1,
    inputlist(N2, LeftList),
    inputlist(Val, LeftList, InputList).
inputlist(null, LeftList, [_|LeftList]). 
inputlist(Val, LeftList, [Val|LeftList]). 

% This sample source codes are distributed under The Unlicense. (https://unlicense.org/)