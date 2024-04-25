# Employee Management API
This application utilises SOA architecture and Repositry design pattern to create an API to capture Staff Titles and then assgn them to employees.

## Database connection
It is created on running the application and the connection string requires editing.

## Testing the API endpoints
We wll focus on the POST and GET Methods
1) Post /titles
    Enter a job title, Like Solution Architect
2) Post /employees
    Using an existing title ID, create a new empoyee 
3) Get /employees/{id}
    Using the Emp ID created above we cn retrieve their information
4) Get /titles/{id}
    Similar to the above retrieve the employees with that title ID
