function FeatureParent(relativeFolder, feature) {
    this.RelativeFolder = relativeFolder;
    this.Feature = new Feature(feature.Name, feature.Description, feature.FeatureElements);
}
function Feature(name, description, featureElements) {
    this.Name = name;
    this.Description = description;
    this.FeatureElements = featureElements;
}
function FeatureElement(name, description) {
    this.Name = name;
    this.Description = description;
}
function Step(keyword, nativeKeyword, name) {
    this.Keyword = keyword;
    this.NativeKeyword = nativeKeyword;
    this.Name = name;
}
function TableArgument(headerRow, dataRows) {
    this.HeaderRow = headerRow;
    this.DataRows = dataRows;
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