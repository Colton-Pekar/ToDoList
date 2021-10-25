# ToDoList

Here is my ASP .Net Core To Do List website using razer pages. 
In appsettings.json, I declare my default connection for the sql server name as '(LocalDB)\\MSSQLLocalDB'
You have to migrate the class to the db and then proceed with 'update-database'.
To run the application, open the solution in Visual Studio (I used 19) with the sql server connected.
Press the green play button (as you would obviously know) and let the application pop up

The main page will be your 'active to do list' where you can view and mark tasks as complete
The modify list button will bring you to a separate page where you may:
  1) Add a new item to the list.
  2) Edit an item that is currently on the list (task or when it is due).
  3) Delete an item that is on the list if you wish.
