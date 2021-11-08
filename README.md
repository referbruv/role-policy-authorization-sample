# role-policy-authorization-sample

Let's talk about two important use cases of authorizing APIs basing on an user role defined by the system and authorizing a user basing on an incoming request claims from the user token.

# What is a Policy?

An authorization policy is a set of requirements and handlers. These requirements define what a request user need to satisfy inorder to proceed further. The respective handlers define how these requirements are processed when a request is made and what action needs to be presented if a rule is satisfied or failed. These requirements and handlers are registered in the Startup when the application bootstraps. 

Once a policy is defined and registered, the runtime applies these policies for validation at the endpoints where the policies are decorated with. When we have these policies in force, we can ensure that the APIs are further secured on top of Authentication and only the set of Authorized users who satisfy these policies are allowed access, else are forbidden (403) from access.

# About the Solution

The solution is a .NET 5 Web API project with seperated tiers for Web, Core and Contracts.

# What to Expect

* Clean Architecture with separated tiers for better encapsulation
* Declared repositories for encapsulating the database logic
* Demonstrated Requirements and Authorization Handlers
* Updated to latest stable .NET 5 

# Read the complete article to learn more:

https://referbruv.com/blog/posts/role-based-and-claims-based-authorization-in-aspnet-core-using-policies-hands-on

Leave a Star if you find the solution useful. If you find the article helpful, support me by:

<a href="https://www.buymeacoffee.com/referbruv" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>

For more detailed articles and how-to guides, visit https://referbruv.com
