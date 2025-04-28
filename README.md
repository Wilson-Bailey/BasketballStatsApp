1. Project Overview
BasketballStatsApp is a full-stack ASP.NET Core MVC web application designed to manage and view basketball player statistics interactively.

Users can:
View player stats on interactive flipping cards
Filter players by team and position
Create, Edit, and Delete player profiles (secured behind login)

2. Technologies Used

Technology	
ASP.NET Core MVC	        Backend web framework
Entity Framework           Core	ORM for database operations
Microsoft Identity	        Authentication and Authorization
Bootstrap 5	Responsive     UI framework
JavaScript / AJAX	        Asynchronous operations and interactivity
SQL Server LocalDB	      Database storage
LocalStorage (Browser)	  Save dark mode setting

3. Database Model
The app includes three interconnected entities:

Entity	Description
Player	Contains stats like name, team, age, PPG, RPG, APG, and image
Team	Represents basketball teams
UserFavorite	Many-to-Many relation between IdentityUser and Player (for favoriting players)

Relationships:

Team ⬌ Player (One-to-Many)
IdentityUser ⬌ Player (Many-to-Many via UserFavorite)

4. Main Features
 CRUD Operations (Create, Read-All, Read, Update, Delete)
Users can create, edit, and delete players after login.

Image uploads supported for player profiles.

 Complex Functionality
User authentication protects create/edit/delete routes.

Interactive search/filter across multiple database entities (players and teams).

 Dashboard
Home page displays summary statistics like total players and teams.

 REST Web API
/api/playerapi provides endpoints for:

GET all players

GET a player by ID

POST new player

PUT update player

DELETE a player

API protected with proper CORS configuration.

 JavaScript and AJAX
AJAX delete allows removing players without page reload.

Interactive flip cards built with JavaScript (click to reveal stats).

Theme preference is saved using browser's localStorage.

Accessibility and Responsive Design
Bootstrap ensures mobile responsiveness.
Added alt attributes and tabindex for keyboard navigation.
Aria labels used where needed.

5. Authentication and Authorization
Login Required to Create, Edit, Delete players.
Unauthorized users are redirected to the login page if trying to access protected actions.
Navigation menu and buttons adjust dynamically based on login status (e.g., Create Player button hidden if logged out).




