@SD_Production @web @parking @Authorized_Customer_SOP3
Feature: List Payments Method of Parking
  As a SOP3 user 
  I want to List all Payments Method of Parking service

  Scenario: List Payments Method of Parking
    5889
    Given User in Home Page
    And User click on Transportation Category
    When User Search for the service <Parking Service Name> and select the service
    Then Production User in Service <Parking Service Name> Catalog Page
    And User Start the Service <Parking Service Name>
    Then Production User Pay for The First Vehicle
    And Production user click Next for 1 hour
    When User Verify the Payment Methods in Payment Page
    | Payment Method                    |
    | بطاقة الائتمان أو الخصم المباشر  |
    | الحساب البنكي                    |
    | Samsung Pay                       |
    | Google Pay                        |
    | تحصيل                             |

  Examples:
    | Parking Service Name              |
    | دفع رسوم المواقف العامة         |
 