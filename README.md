## Getting Started

Tools used:
Visual Studio Community
SQL Server Express

Setup a local SQL Server using defaults with Windows Authentication

Note:
If you have multiple database running locally and have a specific instance name or login you will have to update the connection string in the file
Utility/ConnectionString.cs with the instance name for "DataSource" and add in any userId and Passwords.

Once you have a running local database run the following commands

From Root run
`sqlcmd -S .\ -i ./Scripts/CreateSchema.sql`
This creates and setups the Database Schema

then run the command
`sqlcmd -S .\ -i ./Scripts/Procedures.sql`
This creates stored procedures in the Database to be used for DML statements
