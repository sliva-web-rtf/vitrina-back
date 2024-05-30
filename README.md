# Saritasa Redman

This is a web project that contains API for managing orders in a store.

## Resources

JIRA: <jira_url>  
Development website: <dev_url>  
Test cases: <test_cases_url>

## Tests

To run the application tests, execute the following command:
`dotnet test src`

Or if you are in `src` folder, just run `dotnet test`.

## Commands

This project contains some helper commands that can be used to initialize / interact with basic project data.

General syntax for executing a command (from command line): `Saritasa.RedMan.Web.exe command-name parameters`

### Create User

Creates a new user in the system.

Arguments:
- `--first-name` - user's first name
- `--last-name` - user's last name
- `--email` - user's email
- `--password` user's password

Example usage:

`Saritasa.RedMan.Web.exe create-user --first-name=John --last-name=Doe --email=jdoe@saritasa.com --password=sar1tasa`

### Data seed

Seeds the data with some default values.

Arguments:
- `--name` - name of the data seeder to use
- `--count` - amount of objects the seeder should generate

Available seeders:
- `Products` - adds random products to the database. You can set `--userId` argument to specify id of the user who will be used as "creator" of the products
- `Users` - adds random users to the database. You can set `--password` to set override the generated users' password (default is `11111111Aa`)

Example usage:

`Saritasa.RedMan.Web.exe seed --name=Products --userId=4`
