# Unreal Script Unit Testing

## What is here
Unreal Script Unit Testing (USUT) is the unit testing infrastructure that helps doing unit testing for Unreal Script code which is used for Unreal Development Kit 3 (UDK).  

USUT - provides:
- Unreal Script assertions, 
- parsing library,
- simple GUI for viewing test results. 

## That is because unit tests:
- Even if 'games are so nondeterministic', it is possible to cover some part of the game code with tests.
- A covered code is easier to maintain. The longer you maintain it, the bigger effect of unit testing. 
- It is easier to change the code. You can run tests then and see that nothing is broken. 
- It is easier to identify errors that were fixed before and appears again after some code change (regressions). 
- When one has a ready unit testing infrastructure, **writing at least basic tests doesn't cost so much time**. 

This project is developed to provide the ready unit testing infrastructure for Unreal Script of Unreal Development Kit 3 (UDK). 
