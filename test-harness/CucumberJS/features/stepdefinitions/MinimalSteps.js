'use strict';

var assert = require('assert');
var {defineSupportCode} = require('cucumber');

defineSupportCode(function({Then}) {

    Then('the step with danish characters like æøå and ÆØÅ shall pass',
        function(callback) {
            // nothing to be done here
            callback();
        });

    Then(/^passing step$/,
        function(callback) {
            // nothing to be done here
            callback();
        });

    Then(/^inconclusive step$/,
        function(callback) {
            // we want pending here
            callback(null, 'pending');
        });

    Then(/^failing step$/,
        function(callback) {
            assert.equal("true", "false");
            callback();
        });
});

