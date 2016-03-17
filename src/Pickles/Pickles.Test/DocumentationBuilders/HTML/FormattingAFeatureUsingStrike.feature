Feature: Formatting a Feature Using Strike

@enableExperimentalFeatures
Scenario: Description with image should render correctly

  Given I have this feature description
        """
        Including a picture: ![](./image.png)
        """
    When I generate the documentation
    Then the result should be
       """
       <div id="feature">
         <h1>a feature</h1>
         <div class="description">
           <p>Including a picture: <img src="./image.png" /></p>
         </div>
         <ul id="scenarios" />
       </div>
       """
