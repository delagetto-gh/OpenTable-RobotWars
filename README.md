#### [Thoughts]

Overall I thought it was a very challenging exercise. Having a spec which was not too broad or too specific in regards to concrete requirements, technology or design principles really made me think about design choices, and trying to work out the overall logic, based on the sample input and output.

---

### Dependencies & Packages used

> - Xunit.Gherkin.Quick _(For Gherkin Style Acceptance Test)_

> - Moq                 _(For Mocking Application dependencies)_

> - Xunit               _(As Overall Test Framework)_

---

#### Requirements

A fleet of hand built **robots** are due to `engage` in **battle** for the annual “Robot Wars” competition. Each robot will be placed within a **rectangular battle arena** and will `navigate` their way around the arena using a _built-in computer system._

A _robot’s_ **location** and **heading** is _represented by a combination of x and y co-ordinates and a letter representing one of the four cardinal **compass points**_. The **arena** is __divided up into a grid__ to simplify navigation. 

```An example position might be 0, 0, N which means the robot is in the bottom left corner and facing North. ```

In order to _control_ a robot, the competition organisers have provided a **console** for _sending a simple string of letters to the on-board navigation system._ The possible letters are **‘L’**, **‘R’** and **‘M’**. ‘L’ and ‘R’ make the robot spin 90 degrees to the left or right respectively without moving from its current spot while ‘M’ means move forward one **grid point** and maintain the same heading. 

```Assume that the square directly North from (x, y) is (x, y+1) ```

#### Input
The first line of input is the upper-right coordinates of the arena, the lower-left coordinates are assumed to be (0, 0).
The rest of the input is information pertaining to the robots that have been deployed. 

Each robot has two lines of input - the first gives the robot’s position and the second is a series of instructions telling the robot how to move within the arena.

The position is made up of two integers and a letter separated by spaces, corresponding to the x and y coordinates and the robot’s orientation. Each robot will finish moving sequentially, which means that the second robot won’t start to move until the first one has finished moving.

#### Output

The _output for each robot should be its **final coordinates** and **heading**._

---
###  **Acceptance Critera**

In order to confirm your program is working correctly, the application **must produce the below expected output, given provided input**. 

##### Test
>  RobotWars.Tests.Acceptance.csproj
- [test] ```EnsureRobotWarsApplicationIsWorkingCorrectly()```

##### Test input:

```
5   5       <- [upper right coordinates (x,y) [Arena]]
1   2   N   <- [robot position  [Robot 1]]
LMLMLMLMMM  <- [robot move instructions [Robot 2]]
3   3   E   <- [robot position  [Robot 2]]
MMRMMRMRRM  <- [robot move instructions [Robot 2]]
```
##### Expected output:
```
1  3  N     <- [Final position of Robot 1]
5  1  E     <- [Final position of Robot 2]
```
---

