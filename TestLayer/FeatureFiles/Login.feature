Feature: Login

Verify that the login feature behaves as expected with various designed scenarios that fully exercise the login feature. 

//Background:
	//Given User is already on the login page
@tag1
Scenario: TTT3-2891 Verify that users are able to successfully login to their account.
	Given User is already on the login page
	When User attemps to login with valid credentials
	Then User should be able to see a customised welcome message to confirm that they have successfully login to their account


Scenario Outline: TTT3-2892 Verify that unregistered users are unable to log into the application.
	Given User is already on the login page
	When User attemps to login with invalid credentials
		| username   | password   |
		| <Username> | <Password> |
	Then User should be shown the error message "Your email address or password is incorrect. Please try again."

Examples:
	| Username                     | Password     |
	| jenmifer_chukwudum@yahoo.com | Jenny@m&s241 |
	| jennifer_chukwudum@yahoo.com | jenny@m&s24! |


Scenario: TTT3-2893 Verify that a user can reset their password with a valid email
	//Given User is already on the forgot password page
	//When User enters their valid registered email address "jennifer_chuk@yahoo.co.uk" and submit their request
	//Then A success message "Check your email" should be displayed to the user.
	
Scenario Outline: TTT3-2893 A registered user can request a password reset
	Given User is on the password reset page
	When User requests a password reset using their registered email address "<RegisteredEmail>"
	Then User should see the message "Check your email"

Examples:
	| RegisteredEmail        |
	| jenny.chuk@yahoo.co.uk |


Scenario: TTT3-2894 Verify that a user can reset their password with an invalid email
Scenario: TTT3-2895 Verify that user completes the password reset via the emailed link


