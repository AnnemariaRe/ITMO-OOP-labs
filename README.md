# ITMO-OOP-labs

### Lab 1. Shops
**Objective:** Demonstrate the ability to identify entities and design classes based on them.


**Application area:** shop, customer, delivery, replenishment and purchase of goods. A shop has a unique identifier, name and address. Each shop has a set price for a product and a certain number of items in stock. The buyer can make a purchase. At the time of purchase - he transfers the right amount of money to the shop. Delivery of goods is a set of goods, their prices and quantities to be added to the shop.

 
-------------
### Lab 2. Isu Extra

**Objective:** To learn how to identify areas of responsibility of different entities and design the relationships between them.


**Subject area:** Students, groups, transfers, search. The group has a name (matching the pattern M3XYYY, where X is the course number and YY is the group number). A student can only be in one group. The system should support the mechanism of transfer between groups, adding to the group and removing from the group. Implementing a system for enrolling students in a OGNP.


A OGNP course is a complementary course that students can take. The course is implemented by a specific megafaculty. The course is studied in several streams with a limited number of places.
Students can enrol in two different courses of the GCSE. A student may not enrol in a UGNP which represents the megafaculty of his/her study group. 


-------------
### Lab 3. Backups

**Objective:** To apply the principles from SOLID, GRASP in practice.

**Subject area:**
* Backup - in general case, it is a backup copy of some data, which is done in order to restore this data later, that is, roll back to the point when it was created. 
* A Restore point is a backup copy of objects created at a certain point. 
* Backup job - an entity that contains information about configuration of backups being created and about already created backup points of a given backup. 
* Job object - objects which have been added to a backup job, for which you want to create copies during Job processing.
* Storage - A file that stores a backup copy of a Job object that has been created in a particular location.


#### Making backups
Backing up a file refers to making a copy of the file elsewhere. The system must support extensibility in the backup algorithms. Two algorithms need to be implemented:
* Split storage algorithm - for each object that is added to a joba, a copy is created - a zip file in which the object lies.
* Single storage algorithm - all objects specified in backup are stored in one archive.


#### Storing copies
The lab work assumes that backups will be created locally on the file system. But the execution logic must abstract from this, an abstraction must be introduced - a repository (see the DIP principle from SOLID). 
 

#### Creation of restore points
The backup job is responsible for creating new restore points (i.e. it acts as a kind of front end encapsulating the logic). When creating a backup job it should be possible to specify its name, backup job name, backup method or location and backup file algorithm. It should be possible to add or remove Job objects from Backup Job. The result of the algorithm is the creation of a new recovery point. 


--------------------
### Lab 4. Banks

**Objective:** To apply the SOLID, GRASP, pattern principles in practice.


**Subject area:**
There are several Banks which provide financial services for money transactions.
The bank has Accounts and Customers. A customer has a first name, last name, address and passport number (first and last name is mandatory, the rest is optional).


#### Accounts and interest
There are three types of accounts: Debit account, Deposit account and Credit account. Each account belongs to a certain client.

* A Debit account is an ordinary account with a fixed interest rate on the balance. You can withdraw money at any time and you are not allowed to go into debt. You do not have to pay any commissions.
* A deposit account is an account that cannot be withdrawn or transferred until it expires. Interest on the balance depends on the initial amount. There are no commissions. Interest must be set differently for each bank.
* Credit account - has a credit limit, within which you can go into deficit. There is no interest on the balance. There is a fixed commission if the client is in deficit.


#### Fees
From time to time, banks carry out interest and commission deduction transactions. This means that you need a time-winding mechanism to see what happens in a day/month/year, etc.


#### Central Bank
The registration of all banks and the cooperation between the banks is the responsibility of the central bank. It has to manage the banks and provide the necessary functionality so that the banks can interact with other banks. It is also in charge of notifying other banks to charge balance or commission.


#### Transactions and transactions
Each account must provide a mechanism for withdrawals, deposits and transfers (i.e. accounts need some identifiers).


Another mandatory mechanism that banks must have is the cancellation of transactions. If it suddenly turns out that a transaction has been made by an intruder, then the transaction must be reversed. Cancellation of the transaction involves the bank returning the amount back to the bank. The transaction cannot be reversed.


#### Console operation interface
To interact with the bank, we need to implement a console interface which will interact with the application logic, send and receive data, display the required information and provide an interface for user input.


----------------------
### Lab 5. BackupsExtra

#### Saving and loading data
The system must be able to load its state after a restart. Once loaded, the application is expected to load information about existing jobs, objects added to them, and information about created restore points.
   
#### Point cleanup algorithms
In addition to creation, the number of stored recovery points must be monitored. In order to avoid the accumulation of a large number of old and irrelevant points, you must implement mechanisms for cleaning them - they must control that the chain of recovery points does not exceed the permissible limit.

#### Merge points
It's worth separating the algorithm for selecting points to remove and the removal process. An alternative behaviour for exceeding the limits - merge points - should be supported. 

#### Logging
The backups logic should not be tied directly to the console or other external components. To support the ability to notify the user about the events inside the algorithm, you need to implement a logging interface and call it when needed. 
Logger implementations:
* Console, which logs information to the console.
* File logger, which logs to a specified file


#### Restore
The purpose of backups is to allow recovery from a backup. 
Two recovery modes:
* to original location - restore to the location from which they were backed up (and replace if they still exist)
* to different location - restore to a specified folder



