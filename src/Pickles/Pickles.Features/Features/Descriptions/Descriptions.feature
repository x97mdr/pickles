Feature: Descriptions
	In order to see all of the relevant information contained in the feature file
	As a Pickles user
	I want to see the descriptions associated with each step

	Scenario: Display descriptions
		This is a description that is used to provide further context
		Given a feature file containing a scenario with a description
		When I generate documentation
		Then I should get an HTML file with the descriptions listed in a distinctive text
