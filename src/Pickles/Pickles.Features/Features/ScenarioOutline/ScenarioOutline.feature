Feature: Scenario Outlines
	In order to properly display scenario outlines and examples
	As a Pickles user
	I want to see the examples table rendered as an HTML table and the strings where the examples will be substituted should be rendered so as to stand out

	Scenario Outline: Display Scenario Outlines
		Given a feature file containing a scenario outline referencing <this>
		When I generate documentation
		Then I should get an HTML file with <that>

		Examples: 
		| this | that |
		| A    | B    |
		| C    | D    |
