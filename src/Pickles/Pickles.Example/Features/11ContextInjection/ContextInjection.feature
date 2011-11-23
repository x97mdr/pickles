﻿Feature: Injecting context into step specifications
	In order to don't have to rely on the global shared state
		and to be able to define the contexts required for each scenario.
	As a SpecFlow Evanglist
	I would like to have the system automatically inject an instance of any class as 
		defined in the constructor of a step file

Scenario: Feature with no context
	Given a feature which requires no context
	Then everything is dandy

Scenario: Feature with a single context
	Given a feature which requires a single context
	Then the context is set

Scenario: Feature with multiple contexts
	Given a feature which requires multiple contexts
	Then the contexts are set

Scenario: Feature with recursive contexts
	Given a feature which requires a recursive context
	Then the context is set
	And its sub-context is set

Scenario: Feature with a dependent context
	Given a feature which requires a single context
	Then the context is set
	And the context was created by the feature with a single context scenario