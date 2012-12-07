test("Adding spaces to camel case", function () {
    equal(addSpacesToCamelCasedString("ThisIsATestString"), "This Is A Test String", "Happy path test");
    equal(addSpacesToCamelCasedString("00ThisIsATestString"), "00 This Is A Test String", "String prefaced by numbers");
    equal(addSpacesToCamelCasedString("ThisIsATestString."), "This Is A Test String.", "String ending with puncuation.");
});

test("Getting folder structure from JSON", function () {
    ok(sampleJSON != null, "Sample JSON object isn't null");
    equal(sampleJSON.length, 3, "Sample JSON has 3 items");
    equal(getFoldersFromRelativePath("Workflow\\ClearingScreen.feature"), "Workflow",
        "Get folders from relative path - Has folder");
    equal(getFoldersFromRelativePath("ClearingScreen.feature"), "",
        "Get folders from relative path - Has no folder");
    equal(getFoldersFromRelativePath("12NestedFolders\\ChildFolder\\ChildChildFolder\\NestedFolderExample.feature"), "12NestedFolders\\ChildFolder\\ChildChildFolder",
        "Get folders from relative path - Has multiple folders");
    equal(getFoldersFromRelativePath("12NestedFolders\\ChildFolder\\ChildChildFolder\\NestedFolderExample.feature"), "12NestedFolders\\ChildFolder\\ChildChildFolder",
        "Get folders from relative path - Has multiple folders");
    deepEqual(GetFolderFromScenariosList(sampleJSON), ['Workflow', '12NestedFolders\\ChildFolder\\ChildChildFolder',
        '12NestedFolders\\ChildFolder'], "Get Folders From Scenarios List - Happy path");
    deepEqual(splitDirectoryPathIntoArrayOfFolders('ChildFolder\\ChildChildFolder'), ['ChildFolder', 'ChildChildFolder'],
        'Split Directory Path Into Array Of Folders');
    deepEqual(getFoldersWithASubdirectory(['Workflow', '12NestedFolders\\ChildFolder\\ChildChildFolder']), ['12NestedFolders\\ChildFolder\\ChildChildFolder'],
        "getFoldersWithASubdirectory");
    
    
    //{
    //    var expected = new Array();
    //    _.chain(expected)
    //        .push(new Directory("Level1"));
    //    expected[0].SubDirectories.push(new Directory("Level2"));
    //    deepEqual(convertFlatFoldersToHeirarchy(['Level1', 'Level1\\Level2']), expected,
    //        'Convert Flattened Folders To Heirarchy - One level deep');
    //}

    {
        var paths =
        [
            '12NestedFolders\\ChildFolder\\ChildChildFolder\\NestedFolderExample.feature',
            '12NestedFolders\\ChildFolder\\NestedFolderExample.feature'
        ];
        var expected =
        [   
            ['12NestedFolders','ChildFolder','ChildChildFolder','NestedFolderExample.feature'],
            ['12NestedFolders','ChildFolder','NestedFolderExample.feature']
        
        ];
        
        deepEqual(getFolderArraysFromPaths(paths), expected, "Can split up directories and files");
    }
    //{
        
    //    var paths =
    //    [
    //        '12NestedFolders\\NestedFolderExample.feature',
    //        '12NestedFolders\\ChildFolder\\NestedFolderExample.feature'
    //    ];
    //    var expected =
    //    [
    //        new Directory('12NestedFolders'), 'NestedFolderExample.feature'],
    //        ['12NestedFolders', 'ChildFolder', 'NestedFolderExample.feature']

    //    ];
    //    deepEqual(getFolderArraysFromPaths(paths), expected, "Can split up directories and files");
    //}
    
    {
        var expected = new Array();
        _.chain(expected)
            .push(new Directory("Level1"))
            .push(new Directory("Level2"));
        deepEqual(convertFlatFoldersToHeirarchy(['Level1', 'Level2']), expected,
            'Convert Flattened Folders To Heirarchy - All Same level - Remains in order');
        deepEqual(convertFlatFoldersToHeirarchy(['Level2', 'Level1']), expected,
            'Convert Flattened Folders To Heirarchy - All Same level - Is Sorted');
    }

    {
        var startingVals = 
        [
            '12NestedFolders\\NestedFolderExample.feature',
            '12NestedFolders\\ChildFolder\\NestedFolderExample.feature'
        ];
        var expected = new Array();
        _.chain(expected)
            .push(new Directory("12NestedFolders"))
            .push(new Directory("12NestedFolders"));
        expected[0].SubDirectories.push(new Directory('NestedFolderExample.feature'));
        expected[1].SubDirectories.push(new Directory('ChildFolder'));
        expected[1].SubDirectories[0].SubDirectories.push(new Directory('NestedFolderExample.feature'));
        deepEqual(buildHierarchy(startingVals), expected,
            'Convert Flattened Folders To Heirarchy - All Same level - Remains in order');
    }
    
    // first pass is level 0 
    // second pass is level 1
});

function Directory(name) {
    this.Name = name;
    this.SubDirectories = new Array(); 
}

function buildHierarchy(paths) {
    var result = getFolderArraysFromPaths(paths);

    return result;
}

function getFolderArraysFromPaths(paths) {
    var dirs = new Array();
    $.each(paths, function(key, path) {
        return dirs.push(splitDirectoryPathIntoArrayOfFolders(path));
    });

    return dirs;
}

function splitDirectoryPathIntoArrayOfFolders(path) {
    return $.map(path.split('\\'), function (directory) {
        return directory;
    });
}

function convertFlatFoldersToHeirarchy(folderList) {
    var rootStructure = $.map(folderList, function (folder) {
        if (folder.indexOf('\\') > -1) {
            var directories = splitDirectoryPathIntoArrayOfFolders(folder);
            folder = directories[0];
        }
        return new Directory(folder);
    });
    rootStructure = _.sortBy(rootStructure, function (item) {
        return item.Name;
    });
    rootStructure = _.uniq(rootStructure);

    //rootStructure = _.groupBy(rootStructure, function (item) {
    //        return item.Name;
    //    });
    //rootStructure = $.unique(rootStructure).sort(function (item1, item2) {
    //    return item1.Name > item2.Name;
    //});
    //var level2List = getFoldersWithASubdirectory(folderList);
    //var level2Structure = $.each(level2List, function (folder) {
    //    var directories = splitDirectoryPathIntoArrayOfFolders(folder);

    //    return folder; 
    //});
    return rootStructure;
}

function getFoldersWithASubdirectory(folderList) {
    return $.map(folderList, function (folder) {
        return folderHasSubdirectory(folder) ? folder : null;
    });
}

//function getNextLevelOfHeirarchy()
function folderHasSubdirectory(folder) {
    return folder.indexOf('\\') > -1;
}
function GetFolderFromScenariosList(scenarios) {
    return $.map(scenarios, function (val) {
        return getFoldersFromRelativePath(val.RelativeFolder);
    });
}

function getFoldersFromRelativePath(relativeFolder) {
    if (relativeFolder.indexOf('\\') > -1) {
        return relativeFolder.substring(0, relativeFolder.lastIndexOf('\\'));
    } else {
        return '';
    }
}

var sampleJSON = [
    {
        "RelativeFolder": "Workflow\\ClearingScreen.feature",
        "Feature": {
            "Name": "Clearing Screen",
            "Description": "In order to restart a new set of calculations\r\nAs a math idiot\r\nI want to be able to clear the screen",
            "FeatureElements": [
            ],
            "Tags": []
        }
    },
    {
        "RelativeFolder": "12NestedFolders\\ChildFolder\\ChildChildFolder\\NestedFolderExample.feature",
        "Feature": {
            "Name": "Nested Folder Example",
            "Description": "In order to test nested folder output\r\nAs a silly contributer\r\nI want to create an example of something several folders deep",
            "FeatureElements": [
            ],
            "Tags": []
        }
    },
    {
        "RelativeFolder": "12NestedFolders\\ChildFolder\\NestedFolderExample2.feature",
        "Feature": {
            "Name": "Nested Folder Example",
            "Description": "In order to test nested folder output\r\nAs a silly contributer\r\nI want to create an example of something several folders deep",
            "FeatureElements": [
            ],
            "Tags": []
        }
    }
];
