DotNetNuke-VanityUrls
=====================

Enables custom urls to be created for pages and pages with querystring values.

## How it works

Vanity urls integrates with <code>iFinity Url Master</code> or the standard <code>DNN Url Provider</code> in order to allow for a custom url to be handled and then forwarded to a specific destination. 
This destination can include <code>querystring</code> variables and hash tags.

**Examples**

- www.domain.com/<code>promo</code>
- www.domain.com/<code>hello/world</code>
- www.domain.com/<code>free-stuff</code>

Essentially VanityUrls is simply a UrlRewriter with a few more perks and doesn't force you to modify your web.config (which would restart your application).

## Installation

- Install like any other module.
- Put module on a page. Preferrably under Admin or Host sections.
- Go to module settings and enable one of the available providers. *Note* you only need to enable one.
- Start creating urls!

*Note: Vanity Urls administration only works for Administrators or Super User accounts. This limitation will be resolved in a future version.*

## Features

- You can add querystring values during the redirection
- Google campaign tracking information for tracking how the urls are used.
- Start & end dates for each url redirection. If outside the bounds of those dates you will receive a standard DNN 404.
- Basic analytics on tracking when a url was last used.

## Special Thanks

Thank you to the [American Water Works Association](http://www.awwa.org) for sponsoring this project.

## Requirements

DotNetNuke Version 6.1.4

## Fun Facts

- Uses [KnockoutJS](http://knockoutjs.com/) for the client side administration.
- Integrates with the iFinity Url provider module.

## Screenshots

![ScreenShot](https://dl.dropboxusercontent.com/u/10620012/VanityUrls-General.png)
![ScreenShot](https://dl.dropboxusercontent.com/u/10620012/VanityUrls-Campaign.png)
![ScreenShot](https://dl.dropboxusercontent.com/u/10620012/VanityUrls-Settings.png)
![ScreenShot](https://dl.dropboxusercontent.com/u/10620012/VanityUrls-Installation.png)