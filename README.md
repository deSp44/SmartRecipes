[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]
<br />

<!-- PROJECT LOGO -->
<p align="center">
  <a href="https://github.com/deSp44/SmartRecipes">
	  <img align="center" src="https://user-images.githubusercontent.com/56117577/134784950-41102373-aeba-42d6-b3d5-deef8c35e034.png" alt="Logo" />
  </a>
</p>

<!-- PROJECT TITLES -->
<h1 align="center">Smart Recipes</h1>
<p align="center">
	A web application that allows you to find yourself better in the kitchen and facilitates the planning of daily meals by managing your recipes.
    <br />
    <a href="https://github.com/deSp44/SmartRecipes"><strong>Explore the docs »</strong></a>
	·
    <a href="https://github.com/deSp44/SmartRecipes/issues">Report Bug</a>
	·
    <a href="https://github.com/deSp44/SmartRecipes/issues">Request Feature</a>
</p>

<!-- TABLE OF CONTENTS -->
<details open="open">
	<summary>Table of Contents</summary>
	<ol>
		<li>
			<a href="#about-the-project">About The Project</a>
			<ul>
				<li><a href="#built-with">Built With</a></li>
			</ul>
		</li>
		<li>
			<a href="#getting-started">Getting Started</a>
			<ul>
				<li><a href="#introduction">Introduction</a></li>
				<li><a href="#register">Register</a></li>
				<li><a href="#login">Login</a></li>
			</ul>
		</li>
		<li>
			<a href="#usage">Usage</a>
			<ul>
				<li><a href="#list-of-recipes">List of recipes</a></li>
				<li><a href="#create-recipe">Create recipe</a></li>
				<li><a href="#view-recipe">View Recipe</a></li>
				<li><a href="#edit-recipe">Edit recipe</a></li>
				<li><a href="#delete-recipe">Delete recipe</a></li>
				<li><a href="#show-images-and-manage-them">Show images and manage them</a></li>
				<li><a href="#show-public">Show public</a></li>
			</ul>
		</li>
		<li><a href="#contributing">Contributing</a></li>
		<li><a href="#help">Help</a></li>
		<li><a href="#license">License</a></li>
		<li><a href="#contact">Contact</a></li>
	</ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project
This project was created in order to get to know ASP.NET Core better, implement good programming practices and independently solve the encountered problems in the process of developing the application's functionality.

### Built With
* [Visual Studio 2019](https://visualstudio.microsoft.com/pl/vs/)
* [C# 9.0](https://docs.microsoft.com/pl-pl/dotnet/csharp/whats-new/csharp-9)
* [.NET Core 5](https://docs.microsoft.com/pl-pl/dotnet/core/dotnet-five)
* [Razor](razor-pages/?view=aspnetcore-5.0&tabs=visual-studio)
* [Entity Framework Core (Code First)](https://docs.microsoft.com/pl-pl/ef/core/)
* [MS SQL Server](https://www.microsoft.com/pl-pl/sql-server/sql-server-downloads)
* [Fluent Validation](https://docs.fluentvalidation.net/en/latest/)
* [Auto Mapper](https://docs.automapper.org/en/stable/)
* [SendGrid](https://docs.sendgrid.com/)
* [Google Auth](https://developers.google.com/identity/protocols/oauth2)
* [Facebook Auth](https://developers.facebook.com/docs/facebook-login/)
* [Bootstrap](https://getbootstrap.com/docs/5.1/getting-started/introduction/)
* [Font Awesome](https://fontawesome.com/v5.15/how-to-use/on-the-web/referencing-icons/basic-use)

<!-- GETTING STARTED -->
## Getting Started
This section will introduce you to the main page and show you how to create your individual user account in order to use the application.

### Introduction
When you enter the site for the first time, you should see this start page:
![Zrzut ekranu 2021-09-25 182638](https://user-images.githubusercontent.com/56117577/134778690-828b5178-1513-462b-888c-9f336b42a79e.png)

There you can get acquainted with what the application offers and then proceed to creating an account.

### Register
You can register on the website in two ways. You can use traditional registration by filling in the fields with e-mail and password, and then confirm the account in the message sent to the above e-mail. You can also use an external registration method via Google or Facebook, in which case your account will be confirmed immediately and you will be logged in.
![Zrzut ekranu 2021-09-25 181812](https://user-images.githubusercontent.com/56117577/134778408-590da2af-15cf-420d-bce9-75e27b846b7b.png)

### Login
You can log in to your traditionally created account by entering your email and password. If your account has not been confirmed yet, you can resend the confirmation link to your e-mail. In case you forgot your password, you can change it in the message sent to your e-mail.
![Zrzut ekranu 2021-09-25 182152](https://user-images.githubusercontent.com/56117577/134778600-a3bbc58a-0927-4c22-a360-d75688475450.png)

If your registered mail matches this external login method, it will be added to your account.

<!-- USAGE EXAMPLES -->
## Usage
This section will help you get to know the basic functionalities of the application better.

### List of recipes
You won't have any recipes at first! It's up to you to add your favorite recipes to fill your account with delicious content. With the green button you can add a new recipe, and in the search bar you can search for an existing recipe by name.
![Zrzut ekranu 2021-09-25 212755](https://user-images.githubusercontent.com/56117577/134783997-9c24d362-eb3c-4850-affc-92fb1ed94f59.png)

Maximum of 9 recipes are displayed on one page. They are displayed 3 in a row on computers and in line on telephones.
![Zrzut ekranu 2021-09-25 214207](https://user-images.githubusercontent.com/56117577/134784240-1024e22a-1bee-4fa6-9991-de71f7b7e4a4.png)

If you have more recipes, new pages to choose from will be added in the bar at the bottom.
![Zrzut ekranu 2021-09-25 213614](https://user-images.githubusercontent.com/56117577/134784108-692e0f70-4aae-47dd-a8ec-1ad38fc5ad60.png)

### Create recipe
After selecting to create a new recipe, you will receive a form to fill in. Most of the fields are required. Input fields for ingredients are added dynamically, depending on how many the user needs them.

![Zrzut ekranu 2021-09-25 185816](https://user-images.githubusercontent.com/56117577/134784862-1118ac6c-6b2e-4169-8042-1434e301e3ed.png)

If the form is filled in incorrectly, it will not be sent and fluent validation will show us where the errors are made.
![Zrzut ekranu 2021-09-25 221233](https://user-images.githubusercontent.com/56117577/134784886-082bd9b0-935f-4c44-b4e3-6a6d71d680b6.png)

### View Recipe
The recipe preview consists of four elements: small information box, ingredients list, preparation and photo carousel. Their arrangement adjusts to the user's screen so that everything fits perfectly.
![Zrzut ekranu 2021-09-25 222131](https://user-images.githubusercontent.com/56117577/134785061-1c5404d7-d2aa-4f25-9c04-0cd8ff9d8e3c.png)

The id parameter of the recipe is provided in the link, however, the relevance of the recipe is checked and if it does not agree with the currently logged in user, it will not be displayed.

### Edit recipe
When you click edit recipe, a page similar to creating a recipe is displayed and all fields are filled with the current recipe data. You also need to pay attention to correct validation. The only thing missing is the photo management option, because this one is in a different section.

### Delete recipe
A deleted recipe goes to the trash, from where it can be restored or irretrievably deleted from the database. During these operations, a window pops up to confirm the performed operation. Only if confirmed, the changes will be processed.

### Show images and manage them
The recipe photo view lists all the photos assigned to the recipe. Via the radio button, you can select the main picture of the recipe.
![Zrzut ekranu 2021-09-25 220301](https://user-images.githubusercontent.com/56117577/134784768-1a4bbf4e-506a-40f0-8abb-275cbf16449c.png)

With the buttons on the right you can delete the picture or view it in the modal window.
![Zrzut ekranu 2021-09-25 220330](https://user-images.githubusercontent.com/56117577/134784771-00e6e999-ef89-4ec6-a347-d137f16c1b06.png)

### Show public
Several public recipes have been built into the application for the user to see their appearance and structure.

<!-- CONTRIBUTING -->
## Contributing
Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<!-- HELP -->
## Help

### I want to delete my account. How can I do this?
In the user settings tab there is an option called "Personal Data"
This is the link to this page:
```sh
   https://smart-recipes.hostingasp.pl/Identity/Account/Manage/PersonalData
```
Here you can completely delete your account and associate data with it from the database, or download it in .json format.

### What will happen to the third party authentication providers when I will change the email address?
External login methods will be removed from the user account. They can be added again for a new email.

### Some items on the page are not displaying correctly.
Please report that issue here along with images of the problem: <a href="https://github.com/deSp44/SmartRecipes/issues">report issue</a>.

<!-- LICENSE -->
## License
Distributed under the MIT License. See `LICENSE` for more information.

<!-- CONTACT -->
## Contact
Michał Czaja
<br />
LinkedIn : [Michał Czaja](https://pl.linkedin.com/in/micha%C5%82-czaja-735013209)
<br />
Twitter : [@deSp_97](https://twitter.com/deSp_97)
<br />
Stack Overflow : [deSp](https://stackoverflow.com/users/15499426/desp)
<br />
Project Link: [https://github.com/deSp44/SmartRecipes](https://github.com/deSp44/SmartRecipes)



<!-- MARKDOWN LINKS & IMAGES -->
[contributors-shield]: https://img.shields.io/github/contributors/deSp44/SmartRecipes.svg?style=for-the-badge
[contributors-url]: https://github.com/deSp44/SmartRecipes/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/deSp44/SmartRecipes.svg?style=for-the-badge
[forks-url]: https://github.com/deSp44/SmartRecipes/network/members
[stars-shield]: https://img.shields.io/github/stars/deSp44/SmartRecipes.svg?style=for-the-badge
[stars-url]: https://github.com/deSp44/SmartRecipes/stargazers
[issues-shield]: https://img.shields.io/github/issues/deSp44/SmartRecipes.svg?style=for-the-badge
[issues-url]: https://github.com/deSp44/SmartRecipes/issues
[license-shield]: https://img.shields.io/github/license/deSp44/SmartRecipes.svg?style=for-the-badge
[license-url]: https://github.com/deSp44/SmartRecipes/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/micha%C5%82-czaja-735013209/
