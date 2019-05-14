Feature: Calculate hours and pay from timesheets
	Take a series of checkin and checkout times
	And calculate from these times and the hourly
	Rate how much an employee should be paid

Scenario: A single checkin/checkout entry
	Given I checked-in at 9:00 AM and checked out at 9:00PM
	 And have an hourly rate of £7.00
	When my day is done
	Then the normaiized check-in time should equal 09:00
	And  the normalized check-out time should 21:00
	And  the hours worked is 12.00 
	And  my pay should be £84.00

Scenario: Table style checks Numbers
	Given I have filled out the form as follows
			|Check In Time|Check Out Time|Hourly Rate|Check In Time Normalized|Check Out Time Normalized|Hours Worked|Pay  |
			|9:00 AM      |9:00 PM       |7          |09:00                   |21:00                    |12.00       |84.00|
			|10:30 AM     |12:30PM       |4          |10:30                   |12:30                    |2.00        |8.00 |
	When I select ProcessEmployeeTimes
	Then the result should be as per the table
