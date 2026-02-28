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
- Navigation Stack: Page-based navigation.

-----

## 📸 Demo

<img src="./demo/DemoPic.PNG" width="700" alt="test">

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


