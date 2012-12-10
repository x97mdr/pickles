function getTags(json) {
    var tags = new Array();

    $.each(json, function (key, value) {
        $.each(value.Feature.FeatureElements, function (fKey, fValue) {
            $.merge(tags, fValue.Tags);
        });
    });

    tags = _.uniq(tags);
    tags.sort();

    return tags;
}

function getScenarioAndFeatureNames(json) {
    var names = new Array();

    $.each(json, function (key, value) {
        $.each(value.Feature.FeatureElements, function (fKey, fValue) {
            names.push(fValue.Name);
        });
    });
    
    $.each(json, function (key, value) {
        names.push(value.Feature.Name);
    });

    names = _.uniq(names);
    names.sort();

    return names;
}

function getTagsAndFeatureNames(json) {
    return $.merge(getTags(json), getScenarioAndFeatureNames(json));
}