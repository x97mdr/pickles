Feature: Generate XHTML from a single, simple feature and scenario
	In order to generate documentation from feature files that can be distributed to clients
	As a gherkin language user
	I want to generate an XHTML files from a set of feature files that is organized by folder

	Scenario: Generate XHTML from single feature
		Given this feature file
		"""
		Feature: Test
		    In order to do something
		    As a user
		    I want to run this scenario

		    Scenario: A scenario
			    Given some feature
		        When it runs
		        Then I should see that this thing happens
		"""
		When I generate documentation
		Then I should see this XHTML file
		"""
		<?xml version="1.0" encoding="UTF-8" ?>
		<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"[]>
		<html xml:lang="en" xmlns="http://www.w3.org/1999/xhtml">
			<head>
				<title>Feature: Test</title>
			</head>
			<body>
				<div id="feature" class="feature">
					<h1>Feature: Test</h1>
						<p>In order to do something</p>
						<p>As a user</p>
						<p>I want to run this scenario</p>
				</div>
				<ul class="scenarios">
					<li id="0" class="scenario">
						<div class="scenario-heading">
							<h2>Scenario: A scenario</h2>
							<p></p>
						</div>
						<div class="steps">
							<ul>
								<li class="step"><span class="keyword">Given </span>some feature</li>
								<li class="step"><span class="keyword">When </span>it runs</li>
								<li class="step"><span class="keyword">Then </span>I should see that this thing happens</li>
							</ul>
						</div>
					</li>
				</ul>
			</body>
		</html>
		"""
