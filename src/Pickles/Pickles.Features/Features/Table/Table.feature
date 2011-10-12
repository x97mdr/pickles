Feature: Tables
	In order to render tables for easy reading by clients
	As a Pickles user
	I want to be format tables as HTML tables

	Scenario: Generate Table
		Given a feature file with a table
		| Column1 | Column2 |
		| A       | B       |
		| C       | D       |
		When I generate documentation
		Then I should get an HTML file output with an HTML table
