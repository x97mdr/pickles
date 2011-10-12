Feature: Multiline Strings
	In order to format multiline strings in HTML so they are readable and formatted as entered
	As a Pickles user
	I want to see the multline strings rendered as <pre> elements

	Scenario: A multiline string scenario
		Given some feature with a multiline string
		"""
		This is an
		example of a multiline
		string
		"""
		When I generate documentation
		Then I should see the string rendered in a <pre> element
