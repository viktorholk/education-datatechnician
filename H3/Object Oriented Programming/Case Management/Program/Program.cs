using Context;

ApplicationContext.Setup("Case Management System");

Application application = new Application();

application.CreateUser("admin", "admin", User.UserType.admin);
var customer = application.CreateUser("billy", "password");

application.Products = new List<Product>(){
    new Product("Internal Software Tool", "Cool description", new List<Case>(){
            new Case(customer, Case.CaseType.Feature, "Add logging system", new List<Resource>(){
                   new Resource("Rewrite backend for compatability", Resource.ResourceStatus.completed, 4),
                   new Resource("Write frontend components", Resource.ResourceStatus.ongoing, 4),
                   new Resource("Cleanup etc.", Resource.ResourceStatus.pending, 0)
                }),
            new Case(customer, Case.CaseType.Bug, "API returns status 500 when calling /users/23/magazines", new List<Resource>(){
                   new Resource("Fix API bug", Resource.ResourceStatus.completed, 2)
                })
        }),
    new Product("Remote Satellite Service", "Remote Control of S26", new List<Case>(){
            new Case(customer, Case.CaseType.Bug, "Shit don't work", new List<Resource>(){
                   new Resource("Fix shit", Resource.ResourceStatus.completed, 4),
                   new Resource("Fix other shit", Resource.ResourceStatus.ongoing, 2),
                })
        })

};

application.Run();
