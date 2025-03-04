@SD_Stagging  @web @parking  @Authorized_Customer_SOP3
Feature: Payment of Parking
As a SOP3 user 
I want to login by uaepass and pay Parking Payment

  Scenario: Payment of Parking for Khorfakan Municipality
  9227
    Given User in Home Page
    And User click on Transportation Category
    When User Search for the service <Parking Service Name> and select the service
    Then User in Service <Parking Service Name> Catelog Page
    And User Start the Service <Parking Service Name>
    Then User Pay for The First Vehicle 
    And user click Next for 1 hour for Khorfacan Municipality
    When User Select Payment Details in Payment Page 
 
    Examples:
    | Parking Service Name |
    |دفع رسوم المواقف العامة|  