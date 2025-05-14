@Api_Authorized_Customer  @SD_Stagging  @apiQa
Feature: TC001_Validate OTP Service on Stagging
	As Guest User
	I want Request OTP Then validate the otp
	
    Scenario: Validate OTP Service on Staging
    12913
    Given User in Home Page
    When I request and receive an OTP via the request OTP API
    Then I validate the received OTP via the validate OTP API 