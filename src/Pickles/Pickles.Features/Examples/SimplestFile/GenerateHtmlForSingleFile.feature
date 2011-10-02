Feature: Generate XHTML from a single, simple feature and scenario
	In order to generate documentation from feature files that can be distributed to clients
	As a gherkin language user
	I want to generate an XHTML files from a set of feature files that is organized by folder

	Scenario: Generate XHTML from single feature
			If we have a single feature with a single scenario and we pass it to the documentation generator then we should get a simply formatted XHTML file
		Given the feature file at Examples\SimplestFile\SimplestFile.feature
		When I generate documentation
		Then I should get the XHTML file at Examples\SimplestFile\SimplestFile.xhtml
