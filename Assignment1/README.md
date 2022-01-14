COSC2276 / COSC2277 Assignment 1 Completed by YehHaw Teh and Andrew Nhan Trong Tran

Implementation of design patterns :

Our team has done a total of 3 implementations Facade and Singleton and Dependency Injection.

For our Facade implementation, we implemented it within our database. Since there are four tables within the database,
we made four separate classes to handle our SQL queries. Without using facade in our implementation, our code would have
a lot of unnecessary repetition. Whenever we need to make a query for Customer and Transaction in the same file, we
would first need to make their objects which we can use within our class. By Implementing the facade pattern, we have
simplified the entire project codebase into one file that handles all the calls to each table. This enables us to easily
read and understand the code more clearly, masking the facade class's complex components.

The Second implementation we did was a Dependency injection on our Accounts. Since each table we make has the same call
to grab all the details from the tables, we have created an IManager to maintain the call. If, for any reason, we needed
to add an extra field into the SQL query, we would use IManager and give it the additional call. This saves us time as
we do not need to search for the object and find it directly. With that dependency changed to be being an interface, it
forces us to use it save repetition in code and time needed to find to be called.

The last implementation we did was Singleton which allows the class to have one instance, which allows all the types to
access it globally. Using the Singleton method in conjunction with the facade method allows for easy access to the
current global variables from the singleton class, which can then be passed into the facade method to be used. This
allows us to isolate the encapsulated parts of our code within the facade and access our global variables within the
singleton class instead of making more method calls to get that specific variable.

The class Library that we implemented was the utility class. This is a class full of helper methods we have used within
the application. Instead of adding that method within the course, we have centralized all the helper methods into the
Utility class, so if for any other reason another part of the program needed the same process call, all we need to do is
make a call from the Utility class instead of making the same method again saving time and make code more readable which
also allows us to reuse code where need be.

Our use of asynchronous was when we were getting data from the webserver; we used async when we grab data from the web
services and will for until all the data is held from the web service which then we can move onto the following function
call. If the async were not used, some data would not be loaded, and other functions would not run properly without it
being fully loaded. By utilizing these build functions, we did not need to use Thread.Sleep() or any other waiting parts
wait for all the data to be loaded. Instead, this allows us to execute our program without worrying about how long it
takes.



