@SD_Production  @web 
Feature: Check Weather Service Results
As a Guest user 
I want to view the weather status

  Scenario Outline: Check Weather Service Results in Production
  5888
    Given User in Home Page
    And User click on General Category
    When User Search for the service <Parking Service Name> and select the service
    Then User in Weather Service Page

    Examples:
    | Parking Service Name |
    | الطقس                |