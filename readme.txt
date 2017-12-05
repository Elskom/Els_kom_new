                          Whats new in v1.4.9.8
=============================================================================
	+Removed Deprecated stubs.
	+Made Els_kom.exe an C++ WinForms Application.
	+Replaced PyCryptodome dependency.
	+Added support for packing to and unpacking from KOM V2.
	+Made Core able to handle packing and unpacking to/from whatever
		KOM version the file requires. This is so the GUI can
		properly pack and unpack and all KOM versions.

=============================================================================
                             Els_kom v1.4.9.7
=============================================================================
	+Fixed multiple instance error message icon.
	+Fixed issue where Els_kom main window shows under the
		multiple instance error message.
	+Removed pack.bat and unpack.bat. They been replaced with
		something much more manageable and goes out of
		date far fewer times.
	+Fixed Minimize/Maximize bug.
	+Moved Forms back to exe's.
	+Deprecated the stubs and will be removed on 1.4.9.8.
	+Moved most of the form Controls to custom ones to size the forms
		down considerably by designer and code.
	-This means the Core would have to be built before the forms
		controls can display properly in Visual Studio.

=============================================================================
                             Els_kom v1.4.9.6
=============================================================================
	+Modified settings dialog to Set minimize options.
	+Converted Project Files to C#. Future Code Will be in C# and C++ only.
	+Implimented Settings into Main Dialog for the program to minimize to the
		Taskbar, Tray, or to both.
	-Had To change Everything to work in C# natively and well.

=============================================================================
                             Els_kom v1.4.9.5
=============================================================================
	+Add Detection for x2.exe when Test Mods option is pressed until closed.
	+Add Detection for launcher & x2.exe when running when Launcher
		Option is pressed until closed.

=============================================================================
                             Els_kom v1.4.9.4
=============================================================================
	+Removed Reference and use of Microsoft.VisualBasic.Compatibility in
		Els_kom Project (to the Els_kom.exe file).
	+Removed Reference and Use of Microsoft.VisualBasic.PowerPacks.VS in
		Els_kom Project (to the Els_kom.exe file) as well.
	-Will Replace the WindowFind Class in the core with something much
		more Accurate and can find stuff even if it has no Window by
		it's Image name (AKA x2.exe). (Will come before v1.5.0.0)

=============================================================================
                             Els_kom v1.4.9.3
=============================================================================
	+Replaced module1's FileExists with System.IO.File.Exists.
	+Removed 2 bat files. (Not Needed anymore because System.IO.File.Create
		is better)
	+Changed the way x2.exe is executed. (Your mods would have to be in the
		folder of x2.exe)
	+2 bat file still Exists which allows fully customized kom 
		unpacking/packing.
	+Added a INI Writer to the Core.
	+Added a Config Dialog for setting the Elsword Directory.
	+Made Stubs Aware and Use the Settings for Running the Launcher /
		Testing Mods to not require Els_kom in the folder of elsword.exe
		or voidels.exe

=============================================================================
                             Els_kom v1.4.9.2
=============================================================================
	+Added a Core DLL.
	+Moved All Modules to a Core DLL.
	+Version change as well forgot to change the physical label on the
		binaries last time.
	+Changed some labels not being reset when the Test Mods and the
		Launcher buttons are pressed.
	+Fixed bug where the packing and unpacking timers was not disabled
		after use. (Causes a Conflict when Packing / Unpacking multiple
		times)

=============================================================================
                             Els_kom v1.4.9.1
=============================================================================
	+Fixed some things with the Launcher and Test Mods button flashing
		uncontrolably.
	+Removed all compile Warnings (had to supress some).
	+Changed Pack and Unpack button Hover Texts.
	+Fixed Hovering over the link and then moving the cursor to a label not
		resetting the image back to not hover status.
	+Fix All buttons having a box look in the Center.
	-Algorithm 3 Support Pushed up to v1.5.0.0 As it will take some time to
		get it added.

=============================================================================
                             Els_kom v1.4.9.0
=============================================================================
	+Converted from VB6 to VB.Net in Visual Studio 2015.
	+Made the kom packing/unpacking console apps support Python 3.x.
		(Still built into exe's to not require a python install)
	+Launcher Stub now supports running voidels.exe instead of elsword.exe
		(if present for Void Elsword).

=============================================================================
                             Els_kom v1.4.8.0
=============================================================================
	+Reduced packing/unpacking times by editing the bat files.(Sorry about
		that)
	+Attempt fix on the taskbar icon not being 32 bit(still not fixed)
	-need hacks for the icon thing sadly.

=============================================================================
                             Els_kom v1.4.7.0
=============================================================================
	+ Changed some other things like Guild info names.
	+ Made the python Scripts into Standalone EXE's.

=============================================================================
                             Els_kom v1.4.6.9
=============================================================================
	+ Moved Launching of the Launcher to a Separate file.
	+ Fixed some other stuff.
	+ Made a Project Group File. Now its more easier to make without
		opening Visual Basic 6 3 times.
	+ Edited the about window some more.
	+ Added another Topic link to About window as well.

=============================================================================
                             Els_kom v1.4.6.7
=============================================================================
	+ Fixed the real issue on which I thought I fixed in 1.4.6.6.

=============================================================================
                             Els_kom v1.4.6.6
=============================================================================
	+ Fixed the issue in Test mods again that I didn't see before it
		turns out I should have done the enable part on the else part of the
		if statement that checks to see if Elsword is running sorry for that
		mess.
	+ Fixed the Label bug on the part of that Else statement to change it
		back to "". The way it should be done.
	- The only flaw I see still atm is the tray icon not giving the
		Els_kom Window Focus like it should be doing. The Taskbar icon works
		on that part though.

=============================================================================
                             Els_kom v1.4.6.5
=============================================================================
	+ Fixed the Controls that was suppoed to be Disabled while game is
		running that ended up being enabled for some reason.

=============================================================================
                             Els_kom v1.4.6.4
=============================================================================
	+ Fixed a issue with Minimizing the program in any way that would
		crash the program with a error.
	+ Resized a label that was too small for some things.

=============================================================================
                             Els_kom v1.4.6.3
=============================================================================
	+ Fixed issue with Window resizing itself sometimes for real this
		time.
	+ Fixed some other issues I seen in Windows 8 x64 Pro.
	+ Fixed issue of the fact that when you close Els_kom you can see it
		in Windows Task Manager still even on Windows 8.

=============================================================================
                             Els_kom v1.4.6.0
=============================================================================
	+ Fixed the freezing Window in Els_kom when Launching Game with
		"Test Mods" button.
	+ Moved the Launching to the game to a External Program named
		"Test_Mods.exe".
	+ Changed a Little of the About Window.

=============================================================================
                             Els_kom v1.4.5.0
=============================================================================
	+ Changed the About Window a lot.
	+ Changed some of the Main Window things to.
	+ Reverted the bat files like they was in 1.3 state but without the
		mobs and the protected kom files.
	+ Fixed the Drive letter problem on the Python27 folder in the bat
		files. It should now be able to find Python installed on any Drive
		letter that isnt "C:\" example what if your Version of Windows is on
		a Drive letter of "E:\" it should now work properly.

=============================================================================
                             Els_kom v1.4.4.0
=============================================================================
	+ Fixed Additional issues with "Invalid use of Null" message in
		v1.4.3.
	+ Fixed some other events when testing mods/running launcher to not
		hide the Els_kom Window.
	+ Fixed the Detections if Elsword Window is open or not by using
		EnumWindows API function.

=============================================================================
                             Els_kom v1.4.3.0
=============================================================================
	+ Fixed issues in the Tray Icon.
	+ Fixed issues with the Testing of mods.
	+ Reworked full source since I accidentally used a USB partitioning
		tool inside of Windows for a USB drive and accidentally fucked up
		this hard disc's Master boot file that a Bootable Repartitioning tool
		fixed by Reformating this drive which in doing so lost the Original
		Source Code. Some things in that Code I wasnt able to find again so
		I tried to put a way of fixing other things also. l0l

=============================================================================
                             Els_kom v1.4.2.0
=============================================================================
	+ Added ability to Test mods out.
	+ Added ability to run launcher as well as x2.exe dirrectly.
	- Flaw is that x2.exe looks for the koms in the folder of my
		Els_kom.exe file.(might be within x2.exe itself so I cant help it.)

=============================================================================
                             Els_kom v1.4.1.0
=============================================================================
	+ Fixed about window issue from the main window able to cover
		the about window up.
	+ Added INI's now down to 1 file instead of 6.
	+ Added ability to see if the INI exists or not.
	+ Might add ability to test mods out but whenever the game updates
		you must make a backup of your unpacked copy before packing as
		packing removes them.

=============================================================================
                             Els_kom v1.4.0.0
=============================================================================
	+ Split Program to 3 Editions to hep me get EC for the Game.
		the Editons are "Free", "Pro" (30$ EC code), and "Ultimate"
		(50$ EC Code).
		Free is just like it says, it is free but you can only mod UI koms.
		Pro you can only mod UI koms and Costumes.
		On Ultimate however you can mod any kom you want as long as
		that kom isnt protected. (This would help me determine what koms
		are protected and what arent).
		Note: I hate to do this but at this moment I am so poor and same in
		Game, this way I could get things I like / want in the game or I
		could do giveaways or I could sell EC from it at prob like
		5.5:100~6:100.

=============================================================================
                             Els_kom v1.3.0.18
=============================================================================
	+ Fixed issue on the labels to the Packing and Unpacking buttons
		that show after you press one of them. (Issue was fixed in INI) xD
	+ Fixed Shell icon image quality. xD

=============================================================================
                             Els_kom v1.3.0.17
=============================================================================
	+ Added INI interface that can be edited to translate this program
		however it is needed to be.
	+ Redid a lot of things to fix other issues.

=============================================================================
                             Els_kom v1.3.0.16
=============================================================================
	+ Fixed the crash when packing in 1.3.0.15 it was a mistake I made
		in the original code that I didnt notice till then.
	+ Made Els_kom require Admin so that some of its functions work
		correctly to clean Python 2.7.1's folder of unneeded files created
		from packing/unpacking.

=============================================================================
                             Els_kom v1.3.0.15
=============================================================================
	+ Added way of detecting of Python 2.7.1 and the Scypts for koms
		are installed in "C:\Python27" (if they arent there then the program
		would close without crashing with a error that ppl would not know
		what files are missing.)
	+ Added Support up to data164 atm.

=============================================================================
                             Els_kom v1.3.0.14
=============================================================================
	+ Fixed a Issue where you can accidentally open up a lot of
		Pythons with the Pack/Unpack options within the program. (Fixed by
		disabling them while Packing/Unpacking only.)
	+ Added Support for Unpacking koms named "data153" and
		"data154" and all the way up to 160.

=============================================================================
                             Els_kom v1.3.0.13
=============================================================================
	+ Fixed a issue of program executing more than 1 instance of
		cmd.exe and python.exe which caused a lot of slowing down of
		computer speed.

=============================================================================
                             Els_kom v1.3.0.12
=============================================================================
	+ Replaced VB6's default error when missing a bat file that would
		make the program close has been replaced with a message that says
		what file(s) is missing and wont close the program.

=============================================================================
                             Els_kom v1.3.0.11
=============================================================================
	+ Fixed the issue in 1.3.0.10 where you see System Window Restore
		thing when showing Els_kom from the shell icon.

=============================================================================
                             Els_kom v1.3.0.10
=============================================================================
	+ Fixed another issue I was trying to fix in a previous version (was
		in the shell icon code).
	+ Fixed the issue in 1.3.0.9.
	+ Reconstituted some of the TerminateProcess function for when the
		Option in the Menu on the Shell icon is pressed (option did not work
		in the 1.3.0.10 code test).

=============================================================================
                             Els_kom v1.3.0.9
=============================================================================
	+ Fixed the instance detection issue.
	+ Fixed terminating process
	+ Commented out TerminateProcess function in code (Removed).
	+ When closing you will see the Window try to reopen but close (Will
		look into that for 1.3.0.10).

=============================================================================
                             Els_kom v1.3.0.8
=============================================================================
	+ Added where only 1 instance of Els_kom is possable at 1 time.
	+ Fixed the unpacking issue in the menu on the Tray.
	+ Seems like the 1 instance detection also triggers the 
		TerminateProcess function which also terminates all instances (will
		look for a fix in 1.3.0.9).

=============================================================================
                             Els_kom v1.3.0.7
=============================================================================
	+ now can make use of the menu resources that I made earlier on the
		tray icon when you right click it. and Left click shows Els_kom.
	+ Can now be hidden when you press the minimize system button.
	+ Unpacking from menu on the Tray icon will crash the program atm.
		(Will see how to fix it in 1.3.0.8)
	+ Testing UPX 3.08 on this to instead of Petite 2.3 for now.

=============================================================================
                             Els_kom v1.3.0.6
=============================================================================
	+ Fixed the Closing Program issue(with TerminateProcess function).
	+ No need to have the .bak file until the Virus false positive is
		removed this will not be packed (with Petite 2.3).

=============================================================================
                             Els_kom v1.3.0.5
=============================================================================
	+ Removed the Ability to see Program on Taskbar
	+ Added Icon on System Tray
	+ The Petite Compression of this seems to make Security Essentials
		think it is a virus(False positive as they are stupid).
	+ Problem on Closing Program when you think it is close(to fix this
		you must use Task Manager to terminate the process until it is
		fixed).

=============================================================================
                             Els_kom v1.3.0.4
=============================================================================
	+ Added some more things for Later use.

=============================================================================
                             Els_kom v1.3.0.3
=============================================================================
	+ Fixed some issue with having things not Work when users have Space
		in their user name on Win8 (this could be the cause of why the
		program would not pack or unpack in Win8).

=============================================================================
                             Els_kom v1.3.0.2
=============================================================================
	+ Fixed some problem with detecting if it is unpacking or not from
		1.3.0.1 that I didnt notice till now.

=============================================================================
                             Els_kom v1.3.0.1
=============================================================================
	+ Added Common Controls (6.0)
	+ Edited the bat files.
	+ Added new bat files to help indicate when Packing/Unpacking koms.
	+ Added Microsoft Manifest to Executable Resource Section.
	+ Added the look of Windows Themes to the interface.

=============================================================================
                             Els_kom v1.3.0.0
=============================================================================
	+ 1st innitial build that was working.
	+ seems to not work in Win8 x64 (does not know for sure as indication
		of packing or unpacking was not present).