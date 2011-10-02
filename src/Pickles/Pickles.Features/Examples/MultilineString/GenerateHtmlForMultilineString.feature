Feature: Generate XHTML output from a scenario containing a multi-line string
	In order to view nicely formatted multiline string data
	As a gherkin language user
	I want to generate an XHTML files from a set of feature files that is organized by folder

	Scenario: Generate XHTML for a multiline string
			If we have a single feature with a single scenario and we pass it to the documentation generator then we should get a simply formatted XHTML file
		Given the feature file at Examples\MultilineString\MultilineString.feature
		When I generate documentation
		Then I should get the XHTML file at Examples\MultilineString\MultilineString.xhtml
