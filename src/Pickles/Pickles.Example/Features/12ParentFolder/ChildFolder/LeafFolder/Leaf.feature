Feature: Leaf Feature File
	In order demonstrate what a feature would looks like nested in multiple folders 
	As a Pickles contributer
	I create a simple feature example

@mytag
Scenario: Leaf Example - Add two numbers
	Given I have entered 50 into the calculator
	And I have entered 70 into the calculator
	When I press add
	Then the result should be 120 on the screen
