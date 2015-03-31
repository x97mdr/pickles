Introduction
============

[![Build status](https://ci.appveyor.com/api/projects/status/rqt59hq1m2jt2a5v)](https://ci.appveyor.com/project/dirkrombauts/pickles-715)

[![Documentation Status](https://readthedocs.org/projects/pickles/badge/?version=latest)](https://readthedocs.org/projects/pickles/?badge=latest)

Pickles is an open source living documentation generator that works on feature files written in the Gherkin language, popularized in tools like Cucumber and SpecFlow.  Pickles can be incorporated into your build process to produce living documenation in a format that is more accessible to your clients.  Gherkin language files are written in plain text and stored in your source folder.  This can make them inaccessible to clients who may not know how to work with source control or who are not interested in seeing all of the source code, just the features.

Why stop with just the features though?  Pickles can also read plain text files written in the Markdown format so you can add other files to your feature to add all sorts of context.  Well-written features are great to have but even the best written features can leave out some important context information.  Markdown is very simple to write and is designed to be easily read even in plain text files so they are a great way of adding additional context to your feature files to turn them into a real set of living documentation.

Supported Output Formats
------------------------

- HTML
- DHTML (javascript-enabled, search capabilities)
- JSON
- Word
- Excel

If there are other formats you would like to see then why don't you post on the [newsgroup we have for pickles development][1]

Supported Test Runner Integrations
----------------------------------

- NUnit
- xUnit
- MSTest
- Cucumber JSON
- SpecRun

Contributing
------------

It's easy to contribute to Pickles, just setup an account on github and fork the project.  When you have some code to contribute, send a pull request!  There are plenty of ideas for contributions on the wiki and in the issues list.

Latest Builds
-------------

We're working on making them available again.

License
-------

Pickles is licensed with the Apache License, version 2.0.  You can find more information on the license here: http://www.apache.org/licenses/LICENSE-2.0.html

  [1]: https://groups.google.com/forum/?hl=en-GB#!forum/pickles-dev "Pickles Google Group"
