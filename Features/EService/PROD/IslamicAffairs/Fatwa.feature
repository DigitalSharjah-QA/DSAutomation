@SD_Production  @web @Fatwa  @Authorized_Customer_SOP3
Feature: Ask for Fatwa
As a SOP3 user 
I want to ask for fatwa

  Scenario: Fatwa on Production
  12439
    Given User in Home Page
    And User Scroll down
    Then User Search for the Fatwa Service And Select it
    And User in Fatwa Service Catelog Page
    Then User Submit for Fatwa

        Examples:
    | Parking Service Name |
    | فتوى                |