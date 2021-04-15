# EasyBanking
simulation of Banking API

Transaction List Api: 

Gets the list of all created transactions.

LatestTransaction Api:

Gets the latest transaction, which is used to display the current Account Balance.

Create Api: 

Api to add transactions ..credit or Debit

Request sample : 
{   "TransactionAmount": 1000.0, "TransactionCurrency":"EUR",  "AccountId": 1,   "TransactedBy": null,   "LastUpdatedBy": null, TransactionTypeId:1 }



Future changes or improvements: 

Account related logic needs to be implemented, Currently defaulted to Account ID
Login module to be added, link it to the account ID etc ... Use .Net Identity for authentication and authorisation
Modify list transaction api to accept date ranges and search based on dates for monthly, weekly etc reports
Include more unit tests to improve coverage.
Include Api Gateway for better architecture


Current Technical packages and tools used: 

Used Automapper to map classes like UI related and DB access.
Used Moq to mock classes for unit testing.
Swagger is integrated for API Ui view 




