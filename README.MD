Things to know:

in order to start the react/dotnet mixed app which is located in the webclient folder do the following commands:
dotnet build
dotnet run

while working with front/react side there might be a random error that pops up regrading
the package.json file, simply enter the file and do the keyboard shortcut ctrl + s.




tasks that will make the project ready to be worked on:

fixed the routing and use the default routing made in the react-dotnet bundle, I need to change the routing and use the routing which
is already defined in the AppRoutes.js file.

then finish the form with the jwt and make sure the data recieved in the dot net backend specifically in the following method in the weatherForecastController.cs file:

[HttpPost]
    public IActionResult Post()
    {
    string message = "This is a post action!";
    return Ok(new { message });
    }

and make sure it returns a response.
and then look for a way to create a migration of an sql database thorugh the dot net environment

while configuring the react routes rewrite the routes in hooks not in class (important).