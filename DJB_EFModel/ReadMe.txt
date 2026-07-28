Updating Database Using Package Manager Console
Step 1 : Use DefaultProject as DJB_INfrastructure in package Manager Console dropdown, 
Step 2 : Make Sure in solution Startup project is set as DJB_API highlighted in Bold.
Step 3 : To update Database which is connected to DefaultConnection in ConnectionStrings appsettings.json(Dev) etc,
Step 4 : In the Package Manager Console, make sure that the Default Project is set to your Entity Framework project.
Step 5 : If you want to update the database to a specific migration, you can use the following command:	
       Add-Migration AddProductColumns
Step 6 : To apply the migration and update the database, use the following command:
         Update-Database (Check in the DB which is connected Before you started the Step 1)
