function addSpacesToCamelCasedString(unformattedString) {
    return unformattedString.replace(/([A-Z])/g, " $1").trim();
}