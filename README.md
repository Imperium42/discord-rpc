# Discord RPC

This is a library for interfacing your game with a locally running Discord desktop client. It's known to work on Windows, macOS, and Linux. 

![Preview](https://cdn.discordapp.com/attachments/262809788696494080/426409016713805825/unknown.png)

## About This Fork (TL;DR)

* It works **out of the box** in a standard Unity hierarchy. **Quickstart** guide below.

## About This Fork (Detailed)

In the original documentation, it wants us to build from source, install Python, pip, pip ‘Click’ app, run some CLI, and place the files in an odd manner -- but we’re going to skip all those shenannigans.

* This version is simplified from the original, aiming for Unity (specifically on Windows, although it should also be a headstart on Mac, too). 

* All bloat and source stuff that we Windows simple folk would never look at has all been removed for simplicity.

* The architecture was designed to be intuitive to place into a REAL Unity project with better placmeent.

* The DLL file is already provided for you and placed correctly. Don't even worry about it unless you error out.

* BuildHelper is modified to be less annoying for paths. **Mac/Linux is WIP!** I could use help :)

* IT JUST WORKS!

## Quickstart

1. **Clone** this project. The bloat from the original project has already been removed.
 
2 (a.) **Copy** `/SomeUnityProject/Assets/*` to `<YourUnityProject>/Assets/`

2 (b.) **OR** You can open the `SomeUnityProject` directly within Unity to jump straight to the example project/scene

3. Open the example scene @ `/Assets/Discord/Example Scene/DiscordExampleScene`.

4. Click on `DiscordCtrl` in the hierarchy and paste your SteamId (if you have one; optionally). You can use this sample Discord app id `333435323239383930393830393337373339` (should already be prefilled). Remember to put your own later.

5. Click PLAY and notice your logs. Click the button and notice the click count goes up. Look at your Discord profile! It's there! You did it~

![Step 1](https://i.imgur.com/W6JWNMP.png)

![Step 2](https://i.imgur.com/hgcichs.png)

![Step 3](https://i.imgur.com/LgmolX7.png)

## Now What? Tips!

* Now that you're done, you can completely ignore the main `DiscordRpc.cs` file. I recommend you keep the `DiscordController.cs` clean/high-level and make a `DiscordCtrlInfo.cs` class for all your classes.

* Don't forget that the `presence` object is GLOBAL. This means you should RESET IT ( `= new()` ) every time you call UpdatePresence(). I have common/shared stuff in UpdatePresence() then a few helpers before/after. For example, UpdatePresenceLogin(), UpdatePresenceLobby(). I also made a helper class to set all my defaults that I use almost every time to make it easy (or at least have fallbacks).

* Don't forget you must have a min AND max for the (x of y) to show up.

* When uploading assets, the names are converted to lowercase. Don't forget! You will! Don't! ;) 

* Best practice for me is to use enums to keep track of the names.

## More Tips!

* Pull request here with more tips as you find them. **Seeking tips on building on Mac/Linux!**

## Included Unity Example Project: button-clicker

This is a sample [Unity](https://unity3d.com/) project that wraps a DLL version of the library, and sends presence updates when you click on a button.

## Disclaimer

I just started learning this. There's probably a better way to do this. I've also only confirmed this working on Windows. Linux+Ubuntu are WIP to test when compiled.

However, it's THE only doc on the internet that specifically/simply shows you how to set this up within Unity, so it's better than nothing and hopefully a starting point for you!

## Buy me a beer :)

Found this overwhelmingly useful? Although I can't bear your children, you can check out our (Imperium42 Game Studio) live game, **Throne of Lies: The Online Game of Deceit** @ http://store.steampowered.com/app/595280

## Questions?

I'm on Discord (...of course): https://discord.gg/tol **(Xblade#4242)**

## License

MIT
