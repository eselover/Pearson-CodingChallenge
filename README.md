## Getting Started

Run Install ?

Setup a local SQL Server using defaults

From Root run
`sqlcmd -S .\ -i ./Scripts/CreateSchema.sql`
This creates and setups the Database Schema

then run the command
`sqlcmd -S .\ -i ./Scripts/Procedures.sql`
This creates stored procedures in the Database to be used for DML statements