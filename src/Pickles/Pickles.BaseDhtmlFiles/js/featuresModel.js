function FeatureParent(data) {
    this.RelativeFolder = data.RelativeFolder;
    this.Feature = new Feature(data.Feature);
}

function Feature(data) {
    this.Name = data.Name || '';
    this.Description = data.Description || '';
    this.FeatureElements = data.FeatureElements || new Array();
    this.Result = data.Result || null;
    this.Tags = data.Tags || null;
}
function TableArgument() {
    this.HeaderRow = new Array();
    this.DataRows = new Array();
}
function Examples() {
    this.Name = '';
    this.TableArgument = null;
    this.DataRows = new Array();
}
function Background() {
    this.Name = '';
    this.Description = '';
    this.Steps = new Array();
}
function Result() {
    this.WasExecuted = false;
    this.WasSuccessful = false;
}
// putting it here to define it so I can check if it exists - it is an optional value
var DocStringArgument = '';

/* JSON Sample
        {
        "RelativeFolder": "06CompareToAssist\\CompareTo.feature",
        "Feature": {
            "Name": "Show the compare to feature",
            "Description": "In order to show the compare to features of SpecFlow Assist\r\nAs a SpecFlow evanglist\r\nI want to show how the different versions of compareTo works",
            "FeatureElements": [
              {
                  "Name": "CompareToInstance",
                  "Description": "",
                  "Steps": [
                    {
                        "Keyword": "Given",
                        "NativeKeyword": "Given ",
                        "Name": "I have the following person",
                        "TableArgument": {
                            "HeaderRow": [
                              "Field",
                              "Value"
                            ],
                            "DataRows": [
                              [
                                "Name",
                                "Marcus"
                              ],
                              [
                                "Style",
                                "Butch"
                              ],
                              [
                                "Birth date",
                                "1972-10-09"
                              ]
                            ]
                        }
                    },
END JSON Sample */