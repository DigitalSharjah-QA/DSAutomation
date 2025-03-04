@SD_Production  @web  @Authorized_Customer_SOP3 @AddToFav
Feature: Add Service to Favourites

  Scenario: User Add Service to Favourites then Remove
  6413
    Given User in Home Page
    And User Navigate to First Service
    When User in Service catalog Page He Add The Service to Favourites
    Then Serive Should Appear in the Favourites Services
    Then User remove it from Favourites
 