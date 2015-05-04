function addSpacesToCamelCasedString(unformattedString) {
    // IE does not implement trim() and the following replaces the functionality if unavailable
    // http://stackoverflow.com/questions/2308134/trim-in-javascript-not-working-in-ie
    if (typeof String.prototype.trim !== 'function') {
        String.prototype.trim = function() {
            return this.replace(/^\s+|\s+$/g, '');
        }
    }
    
    return unformattedString
      .replace(/([A-Z][^A-Z+])/g, " $1")
      .replace(/([A-Z][^a-z+])/g, " $1")
      .replace(/\s\s/g, '')
      .trim();
}

function removeBeginningHash(hashedString) {
    if (hashedString.substring(0, 1) == "#") {
        return hashedString.substring(1, hashedString.length);
    } else {
        return hashedString;
    }
}

function renderMarkdownBlock(markdownText) {
    if (markdownText != '') {
        // More info: http://code.google.com/p/pagedown/wiki/PageDown
        var converter = new Markdown.Converter();

        setupMarkdownExtraWithBootstrapTableStyles(converter);

        var transformed = '<p>' + converter.makeHtml(markdownText); + '</p>'
        return transformed;
    }
    return markdownText;
}

function setupMarkdownExtraWithBootstrapTableStyles(converter) {
    // More Info: https://github.com/jmcmanus/pagedown-extra
    Markdown.Extra.init(converter, { table_class: "table table-bordered table-condensed table-striped" });
}