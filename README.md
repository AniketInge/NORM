# NORM - A Nano ORM

**Nano ORM**: A simple, lightweight, easy to use, easy to understand ORM for .NET 4.5 and above

Features of NORM:
-----------------------
1) It is simple to understand and use

2) It is extensible via Managed Extensibility Framework

3) It is lightweight (about 11KB in Debug build)

Usage:
-------
NORM can be installed in any project via Nuget Package Manager using the command

    PM > Install-Package LostProtocol.NORM

NORM can map data from a query as well as a stored procedure.

`NormDbConnection` class establishes connection with the database and gets a database interaction object called `Current`. 

the `Current` method contains 5 methods:

1. `ExecuteNonQuery` which takes in two paramters `command`(A SQL statement) and `paramters`. It returns the number of rows affected.

2. `ExecuteQuery<T>` which takes in two paramters `command`(A SQL statement) and `paramters`. It returns the first element from the result set, or returns `default(T)`

3. `ExecuteStoredProcedure<T>` takes in two parameters `spName`(which is the stored procedure name) and `parameters`. It returns the first element from the result set, or returns `default(T)`

4. `ListExecuteQuery<T>` is similar to `ExecuteQuery<T>` but returns the result set as `IList<T>`

5. `ListExecuteStoredProcedure<T>` is similar to `ExecuteStoredProcedure<T>` but returns the result set as `IList<T>`

Lets say we have a Domain Model class like so: 

    class MTLog
    {
        public string MTLogId {get;set;}
        public string UserId {get;set;}
        public string Sender { get; set; }
        public DateTime ProcessingTime { get; set; }
    }


To use NORM to map data from DB:

    var db = new NormDbConnection();
    IList<MTLog> logs = db.Current.ListExecuteQuery<MTLog>(@"select MTLogId, UserId, Sender, ProcessingTime 
                                                  from Table1 
                                                  where Status=@Status", new{@Status="Sent"});

    MTLog firstLog = db.Current.ExecuteQuery<MTLog>(@"select MTLogId, UserId, Sender, ProcessingTime 
                                                  from Table1 
                                                  where Status=@Status", new{@Status="Sent"});

Similary for **Stored procedures**:

    var db = new NormDbConnection();
    IList<MTLog> logs = db.Current.ListExecuteStoredProcedure<MTLog>("SP_NAME", new{@Status="Sent"});

    MTLog firstLog = db.Current.ExecuteStoredProcedure<MTLog>("SP_NAME", new{@Status="Sent"});

    // that's all folks

**Caution !!**

The Domain Model property names *must* match the database result set column names. In cases where that's not feasible, NORM allows you to specify the column name property:

    class MTLog
    {
        public string MTLogId {get;set;}
        public string UserId {get;set;}
        public string Sender { get; set; }

        [Norm(ColumnName="ProcessingTime")]
        public DateTime ProcessTime { get; set; }
    }

The constructor for `NormDbConnection` takes 0, 1 or 2 arguments.

1) `new NormDbConnection()` assumes there is a connection string named `DefaultConnection` defined in the `.config` file of the assembly.
Also it sets the application name property while executing the query to `NormApplication`

However, if a connection string or connection name is mentioned for the Domain Model entity like:
    
    [NormEntity(Connection="ConnectionName")]
    class MTLog
    {
        public string MTLogId {get;set;}
        public string UserId {get;set;}
        public string Sender { get; set; }
        public DateTime ProcessingTime { get; set; }
    }

or

    [NormEntity(ConnectionString ="connectionstring")]
    class MTLog
    {
        public string MTLogId {get;set;}
        public string UserId {get;set;}
        public string Sender { get; set; }
        public DateTime ProcessingTime { get; set; }
    }

then that connection string will be used while executing `Current` object commands.

2) `new NormDbConnection(string connectionString)` accepts a connection string, sets the application name property while executing the query to `NormApplication`

3) `new NormDbConnection(string connectionString, string applicationName)` accepts a connection string as well as an application name 



