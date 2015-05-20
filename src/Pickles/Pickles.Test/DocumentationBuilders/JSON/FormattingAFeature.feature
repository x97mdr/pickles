Feature: Formatting A Feature

@json
Scenario: A simple feature

    Given I have this feature description
        """
        Feature: Clearing Screen
            In order to restart a new set of calculations
            As a math idiot
            I want to be able to clear the screen

        @workflow @slow
        Scenario: Clear the screen
            Given I have entered 50 into the calculator
            And I have entered 70 into the calculator
            When I press C
            Then the screen should be empty

        """
	When I generate the documentation
    Then the JSON file should contain
        """
        {
          "Features": [
            {
              "RelativeFolder": "",
              "Feature": {
                "Name": "Clearing Screen",
                "Description": "    In order to restart a new set of calculations\r\n    As a math idiot\r\n    I want to be able to clear the screen",
                "FeatureElements": [
                  {
                    "Name": "Clear the screen",
                    "Description": "",
                    "Steps": [
                      {
                        "Keyword": "Given",
                        "NativeKeyword": "Given ",
                        "Name": "I have entered 50 into the calculator"
                      },
                      {
                        "Keyword": "And",
                        "NativeKeyword": "And ",
                        "Name": "I have entered 70 into the calculator"
                      },
                      {
                        "Keyword": "When",
                        "NativeKeyword": "When ",
                        "Name": "I press C"
                      },
                      {
                        "Keyword": "Then",
                        "NativeKeyword": "Then ",
                        "Name": "the screen should be empty"
                      }
                    ],
                    "Tags": [
                      "@workflow",
                      "@slow"
                    ],
                    "Result": {
                      "WasExecuted": false,
                      "WasSuccessful": false
                    }
                  }
                ],
                "Result": {
                  "WasExecuted": false,
                  "WasSuccessful": false
                },
                "Tags": []
              },
              "Result": {
                "WasExecuted": false,
                "WasSuccessful": false
              }
            }
          ],
          "Configuration": {
        """