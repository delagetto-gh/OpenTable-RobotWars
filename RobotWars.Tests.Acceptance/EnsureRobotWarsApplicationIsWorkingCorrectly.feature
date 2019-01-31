Feature: EnsureRobotWarsApplicationIsWorkingCorrectly 
         As An OpenTable Hiring Developer
         I want ensure the candidates application is working correctly

Scenario: EnsureRobotWarsApplicationIsWorkingCorrectly
            Given I have set size of the battle arena as:
            | 5 5 |
            And I deploy 2 robots at the following locations:
            | 1 2 N |
            | 3 3 E |
                When I input the first robot position and move instruction:
                | 1 2 N |
                | LMLMLMLMM |
                And I input the second robot position and move instruction:
                | 3 3 E |
                | MMRMMRMRRM |
            Then I should expect to see the following output:
            | 1 3 N |
            | 5 1 E |



