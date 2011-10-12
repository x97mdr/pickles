Feature: Table Of Contents
	In order to navigate the features easily
	As a reader of features
	I want to a table of contents on each page that allows me to navigate to each feature page

	Scenario: Generate Table of Contents
		Given a folder hierarchy of HTML files
		When I generate documentation
		Then I should get an HTML file output with a table of contents
