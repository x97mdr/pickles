var crap =
    {
        "RelativeFolder": "04Background\\BackgroundFeature.feature",
        "Feature": {
            "Name": "Show the use of background",
            "Description": "In order to show how to use the Background keyword of Gherkin\r\nAs a SpecFlow evanglist\r\nI want to show that background steps are called before any scenario step",
            "FeatureElements":
                [
                    {
                        "Name": "Add 1 to the sum",
                        "Description": "",
                        "Steps":
                            [
                                {
                                    "Keyword": "When",
                                    "NativeKeyword": "When ",
                                    "Name": "I add 1 to the Sum-variable"
                                },
                                {
                                    "Keyword": "Then",
                                    "NativeKeyword": "Then ",
                                    "Name": "the total sum should be 2"
                                }
                            ],
                        "Tags": []
                    },
                    {
                        "Name": "Add 2 to the sum",
                        "Description": "",
                        "Steps":
                            [
                                {
                                    "Keyword": "When",
                                    "NativeKeyword": "When ",
                                    "Name": "I add 2 to the Sum-variable"
                                },
                                {
                                    "Keyword": "Then",
                                    "NativeKeyword": "Then ",
                                    "Name": "the total sum should be 3"
                                }
                            ],
                        "Tags": []
                    }
                ],
            "Background":
                {
                    "Name": "",
                    "Description": "",
                    "Steps":
                        [
                            {
                                "Keyword": "Given",
                                "NativeKeyword": "Given ",
                                "Name": "I have initialized the Sum-variable to 0"
                            },
                            {
                                "Keyword": "When",
                                "NativeKeyword": "When ",
                                "Name": "I add 1 to the Sum-variable"
                            }],
                    "Tags": []
                },
            "Tags": []
        },
        "Result": {
            "WasExecuted": false,
            "WasSuccessful": false
        }
    }