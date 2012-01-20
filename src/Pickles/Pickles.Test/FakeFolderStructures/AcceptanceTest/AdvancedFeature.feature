@FeatureTag
Feature: Feature title
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: Background name
	Given background given
	When background when
	Then background then

@simpleScenario
Scenario: Simple Scenario
	Given simple scenario given
	And simple scenario given and
	When simple scenario when
	Then simple scenario then

@scenarioOutline
Scenario Outline: Scenario outline example
	Given outline given <value 1>
	When outline when <value 2>
	Then outline then <value 3>
		But nothing important here

Scenarios: 
	| value 1 | value 2 | value 3 |
	| 123     | 456     | 789     |
	| abc     | def     | ghi     |