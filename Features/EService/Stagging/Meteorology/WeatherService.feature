@SD_Stagging  @web 
Feature: Check Weather Service Results
As a Guest user 
I want to view the weather status

  Scenario Outline: Check Weather Service Results in Stagging
  5893
    Given User in Home Page
    And User click on General Category
    When User Search for the service <Service Name> and select the service
    Then User in Weather Service Page

    Examples:
    | Service Name |
    | الطقس                |