Feature: HasUnreadItems

Background: 
	Given user USER1


Scenario: There are no release notes
Then has unread items should be false


Scenario: No release notes are read
Given release notes:
	| Version | Release Date |
	| v1      | 1-1-2023     |
Then has unread items should be true


Scenario: Marking a release notes as read
Given release notes:
	| Version | Release Date |
	| v1      | 1-1-2023     |
When marking release notes as read
Then has unread items should be false


Scenario: The latest release notes are not read
Given release notes:
	| Version | Release Date |
	| v1      | 1-1-2023     |
Given the current release notes are marked as read
Given release notes:
	| Version | Release Date |
	| v2      | 1-2-2023     |
Then has unread items should be true


Scenario: Marking a release notes as read multiple times
Given release notes:
	| Version | Release Date |
	| v1      | 1-1-2023     |
Given the current release notes are marked as read
Given release notes:
	| Version | Release Date |
	| v2      | 1-2-2023     |
When marking release notes as read
Then has unread items should be false

