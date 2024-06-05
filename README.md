# Kata Toy Robot Simulator

[![CICD](https://github.com/PABERTHIER/kata-toy-robot-simulator/actions/workflows/build.yml/badge.svg)](https://github.com/PABERTHIER/kata-toy-robot-simulator/actions/workflows/build.yml)

## Description

The Kata Toy Robot Simulator is an application that simulates a toy robot moving on a square tabletop with dimensions of 5 units by 5 units. The robot can roam freely on the table surface but must be prevented from falling off the table.

## Commands

The application reads commands to control the robot. The following commands are supported:

- `PLACE X,Y,F`: Places the toy robot on the table at position (X, Y) facing the direction F. F can be `NORTH`, `SOUTH`, `EAST`, or `WEST`.
- `MOVE`: Moves the toy robot one unit forward in the direction it is currently facing.
- `LEFT`: Rotates the robot 90 degrees to the left without changing its position.
- `RIGHT`: Rotates the robot 90 degrees to the right without changing its position.
- `REPORT`: Outputs the current position and direction of the robot.

## Rules

- The origin (0, 0) is considered to be the south-west corner of the table.
- The first valid command must be a `PLACE` command. Any commands before a valid `PLACE` command are ignored.
- Any movement that would cause the robot to fall off the table is ignored.
- Commands can be read from a file or standard input.

## Constraints

- The robot must not fall off the table during movement or placement.
- Any move that would cause the robot to fall must be ignored.

## Example Usage

### Example A

**Input:**

```text
PLACE 0,0,NORTH
MOVE
REPORT
```

**Expected Output:**

```text
0,1,NORTH
```

### Example B

**Input:**

```text
PLACE 0,0,NORTH
LEFT
REPORT
```

**Expected Output:**

```text
0,0,WEST
```

### Example C

**Input:**

```text
PLACE 1,2,EAST
MOVE
MOVE
LEFT
MOVE
REPORT
```

**Expected Output:**

```text
3,3,NORTH
```

## Deliverables

- Source code of the application.
- Test code and data used during development.

## How to Run

1. Clone the repository.
2. Compile the source code.
3. Run the application with commands provided through a file or standard input.

**Through a file, open a terminal in the project and run the following commands:**

```shell
cd .\KataToyRobotSimulator\
dotnet run "../Commands/command1.txt"
```

In the `Commands` folder, you can find 3 files, correspondig to the 3 commands provided.

**Standard input, open a terminal in the project and run the following commands:**

```shell
cd .\KataToyRobotSimulator\
dotnet run
```

Then add a line of a command, press enter and add the other one till the end. To finish the command, press enter.

Expected Input:

```shell
PLACE 0,0,NORTH
MOVE
REPORT

```

Expected Output:

```shell
0,1,NORTH
```

## Example Command File

```text
PLACE 0,0,NORTH
MOVE
REPORT
```

## Testing

- Provide test cases to verify the correctness of the application.
- Ensure the robot does not fall off the table and handles invalid commands gracefully.

## Engineering Standards

The solution should be engineered to a standard suitable for production, though no graphical output is required to show the movement of the toy robot.
