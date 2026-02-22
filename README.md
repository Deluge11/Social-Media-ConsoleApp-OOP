# 🌐 SocialApp - Console Based Social Network

SocialApp is a console-based social networking engine built with C#.  
Unlike traditional CLI applications, it uses a custom Grid Rendering Engine and a Navigation Stack to simulate a modern app-like experience inside the console.

-----

## ✨ Features

- Authentication: Login and registration flow with user accounts.
- Social Feed: Create posts, view the feed, and interact using likes.
- Networking: Send, accept, and manage friend requests.
- Messaging: Real-time-like chat simulation with sender/receiver alignment.
- Custom UI Engine: 4 × 3 grid-based layout with dynamic header and content.
- Navigation Stack: Page-based navigation with a smooth “back” experience.

-----

## 🏗️ Architecture & Page Flow

At the core of the system is a Control Loop that separates:

- Navigation logic between pages (Navigation Stack).
- Visual rendering in the console (Renderer / Frame).
- User input handling (Input Controller).


       NAVIGATION STACK                     RENDERER (The Frame)
      +----------------+                   *-----------------------*
      |     Page 3     |  --- (Peek) --->  |      [ HEADER ]       |
      +----------------+                   |-----------------------|
      |     Page 2     |                   |                       |
      +----------------+                   |      [ CONTENT ]      |
      |     Page 1     |                   |    (Injected Page)    |
      +----------------+                   |                       |
                                           |                       |
             ^                             *-----------------------*
             |                                         |
             |            (Update UI)                  |
             +-----------------------------------------+
             |
      [ INPUT CONTROLLER ] <--- (User KeyPress)
      +------------------+
      |  W/S : Scroll    |
      |  X   : Action    |
      |  B   : Go Back   |
      +------------------+

-----

## 🧱 Abstractions (Data Structure & Logic)
The system follows a layered OOP hierarchy to keep the code scalable and reusable:

AbPage (The Blueprint)
Defines a 12-cell grid matrix (4 rows × 3 columns), handles basic layout initialization, and manages the page header.

AbScrollPage (List Management)
Introduces pagination logic. It uses a Start index to map a large data list into the three visible content rows.

AbScrollCursor (Interactive Layer)
The most advanced layer. It manages a movable cursor synchronized with the Start index to ensure the selected item is always visible (auto-scrolling behavior).

You can additionally include a short code snippet or a small class diagram here if you want to illustrate the abstraction.

----

## 🎮 Controls & Usage
Once the application is running, navigation is done through:

W / S: Scroll up and down through lists.

X: Perform an action on the selected item (open page, accept friend, etc.).

B: Go back to the previous page using the Navigation Stack.


-----

## 📸 Demo

<img src="./demo/DemoPic.PNG" width="700" alt="test">

-----

