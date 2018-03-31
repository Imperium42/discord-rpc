# Discord RPC

This is a library for interfacing your game with a locally running Discord desktop client. It's known to work on Windows, macOS, and Linux.

![Preview](https://cdn.discordapp.com/attachments/262809788696494080/426409016713805825/unknown.png)

## About This Fork (TL;DR)

* It works **out of the box** in a standard Unity hierarchy, compiled as: 
> Windows (32/64)
> Mac (64/Universal)
> Linux (64/Universal). 

* **Quickstart** guide below. 

## About This Fork (Detailed)

In the original documentation, it wants us to build from source, install Python, pip, pip ‘Click’ app, install "CMake" (not mentioned in current docs), run some CLI, do some very specific DLL placement with an editor script to place individual ones, and do a bunch of different things for different platforms -- but we’re going to skip all those shenannigans.

* This version is simplified from the original, aiming for use with Unity (Specifically on Windows, but probably works on other OS's)

* At the expense of only +0.5mb, all the DLL files are placed for you for Windows, Mac & Linux. You can always organize it later.

* Because DLL files are placed for you, the Editor file is now renamed to `.cs.bak` since it's not needed.

* All bloat and source stuff that we Windows simple folk would never look at has all been removed for simplicity.

* The architecture was redesigned to be intuitive to place into a REAL Unity project with better placement.

* IT JUST WORKS!

## Quickstart

1. **Clone** this project. The bloat from the original project has already been removed.
 
2. (a) **Copy** `/SomeUnityProject/Assets/*` to `<YourUnityProject>/Assets/`

2. (b) **OR** You can open the `SomeUnityProject` directly within Unity to jump straight to the example project/scene

3. Open the example scene @ `/Assets/Discord/Example Scene/DiscordExampleScene`.

4. Click on `DiscordCtrl` in the hierarchy and paste your SteamId (if you have one; optionally). You can use this sample Discord app id `333435323239383930393830393337373339` (should already be prefilled). Remember to put your own later.

5. Click PLAY and notice your logs. Click the button and notice the click count goes up. Look at your Discord profile! It's there! You did it~

![Step 1](https://i.imgur.com/W6JWNMP.png)

![Step 2](https://i.imgur.com/hgcichs.png)

![Step 3](https://i.imgur.com/LgmolX7.png)

## Now What? Tips!

* Now that you're done, you can completely ignore the main `DiscordRpc.cs` file. I recommend you keep the `DiscordController.cs` clean/high-level and make a new `DiscordCtrlInfo.cs` class for all your classes. You can interact with your DiscordController.cs however way you want (I prefer a singleton).

* Don't forget that the `presence` object is on the GLOBAL scope. This means you should RESET IT ( `= new()` ) every time you call UpdatePresence(), unless you want the old values to remain. I have common/shared stuff in UpdatePresence() then a few helpers before/after. For example, UpdatePresenceLogin(), UpdatePresenceLobby(). I also made a helper class to set all my defaults that I use almost every time to make it easy (or at least have fallbacks).

* Don't forget you must have a min AND max for the (x of y) to show up.

* When uploading assets, the names are converted to lowercase. Don't forget! You will! Don't! ;) 

* Best practice is probably to use enums to keep track of the names of the uploaded assets (in lowercase).

## Community Tips

* Pull request here with more tips or sample scenes/scripts as you find them.

## Included Unity Example Project: button-clicker

This is a sample [Unity](https://unity3d.com/) project that wraps a DLL version of the library, and sends presence updates when you click on a button.

## Disclaimer

I just started learning this. There's probably a better way to do this. However, it's (currently) THE first/only doc on the internet that specifically/simply shows you how to set this up within Unity, so it's better than nothing and hopefully a starting point for you!

## This Forking Changed My Life

Found this overwhelmingly useful? Although I can't bear your children, you can allow me to shamelessly self-promote our (Imperium42 Game Studio) live game, **Throne of Lies: The Online Game of Deceit** -- check it out @ http://store.steampowered.com/app/595280

## Questions?

I'm on ~~Skype~~ **Discord** (...of course!): https://discord.gg/tol **(Xblade#4242)**

## License

MIT
