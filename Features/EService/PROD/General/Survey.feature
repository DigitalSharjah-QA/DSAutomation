@SD_Production  @web 
Feature: Submit Survey
As a Guest user 
I want to submit a sruvey

  Scenario Outline: User submits a survey
  5887
    Given User in Home Page
    Then User Click on Survey Icon
    Then User Select <satisfaction>
    And Select Random Entity
    When User fill the Survey Form
    And User submit the Survey
    Then User should see the Survey Submission Success Message

    Examples:
      | satisfaction |
      | راضي |