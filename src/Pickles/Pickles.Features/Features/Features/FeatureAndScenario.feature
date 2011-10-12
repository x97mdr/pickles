Feature: Features and Scenarios
	In order to see Gherkin language files formatted in HTML so that my clients can read it
	As a Pickles user
	I want to format features and scenarios into HTML that is easy to read

	Scenario: Generate HTML
		Given a feature file
		When I generate documentation
		Then I should see the features and scenarios rendered in HTML
