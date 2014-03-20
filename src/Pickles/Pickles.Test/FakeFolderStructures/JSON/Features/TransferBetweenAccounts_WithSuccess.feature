Feature: Transfer funds between accounts

#Background: Two accounts exists with a amount of 500 each

@dev
Scenario: Transfer money with coverage from one account to another
	Given The account one contains 70
	And account two contains 50
	When I transfer 50 from account one to account two
	Then account one should contain 20
	And account two should contain 100
